import * as React from 'react';
import { process, filterBy } from '@progress/kendo-data-query';
import {
  Grid,
  GridColumn as Column,
  GridColumnMenuFilter,
  getSelectedState,
  GridColumnMenuWrapper,
  GridToolbar,
} from '@progress/kendo-react-grid';
import { getter } from '@progress/kendo-react-common';
import './index.scss';
import { useState } from 'react';
import { MyCommandCell } from './mycommandcell';
import { LocalizationProvider, loadMessages } from '@progress/kendo-react-intl';
import { Tooltip } from '@progress/kendo-react-tooltip';
import { CellRender, RowRender } from './renderers';
import { ExcelExport, ExcelExportColumn } from '@progress/kendo-react-excel-export';
import PrimaryButton from '../Button/PrimaryButton';
import { v4 as uuidv4 } from 'uuid';
import { GridPDFExport } from '@progress/kendo-react-pdf';
import { setGroupIds, setExpandedState } from '@progress/kendo-react-data-tools';
import moment from 'moment';
import ModalPopup from '../ModalPopup';

const processWithGroups = (data: any, dataState: any) => {
  const newDataState = process(data, dataState);
  setGroupIds({
    data: newDataState.data,
    group: dataState.group,
  });
  return newDataState;
};
const ColumnMenuconst: any = (ColumnMenuitem: any) => {
  return (
    <div>
      <GridColumnMenuFilter {...ColumnMenuitem} expanded={true} hideSecondFilter />
    </div>
  );
};

