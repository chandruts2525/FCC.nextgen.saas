import React from 'react';
import {
  SearchBox,
  DataTable,
  Card,
  Breadcrumb,
  Export,
} from '../CommonComponent';
import './employeelisting.scss';

interface IProps {
  breadcrumlist?: any;
  EmployeeListingColumns?: any;
  excelExportClick?: any;
  showExportOption?: any;
  data?: any;
  onSave?: any;
  onDelete?: any;
  onEditClick?: any;
  onUpdate?: any;
  onDiscard?: any;
  onPageChange?: any;
  take?: any;
  skip?: any;
  total?: any;
  onSortChange?: any;
  sort?: any;
  onFilterChange?: any;
  filter?: any;
  _exportExcel?: any;
  _exportPDF?: any;
  exportFileName?: any;
  handleClear?: any;
  handleChange?: any;
  excelExport?: any;
  searchKeyword?: any;
  createToast?: any;
  disableSearch?: any;
  setDisableSearch?: any;
  setShowExportOption?:any
}
const EmployeeListing = ({
  breadcrumlist,
  EmployeeListingColumns,
  excelExportClick,
  showExportOption,
  data,
  onSave,
  onDelete,
  onEditClick,
  onUpdate,
  onDiscard,
  onPageChange,
  take,
  skip,
  total,
  onSortChange,
  sort,
  onFilterChange,
  filter,
  _exportExcel,
  _exportPDF,
  exportFileName,
  handleClear,
  handleChange,
  excelExport,
  searchKeyword,
  createToast,
  disableSearch,
  setDisableSearch,
  setShowExportOption
}: IProps) => {
  return (
    <>
      <div className="breadcrumb-block">
        <Card>
          <Breadcrumb items={breadcrumlist} />
        </Card>
      </div>
      <div className="FCC_right-Content card-container">
        <Card>
          <div className="employee-listing-container">
            <p className="heading-txt">Employee Types</p>
            <div className="top-section mb-3">
              <SearchBox
                placeholder="Search by Name, Status"
                disabled={disableSearch}
                className="pull-left"
                onChange={handleChange}
                onClear={handleClear}
                value={searchKeyword}
              />
              <div className="pull-right my-sm-0 my-3">
                <Export 
                  excelExportClick={excelExportClick} 
                  showExportOption={showExportOption}
                  excelExport={excelExport}
                  setShowExportOption={setShowExportOption}
                  exportbtnName="export-btn-employee"
                />     
              </div>
            </div>

            <DataTable
              data={data}
              dataColumn={EmployeeListingColumns}
              className="employeelisting_grid"
              AddnewRowButon={true}
              AddnewRowButontxt={'Create Employee Type'}
              inlineRowEdit={true}
              onSave={onSave}
              onDelete={onDelete}
              onEditClick={onEditClick}
              onUpdate={onUpdate}
              fieldId="employeeTypeId"
              onDiscard={onDiscard}
              onPageChange={onPageChange}
              take={take}
              skip={skip}
              total={total}
              onSortChange={onSortChange}
              sort={sort}
              onFilterChange={onFilterChange}
              filter={filter}
              _exportExcel={_exportExcel}
              _exportPDF={_exportPDF}
              exportFileName={exportFileName}
              createToast={createToast}
              setDisableSearch={setDisableSearch}
            />
          </div>
        </Card>
      </div>
    </>
  );
};

export default EmployeeListing;
