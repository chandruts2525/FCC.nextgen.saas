import React from 'react';
import {
  SearchBox,
  PrimaryButton,
  DataTable,
  Card,
  Breadcrumb,
  Panel,
  ModalPopup,
  Export
} from '../CommonComponent';
import './schedulelisting.scss';
import AddSchedule from './AddSchedule';
import MessageContainer from '../CommonComponent/Message/messagecontainer';

const ScheduleListing = ({
  data,
  breadcrumlist,
  groupListingColumns,
  unitComponentOptions,
  openPanel,
  isOpen,
  dismissPanel,
  action,
  excelExportClick,
  showExportOption,
  formData,
  setFormData,
  onSave,
  onUpdate,
  isFormValid,
  onSearchChange,
  searchKeyword,
  handleClear,
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
  toast,
  setToast,
  confirmationPopup,
  handlePopupChange,
  onDelete,
  setShowExportOption
}: any) => {
  return (
    <>
      <div className="breadcrumb-block">
        <Card>
          <Breadcrumb items={breadcrumlist} />
        </Card>
      </div>
      <div className="FCC_right-Content card-container">
        <Card>
          <div className="schedule-listing-container">
            <p className="heading-txt">Schedule Types</p>
            <div className="top-section mb-3">
              <SearchBox
                placeholder="Search by Code, Name, Unit/Component"
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
                />    
                <PrimaryButton
                  text="Create Schedule Type"
                  onClick={() => {
                    // setAction('Add')
                    openPanel();
                  }}
                ></PrimaryButton>
              </div>
            </div>
            <DataTable
              fieldId="scheduleTypeId"
              data={data}
              dataColumn={groupListingColumns}
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
              landscape={true}
              exportFileName={'ScheduleTypes'}
            />
            <Panel
              slideType="extra-small"
              headerText={action === 'Add' ? 'New Schedule Type' : 'Edit Schedule Type'}
              isOpen={isOpen}
              onDismiss={dismissPanel}
            >
              <MessageContainer
                type={toast.type}
                text={toast.message}
                show={toast.show}
                setToast={setToast}
              />
              <AddSchedule
                dismissPanel={dismissPanel}
                formData={formData}
                setFormData={setFormData}
                onSave={onSave}
                onUpdate={onUpdate}
                isFormValid={isFormValid}
                unitComponentOptions={unitComponentOptions}
              />
            </Panel>
          </div>
          {confirmationPopup.show ? (
            <ModalPopup
              ShowModalPopupFooter={true}
              ModalPopupTitle="Confirmation"
              ModalPopupType="medium"
              FooterSecondaryBtnTxt="No"
              FooterPrimaryBtnTxt="Yes"
              closeModalPopup={() => handlePopupChange(false)}
              PrimaryBtnOnclick={() => onDelete(confirmationPopup.dataItem)}
              SecondryBtnOnclick={() => handlePopupChange(false)}
              ModalPopupName="confirmation-popup"
            >
              <p>Are you sure you want to delete this record ?</p>
            </ModalPopup>
          ) : (
            <></>
          )}
        </Card>
      </div>
    </>
  );
};

export default ScheduleListing;
