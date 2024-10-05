import { useCallback, useEffect, useMemo, useRef, useState } from 'react';
import debounce from 'lodash/debounce';
import RoleListing from './rolelisting';
import { ContextMenu, Loader } from '../CommonComponent';
import MessageContainer from '../CommonComponent/Message/messagecontainer';
import { dashboardIcon } from '../../assets/images';
import { callRoleApi } from './rolelistingapicalls';
import { encodeQuery } from '../SharedComponent/Common-function';
import { onExportCSV } from '../SharedComponent/Common-function';
import Constants from '../../utils/Constants';

const RoleListingContainer = () => {
  const [action, setAction] = useState('Add');
  const [isOpen, setIsOpen] = useState({ visible: false, data: {} });
  const [callAPI, setCallAPI] = useState<boolean>(false);
  const [showExportOption, setShowExportOption] = useState(false);
  const [componentLoader, setComponentLoader] = useState<boolean>(false);
  const [searchKeyword, setSearchKeyword] = useState('');
  const [isFormValid, setIsFormValid] = useState(false);
  const [data, setData] = useState([]);
  const [totalRecords, setTotalRecords] = useState(0);
  const [sort, setSort] = useState<any>([]);

  const [isSlideOpen, setSlideOpen] = useState(false);
  const openSlide = () => {
     setSlideOpen(true);
  };
  const closeSlide = () => {
     setSlideOpen(false);
  };

  const [filter, setFilter] = useState({
    logic: 'and',
    filters: [],
  });
  const [page, setPage] = useState({
    skip: 0,
    take: 10,
    currentPage: 1,
  });

  const [toast, setToast] = useState({
    show: false,
    message: '',
    type: '',
  });

  const _exportExcel: any = useRef(null);
  const _exportPDF: any = useRef(null);

  const openPanel = (data: any) => {
    setIsOpen({ visible: true, data: data });
  };

  const dismissPanel = (isSubmit: any) => {
    setIsOpen({ visible: false, data: {} });
    if (isSubmit) {
      handleCallAPI(true);
    }
  };

  // call method when submit add/edit form and recall grid API
  const handleCallAPI = (flag: boolean) => {
    setCallAPI(flag);
  };

  /**
   *
   * @param record
   * @returns
   * render custom cell
   */
  const CustomCell = (record: any) => {
    const [showContextualMenu, setShowContextualMenu] = useState(false);
    const onShowContextualMenu = (ev: any) => {
      ev.preventDefault(); // don't navigate
      setShowContextualMenu(true);
    };

    /**
     * action menu
     */
    const menuItems = [
      {
        key: 'Edit',
        text: 'Edit',
        onClick: (e: any) => {
          setAction('Edit');
          openPanel({
            roleId: record.dataItem.roleId,
            roleName: record.dataItem.roleName,
          });
        },
      },
      // {
      //   key: 'AssignUser',
      //   text: 'Assign User'
      // },
      // {
      //   key: 'Clone',
      //   text: 'Clone'
      // }
    ];

    const onHideContextualMenu = (item: any) => {
      setShowContextualMenu(false);
    };
    const linkRef = useRef(null);
    return (
      <>
        <ContextMenu
          items={menuItems}
          hidden={showContextualMenu}
          target={linkRef}
          onItemClick={onHideContextualMenu}
          onDismiss={onHideContextualMenu}
          className="grid-context-menu"
          contextClassName="contextmenu-td"
        >
          <a
            className="context-menu"
            ref={linkRef}
            href="/"
            onClick={onShowContextualMenu}
          >
            <span className="fa fa-ellipsis-h"></span>
          </a>
        </ContextMenu>
      </>
    );
  };

  /**
   * grid columns
   */
  const groupListingColumns = [
    {
      field: 'Action',
      title: 'Action',
      sortable: false,
      width: 100,
      cell: (record: any) => CustomCell(record),
      style: { textAlign: 'center' },
      id: 'action-column',
    },
    {
      field: 'roleName',
      title: 'Role Name',
      sortable: true,
      columnMenu: 'ColumnMenu',
      cell: (record: any) => (
        <td className="text-capitalise">{record.dataItem.roleName}</td>
      ),
    },
    {
      field: 'assignedUser',
      title: '# of Assigned Users',
      sortable: true,
      columnMenu: 'ColumnMenu',
      id: 'assigner-user',
      width: 260,
      cell: (record: any) => (
        <td className="text-center">{record.dataItem.assignedUser}</td>
      ),
    },
  ];

  /**
   * breadcrumlist
   */
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
      text: 'Role Management',
      key: 'f4',
      href: '/',
      onClick: (e: any) => {
        e.preventDefault();
      },
    },
  ];

  /**
   *
   * @param type
   * @param message
   * create toast message
   */
  const createToast = (type: string, message: string) => {
    setToast({
      message,
      type,
      show: true,
    });
  };

  useEffect(() => {
    fetchData(page.currentPage, page.take, searchKeyword);
  }, [sort, filter]);

  useEffect(() => {
    if (callAPI) {
      fetchData(page.currentPage, page.take, searchKeyword);
      handleCallAPI(false);
    }
  }, [callAPI]);

  /**
   *
   * @param currentPage
   * @param pageSize
   * @param searchText
   * @returns
   * get paginated payload
   */
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

    let userCount = '';
    if (filterArray && filterArray.length > 0) {
      filterArray.forEach((element: any) => {
        if (element.field === 'roleName') {
          searchText = element.value;
        } else if (element.field === 'assignedUser') {
          userCount = element.value;
        }
      });
    }

    const param = {
      roleName: searchText ? searchText : '',
      userCount: userCount,
      pageNumber: currentPage,
      pageSize: pageSize,
      orderBy: sort.length > 0 ? sort[0].field : '',
      sortOrder: sort.length > 0 ? sort[0].dir : '',
      fileType: fileType ? fileType : null,
      //userFilter: JSON.stringify(filterArray)
    };
    return { param, filterArray };
  };

  /**
   *
   * @param currentPage
   * @param pageSize
   * @param searchText
   * get grid data
   */
  const fetchData = async (currentPage = 1, pageSize = 10, searchText = '') => {
    // Static Logic
    // const skip = (currentPage - 1) * 10;
    // const l = JSON.parse(JSON.stringify(groupList));
    // const d: any = l.splice(skip, pageSize);
    try {
      setComponentLoader(true);
      const { param, filterArray } = getPaginationPayload(
        currentPage,
        pageSize,
        searchText
      );
      const url = encodeQuery(param);
      const payload = {
        endpoint: '/api/Role/Search?' + url,
        apiType: Constants.IAC,
        apiMethod: 'Get',
        payload: filterArray,
      };
      const res: any = await callRoleApi(payload);
      if (res.status === 200) {
        if (
          res.data &&
          res.data.searchRoleFilerResponse &&
          res?.data?.searchRoleFilerResponse?.length > 0
        ) {
          setData(res?.data?.searchRoleFilerResponse);
          setTotalRecords(res?.data?.count);
        } else {
          setData([]);
          setTotalRecords(0);
        }
      }
    } catch (error) {
      createToast('error', 'Something went wrong. Please try again');
    } finally {
      setComponentLoader(false);
    }
  };

  /**
   * get grid data when search
   */
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

  /**
   *
   * @param event
   * page change event
   */
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

  /**
   *
   * @param event
   * sorting event
   */
  const onSortChange = (event: any) => {
    setPage({
      ...page,
      currentPage: 1,
      skip: 0,
    });
    setSort(event?.sort);
  };

  /**
   *
   * @param event
   * filter change event
   */
  const onFilterChange = (event: any) => {
    setFilter(event.filter);
  };

  /**
   * search debounce
   */
  const debounceRequest = useMemo(() => {
    return debounce(fetchSearchData, 500);
  }, [fetchSearchData]);

  /**
   *
   * @param e
   * handle search event
   */
  const handleChange = (e: any) => {
    if (e?.target?.value.length <= 100) {
      setSearchKeyword(e.target.value);

      // Debounce the search callback
      debounceRequest(e.target.value);
    }
  };

  /**
   *
   * @param isExcel
   * export excel
   */
  const excelExport = async (isExcel = false, fileType: any) => {
    try {
      const { param, filterArray } = getPaginationPayload(
        1,
        99999,
        searchKeyword,
        fileType
      );
      setComponentLoader(true);
      const url = encodeQuery(param);
      const payload = {
        endpoint: '/api/Role/Search?' + url,
        apiType: Constants.IAC,
        apiMethod: 'Get',
        payload: filterArray,
      };
      const res: any = await callRoleApi(payload);
      if (res.status === 200) {
        if (
          res.data &&
          res?.data?.searchRoleFilerResponse &&
          res?.data?.searchRoleFilerResponse?.length > 0
        ) {
          if (isExcel && fileType === 'excel' && _exportExcel.current !== null) {
            _exportExcel.current.save(res?.data?.searchRoleFilerResponse);
            createToast('success', 'Data exported to Excel');
          } else if (!isExcel && _exportPDF.current !== null) {
            _exportPDF.current.save(res?.data?.searchRoleFilerResponse);
            createToast('success', 'Data exported to PDF');
          } else if (isExcel && fileType === 'csv') {
            onExportCSV(
              res?.data?.searchRoleFilerResponse,
              groupListingColumns,
              'RoleListing'
            );
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

  /**
   *
   * @param e
   * clear searchbox
   */
  const handleClear = (e: any) => {
    e.preventDefault();
    e.stopPropagation();
    setSearchKeyword('');
    setCallAPI(true);
  };

  /**
   * export click
   */
  const excelExportClick = () => {
    setShowExportOption(!showExportOption);
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
      <RoleListing
              groupListingColumns={groupListingColumns}
              isOpen={isOpen}
              openPanel={openPanel}
              dismissPanel={dismissPanel}
              breadcrumlist={breadcrumlist}
              action={action}
              setAction={setAction}
              createToast={createToast}
              showExportOption={showExportOption}
              excelExport={excelExport}
              _exportExcel={_exportExcel}
              _exportPDF={_exportPDF}
              onSearchChange={handleChange}
              searchKeyword={searchKeyword}
              handleClear={handleClear}
              excelExportClick={excelExportClick}
              data={data}
              onPageChange={onPageChange}
              take={page.take}
              skip={page.skip}
              total={totalRecords}
              onSortChange={onSortChange}
              sort={sort}
              onFilterChange={onFilterChange}
              filter={filter}
              isFormValid={isFormValid}
              setIsFormValid={setIsFormValid}
              setShowExportOption={setShowExportOption}
              isSlideOpen={isSlideOpen}
              openSlide={openSlide}
              closeSlide={closeSlide}

      />
    </>
  );
};

export default RoleListingContainer;
