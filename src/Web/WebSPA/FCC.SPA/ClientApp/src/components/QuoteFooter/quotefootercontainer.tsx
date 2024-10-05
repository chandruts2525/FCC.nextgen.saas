import { useCallback, useEffect, useMemo, useRef, useState } from 'react';
import moment from 'moment';
import debounce from 'lodash/debounce';
import MessageContainer from '../CommonComponent/Message/messagecontainer';
import { ContextMenu, Callout, Loader } from '../CommonComponent';
import QuoteFooters from './quotefooter';
import { dashboardIcon } from '../../assets/images';
import { callQuoteFooterApi } from './quotefooterapicalls';
import { DropDownCell } from './dropdowncell';
import { onExportCSV } from '../SharedComponent/Common-function';
import { encodeQuery } from '../SharedComponent/Common-function';
import Constants from '../../utils/Constants';

/**
 *
 * @param props
 * @returns
 * show company and module name on click of count
 */
const CompanyCount = (props: any) => {
  const { showTooltip, name, id, count, uniqueId } = props;

  const [showToolTipContent, setShowToolTipContent] = useState(false);
  return (
    <td key={id}>
      <a
        href="/"
        id={uniqueId}
        onMouseEnter={() => {
          setShowToolTipContent(!showToolTipContent);
        }}
        onMouseLeave={() => setShowToolTipContent(false)}
        onClick={(e: any) => e.preventDefault(e)}
        className="grid_anchor"
      >
        {count}
      </a>
      {showToolTipContent ? (
        <Callout
          calloutWidth={250}
          targetId={uniqueId}
          toggleIsCalloutVisible={() => {
            setShowToolTipContent(!showToolTipContent);
          }}
        >
          <>{showTooltip(name, id)}</>
        </Callout>
      ) : null}
    </td>
  );
};

