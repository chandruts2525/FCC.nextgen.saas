import React from 'react';
import {
  SearchBox,
  PrimaryButton,
  DataTable,
  Panel,
  Card,
  Breadcrumb,
  Loader,
  Export
} from '../CommonComponent';
import './userlisting.scss';
import AddUser from './AddUser';

const UserListingComp = ({
  groupListingColumns,
  dismissPanel,
  openPanel,
  isOpen,
  breadcrumlist,
  action,
  setAction,
  createToast,
  showExportOption,
  setShowExportOption,
  componentLoader,
  handleChange,
  handleClear,
  searchKeyword,
  excelExport,
  data,
  onPageChange,
  page,
  totalRecords,
  onSortChange,
  sort,
  onFilterChange,
  filter,
  _exportExcel,
  _exportPDF,
  setIsFormValid,
  isFormValid,
}: any) => {
  return (
    <>
      <div className="breadcrumb-block">
        {/* <Loader/> */}
        <Card>
          <Breadcrumb items={breadcrumlist} />
        </Card>
      </div>
      <div className="FCC_right-Content card-container">
        {componentLoader && (
          <div className="component-loader">
            <Loader />
          </div>
        )}
        <Card>
          <div className="user-listing-container">
            <p className="heading-txt">User Management</p>
            <div className="top-section mb-3">
              <SearchBox
                placeholder="Search by user last and first name, email and employee name"
                className="pull-left"
                onChange={handleChange}
                onClear={handleClear}
                value={searchKeyword}
              />
              <div className="pull-right my-sm-0 my-3">
              <Export 
                  excelExportClick={(e: any) => setShowExportOption(!showExportOption)} 
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
              className="userlisting_grid"
              setAction={setAction}
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
              exportFileName={'UserListing'}
            />
            <Panel
              headerText={action === 'Add' ? 'New User' : 'Edit User'}
              isOpen={isOpen?.visible}
              onDismiss={dismissPanel}
            >
              <AddUser
                action={action}
                setIsFormValid={setIsFormValid}
                createToast={createToast}
                isFormValid={isFormValid}
                dismissPanel={dismissPanel}
                selectedUserData={isOpen?.data}
              />
            </Panel>
          </div>
        </Card>
      </div>
    </>
  );
};

export default UserListingComp;
