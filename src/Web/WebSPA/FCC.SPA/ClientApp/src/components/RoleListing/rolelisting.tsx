import {
  SearchBox,
  PrimaryButton,
  DataTable,
  Panel,
  Card,
  Breadcrumb,
  Export
} from '../CommonComponent';
import './rolelisting.scss';
import AddRole from './AddRole';

interface IProps {
  groupListingColumns?: any;
  dismissPanel?: any;
  openPanel?: any;
  isOpen?: any;
  breadcrumlist?: any;
  action?: any;
  setAction?: any;
  createToast?: any;
  showExportOption?: any;
  excelExport?: any;
  _exportExcel?: any;
  _exportPDF?: any;
  onSearchChange?: any;
  searchKeyword?: any;
  handleClear?: any;
  excelExportClick?: any;
  data?: any;
  take?: any;
  skip?: any;
  total?: any;
  onSortChange?: any;
  sort?: any;
  onFilterChange?: any;
  filter?: any;
  onPageChange?: any;
  isFormValid?: any;
  setIsFormValid?: any;
  setShowExportOption?:any,
  isSlideOpen?:any,
  openSlide?:any,
  closeSlide?:any,
}

const RoleListing = ({
  groupListingColumns,
  dismissPanel,
  openPanel,
  isOpen,
  breadcrumlist,
  action,
  setAction,
  createToast,
  showExportOption,
  excelExport,
  _exportExcel,
  _exportPDF,
  onSearchChange,
  searchKeyword,
  handleClear,
  excelExportClick,
  data,
  take,
  skip,
  total,
  onSortChange,
  sort,
  onFilterChange,
  filter,
  onPageChange,
  isFormValid,
  setIsFormValid,
  setShowExportOption,
  isSlideOpen,
  openSlide,
    closeSlide,
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
          <div className="role-listing-container">
            <p className="heading-txt">Role Management</p>
                      <div className="top-section mb-3">
                           
              <SearchBox
                placeholder="Search by Roles"
                onClear={(e: any) => handleClear(e)}
                onChange={(e: any) => onSearchChange(e)}
                value={searchKeyword}
                className="pull-left"
              />
              <div className="pull-right my-sm-0 my-3">
                <Export 
                  excelExportClick={excelExportClick} 
                  showExportOption={showExportOption}
                  excelExport={excelExport}
                  setShowExportOption={setShowExportOption}
                  exportDropdownClassName="listing"
                />
                <PrimaryButton
                  text="Add"
                  onClick={() => {
                    setAction('Add');
                    openPanel();
                  }}
                ></PrimaryButton>
              </div>
            </div>
            <DataTable
              data={data}
              dataColumn={groupListingColumns}
              className="rolelisting_grid"
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
              exportFileName={'RoleListing'}
            />
            <Panel
              isOpen={isOpen?.visible}
              onDismiss={() => dismissPanel(false)}
              headerText={action === 'Add' ? 'New Role' : 'Edit Role'}
            >
              <AddRole
                setIsFormValid={setIsFormValid}
                createToast={createToast}
                isFormValid={isFormValid}
                dismissPanel={dismissPanel}
                selectedRoleData={isOpen?.data}
              />
            </Panel>
                  </div>       

        </Card>
      </div>
    </>
  );
};

export default RoleListing;