const QuoteFooterContainer = () => {
  const [showExportOption, setShowExportOption] = useState(false);
  const [isOpen, setIsOpen] = useState({ visible: false, data: {} });
  const [callAPI, setCallAPI] = useState<boolean>(false);
  const [action, setAction] = useState('Add');
  const [componentLoader, setComponentLoader] = useState<boolean>(false);
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

  const [toast, setToast] = useState({
    show: false,
    message: '',
    type: '',
  });
  const [confirmationPopup, showConfirmationPopup] = useState({
    show: false,
    dataItem: null,
  });

  const _exportExcel: any = useRef(null);
  const _exportPDF: any = useRef(null);

  // Confirmation popup handling
  const handlePopupChange = (show: boolean, dataItem: any = null) => {
    showConfirmationPopup({
      show,
      dataItem,
    });
  };

  /**
   *
   * @param data
   * opne panel
   */
  const openPanel = (data: any) => {
    setIsOpen({ visible: true, data: data });
  };

  /**
   *
   * @param isSubmit
   * close panel
   */
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
   * breadcrumn list
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
      text: 'CPQ',
      key: 'f2',
      href: '/',
      onClick: (e: any) => {
        e.preventDefault();
      },
    },
    {
      text: 'Configure',
      key: 'f3',
      href: '/',
      onClick: (e: any) => {
        e.preventDefault();
      },
    },
    {
      text: 'Quote Footers',
      key: 'f4',
      href: '/',
      onClick: (e: any) => {
        e.preventDefault();
      },
    },
  ];

  /**
   *
   * @param id
   * delete selected footer
   */
  const deleteSelectedFooter = async (id: number | String) => {
    handlePopupChange(false);
    try {
      setComponentLoader(true);
      // after login implementation need to pass login user name
      const param = {
        QuoteFooterId: id,
      };
      const url = encodeQuery(param);
      const payload = {
        endpoint: '/api/QuoteFooters/DeleteQuoteFooter?' + url,
        apiType: Constants.WorkManagement,
        apiMethod: 'Put',
        payload: null,
      };
      const res: any = await callQuoteFooterApi(payload);
      if (res.status === 200) {
        createToast('success', `Footer template  deleted successfully.`);
        handleCallAPI(true);
      }
    } catch (err) {
      console.log('Oops something went Wrong!.');
      createToast('error', 'Something went wrong. Please try again');
    } finally {
      setComponentLoader(false);
    }
  };

  // Call method to activate/deactivate Quote Footer
  const deactivateQuoteFooterAction = async (quoteFooterID: any, isActive: number) => {
    try {
      setComponentLoader(true);
      const payload = {
        endpoint: `/api/QuoteFooters/ActivateDeactivate?QuoteFooterId=${quoteFooterID}&Status=${!!isActive}`,
        apiType: Constants.WorkManagement,
        apiMethod: 'Put',
        payload: null,
      };
      const res: any = await callQuoteFooterApi(payload);
      if (res.status === 200) {
        createToast(
          'success',
          `Quote Footer has been ${isActive ? 'activated' : 'deactivated'}!`
        );
        handleCallAPI(true);
      }
    } catch (e) {
      console.log('Something went wrong', e);
    } finally {
      setComponentLoader(false);
    }
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
     * bind action option
     */
    const menuItems = [
      {
        key: 'Edit',
        text: 'Edit',
        onClick: (e: any) => {
          setAction('Edit');
          openPanel({ quoteFooterId: record.dataItem.quoteFooterID });
        },
      },
      {
        key: 'Delete',
        text: 'Delete',
        onClick: (e: any) => {
          handlePopupChange(true, record.dataItem.quoteFooterID);
        },
      },
      {
        key: 'Deactivate',
        text: record.dataItem.status ? 'Deactivate' : 'Activate',
        onClick: () => {
          deactivateQuoteFooterAction(
            record.dataItem.quoteFooterID,
            record?.dataItem?.status ? 0 : 1
          );
        },
      },
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
   *
   * @param data
   * @param id
   * @returns
   * show tooltip
   */
  const showTooltip = (data: string | any, id: number | string) => {
    const tooltipData = data ? data.split(',') : [];
    return (
      <ul className="module-company-list">
        <>
          {tooltipData &&
            tooltipData?.map((item: any, index: number) => {
              return <li key={`${item}-${index}`}>{item}</li>;
            })}
        </>
      </ul>
    );
  };

  /**
   * grid column list
   */
  const quoteFootersListingColumns = [
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
      field: 'quoteFooterName',
      title: 'Name',
      sortable: true,
      columnMenu: 'ColumnMenu',
      width: 220,
    },
    {
      field: 'companyCount',
      title: 'Company(ies)',
      sortable: true,
      columnMenu: 'ColumnMenu',
      width: 160,
      cell: (row: any) => {
        return (
          <CompanyCount
            showTooltip={showTooltip}
            name={row.dataItem?.companyName}
            id={row.dataItem.quoteFooterID}
            count={row.dataItem.companyCount}
            uniqueId={`companylink-id-${row.dataItem.quoteFooterID}`}
          />
        );
      },
    },
    {
      field: 'moduleCount',
      title: 'Module(s)',
      sortable: true,
      columnMenu: 'ColumnMenu',
      width: 160,
      cell: (row: any) => {
        return (
          <CompanyCount
            showTooltip={showTooltip}
            name={row.dataItem?.moduleName}
            id={row.dataItem.quoteFooterID}
            count={row.dataItem.moduleCount}
            uniqueId={`modulelink-id-${row.dataItem.quoteFooterID}`}
          />
        );
      },
    },
    {
      field: 'status',
      title: 'Status',
      sortable: true,
      columnMenu: 'ColumnMenu',
      width: 120,
      cell: (props: any) => <DropDownCell {...props} />,
    },
    {
      field: 'createdBy',
      title: 'Created By',
      sortable: true,
      columnMenu: 'ColumnMenu',
      editable: false,
      width: 200,
      cell: (props: any) => (
        <td>{props?.dataItem?.createdBy ? props?.dataItem?.createdBy : '-'}</td>
      ),
    },
    {
      field: 'createdDate',
      title: 'Created Date',
      sortable: true,
      columnMenu: 'ColumnMenu',
      editable: false,
      format: '{0:dd/MM/yyy}',
      filter: 'date',
      width: 150,
      cell: (props: any) => (
        <td>
          {props?.dataItem?.createdDate
            ? moment(props?.dataItem?.createdDate).format('MM/DD/YYYY')
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
      field: 'modifiedDate',
      title: 'Modified Date',
      sortable: true,
      columnMenu: 'ColumnMenu',
      editable: false,
      format: '{0:dd/MM/yyy}',
      filter: 'date',
      width: 180,
      cell: (props: any) => (
        <td>
          {props?.dataItem?.modifiedDate
            ? moment(props?.dataItem?.modifiedDate).format('MM/DD/YYYY')
            : '-'}
        </td>
      ),
    },
  ];

  /**
   * export export
   */
  const excelExportClick = () => {
    setShowExportOption(!showExportOption);
  };

  /**
   *
   * @param type
   * @param message
   * set toast message
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
   * @param isExcel
   * excel export
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
        endpoint: '/api/QuoteFooters/Search?' + url,
        apiType: Constants.WorkManagement,
        apiMethod: 'Post',
        payload: filterArray,
      };
      const res: any = await callQuoteFooterApi(payload);

      if (res.status === 200) {
        if (res.data && res?.data?.quoteFooters && res?.data?.quoteFooters?.length > 0) {
          let list: any = res.data?.quoteFooters.map((item: any) => {
            item['status'] = item.status ? 'Active' : 'Inactive';
            item['createdBy'] = item.createdBy || '-';
            item['modifiedBy'] = item.modifiedBy || '-';
            if (isExcel && _exportExcel.current !== null) {
              item['createdDate'] = item.createdDate
                ? moment(item.createdDate).format('MM/DD/YYYY')
                : '-';
              item['modifiedDate'] = item.modifiedDate
                ? moment(item.modifiedDate).format('MM/DD/YYYY')
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
            onExportCSV(list, quoteFootersListingColumns, 'QuoteFooter');
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
   * @param currentPage
   * @param pageSize
   * @param searchText
   * @returns
   * grid pagination payload
   */
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
          if (payload.field === 'createdDate' || payload.field === 'modifiedDate') {
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

  /**
   *
   * @param currentPage
   * @param pageSize
   * @param searchText
   * get all the grid data
   */
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
      const url = encodeQuery(param);
      setComponentLoader(true);
      const payload = {
        endpoint: '/api/QuoteFooters/Search?' + url,
        apiType: Constants.WorkManagement,
        apiMethod: 'Post',
        payload: filterArray,
      };
      const res: any = await callQuoteFooterApi(payload);
      if (res.status === 200) {
        if (res.data && res?.data?.quoteFooters && res?.data?.quoteFooters?.length > 0) {
          setData(res?.data?.quoteFooters);
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

  /**
   * get data when search
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
   * handle debounce
   */
  const debounceRequest = useMemo(() => {
    return debounce(fetchSearchData, 500);
  }, [fetchSearchData]);

  /**
   *
   * @param e
   * get searchbox value
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
   * @param event
   * handle page change event
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
   * handle sorting event
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
   * handle filter change event
   */
  const onFilterChange = (event: any) => {
    setFilter(event.filter);
  };

  /**
   *
   * @param e
   * searchbox clear
   */
  const handleClear = (e: any) => {
    e.preventDefault();
    e.stopPropagation();
    setSearchKeyword('');
    handleCallAPI(true);
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
      <QuoteFooters
        showExportOption={showExportOption}
        excelExportClick={excelExportClick}
        breadcrumlist={breadcrumlist}
        quoteFootersListingColumns={quoteFootersListingColumns}
        isOpen={isOpen}
        openPanel={openPanel}
        dismissPanel={dismissPanel}
        createToast={createToast}
        action={action}
        setAction={setAction}
        confirmationPopup={confirmationPopup}
        handlePopupChange={handlePopupChange}
        deleteSelectedFooter={deleteSelectedFooter}
        excelExport={excelExport}
        _exportExcel={_exportExcel}
        _exportPDF={_exportPDF}
        onSearchChange={handleChange}
        searchKeyword={searchKeyword}
        handleClear={handleClear}
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
      />
    </>
  );
};

export default QuoteFooterContainer;
