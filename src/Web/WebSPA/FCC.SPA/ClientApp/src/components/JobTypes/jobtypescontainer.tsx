import JobTypes from './jobtypes';
import { dashboardIcon } from '../../assets/images';
import { DropDownCell } from './dropdowncell';
import { TextFieldCell } from './textfieldcell';
import { useCallback, useEffect, useMemo, useRef, useState } from 'react';
import { Loader } from '../CommonComponent';
import MessageContainer from '../CommonComponent/Message/messagecontainer';
import { callJobTypeApi } from './jobtypesapicalls';
import { debounce } from 'lodash';
import moment from 'moment';
import {
  alphaNumericOnly,
  alphaNumericOnlyOnPaste,
  alphaNumericOnlyWithSpace,
  alphaNumericOnlyWithSpaceOnPaste,
} from '../../utils/Validators';
import { onExportCSV } from '../SharedComponent/Common-function';
import { encodeQuery } from '../../utils/Utils';
import Constants from '../../utils/Constants';

const JobTypesContainer = () => {
  const initialState = {
    isActive: { value: 1, label: 'Active' },
    jobTypeName: '',
    jobTypeCode: '',
    jobTypeId: 0,
  };

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

  const [disableSearch, setDisableSearch] = useState(false);
  const [formData, setFormData] = useState(initialState);
  const [showExportOption, setShowExportOption] = useState(false);
  const [componentLoader, setComponentLoader] = useState<boolean>(false);
  const [toast, setToast] = useState({
    show: false,
    message: '',
    type: '',
  });

  const createToast = (type: string, message: string) => {
    setToast({
      message,
      type,
      show: true,
    });
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
      text: 'Job Types',
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

  const jobTypesListingColumns = [
    {
      field: 'jobTypeCode',
      title: 'Code',
      sortable: true,
      columnMenu: 'ColumnMenu',
      cell: (props: any) => (
        <TextFieldCell
          {...props}
          defaultValue={formData.jobTypeCode}
          placeholder={'Code'}
          maxLength={100}
          disabled={formData?.jobTypeId}
          onChange={(e: any) => {
            formData.jobTypeCode = e?.target?.value;
          }}
          onKeyPress={(e: any) => alphaNumericOnly(e)}
          onPaste={(e: any) => {
            alphaNumericOnlyOnPaste(e);
          }}
        />
      ),
    },
    {
      field: 'jobTypeName',
      title: 'Name',
      sortable: true,
      columnMenu: 'ColumnMenu',
      width: 220,
      cell: (props: any) => (
        <TextFieldCell
          {...props}
          defaultValue={formData.jobTypeName}
          placeholder={'Name'}
          maxLength={50}
          onChange={(e: any) => {
            formData.jobTypeName = e?.target?.value;
          }}
          onKeyPress={(e: any) => alphaNumericOnlyWithSpace(e)}
          onPaste={(e: any) => {
            alphaNumericOnlyWithSpaceOnPaste(e);
          }}
        />
      ),
    },
    {
      field: 'isActive',
      title: 'Status',
      sortable: true,
      columnMenu: 'ColumnMenu',
      width: 120,
      cell: (props: any) => (
        <DropDownCell
          {...props}
          statusOptions={statusOptions}
          placeholder={'Status'}
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

  const excelExportClick = () => {
    setShowExportOption(!showExportOption);
  };

  //=================================methods================
  const onSave = async () => {
    try {
      setComponentLoader(true);
      if (formData?.jobTypeCode?.length > 0 && formData?.jobTypeName?.length > 0) {
        const data: any = formData;
        data.isActive = !!formData.isActive.value;
        delete data.jobTypeId;

        const payload = {
          endpoint: '/api/Job',
          apiType: Constants.WorkManagement,
          apiMethod: 'Post',
          payload: data,
        };

        const res: any = await callJobTypeApi(payload);

        if (res?.status === 200) {
          setFormData(initialState);
          createToast('success', 'Job type created successfully.');
          setCallAPI(true);
        } else if (res?.status === 203) {
          createToast('error', res?.data);
          formData?.isActive
            ? (formData.isActive = { value: 1, label: 'Active' })
            : (formData.isActive = { value: 0, label: 'Inactive' });
        }
      } else {
        createToast('error', 'All fields must required.');
      }
    } catch (e) {
      createToast('error', 'Something went wrong. Please try again');
    } finally {
      setComponentLoader(false);
    }
  };

  const onDelete = async (dataItem: any) => {
    try {
      setComponentLoader(true);
      const data = {
        isActive: !!(dataItem?.isActive === 'Active' || dataItem?.isActive === true),
        jobTypeName: dataItem?.jobTypeName,
        jobTypeCode: dataItem?.jobTypeCode,
        isDeleted: true,
        jobTypeId: dataItem?.jobTypeId,
      };

      const payload = {
        endpoint: '/api/Job',
        apiType: Constants.WorkManagement,
        apiMethod: 'Put',
        payload: data,
      };

      const res: any = await callJobTypeApi(payload);

      if (res.status === 200) {
        setFormData(initialState);
        createToast('success', 'Job type deleted successfully.');
        setCallAPI(true);
      }
    } catch (e) {
      createToast('error', 'Something went wrong. Please try again');
    } finally {
      setComponentLoader(false);
    }
  };

  const onUpdate = async () => {
    try {
      setComponentLoader(true);
      const data = {
        isActive: !!(formData?.isActive?.value === 1),
        jobTypeName: formData?.jobTypeName,
        jobTypeCode: formData?.jobTypeCode,
        isDeleted: false,
        jobTypeId: formData?.jobTypeId,
      };
      const payload = {
        endpoint: '/api/Job',
        apiType: Constants.WorkManagement,
        apiMethod: 'Put',
        payload: data,
      };

      const res: any = await callJobTypeApi(payload);

      if (res.status === 200) {
        setFormData(initialState);
        createToast('success', 'Job type updated successfully.');
        setCallAPI(true);
      } else if (res.status === 203) {
        createToast('error', res?.data);
        formData?.isActive
          ? (formData.isActive = { value: 1, label: 'Active' })
          : (formData.isActive = { value: 0, label: 'Inactive' });
      }
    } catch (e) {
      createToast('error', 'Something went wrong. Please try again');
    } finally {
      setComponentLoader(false);
    }
  };

  const onEditClick = async (dataItem: any) => {
    setFormData({
      isActive: dataItem?.isActive === 'Active' ? statusOptions[0] : statusOptions[1],
      jobTypeName: dataItem?.jobTypeName,
      jobTypeCode: dataItem?.jobTypeCode,
      jobTypeId: dataItem?.jobTypeId,
    });
  };

  const onDiscard = async () => {
    setFormData(initialState);
  };
  //==========================================================

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
        endpoint: '/api/Job/Search?' + url,
        apiType: Constants.WorkManagement,
        apiMethod: 'Post',
        payload: filterArray,
      };

      const res: any = await callJobTypeApi(payload);

      if (res.status === 200) {
        if (res?.data?.count > 0) {
          let list: any = res?.data?.jobTypes.map((item: any) => {
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
            onExportCSV(list, jobTypesListingColumns, 'JobTypes');
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
    // const filterArray: any = []
    // if (filter && filter.filters.length > 0) {
    //     filter.filters.forEach((element: any) => {
    //         if (element.filters.length > 0) {
    //             filterArray.push(element.filters[0])
    //         }
    //     });
    // }

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

    try {
      setComponentLoader(true);
      let url = encodeQuery(param);
      const payload = {
        endpoint: '/api/Job/Search?' + url,
        apiType: Constants.WorkManagement,
        apiMethod: 'Post',
        payload: filterArray,
      };

      const res: any = await callJobTypeApi(payload);
      if (res.status === 200) {
        if (res?.data?.count > 0) {
          setData(res?.data?.jobTypes);
          setTotalRecords(res?.data?.count);
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

      <JobTypes
        breadcrumlist={breadcrumlist}
        jobTypesListingColumns={jobTypesListingColumns}
        excelExportClick={excelExportClick}
        showExportOption={showExportOption}
        jobTypeData={data}
        onSave={onSave}
        onDelete={onDelete}
        onUpdate={onUpdate}
        onEditClick={onEditClick}
        onSearchChange={handleChange}
        searchKeyword={searchKeyword}
        handleClear={handleClear}
        onDiscard={onDiscard}
        excelExport={excelExport}
        onPageChange={onPageChange}
        take={page.take}
        skip={page.skip}
        total={totalRecords}
        onSortChange={onSortChange}
        sort={sort}
        onFilterChange={onFilterChange}
        filter={filter}
        disableSearch={disableSearch}
        setDisableSearch={setDisableSearch}
        _exportExcel={_exportExcel}
        _exportPDF={_exportPDF}
        exportFileName={'JobTypes'}
        createToast={createToast}
        setShowExportOption={setShowExportOption}
      />
    </>
  );
};

export default JobTypesContainer;