const DataTable = (props: any) => {
  const id = props?.fieldId || 'id';
  const [collpaseExpandMode, setCollapseExpandMode] = useState<boolean>(false);
  const [isSelectAll, setIsSelectAll] = useState(false);
  const [isSelectChangeEventCallRequired, setIsSelectChangeEventCallRequired] =
    useState(false);
  loadMessages(
    {
      grid: {
        groupPanelEmpty: 'Drag a column header and drop it here to group by that column',
        pagerItemsPerPage: 'Items per page',
        pagerFirstPage: 'Go to the first page',
        pagerPreviousPage: 'Go to the previous page',
        pagerNextPage: 'Go to the next page',
        pagerLastPage: 'Go to the last page',
        pagerPage: 'Page',
        pagerOf: 'of',
        pagerTotalPages: '{0}',
        pagerInfo: '{0} - {1} to {2} Item',
        pagerItemPerPage: 'Items per page',
        filterClearButton: 'Clear',
        filterEqOperator: 'Is equal to',
        filterNotEqOperator: 'Is not equal to',
        filterIsNullOperator: 'Is null',
        filterIsNotNullOperator: 'Is not null',
        filterIsEmptyOperator: 'Is empty',
        filterIsNotEmptyOperator: 'Is not empty',
        filterStartsWithOperator: 'Starts with',
        filterContainsOperator: 'Contains',
        filterNotContainsOperator: 'Does not contain',
        filterEndsWithOperator: 'Ends with',
        filterGteOperator: 'Is greater than or equal to',
        filterGtOperator: 'Is greater than',
        filterLteOperator: 'Is less than or equal to',
        filterLtOperator: 'Is less than',
        filterIsTrue: 'Is true',
        filterIsFalse: 'Is false',
        filterBooleanAll: '(All)',
        filterAfterOrEqualOperator: 'Is after or equal to',
        filterAfterOperator: 'Is after',
        filterBeforeOperator: 'Is before',
        filterBeforeOrEqualOperator: 'Is before or equal to',
        filterSubmitButton: 'Filter',
        filterAndLogic: 'And',
        filterOrLogic: 'Or',
        filterTitle: 'Show items with value that:',
        filterChooseOperator: 'Choose Operator',
        filterSelectedItems: 'selected items',
        filterAriaLabel: 'Filter',
        sortAscending: 'Sort Ascending',
        sortDescending: 'Sort Descending',
        sortAriaLabel: 'Sortable',
        searchPlaceholder: 'Search',
        noRecords: 'No records available.',
        filterCheckAll: 'Check All',
        groupColumn: 'Group Column',
        groupExpand: 'Expand group',
        groupCollapse: 'Collapse Group',
        groupPanelAriaLabel: 'Group panel',
        ungroupColumn: 'Ungroup Column',
        detailExpand: 'Expand detail row',
        detailCollapse: 'Collapse detail row',
      },
    },
    'en'
  );

  const actionEnum = {
    discard: 'Discard',
    delete: 'Delete',
  };
  const [selectedState, setSelectedState] = useState<any>({});
  const [confirmationPopup, showConfirmationPopup] = useState({
    action: '',
    show: false,
    dataItem: null,
  });
  const {
    data,
    selection = false,
    DetailRow = false,
    inlineEdit = false,
    inlineRowEdit = false,
    sortable = true,
    pagination = true,
    getSelectedValue,
    edit,
    total,
    onResultChange,
    onSortChange,
    onPageChange,
    skip = 0,
    take = 10,
    sort,
    filter,
    _exportExcel = null,
    _exportPDF = null,
    setDisableSearch = null,
  } = props;

  React.useEffect(() => {
    if (selection) {
      const getClass = document.querySelector('.SelectAllcls');
      const paraClss = document.createElement('span');
      paraClss.setAttribute('class', 'CustomSelectalltxt');
      paraClss.setAttribute('title', 'Select All');
      paraClss.innerText = '';
      getClass?.appendChild(paraClss);
    }
  }, []);

  const SELECTED_FIELD = props.selectedField;
  const DATA_ITEM_KEY = props.dataItemKey;
  const idGetter = getter(DATA_ITEM_KEY);
  const EDIT_FIELD = 'inEdit';

  const createDataState = (dataState: any) => {
    return {
      result: process(data, dataState),
      dataState: dataState,
    };
  };
  const createState = (skip: any, take: any) => {
    let pagerSettings = {
      buttonCount: 5,
      info: true,
      type: 'numeric',
      pageSizes: [3, 5, 10, 20],
      previousNext: true,
    };

    return {
      pageSize: take,
      pageable: pagerSettings,
    };
  };
  let initialState = createDataState({
    take: props?.defaultPageSize,
    skip: 0,
  });

  const [result, setResult] = useState(initialState.result);
  const [dataState, setDataState] = useState(initialState.dataState);
  const [state] = useState(createState(skip, take));
  const [collapsedState, setCollapsedState] = React.useState([]);

  React.useEffect(() => {
    if (props.groupable) {
      if (data?.length) {
        setResult(processWithGroups(data, dataState));
      }
    } else {
      if (!collpaseExpandMode) {
        setResult(process(data, dataState));
      } else {
        setResult(processWithGroups(data, dataState));
      }
    }
  }, [data]);

  React.useEffect(() => {
    if (props.groupable) {
      const newData = setExpandedState({
        data: result?.data,
        collapsedIds: collapsedState,
      });
      setResult({ ...result, data: newData });
    }
  }, [collapsedState]);

  const dataStateChange = (event: any) => {
    const dataFilterTemp = filterBy(data, event.dataState.filter);
    if (props?.exportRef) {
      if (!props?.exportRef?.current?.data) {
        props.exportRef.current = { data: [] };
      }
      props.exportRef.current.data = dataFilterTemp;
    }

    if (props.groupable) {
      if (
        event.dataState.group &&
        props?.dataColumn.length > event.dataState.group.length
      ) {
        props?.dataColumn.map((col: any) => {
          col.show = true;
          return col;
        });
        event.dataState.group.forEach((group: any) => {
          for (let i = 0; i < props?.dataColumn.length; i++) {
            if (group.title == props?.dataColumn[i].title) {
              props.dataColumn[i].show = false;
            }
          }
        });
      }
      if (event.dataState.group?.length) {
        const uniqueObjects: any = {};
        const outputArray = [];
        for (const obj of event.dataState.group) {
          if (!uniqueObjects[obj.field]) {
            uniqueObjects[obj.field] = true;
            outputArray.push(obj);
          }
        }
        event.dataState.group = outputArray;
      }
      setResult(processWithGroups(data, event.dataState));
    } else {
      setResult(process(data, event.dataState));
      onSortChange(data);
    }
    setDataState(event.dataState);
  };

  const onExpandChange = React.useCallback(
    (event: any) => {
      if (props.groupable) {
        const item = event.dataItem;
        if (item.groupId) {
          const collapsedIds: any = !event.value
            ? [...collapsedState, item.groupId]
            : collapsedState.filter((groupId) => groupId !== item.groupId);
          setCollapsedState(collapsedIds);
        }
      } else {
        let newDataNested = result?.data?.map((item) => {
          if (item?.[id] === event.dataItem?.[id]) {
            item.expanded = !event.dataItem.expanded;
          }
          return item;
        });
        setResult({ ...result, data: newDataNested });
        setCollapseExpandMode(true);
      }
    },
    [collapsedState, result?.data]
  );

  const customCellRender = (td: any, props: any) => (
    <CellRender
      originalProps={props}
      td={td}
      enterEdit={enterEdit}
      editField={EDIT_FIELD}
    />
  );
  const customRowRender = (tr: any, props: any) => (
    <RowRender originalProps={props} tr={tr} exitEdit={exitEdit} editField={EDIT_FIELD} />
  );

  React.useEffect(() => {
    if (props?.rememberSelected) {
      if (data?.length > 0 && props?.selectedEventData.dataItems?.length > 0) {
        setIsSelectChangeEventCallRequired(true);
        const resultObject: any = {};
        props?.selectedEventData?.dataItems?.forEach((item: any) => {
          if (item.selected) {
            resultObject[item.masterID] = true;
          }
        });
        setSelectedState(resultObject);
      }
    }
  }, [data]);

  React.useEffect(() => {
    if (isSelectChangeEventCallRequired) {
      onSelectionChange(props.selectedEventData);
    }
  }, [selectedState]);

  const onSelectionChange = React.useCallback(
    (event: any) => {
      setIsSelectChangeEventCallRequired(false);
      const newSelectedState = getSelectedState({
        event,
        selectedState: selectedState,
        dataItemKey: DATA_ITEM_KEY,
      });
      setSelectedState(newSelectedState);
      setIsSelectAll(false);
      if (props.rememberSelected) props.setSelectedStateEvent(event);
      getSelectedItems(newSelectedState, data);
    },
    [selectedState]
  );

  const onHeaderSelectionChange = React.useCallback(
    (event: any) => {
      const checkboxElement = event.syntheticEvent.target;
      const checked = checkboxElement.checked;
      const newSelectedState: any = {};
      data.forEach((item: any) => {
        newSelectedState[idGetter(item)] = checked;
      });
      setIsSelectAll(checked);
      setSelectedState(newSelectedState);
      getSelectedItems(newSelectedState, data);
    },
    [data]
  );

  const getSelectedItems = (selectedState: any, dataList: any) => {
    if (selectedState != null) {
      let selectedIds: any = [];
      let keys = Object.keys(selectedState);
      for (let key in keys) {
        if (selectedState[keys[key]]) {
          selectedIds.push(keys[key]);
        }
      }
      let selectedData = dataList.filter(
        (item: any) => selectedIds.indexOf(item[DATA_ITEM_KEY]?.toString()) > -1
      );
      getSelectedValue(selectedData ?? []);
    }
  };

  const headerCellComponent = (props: any) => {
    return (
      <>
        <a className="k-link" onClick={props.onClick} onKeyDown={props.onClick}>
          <span className="k-column-title" title={props.title}>
            {props.title}
          </span>
          {props.children}
        </a>
        <GridColumnMenuWrapper {...props.columnMenuWrapperProps} />
      </>
    );
  };

  const cellComponent = (props: any) => {
    return (
      <>
        <td title={props.dataItem[props.field]}>{props.dataItem[props.field]}</td>
      </>
    );
  };

  const itemChange = (event: any) => {
    const d = [...result.data];
    let newData: any = [];
    d.forEach((item) => {
      if (item?.[id] === event.dataItem?.[id]) {
        const x = { ...item, [event.field]: event.value };
        item = x;
      }
      newData.push(item);
    });
    const payload = { ...result, data: newData };
    setResult(payload);
    getSelectedItems(selectedState, newData);
  };

  // =======================================================>
  // Code modified
  // =======================================================>
  const CommandCell = (props: any) => (
    <MyCommandCell
      {...props}
      add={add}
      edit={enterEdit}
      update={update}
      discard={discard}
      editField={EDIT_FIELD}
      handlePopupChange={handlePopupChange}
      actionEnum={actionEnum}
      id={id}
    />
  );

  const addNew = () => {
    toggleDisabledState(true);
    if (setDisableSearch) {
      setDisableSearch(true);
    }
    const list = result?.data?.find((o: any) => o.inEdit === true);
    const isAddOrEditInProgress = list?.inEdit;

    if (!isAddOrEditInProgress) {
      const newDataItem = { inEdit: true, Discontinued: false, key: uuidv4() };
      const newData = { ...result, data: [newDataItem, ...result.data] };
      setResult(newData);
      onResultChange(newData);
    } else {
      props?.createToast(
        'error',
        'Please save/discard unsaved changes first and then try again!'
      );
    }
  };

  const add = () => {
    toggleDisabledState(false);
    if (setDisableSearch) {
      setDisableSearch(false);
    }
    props?.onSave();
  };

  const enterEdit = (dataItem: any) => {
    toggleDisabledState(true);
    if (setDisableSearch) {
      setDisableSearch(true);
    }
    const list = result?.data?.find((o: any) => o.inEdit === true);
    const isAddOrEditInProgress = list?.inEdit;

    if (!isAddOrEditInProgress) {
      props?.onEditClick(dataItem);
      setResult({
        ...result,
        data: data.map((item: any) =>
          item?.[id] === dataItem?.[id] ? { ...item, inEdit: true } : item
        ),
      });
    } else {
      handlePopupChange(true, actionEnum.discard, dataItem);
    }
  };

  const update = (dataItem: any) => {
    toggleDisabledState(false);
    if (setDisableSearch) {
      setDisableSearch(false);
    }
    props?.onUpdate();
  };

  const discard = (isAdd: boolean = false, dataItem: any) => {
    toggleDisabledState(false);
    if (setDisableSearch) {
      setDisableSearch(false);
    }
    const newData = [...data];
    const updatedData: any = [];
    newData.forEach((item: any) => {
      if (item.inEdit) {
        if (!isAdd) {
          delete item.inEdit;
          updatedData.push(item);
        }
      } else {
        updatedData.push(item);
      }
    });
    setResult({ ...result, data: updatedData });
    props?.onDiscard();
  };

  // Confirmation popup handling
  const handlePopupChange = (show: boolean, action: any = '', dataItem: any = null) => {
    showConfirmationPopup({
      show,
      action,
      dataItem,
    });
  };

  const onConfirmation = (action: any, dataItem: any) => {
    if (action === actionEnum.discard) {
      handlePopupChange(false);
      const isAdd = dataItem.inEdit && dataItem.key ? true : false;
      discard(isAdd ? true : false, dataItem);
    } else if (action === actionEnum.delete) {
      props?.onDelete(dataItem);
      handlePopupChange(false);
    }
  };

  // =======================================================>
  // Code modified end
  // =======================================================>

  const exitEdit = () => {
    const newData = data.map((item: any) => ({ ...item, [EDIT_FIELD]: undefined }));
    let resultData = process(newData, dataState);
    setResult(resultData);
  };
  const isColumnActive = (field: any, dataState: any) => {
    return GridColumnMenuFilter.active(field, dataState.filter);
  };

  const filterOperators = {
    text: [
      { text: 'grid.filterContainsOperator', operator: 'contains' },
      // { text: 'grid.filterStartsWithOperator', operator: 'startswith' },
    ],
    numeric: [
      { text: 'grid.filterEqOperator', operator: 'eq' },
      { text: 'grid.filterGtOperator', operator: 'gt' },
      { text: 'grid.filterLtOperator', operator: 'lt' },
    ],
    date: [
      { text: 'grid.filterEqOperator', operator: 'eq' },
      { text: 'grid.filterAfterOperator', operator: 'gt' },
      { text: 'grid.filterBeforeOperator', operator: 'lt' },
      // { text: 'grid.filterBeforeOperator', operator: 'lt' },
    ],
    boolean: [{ text: 'grid.filterEqOperator', operator: 'eq' }],
  };

  const toggleDisabledState = (value: boolean) => {
    setTimeout(() => {
      let grid = document.querySelector('.Grid1');
      if (grid) {
        let headerRow = grid.querySelector('.k-grid-header-wrap');
        if (headerRow) {
          if (value && !headerRow?.className.includes('disabled')) {
            headerRow.className = headerRow.className + ' disabled';
            var childNodes: any = headerRow.getElementsByTagName('*');
            for (var node of childNodes) {
              node.disabled = true;
            }
          } else if (!value) {
            headerRow.className = headerRow.className.replace(' disabled', '');
            var childNodes: any = headerRow.getElementsByTagName('*');
            for (var node of childNodes) {
              node.disabled = false;
            }
          }
        }
      }
    });
  };

  const columnMapping = (dataItems: any, isPDF: boolean = false) => {
    return dataItems?.map((item: any) => {
      if ((isPDF && item.field != 'Action') || !isPDF) {
        return (
          (item.show || item.show === undefined) && (
            <Column
              field={item.field}
              id={item?.[id]}
              {...(!isPDF && { columnMenu: item.columnMenu ? ColumnMenuconst : null })}
              // {...(!isPDF && {
              //   headerClassName: isColumnActive(item.field, dataState) ? `active ${item.headerClassName}` : item.headerClassName,
              // })}
              {...(!isPDF && {
                headerClassName:item.headerClassName,
              })}
              sortable={item.sortable}
              title={item.title}
              filter={item.filter}
              width={item.width}
              cell={item?.cell ? item.cell : cellComponent}
              {...(!isPDF && {
                headerCell: item.headerCell ? item.headerCell : headerCellComponent,
              })}
              editor={item.editor}
              format={item.format}
              editable={item.editable}
              key={item.field}
              locked={item?.locked}
            >
              {item?.children ? columnMapping(item?.children, isPDF) : null}
            </Column>
          )
        );
      }
    });
  };

  const gridDataState = props.dataState ? props.dataState : dataState;
  const rowRenderStyle = (trElement: any, props: any) => {
    const trProps = {
      className: props.dataItem.IsRead === false ? 'IsUnRead' : '',
    };
    return React.cloneElement(
      trElement,
      {
        ...trProps,
      },
      trElement.props.children
    );
  };

  const getFileName = (isExcel: boolean = false) => {
    let name = props?.exportFileName ? props?.exportFileName + '_' : '';
    let date = moment().format('DD-MM-YYYY H.mm.ss');
    return name + date + (isExcel ? '.xlsx' : '.pdf');
  };

  const gridElements = (isPDF: boolean = false) => (
    <Grid
      data={
        !selection
          ? result
          : {
              total: result.total,
              data:
                result.data?.length > 0
                  ? result.data.map((item) => ({
                      ...item,
                      [SELECTED_FIELD]: selectedState[idGetter(item)],
                    }))
                  : [],
            }
      }
      detail={DetailRow ? props.DetailComponent : null}
      {...gridDataState}
      total={total}
      onDataStateChange={
        props.onDataStateChange ? props.onDataStateChange : dataStateChange
      }
      sortable={sortable}
      fixedScroll={true}
      headerCell={true}
      dataItemKey={DATA_ITEM_KEY}
      pageable={pagination ? state.pageable : null}
      pageSize={pagination ? state.pageSize : null}
      skip={pagination ? skip : null}
      take={pagination ? take : null}
      selectedField={SELECTED_FIELD}
      className="Grid1"
      selectable={{
        enabled: true,
        drag: false,
        cell: false,
        mode: 'multiple',
      }}
      onSelectionChange={onSelectionChange}
      onHeaderSelectionChange={onHeaderSelectionChange}
      expandField={'expanded'}
      onItemChange={inlineEdit || inlineRowEdit ? itemChange : props.onItemChange}
      cellRender={inlineEdit ? customCellRender : null}
      rowRender={inlineEdit ? customRowRender : rowRenderStyle}
      editField={EDIT_FIELD}
      editable={true}
      resizable={true}
      onExpandChange={onExpandChange}
      groupable={props.groupable}
      reorderable={true}
      onFilterChange={props.onFilterChange ? props.onFilterChange : null}
      filterOperators={filterOperators}
      exitEdit={exitEdit}
      onSortChange={pagination ? onSortChange : null}
      onPageChange={pagination ? onPageChange : null}
      ref={props.gridRef ? props.gridRef : null}
      sort={pagination ? sort : null}
      filter={pagination ? filter : null}
      scrollable={{ virtual: true, mobile: true }}
    >
      {props.AddnewRowButon ? (
        <GridToolbar>
          <PrimaryButton
            className="SearchBtnExpandIconBtn"
            onClick={addNew}
            text={props.AddnewRowButontxt}
          ></PrimaryButton>
        </GridToolbar>
      ) : (
        ''
      )}

      {selection ? (
        <Column
          field={SELECTED_FIELD}
          headerClassName="SelectAllcls"
          width="80px"
          editable={false}
          key={SELECTED_FIELD}
          locked={true}
          headerSelectionValue={isSelectAll}
        />
      ) : null}
      {inlineRowEdit && !isPDF ? (
        <Column editable={false} cell={CommandCell} title="Action" width="70px" />
      ) : null}

      {columnMapping(props.dataColumn, isPDF)}
    </Grid>
  );

  return (
    <div className={`Fcc_dataTable ${props.className}`}>
      <LocalizationProvider language="en">
        <Tooltip openDelay={100} position="bottom" anchorElement="target">
          <ExcelExport ref={_exportExcel} fileName={getFileName(true)}>
            {props.dataColumn.map((item: any) => {
              return (
                <ExcelExportColumn
                  key={item.title}
                  field={item.field}
                  title={item.title}
                  hidden={item.field === 'Action'}
                />
              );
            })}
          </ExcelExport>
          {gridElements()}
          <GridPDFExport
            paperSize="A4"
            landscape={props?.landscape}
            scale={0.5}
            ref={_exportPDF}
            margin="1cm"
            fileName={getFileName()}
          >
            {gridElements(true)}
          </GridPDFExport>
        </Tooltip>
      </LocalizationProvider>
      {confirmationPopup.show ? (
        <ModalPopup
          ShowModalPopupFooter={true}
          ModalPopupTitle="Confirmation"
          ModalPopupType="medium"
          FooterSecondaryBtnTxt="No"
          FooterPrimaryBtnTxt="Yes"
          closeModalPopup={() => handlePopupChange(false)}
          PrimaryBtnOnclick={() =>
            onConfirmation(confirmationPopup.action, confirmationPopup.dataItem)
          }
          SecondryBtnOnclick={() => handlePopupChange(false)}
          ModalPopupName="confirmation-popup"
        >
          <p>
            Are you sure you want to{' '}
            {confirmationPopup.action === actionEnum.discard
              ? 'discard the changes ?'
              : 'delete this record ?'}
          </p>
        </ModalPopup>
      ) : (
        <></>
      )}
    </div>
  );
};
DataTable.defaultProps = {
  data: [],
  dataColumn: [],
  Selection: false,
  DetailRow: false,
  inlineEdit: false,
  sortable: true,
  pagination: true,
  selectedField: 'selected',
  dataItemKey: 'id',
  getSelectedValue: () => {},
  edit: () => {},
  update: () => {},
  cancel: () => {},
  onItemChange: () => {},
  editable: false,
  resizable: false,
  inlineRowEdit: false,
  reorderable: false,
  contextMenu: false,
  fixedScroll: false,
  AddnewRowButon: false,
  AddnewRowButontxt: 'Add Row',
  onResultChange: () => {},
  onSortChange: () => {},
  className: '',
};
export default DataTable;
