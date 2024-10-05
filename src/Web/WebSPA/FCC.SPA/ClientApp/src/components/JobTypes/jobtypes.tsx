import {
  Breadcrumb,
  Card,
  DataTable,
  Export,
  SearchBox,
  SecondaryButton,
} from '../CommonComponent';
import './jobtypes.scss';

interface IProps {
  breadcrumlist?: any;
  jobTypesListingColumns?: any;
  excelExportClick?: any;
  showExportOption?: any;
  jobTypeData?: any;
  onSave?: any;
  onDelete?: any;
  onUpdate?: any;
  onEditClick?: any;
  onSearchChange?: any;
  searchKeyword?: any;
  handleClear?: any;
  onDiscard?: any;
  excelExport?: any;
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
  createToast?: any;
  disableSearch?: any;
  setDisableSearch?: any;
  setShowExportOption?:any
}
const JobTypes = (props: IProps) => {
  const {
    breadcrumlist,
    jobTypesListingColumns,
    excelExportClick,
    showExportOption,
    onSave,
    onDelete,
    onUpdate,
    onEditClick,
    onSearchChange,
    searchKeyword,
    handleClear,
    jobTypeData,
    onDiscard,
    excelExport,
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
    createToast,
    disableSearch,
    setDisableSearch,
    setShowExportOption
  } = props;

  return (
    <>
      <div className="breadcrumb-block">
        <Card>
          <Breadcrumb items={breadcrumlist} />
        </Card>
      </div>
      <div className="FCC_right-Content card-container">
        <Card>
          <div className="role-listing-container">
            <p className="heading-txt">Job Types</p>
            <div className="top-section mb-3">
              <SearchBox
                placeholder="Search by Code, Name, Status"
                disabled={disableSearch}
                className="pull-left"
                value={searchKeyword}
                onClear={(e: any) => handleClear(e)}
                onChange={(e: any) => onSearchChange(e)}
              />
              <div className="pull-right my-sm-0 my-3">
              <Export 
                  excelExportClick={excelExportClick} 
                  showExportOption={showExportOption}
                  excelExport={excelExport}
                  setShowExportOption={setShowExportOption}
                  exportbtnName="export-btn"
                />                  
              </div>
            </div>
            <DataTable
              fieldId="jobTypeId"
              data={jobTypeData}
              dataColumn={jobTypesListingColumns}
              className="jobtypeslisting_grid"
              AddnewRowButon={true}
              AddnewRowButontxt={'Create Job Type'}
              inlineRowEdit={true}
              onSave={onSave}
              onDelete={onDelete}
              onUpdate={onUpdate}
              onEditClick={onEditClick}
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

export default JobTypes;
