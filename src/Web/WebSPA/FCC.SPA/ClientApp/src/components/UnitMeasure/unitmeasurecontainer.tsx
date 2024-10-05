import React, { useState, useRef, useEffect, useCallback, useMemo } from 'react';
import dashboardIcon from '../../assets/images/Home.svg';
import { unitOfMeasureAPI } from './unitmeasureapicalls';
import MessageContainer from '../CommonComponent/Message/messagecontainer';
import UnitMeasure from './unitmeasure';
import { ContextMenu, Loader } from '../CommonComponent';
import moment from 'moment';
import { debounce } from 'lodash';
import { onExportCSV } from '../SharedComponent/Common-function';
import Constants from '../../utils/Constants';

const UnitMeasureContainer = () => {
  const [showExportOption, setShowExportOption] = useState(false);
  const [callAPI, setCallAPI] = useState<boolean>(false);
  const [isOpen, setIsOpen] = useState({ visible: false, data: {} });
  const [action, setAction] = useState('Add');
  const [componentLoader, setComponentLoader] = useState<boolean>(false);
  const [toast, setToast] = useState({
    show: false,
    message: '',
    type: '',
  });
  const [confirmationPopup, showConfirmationPopup] = useState({
    show: false,
    dataItem: null,
  });
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

  const openPanel = (data: any) => {
    setIsOpen({ visible: true, data: data });
  };
  const dismissPanel = (isSubmit: any) => {
    setIsOpen({ visible: false, data: {} });
    if (isSubmit) {
      handleCallAPI(true);
    }
  };
  // Confirmation popup handling
  const handlePopupChange = (show: boolean, dataItem: any = null) => {
    showConfirmationPopup({
      show,
      dataItem,
    });
  };

  // Call method to activate/deactivate UoM
  const deactivateUoMAction = async (unitMeasureId: any, isActive: number) => {
    try {
      setComponentLoader(true);
      const payload = {
        endpoint: `/api/UnitOfMeasure/Status?unitMeasureId=${unitMeasureId}&Status=${!!isActive}`,
        apiType: Constants.WorkManagement,
        apiMethod: 'Put',
        payload: null,
      };
      const res: any = await unitOfMeasureAPI(payload);
      if (res.status === 200) {
        createToast(
          'success',
          `Unit Of Measure has been ${isActive ? 'activated' : 'deactivated'}!`
        );
        handleCallAPI(true);
      }
    } catch (e) {
      console.log('Something went wrong', e);
    } finally {
      setComponentLoader(false);
    }
  };

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
      text: 'System Setting',
      key: 'f3',
      href: '/',
      onClick: (e: any) => {
        e.preventDefault();
      },
    },
    {
      text: 'Unit of Measure',
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
          openPanel({ unitMeasureData: record.dataItem });
        },
      },
      {
        key: 'Delete',
        text: 'Delete',
        onClick: () => {
          handlePopupChange(true, record.dataItem);
        },
      },
      {
        key: 'Deactivate',
        text: record.dataItem.isActive === 'Active' ? 'Deactivate' : 'Activate',
        onClick: () => {
          deactivateUoMAction(
            record.dataItem.unitMeasureId,
            record.dataItem.isActive === 'Active' ? 0 : 1
          );
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

  const deleteUnitOfMeasure = async (record: any) => {
    handlePopupChange(false);
    try {
      const requestData = {
        unitMeasureId: record.unitMeasureId,
        unitMeasureCode: record.unitMeasureCode,
        unitMeasureName: record.unitMeasureDisplayValue,
        unitMeasureTypeId: record.unitMeasureTypeId,
        conversionFactor: record.conversionFactor,
        isActive: record.isActive === 'Active' ? true : false,
        isDelete: true,
      };
      setComponentLoader(true);
      const payload = {
        endpoint: `/api/UnitOfMeasure`,
        apiType: Constants.WorkManagement,
        apiMethod: 'Put',
        payload: requestData,
      };
      const res: any = await unitOfMeasureAPI(payload);
      if (res.status === 200) {
        createToast('success', 'Unit Of Measure deleted successfully!');
        dismissPanel(true);
      }
    } catch (error: any) {
      createToast('error', 'Something went wrong. Please try again');
    } finally {
      setComponentLoader(false);
    }
  };

  const unitMeasureColumns = [
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
      field: 'unitMeasureCode',
      title: 'Code',
      sortable: true,
      columnMenu: 'ColumnMenu',
      width: 120,
    },
    {
      field: 'unitMeasureDisplayValue',
      title: 'Name',
      sortable: true,
      columnMenu: 'ColumnMenu',
      width: 155,
    },
    {
      field: 'unitMeasureTypeDescription',
      title: 'Measure Type',
      sortable: true,
      columnMenu: 'ColumnMenu',
      width: 150,
      cell: (record: any) => <td>{record.dataItem.unitMeasureTypeDescription || '-'}</td>,
    },
    {
      field: 'conversionFactor',
      title: 'Conversion Factor',
      sortable: true,
      columnMenu: 'ColumnMenu',
      width: 180,
      cell: (record: any) => (
        <td className="text-right">{record.dataItem.conversionFactor || '-'}</td>
      ),
      // cell: (record: any) => <td className="text-right">{record.dataItem.conversionFactor}</td>
    },
    {
      field: 'isActive',
      title: 'Status',
      sortable: true,
      cell: (record: any) => (
        <td>{record.dataItem.isActive === 'Active' ? 'Active' : 'Inactive'}</td>
      ),
      width: 130,
      columnMenu: 'ColumnMenu',
      id: 'status-column',
    },
    {
      field: 'createdBy',
      title: 'Created By',
      sortable: true,
      columnMenu: 'ColumnMenu',
      width: 150,
      cell: (record: any) => <td>{record.dataItem.createdBy || '-'}</td>,
    },
    {
      field: 'createdDateUTC',
      title: 'Created Date',
      sortable: true,
      columnMenu: 'ColumnMenu',
      width: 160,
      format: '{0:dd/MM/yyy}',
      filter: 'date',
      cell: (record: any) => <td>{record.dataItem.createdDateUTC || '-'}</td>,
    },
    {
      field: 'modifiedBy',
      title: 'Modified By',
      sortable: true,
      columnMenu: 'ColumnMenu',
      width: 150,
      cell: (record: any) => <td>{record.dataItem.modifiedBy || '-'}</td>,
    },
    {
      field: 'modifiedDateUTC',
      title: 'Modified Date',
      sortable: true,
      columnMenu: 'ColumnMenu',
      width: 160,
      format: '{0:dd/MM/yyy}',
      filter: 'date',
      cell: (record: any) => <td>{record.dataItem.modifiedDateUTC || '-'}</td>,
    },
  ];
  const excelExportClick = () => {
    setShowExportOption(!showExportOption);
  };

  // export and pagination ===========

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
      const param = getPaginationPayload(1, 99999, searchKeyword, fileType);
      setComponentLoader(true);
      const payload = {
        endpoint: `/api/UnitOfMeasure/Search`,
        apiType: Constants.WorkManagement,
        apiMethod: 'Post',
        payload: param,
      };
      const res: any = await unitOfMeasureAPI(payload);

      if (res.status === 200) {
        if (
          res.data &&
          res.data.getAllUnitOfMeasuresListFilter &&
          res.data.getAllUnitOfMeasuresListFilter.length > 0
        ) {
          let list: any = res.data.getAllUnitOfMeasuresListFilter.map((item: any) => {
            item['isActive'] = item.isActive === 'Active' ? 'Active' : 'Inactive';
            item['unitMeasureTypeDescription'] = item?.unitMeasureTypeDescription || '-';
            item['conversionFactor'] = item?.conversionFactor || '-';
            item['modifiedBy'] = item?.modifiedBy || '-';
            item['modifiedDateUTC'] = item?.modifiedDateUTC || '-';
            item['createdBy'] = item?.createdBy || '-';
            item['createdDateUTC'] = item?.createdDateUTC || '-';
            return item;
          });
          if (isExcel && fileType === 'excel' && _exportExcel.current !== null) {
            _exportExcel.current.save(list);
            createToast('success', 'Data exported to Excel');
          } else if (!isExcel && _exportPDF.current !== null) {
            _exportPDF.current.save(list);
            createToast('success', 'Data exported to PDF');
          } else if (isExcel && fileType === 'csv') {
            onExportCSV(list, unitMeasureColumns, 'UnitOfMeasure');
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
    setShowExportOption(false);
  };

  const getPaginationPayload = (
    currentPage = 1,
    pageSize = 10,
    searchText = '',
    fileType = null
  ) => {
    const filterArray: any = [];
    const currentFilter: any =
      filter?.filters.length > 0 ? JSON.parse(JSON.stringify([...filter.filters])) : [];
    if (currentFilter && currentFilter.length > 0) {
      currentFilter.forEach((element: any) => {
        if (element.filters.length > 0) {
          let payload = element.filters[0];
          if (payload.field === 'createdDateUTC' || payload.field === 'modifiedDateUTC') {
            payload.value = moment(payload.value).format('MM/DD/YYYY');
            filterArray.push(payload);
          } else {
            filterArray.push(element.filters[0]);
          }
        }
      });
    }
    const param = {
      globalSearch: searchText ? searchText : null,
      PageNumber: currentPage,
      PageSize: pageSize,
      sortBy: sort.length > 0 ? sort[0].field : null,
      sortOrder: sort.length > 0 ? sort[0].dir : null,
      columnFilters: filterArray,
      fileType: fileType ? fileType : null,
    };
    return param;
  };

  const fetchData = async (currentPage = 1, pageSize = 10, searchText = '') => {
    const param = getPaginationPayload(currentPage, pageSize, searchText);

    try {
      setComponentLoader(true);
      const payload = {
        endpoint: `/api/UnitOfMeasure/Search`,
        apiType: Constants.WorkManagement,
        apiMethod: 'Post',
        payload: param,
      };
      const res: any = await unitOfMeasureAPI(payload);
      if (res.status === 200) {
        if (
          res.data &&
          res.data.getAllUnitOfMeasuresListFilter &&
          res.data.getAllUnitOfMeasuresListFilter.length > 0
        ) {
          setData(res.data.getAllUnitOfMeasuresListFilter);
          setTotalRecords(res.data.count);
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

  //==================================

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
      <UnitMeasure
        data={data}
        unitMeasureColumns={unitMeasureColumns}
        breadcrumlist={breadcrumlist}
        openPanel={openPanel}
        dismissPanel={dismissPanel}
        isOpen={isOpen}
        action={action}
        excelExportClick={excelExportClick}
        showExportOption={showExportOption}
        createToast={createToast}
        handleCallAPI={handleCallAPI}
        callAPI={callAPI}
        setShowExportOption={setShowExportOption}
        setComponentLoader={setComponentLoader}
        setAction={setAction}
        componentLoader={componentLoader}
        confirmationPopup={confirmationPopup}
        handlePopupChange={handlePopupChange}
        deleteUnitOfMeasure={deleteUnitOfMeasure}
        handleChange={handleChange}
        handleClear={handleClear}
        searchKeyword={searchKeyword}
        excelExport={excelExport}
        onPageChange={onPageChange}
        page={page}
        totalRecords={totalRecords}
        onSortChange={onSortChange}
        sort={sort}
        onFilterChange={onFilterChange}
        filter={filter}
        _exportExcel={_exportExcel}
        _exportPDF={_exportPDF}
      />
    </>
  );
};

export default UnitMeasureContainer;
