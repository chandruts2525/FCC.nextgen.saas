import { useCallback, useEffect, useMemo, useRef, useState } from 'react';
import { ContextMenu, Loader } from '../CommonComponent';
import UserListing from './userlisting';
import { userAPI } from './userlistingapicalls';
import MessageContainer from '../CommonComponent/Message/messagecontainer';
import { dashboardIcon } from '../../assets/images';
import { debounce } from 'lodash';
import { encodeQuery, onExportCSV } from '../SharedComponent/Common-function';
import Constants from '../../utils/Constants';

const UserListingContainer = () => {
  const [isOpen, setIsOpen] = useState({ visible: false, data: {} });
  const [action, setAction] = useState('Add');
  const [callAPI, setCallAPI] = useState<boolean>(false);
  const [showExportOption, setShowExportOption] = useState(false);
  const [componentLoader, setComponentLoader] = useState<boolean>(false);

  const [toast, setToast] = useState({
    show: false,
    message: '',
    type: '',
  });
  const [isFormValid, setIsFormValid] = useState(false);
  const [searchKeyword, setSearchKeyword] = useState('');
  const [data, setData] = useState([]);
  const [totalRecords, setTotalRecords] = useState(0);
  const [sort, setSort] = useState<any>([]);
  const [filter, setFilter] = useState({
    logic: 'and',
    filters: [],
  });
  const [page, setPage] = useState({
    skip: 0,
    take: 10,
    currentPage: 1,
  });
  const _exportExcel: any = useRef(null);
  const _exportPDF: any = useRef(null);

  useEffect(() => {
    fetchData(page.currentPage, page.take, searchKeyword);
  }, [sort, filter]);

  useEffect(() => {
    if (callAPI) {
      fetchData(page.currentPage, page.take, searchKeyword);
      handleCallAPI(false);
    }
  }, [callAPI]);

  const excelExport = async (isExcel = false, fileType: any) => {
    try {
      const { param, filterArray } = getPaginationPayload(
        1,
        99999,
        searchKeyword,
        fileType
      );
      setComponentLoader(true);
      const url: any = encodeQuery(param);
      const payload = {
        endpoint: '/api/User/Search?' + url,
        apiType: Constants.IAC,
        apiMethod: 'Post',
        payload: filterArray,
      };
      const res: any = await userAPI(payload);
      if (res.status === 200) {
        if (
          res.data &&
          res?.data?.securityUserResponse &&
          res?.data?.securityUserResponse?.length > 0
        ) {
          let list: any = res?.data?.securityUserResponse.map((item: any) => {
            item['status'] = item.status ? 'Active' : 'Inactive';
            return item;
          });
          if (isExcel && fileType === 'excel' && _exportExcel.current !== null) {
            _exportExcel.current.save(list);
            createToast('success', 'Data exported to Excel');
          } else if (!isExcel && _exportPDF.current !== null) {
            _exportPDF.current.save(list);
            createToast('success', 'Data exported to PDF');
          } else if (isExcel && fileType === 'csv') {
            onExportCSV(list, groupListingColumns, 'UserListing');
            createToast('success', 'Data exported to CSV');
          }
        }
      }
    } catch (e) {
      console.log('Something went wrong', e);
      createToast('error', 'Something went wrong. Please try again');
    } finally {
      setComponentLoader(false);
    }
    setShowExportOption(!showExportOption);
  };

  const getPaginationPayload = (
    currentPage = 1,
    pageSize = 10,
    searchText = '',
    fileType = null
  ) => {
    const filterArray: any = [];
    if (filter && filter.filters.length > 0) {
      filter.filters.forEach((element: any) => {
        if (element.filters.length > 0) {
          filterArray.push(element.filters[0]);
        }
      });
    }

    const param = {
      SearchFilter: searchText ? searchText : '',
      PageNumber: currentPage,
      PageSize: pageSize,
      OrderBy: sort.length > 0 ? sort[0].field : '',
      SortOrderBy: sort.length > 0 ? sort[0].dir : '',
      fileType: fileType ? fileType : null,
      // userFilter: JSON.stringify(filterArray)
    };
    return { param, filterArray };
  };

  const fetchData = async (currentPage = 1, pageSize = 10, searchText = '') => {
    const { param, filterArray } = getPaginationPayload(
      currentPage,
      pageSize,
      searchText
    );

    // Static Logic
    // const skip = (currentPage - 1) * 10;
    // const l = JSON.parse(JSON.stringify(groupList));
    // const d: any = l.splice(skip, pageSize);
    // setData(d);

    try {
      setComponentLoader(true);
      const url = encodeQuery(param);
      const payload = {
        endpoint: '/api/User/Search?' + url,
        apiType: Constants.IAC,
        apiMethod: 'Post',
        payload: filterArray,
      };
      const res: any = await userAPI(payload);
      if (res.status === 200) {
        if (
          res.data &&
          res.data?.securityUserResponse &&
          res.data?.securityUserResponse?.length > 0
        ) {
          setData(res?.data?.securityUserResponse);
          setTotalRecords(res?.data?.count);
        } else {
          setData([]);
          setTotalRecords(0);
        }
      }
    } catch (error) {
      console.log('error&&&&', error);
      createToast('error', 'Something went wrong. Please try again');
    } finally {
      setComponentLoader(false);
    }
  };

  const fetchSearchData = useCallback((value: string) => {
    const currentPage = 1;
    const skip = 0;
    setPage({
      ...page,
      skip,
      currentPage,
    });
    fetchData(currentPage, page.take, value);
  }, []);

  const debounceRequest = useMemo(() => {
    return debounce(fetchSearchData, 500);
  }, [fetchSearchData]);

  const handleChange = (e: any) => {
    if (e?.target?.value.length <= 100) {
      setSearchKeyword(e.target.value);

      // Debounce the search callback
      debounceRequest(e.target.value);
    }
  };

  const onPageChange = (event: any) => {
    const take = event.page.take;
    const skip = event.page.skip;
    const currentPage = skip / take + 1;
    setPage({
      ...page,
      take,
      currentPage,
      skip,
    });
    fetchData(currentPage, take, searchKeyword);
  };

  const onSortChange = (event: any) => {
    setPage({
      ...page,
      currentPage: 1,
      skip: 0,
    });
    setSort(event?.sort);
  };

  const onFilterChange = (event: any) => {
    setFilter(event.filter);
  };

  const handleClear = (e: any) => {
    e.preventDefault();
    e.stopPropagation();
    setSearchKeyword('');
    handleCallAPI(true);
  };

  const openPanel = (data: any) => {
    setIsOpen({ visible: true, data: data });
  };

  const dismissPanel = (isSubmit: any) => {
    setIsOpen({ visible: false, data: {} });
    if (isSubmit) {
      handleCallAPI(true);
    }
  };

  // Call method to activate/deactivate user
  const deactivateUserAction = async (userId: any, isActive: number) => {
    try {
      setComponentLoader(true);
      const payload = {
        endpoint: `/api/User/UserActivateDeactivate?userId=${userId}&status=${!!isActive}`,
        apiType: Constants.IAC,
        apiMethod: 'Put',
        payload: null,
      };
      const res: any = await userAPI(payload);
      if (res.status === 200) {
        createToast(
          'success',
          `User has been ${isActive ? 'activated' : 'deactivated'}!`
        );
        handleCallAPI(true);
      }
    } catch (e) {
      console.log('Something went wrong', e);
    } finally {
      setComponentLoader(false);
    }
  };

  // call method when submit add/edit form and recall grid API
  const handleCallAPI = (flag: boolean) => {
    setCallAPI(flag);
  };

  const CustomCell = (record: any) => {
    const [showContextualMenu, setShowContextualMenu] = useState(false);
    const onShowContextualMenu = (ev: any) => {
      ev.preventDefault(); // don't navigate
      setShowContextualMenu(true);
    };

    const menuItems = [
      {
        key: 'Edit',
        text: 'Edit',
        onClick: () => {
          setAction('Edit');
          openPanel({ userId: record.dataItem.userID });
        },
      },
      {
        key: 'Deactivate',
        text: record.dataItem.status ? 'Deactivate' : 'Activate',
        onClick: () => {
          deactivateUserAction(record.dataItem.userID, record.dataItem.status ? 0 : 1);
        },
      },
    ];

    const onHideContextualMenu = () => setShowContextualMenu(false);
    const linkRef = useRef(null);
    return (
      <ContextMenu
        items={menuItems}
        hidden={showContextualMenu}
        target={linkRef}
        onItemClick={onHideContextualMenu}
        onDismiss={onHideContextualMenu}
        className="grid-context-menu"
        contextClassName="contextmenu-td"
      >
        <a className="context-menu" ref={linkRef} href="/" onClick={onShowContextualMenu}>
          <span className="fa fa-ellipsis-h"></span>
        </a>
      </ContextMenu>
    );
  };

  const firstNamecell = (record: any) => {
    return <td className="text-capitalise">{record}</td>;
  };
  const lastNamecell = (record: any) => {
    return <td className="text-capitalise">{record}</td>;
  };
  const statuscell = (record: any) => {
    return <td>{record ? 'Active' : 'Inactive'}</td>;
  };

  const employeeNamecell = (record: string) => {
    return <td>{record ?? '-'}</td>;
  };

  const groupListingColumns = [
    {
      field: 'Action',
      title: 'Action',
      sortable: false,
      width: 60,
      cell: (record: any) => CustomCell(record),
      style: { textAlign: 'center' },
      id: 'action-column',
      locked: true,
    },
    {
      field: 'firstName',
      title: 'First Name',
      sortable: true,
      columnMenu: 'ColumnMenu',
      width: 200,
      cell: (record: any) => (
        <td className="text-capitalise">{record.dataItem.firstName}</td>
      ),
    },
    {
      field: 'lastName',
      title: 'Last Name',
      sortable: true,
      columnMenu: 'ColumnMenu',
      width: 200,
      cell: (record: any) => (
        <td className="text-capitalise">{record.dataItem.lastName}</td>
      ),
    },
    {
      field: 'loginEmail',
      title: 'Login Email',
      sortable: true,
      columnMenu: 'ColumnMenu',
      width: 315,
    },
    {
      field: 'status',
      title: 'Status',
      sortable: true,
      cell: (record: any) => <td>{record.dataItem.status ? 'Active' : 'Inactive'}</td>,
      width: 140,
      columnMenu: 'ColumnMenu',
      id: 'status-column',
    },
    {
      field: 'employeeName',
      title: 'Employee',
      sortable: true,
      columnMenu: 'ColumnMenu',
      width: 200,
      cell: (record: any) => (
        <td>{record.dataItem.employeeName ? record.dataItem.employeeName : '-'}</td>
      ),
    },
    {
      field: 'maximumATOMDevices',
      title: 'Maximum # Registered Atom device',
      sortable: true,
      columnMenu: 'ColumnMenu',
      width: 295,
    },
  ];

  const breadcrumlist = [
    {
      text: (
        <p>
          <img src={dashboardIcon} alt="Home" /> Dashboard
        </p>
      ),
      key: 'f1',
      href: '/',
      onClick: (e: any) => {
        e.preventDefault();
      },
    },
    {
      text: 'General Settings',
      key: 'f2',
      href: '/',
      onClick: (e: any) => {
        e.preventDefault();
      },
    },
    {
      text: 'Security & Compliance',
      key: 'f3',
      href: '/',
      onClick: (e: any) => {
        e.preventDefault();
      },
    },
    {
      text: 'User Management',
      key: 'f4',
      href: '/',
      onClick: (e: any) => {
        e.preventDefault();
      },
    },
  ];

  const createToast = (type: string, message: string) => {
    setToast({
      message,
      type,
      show: true,
    });
  };

  return (
    <>
      {componentLoader && (
        <div className="component-loader">
          <Loader />
        </div>
      )}
      <MessageContainer
        type={toast.type}
        text={toast.message}
        show={toast.show}
        setToast={setToast}
      />
      <UserListing
        groupListingColumns={groupListingColumns}
        isOpen={isOpen}
        openPanel={openPanel}
        dismissPanel={dismissPanel}
        breadcrumlist={breadcrumlist}
        action={action}
        setAction={setAction}
        handleCallAPI={handleCallAPI}
        callAPI={callAPI}
        createToast={createToast}
        showExportOption={showExportOption}
        setShowExportOption={setShowExportOption}
        componentLoader={componentLoader}
        handleChange={handleChange}
        handleClear={handleClear}
        searchKeyword={searchKeyword}
        excelExport={excelExport}
        data={data}
        onPageChange={onPageChange}
        page={page}
        totalRecords={totalRecords}
        onSortChange={onSortChange}
        sort={sort}
        onFilterChange={onFilterChange}
        filter={filter}
        _exportExcel={_exportExcel}
        _exportPDF={_exportPDF}
        setIsFormValid={setIsFormValid}
        isFormValid={isFormValid}
      />
    </>
  );
};

export default UserListingContainer;
