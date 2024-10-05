import React, { useState, useRef, useEffect, useCallback, useMemo } from 'react';
import ScheduleListing from './schedulelisting';
import { ContextMenu, Loader } from '../CommonComponent';
import { dashboardIcon } from '../../assets/images';
import MessageContainer from '../CommonComponent/Message/messagecontainer';
import { callScheduleTypeApi } from './schedulelistingapicalls';
import moment from 'moment';
import { debounce } from 'lodash';
import { onExportCSV } from '../SharedComponent/Common-function';
import { encodeQuery } from '../../utils/Utils';
import Constants from '../../utils/Constants';

interface FormProps {
  scheduleTypeId?: any;
  scheduleTypeCode?: any;
  scheduleTypeName?: any;
  unitOrComponent?: any;
  isActive?: any;
  schedulable?: any;
  gbp?: any;
}
const defaultFormData: FormProps = {
  scheduleTypeCode: null,
  scheduleTypeName: null,
  isActive: { value: 1, label: 'Active' },
  unitOrComponent: null,
  schedulable: false,
  gbp: false,
};

// const editFormData: FormProps = {
//   scheduleTypeId: 1,
//   scheduleTypeCode: 'editCodeTest',
//   scheduleTypeName: 'editNameTest',
//   isActive: { value: 0, label: 'Inactive' },
//   unitOrComponent: { value: 'Component', label: 'Component' },
//   schedulable: false,
//   gbp: true

// }

