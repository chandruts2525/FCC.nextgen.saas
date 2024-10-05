import React from 'react';
import {
  SearchBox,
  PrimaryButton,
  DataTable,
  Card,
  Breadcrumb,
  Panel,
  Loader,
  ModalPopup,
  Export
} from '../CommonComponent';
import './unitmeasure.scss';
// import data from "./data.json";
import AddUnitMeasure from './AddUnitMeasure';

const UnitMeasure = ({
  data,
  componentLoader,
  breadcrumlist,
  unitMeasureColumns,
  isOpen,
  dismissPanel,
  openPanel,
  action,
  excelExportClick,
  showExportOption,
  createToast,
  setAction,
  confirmationPopup,
  handlePopupChange,
  deleteUnitOfMeasure,
  handleChange,
  handleClear,
  searchKeyword,
  excelExport,
  onPageChange,
  page,
  totalRecords,
  onSortChange,
  sort,
  onFilterChange,
  filter,
  _exportExcel,
  _exportPDF,
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
        {componentLoader && (
          <div className="component-loader">
            <Loader />
          </div>
        )}
        <Card>
          <div className="unit-measure-container">
            <p className="heading-txt">Unit of Measure</p>
            <div className="top-section mb-3">
              <SearchBox
                placeholder="Search by Code, Name, Measure Type"
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
                />    
                <PrimaryButton
                  text="Create UoM"
                  onClick={() => {
                    setAction('Add');
                    openPanel();
                  }}
                ></PrimaryButton>
              </div>
            </div>
            <DataTable
              data={data}
              dataColumn={unitMeasureColumns}
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
              exportFileName={'UnitOfMeasure'}
              _exportExcel={_exportExcel}
              _exportPDF={_exportPDF}
            />
            <Panel
              slideType="extra-small"
              headerText={
                action === 'Add' ? 'New Unit of Measure' : 'Edit Unit of Measure'
              }
              isOpen={isOpen?.visible}
              onDismiss={() => dismissPanel(false)}
            >
              <AddUnitMeasure
                action={action}
                dismissPanel={dismissPanel}
                selectedData={isOpen?.data?.unitMeasureData}
                createToast={createToast}
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
              PrimaryBtnOnclick={() => deleteUnitOfMeasure(confirmationPopup.dataItem)}
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

export default UnitMeasure;
