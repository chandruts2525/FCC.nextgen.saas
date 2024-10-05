import React, { useCallback, useEffect, useMemo, useRef, useState } from 'react';
import { debounce } from 'lodash';
import moment from 'moment';
import EmployeeListing from './employeelisting';
import { dashboardIcon } from '../../assets/images';
import { DropDownCell } from './dropdowncell';
import { TextFieldCell } from './textfieldcell';
import { Loader } from '../CommonComponent';
import { employeeAPI } from './employeelistingapicall';
import MessageContainer from '../CommonComponent/Message/messagecontainer';
import { onExportCSV } from '../SharedComponent/Common-function';
import Constants from '../../utils/Constants';

const EmployeeListingContainer = () => {
  const initialState = {
    isActive: {
      label: 'Active',
      value: 1,
    },
    employeeTypeName: '',
    employeeTypeId: 0,
  };
  const [showExportOption, setShowExportOption] = useState(false);
  const [formData, setFormData] = useState(initialState);
  const [componentLoader, setComponentLoader] = useState<boolean>(false);
  const [toast, setToast] = useState({
    show: false,
    message: '',
    type: '',
  });
  const [data, setData] = useState([]);
  const [searchKeyword, setSearchKeyword] = useState('');
  const [totalRecords, setTotalRecords] = useState(0);
  const [sort, setSort] = useState<any>([]);
  const [callAPI, handleCallAPI] = useState(false);
  const [disableSearch, setDisableSearch] = useState(false);
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

  const encodeQuery = (params: any) => {
    let query = '';
    for (let d in params)
      query += encodeURIComponent(d) + '=' + encodeURIComponent(params[d]) + '&';
    return query.slice(0, -1);
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
      text: 'Employees',
      key: 'f3',
      href: '/',
      onClick: (e: any) => {
        e.preventDefault();
      },
    },
    {
      text: 'Employee Types',
      key: 'f4',
      href: '/',
      onClick: (e: any) => {
        e.preventDefault();
      },
    },
  ];

  const statusOptions: any = [
    { value: 1, label: 'Active' },
    { value: 0, label: 'Inactive' },
  ];

  const EmployeeListingColumns = [
    {
      field: 'employeeTypeName',
      title: 'Name',
      sortable: true,
      columnMenu: 'ColumnMenu',
      width: 390,
      cell: (props: any) => (
        <TextFieldCell
          {...props}
          defaultValue={formData.employeeTypeName}
          maxLength={100}
          placeholder="Name"
          onChange={(e: any) => {
            formData.employeeTypeName = e.target.value;
          }}
        />
      ),
    },
    {
      field: 'isActive',
      title: 'Status',
      sortable: true,
      columnMenu: 'ColumnMenu',
      width: 150,
      cell: (props: any) => (
        <DropDownCell
          {...props}
          statusOptions={statusOptions}
          defaultValue={formData.isActive}
          onChange={(e: any) => {
            formData.isActive = e;
          }}
        />
      ),
    },
    {
      field: 'createdBy',
      title: 'Created By',
      sortable: true,
      columnMenu: 'ColumnMenu',
      editable: false,
      width: 200,
      cell: (props: any) => (
        <td>{props.dataItem.createdBy ? props.dataItem.createdBy : '-'}</td>
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
      width: 200,
      cell: (props: any) => (
        <td>
          {props.dataItem.createdDateUTC
            ? moment(props.dataItem.createdDateUTC).format('MM/DD/YYYY')
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
      width: 200,
      cell: (props: any) => (
        <td>{props.dataItem.modifiedBy ? props.dataItem.modifiedBy : '-'}</td>
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
      width: 208,
      cell: (props: any) => (
        <td>
          {props.dataItem.modifiedDateUTC
            ? moment(props.dataItem.modifiedDateUTC).format('MM/DD/YYYY')
            : '-'}
        </td>
      ),
    },
  ];

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
        endpoint: '/api/Employee/Search?' + url,
        apiType: Constants.WorkManagement,
        apiMethod: 'Post',
        payload: filterArray,
      };
      const res: any = await employeeAPI(payload);

      if (res.status === 200) {
        if (
          res.data &&
          res.data?.getAllEmployeeTypes &&
          res.data?.getAllEmployeeTypes?.length > 0
        ) {
          let list: any = res?.data?.getAllEmployeeTypes?.map((item: any) => {
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
            onExportCSV(list, EmployeeListingColumns, 'EmployeeTypes');
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
      const url: any = encodeQuery(param);
      const payload = {
        endpoint: '/api/Employee/Search?' + url,
        apiType: Constants.WorkManagement,
        apiMethod: 'Post',
        payload: filterArray,
      };
      const res: any = await employeeAPI(payload);
      if (res.status === 200) {
        if (
          res.data &&
          res?.data?.getAllEmployeeTypes &&
          res?.data?.getAllEmployeeTypes.length > 0
        ) {
          setData(res?.data?.getAllEmployeeTypes);
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

  const excelExportClick = () => {
    setShowExportOption(!showExportOption);
  };

  const createToast = (type: string, message: string) => {
    setToast({
      message,
      type,
      show: true,
    });
  };

  const onUpdate = () => {
    const payload = {
      employeeTypeId: formData.employeeTypeId,
      employeeTypeName: formData.employeeTypeName,
      isActive: formData.isActive.value ? true : false,
    };
    updateRecord(payload, 'Employee type updated successfully.', true);
  };

  const updateRecord = async (request: any, message: string, isUpdate = false) => {
    try {
      setComponentLoader(true);
      const payload = {
        endpoint: '/api/Employee',
        apiType: Constants.WorkManagement,
        apiMethod: 'Put',
        payload: request,
      };
      const res: any = await employeeAPI(payload);
      if (res.status === 200) {
        setFormData(initialState);
        createToast('success', message);
        handleCallAPI(true);
      } else if (isUpdate && res.status === 203) {
        createToast('error', res.data || 'Employee already exists');
      }
    } catch (e) {
      console.log('Something went wrong', e);
      createToast('error', 'Something went wrong. Please try again');
    } finally {
      setComponentLoader(false);
    }
  };

  const onDelete = (dataItem: any) => {
    const payload = {
      employeeTypeId: dataItem.employeeTypeId,
      employeeTypeName: dataItem.employeeTypeName,
      isActive: true,
      isDeleted: true,
    };
    updateRecord(payload, 'Employee type deleted successfully.');
  };

  const onEditClick = async (dataItem: any) => {
    setFormData({
      employeeTypeId: dataItem.employeeTypeId,
      employeeTypeName: dataItem.employeeTypeName,
      isActive: dataItem.isActive ? statusOptions[0] : statusOptions[1],
    });
  };

  const onDiscard = () => {
    setFormData(initialState);
  };

  const onSave = async () => {
    try {
      if (formData.employeeTypeName) {
        setComponentLoader(true);
        const request = {
          employeeTypeName: formData.employeeTypeName,
          isActive: formData?.isActive?.value ? true : false,
        };
        const payload = {
          endpoint: '/api/Employee',
          apiType: Constants.WorkManagement,
          apiMethod: 'Post',
          payload: request,
        };
        const res: any = await employeeAPI(payload);

        if (res.status === 200) {
          setFormData(initialState);
          createToast('success', 'Employee type created successfully.');
          handleCallAPI(true);
          // setData(JSONData);
        } else if (res.status === 203) {
          createToast('error', res.data || 'Employee already exists');
        }
      } else {
        createToast('error', 'All fields must required.');
      }
    } catch (e) {
      console.log('Something went wrong', e);
      createToast('error', 'Something went wrong. Please try again');
    } finally {
      setComponentLoader(false);
    }
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
      <EmployeeListing
        breadcrumlist={breadcrumlist}
        EmployeeListingColumns={EmployeeListingColumns}
        excelExportClick={excelExportClick}
        showExportOption={showExportOption}
        data={data}
        onSave={onSave}
        onDelete={onDelete}
        onEditClick={onEditClick}
        onUpdate={onUpdate}
        onDiscard={onDiscard}
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
        exportFileName={'EmployeeTypes'}
        handleClear={handleClear}
        handleChange={handleChange}
        excelExport={excelExport}
        disableSearch={disableSearch}
        setDisableSearch={setDisableSearch}
        searchKeyword={searchKeyword}
        createToast={createToast}
        setShowExportOption={setShowExportOption}
      />
    </>
  );
};

export default EmployeeListingContainer;