const ScheduleListingContainer = () => {
  const [searchKeyword, setSearchKeyword] = useState('');
  const [data, setData] = useState([]);
  const [totalRecords, setTotalRecords] = useState(0);
  const [sort, setSort] = useState<any>([]);
  const [callAPI, setCallAPI] = useState<boolean>();
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

  const [isOpen, setIsOpen] = useState(false);
  const [action, setAction] = useState('Add');
  const [formData, setFormData] = useState(defaultFormData);
  const [isFormValid, setIsFormValid] = useState(false);
  const [showExportOption, setShowExportOption] = useState(false);
  const [unitComponentOptions, setUnitComponentOptions] = useState([]);
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

  const createToast = (type: string, message: string) => {
    setToast({
      message,
      type,
      show: true,
    });
  };

  // Confirmation popup handling
  const handlePopupChange = (show: boolean, dataItem: any = null) => {
    showConfirmationPopup({
      show,
      dataItem,
    });
  };

  const openPanel = () => {
    setIsOpen(true);
  };
  const dismissPanel = () => {
    setFormData(defaultFormData);
    setAction('Add');
    setIsOpen(false);
  };

  const getUnitOrComponentOptions = async () => {
    try {
      const payload = {
        endpoint: '/api/ScheduleType/GetUnitorComponent',
        apiType: Constants.WorkManagement,
        apiMethod: 'Get',
      };

      const res: any = await callScheduleTypeApi(payload);

      if (res.status === 200) {
        const options: any = [];
        res?.data?.forEach((el: any) => {
          options.push({ value: el?.unitComponentsID, label: el?.unitOrComponent });
        });
        setUnitComponentOptions(options);
      }
    } catch (e) {
      console.log('error', 'Something went wrong to get Unit/Component list.');
    }
  };

  // Call method to activate/deactivate Schedule Listing
  const deactivateScheduleTypeAction = async (scheduleTypeId: any, isActive: number) => {
    try {
      setComponentLoader(true);

      const payload = {
        endpoint: `/api/ScheduleType/ScheduleTypeActivateDeactivate?ScheduleTypeId=${scheduleTypeId}&Status=${!!isActive}`,
        apiType: Constants.WorkManagement,
        apiMethod: 'Put',
      };

      const res: any = await callScheduleTypeApi(payload);
      if (res.status === 200) {
        createToast(
          'success',
          `Schedule Type has been ${isActive ? 'activated' : 'deactivated'}!`
        );
        setCallAPI(true);
      }
    } catch (e) {
      console.log('Something went wrong', e);
    } finally {
      setComponentLoader(false);
    }
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
          let isActive = record?.dataItem?.isActive
            ? { value: 1, label: 'Active' }
            : { value: 0, label: 'Inactive' };
          let unitOrComponent = record?.dataItem?.unitOrComponent
            ? record?.dataItem?.unitOrComponent === 'Component'
              ? { value: 2, label: 'Component' }
              : { value: 1, label: 'Unit' }
            : null;
          setAction('Edit');
          setFormData({ ...record?.dataItem, isActive, unitOrComponent });
          openPanel();
        },
      },
      {
        key: 'Delete',
        text: 'Delete',
        onClick: () => {
          let isActive = record?.dataItem?.isActive
            ? { value: 1, label: 'Active' }
            : { value: 0, label: 'Inactive' };
          let unitOrComponent = record?.dataItem?.unitOrComponent
            ? record?.dataItem?.unitOrComponent === 2
              ? { value: 2, label: 'Component' }
              : { value: 1, label: 'Unit' }
            : null;
          handlePopupChange(true, { ...record?.dataItem, isActive, unitOrComponent });
        },
      },
      {
        key: 'Deactivate',
        text: record.dataItem.isActive ? 'Deactivate' : 'Activate',
        onClick: () => {
          deactivateScheduleTypeAction(
            record.dataItem.scheduleTypeId,
            record?.dataItem?.isActive ? 0 : 1
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

  const groupListingColumns = [
    {
      field: 'Action',
      title: 'Action',
      sortable: false,
      width: 80,
      cell: (record: any) => CustomCell(record),
      style: { textAlign: 'center' },
      id: 'action-column',
      locked: true,
    },
    {
      field: 'scheduleTypeCode',
      title: 'Code',
      sortable: true,
      columnMenu: 'ColumnMenu',
      width: 160,
    },
    {
      field: 'scheduleTypeName',
      title: 'Name',
      sortable: true,
      columnMenu: 'ColumnMenu',
      width: 200,
    },
    {
      field: 'unitOrComponent',
      title: 'Unit Or Component',
      sortable: true,
      // cell: (record: any) => <td>{record?.dataItem?.unitOrComponent === 2 ? "Component" : "Unit"}</td>,
      columnMenu: 'ColumnMenu',
      width: 200,
      cell: (props: any) => (
        <td>
          {props?.dataItem?.unitOrComponent ? props?.dataItem?.unitOrComponent : '-'}
        </td>
      ),
    },
    {
      field: 'isActive',
      title: 'Status',
      sortable: true,
      cell: (record: any) => (
        <td>{record?.dataItem?.isActive ? 'Active' : 'Inactive'}</td>
      ),
      columnMenu: 'ColumnMenu',
      id: 'status-column',
      width: 130,
    },
    {
      field: 'createdBy',
      title: 'Created By',
      sortable: true,
      columnMenu: 'ColumnMenu',
      editable: false,
      width: 160,
      cell: (props: any) => (
        <td>{props?.dataItem?.createdBy ? props?.dataItem?.createdBy : '-'}</td>
      ),
    },
    {
      field: 'createdDateUTC',
      title: 'Created Date',
      sortable: true,
      columnMenu: 'ColumnMenu',
      editable: false,
      format: '{0:dd/MM/yyy}',
      filter: 'date',
      width: 160,
      cell: (props: any) => (
        <td>
          {props?.dataItem?.createdDateUTC
            ? moment(props?.dataItem?.createdDateUTC).format('MM/DD/YYYY')
            : '-'}
        </td>
      ),
    },
    {
      field: 'modifiedBy',
      title: 'Modified By',
      sortable: true,
      columnMenu: 'ColumnMenu',
      editable: false,
      width: 160,
      cell: (props: any) => (
        <td>{props?.dataItem?.modifiedBy ? props?.dataItem?.modifiedBy : '-'}</td>
      ),
    },
    {
      field: 'modifiedDateUTC',
      title: 'Modified Date',
      sortable: true,
      columnMenu: 'ColumnMenu',
      editable: false,
      format: '{0:dd/MM/yyy}',
      filter: 'date',
      width: 160,
      cell: (props: any) => (
        <td>
          {props?.dataItem?.modifiedDateUTC
            ? moment(props?.dataItem?.modifiedDateUTC).format('MM/DD/YYYY')
            : '-'}
        </td>
      ),
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
      text: 'Resources',
      key: 'f3',
      href: '/',
      onClick: (e: any) => {
        e.preventDefault();
      },
    },
    {
      text: 'Schedule Types',
      key: 'f4',
      href: '/',
      onClick: (e: any) => {
        e.preventDefault();
      },
    },
  ];
  const excelExportClick = () => {
    setShowExportOption(!showExportOption);
  };

  // methods=====================================
  const onSave = async (data: any) => {
    try {
      setComponentLoader(true);
      if (data?.scheduleTypeName?.length > 0 && data?.scheduleTypeCode?.length > 0) {
        const dataObj = {
          scheduleTypeName: data?.scheduleTypeName,
          scheduleTypeCode: data?.scheduleTypeCode,
          unitOrComponent: data?.unitOrComponent ? data?.unitOrComponent?.value : null,
          schedulable: data?.schedulable,
          gbp: data?.gbp,
          isActive: !!data?.isActive?.value,
        };

        const payload = {
          endpoint: '/api/ScheduleType',
          apiType: Constants.WorkManagement,
          apiMethod: 'Post',
          payload: dataObj,
        };

        const res: any = await callScheduleTypeApi(payload);

        if (res.status === 200) {
          setFormData(defaultFormData);
          createToast('success', 'Schedule Type created successfully..');
          setCallAPI(true);
          dismissPanel();
        } else if (res.status === 203) {
          createToast('error', res?.data);
        }
      } else if (data?.scheduleTypeCode?.length === 0) {
        createToast('error', 'ScheduleTypeCode must required.');
      } else if (data?.scheduleTypeName?.length === 0) {
        createToast('error', 'ScheduleTypeName must required.');
      }
    } catch (e) {
      createToast('error', 'Something went wrong. Please try again');
    } finally {
      setComponentLoader(false);
    }
  };
  const onUpdate = async (data: any) => {
    try {
      setComponentLoader(true);
      if (data?.scheduleTypeName?.length > 0 && data?.scheduleTypeCode?.length > 0) {
        const dataObj = {
          scheduleTypeId: data?.scheduleTypeId,
          scheduleTypeName: data?.scheduleTypeName,
          scheduleTypeCode: data?.scheduleTypeCode,
          unitOrComponent: data?.unitOrComponent ? data?.unitOrComponent?.value : null,
          schedulable: data?.schedulable,
          gbp: data?.gbp,
          isActive: !!data?.isActive?.value,
          isDeleted: false,
        };

        const payload = {
          endpoint: '/api/ScheduleType',
          apiType: Constants.WorkManagement,
          apiMethod: 'Put',
          payload: dataObj,
        };

        const res: any = await callScheduleTypeApi(payload);

        if (res.status === 200) {
          setFormData(defaultFormData);
          createToast('success', 'Schedule Type updated successfully..');
          setCallAPI(true);
          dismissPanel();
        } else if (res.status === 203) {
          createToast('error', res?.data);
        }
      } else if (data?.scheduleTypeCode?.length === 0) {
        createToast('error', 'ScheduleTypeCode must required.');
      } else if (data?.scheduleTypeName?.length === 0) {
        createToast('error', 'ScheduleTypeName must required.');
      }
    } catch (e) {
      createToast('error', 'Something went wrong. Please try again');
    } finally {
      setComponentLoader(false);
    }
  };
  const onDelete = async (data: any) => {
    handlePopupChange(false);
    try {
      setComponentLoader(true);
      if (data?.scheduleTypeId) {
        const dataObj = {
          scheduleTypeId: data?.scheduleTypeId,
          scheduleTypeName: data?.scheduleTypeName,
          scheduleTypeCode: data?.scheduleTypeCode,
          unitOrComponent: data?.unitOrComponent ? data?.unitOrComponent?.value : null,
          schedulable: data?.schedulable,
          gbp: data?.gbp,
          isActive: !!data?.isActive?.value,
          isDeleted: true,
        };

        const payload = {
          endpoint: '/api/ScheduleType',
          apiType: Constants.WorkManagement,
          apiMethod: 'Put',
          payload: dataObj,
        };

        const res: any = await callScheduleTypeApi(payload);

        if (res.status === 200) {
          setFormData(defaultFormData);
          createToast('success', 'Schedule Type deleted successfully..');
          setCallAPI(true);
        }
      }
    } catch (e) {
      createToast('error', 'Something went wrong. Please try again');
    } finally {
      setComponentLoader(false);
    }
  };
  // methods end=====================================

  useEffect(() => {
    if (unitComponentOptions?.length === 0) {
      getUnitOrComponentOptions();
    }
  }, []);

  useEffect(() => {
    if (
      formData?.scheduleTypeName?.length > 0 &&
      formData?.scheduleTypeCode?.length > 0
    ) {
      setIsFormValid(true);
    } else {
      setIsFormValid(false);
    }
  }, [formData]);

  //===pagination and exports================

  useEffect(() => {
    fetchData(page.currentPage, page.take, searchKeyword);
  }, [sort, filter]);

  useEffect(() => {
    if (callAPI) {
      fetchData(page.currentPage, page.take, searchKeyword);
      setCallAPI(false);
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
      let url = encodeQuery(param);
      const payload = {
        endpoint: '/api/ScheduleType/Search?' + url,
        apiType: Constants.WorkManagement,
        apiMethod: 'Post',
        payload: filterArray,
      };

      const res: any = await callScheduleTypeApi(payload);

      if (res.status === 200) {
        if (res.data && res.data.scheduleTypes && res.data.scheduleTypes.length > 0) {
          let list: any = res.data.scheduleTypes.map((item: any) => {
            item['isActive'] = item.isActive ? 'Active' : 'Inactive';
            item['createdBy'] = item.createdBy || '-';
            item['modifiedBy'] = item.modifiedBy || '-';
            if (isExcel && _exportExcel.current !== null) {
              item['createdDateUTC'] = item.createdDateUTC
                ? moment(item.createdDateUTC).format('MM/DD/YYYY')
                : '-';
              item['modifiedDateUTC'] = item.modifiedDateUTC
                ? moment(item.modifiedDateUTC).format('MM/DD/YYYY')
                : '-';
            }
            return item;
          });
          if (isExcel && fileType === 'excel' && _exportExcel.current !== null) {
            _exportExcel.current.save(list);
            createToast('success', 'Data exported to Excel');
          } else if (!isExcel && _exportPDF.current !== null) {
            _exportPDF.current.save(list);
            createToast('success', 'Data exported to PDF');
          } else if (isExcel && fileType === 'csv') {
            onExportCSV(list, groupListingColumns, 'ScheduleTypes');
            createToast('success', 'Data exported to CSV');
          }
        }
      }
    } catch (e) {
      createToast('error', 'Something went wrong. Please try again!');
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
      SearchFilter: searchText || '',
      PageNumber: currentPage,
      PageSize: pageSize,
      OrderBy: sort.length > 0 ? sort[0].field : '',
      SortOrderBy: sort.length > 0 ? sort[0].dir : '',
      fileType: fileType ? fileType : null,
    };
    return { param, filterArray };
  };

  const fetchData = async (currentPage = 1, pageSize = 10, searchText = '') => {
    const { param, filterArray } = getPaginationPayload(
      currentPage,
      pageSize,
      searchText
    );

    try {
      setComponentLoader(true);
      let url = encodeQuery(param);
      const payload = {
        endpoint: '/api/ScheduleType/Search?' + url,
        apiType: Constants.WorkManagement,
        apiMethod: 'Post',
        payload: filterArray,
      };

      const res: any = await callScheduleTypeApi(payload);
      if (res.status === 200) {
        if (res.data && res.data.scheduleTypes && res.data.scheduleTypes.length > 0) {
          setData(res?.data?.scheduleTypes);
          setTotalRecords(res?.data.count);
        } else {
          setData([]);
          setTotalRecords(0);
        }
      }
    } catch (error) {
      createToast('error', 'Something went wrong. Please try again!');
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
    setCallAPI(true);
  };
  //=========================================

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

      <ScheduleListing
        data={data}
        groupListingColumns={groupListingColumns}
        breadcrumlist={breadcrumlist}
        unitComponentOptions={unitComponentOptions}
        openPanel={openPanel}
        dismissPanel={dismissPanel}
        isOpen={isOpen}
        action={action}
        excelExportClick={excelExportClick}
        showExportOption={showExportOption}
        formData={formData}
        setFormData={setFormData}
        onSave={onSave}
        onUpdate={onUpdate}
        isFormValid={isFormValid}
        onSearchChange={handleChange}
        searchKeyword={searchKeyword}
        handleClear={handleClear}
        excelExport={excelExport}
        onPageChange={onPageChange}
        take={page.take}
        skip={page.skip}
        total={totalRecords}
        onSortChange={onSortChange}
        sort={sort}
        onFilterChange={onFilterChange}
        filter={filter}
        _exportExcel={_exportExcel}
        _exportPDF={_exportPDF}
        toast={toast}
        setToast={setToast}
        confirmationPopup={confirmationPopup}
        handlePopupChange={handlePopupChange}
        onDelete={onDelete}
        setShowExportOption={setShowExportOption}
      />
    </>
  );
};

export default ScheduleListingContainer;
