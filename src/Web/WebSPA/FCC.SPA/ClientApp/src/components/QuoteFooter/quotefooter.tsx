import {
  Breadcrumb,
  Card,
  DataTable,
  Panel,
  PrimaryButton,
  SearchBox,
  ModalPopup,
  Export,
} from '../CommonComponent';
import AddQuoteFooter from './AddQuoteFooter';
import './quotefooter.scss';

interface IProps {
  showExportOption?: any;
  excelExportClick?: any;
  breadcrumlist?: any;
  quoteFootersListingColumns?: any;
  isOpen?: any;
  openPanel?: any;
  dismissPanel?: any;
  createToast?: any;
  action?: any;
  setAction?: any;
  confirmationPopup?: any;
  handlePopupChange?: any;
  deleteSelectedFooter?: any;
  excelExport?: any;
  _exportExcel?: any;
  _exportPDF?: any;
  onSearchChange?: any;
  searchKeyword?: any;
  handleClear?: any;
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
  setShowExportOption?:any
}

const QuoteFooter = ({
  showExportOption,
  excelExportClick,
  breadcrumlist,
  quoteFootersListingColumns,
  isOpen,
  openPanel,
  dismissPanel,
  createToast,
  action,
  setAction,
  confirmationPopup,
  handlePopupChange,
  deleteSelectedFooter,
  excelExport,
  _exportExcel,
  _exportPDF,
  onSearchChange,
  searchKeyword,
  handleClear,
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
          <div className="role-listing-container">
            <p className="heading-txt">Quote Footers</p>
            <div className="top-section mb-3">
              <SearchBox
                placeholder="Search by Name, Company, Module"
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
                />   
                <PrimaryButton
                  text="Create Quote Footer"
                  onClick={() => {
                    setAction('Add');
                    openPanel();
                  }}
                ></PrimaryButton>
              </div>
            </div>
            <DataTable
              data={data}
              dataColumn={quoteFootersListingColumns}
              className="quotefooterlisting_grid"
              setAction={setAction}
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
              exportFileName={'QuoteFooter'}
            />
            <Panel
              isOpen={isOpen?.visible}
              onDismiss={dismissPanel}
              headerText={action === 'Add' ? 'New Quote Footer' : 'Edit Quote Footer'}
              slideType="medium"
            >
              <AddQuoteFooter
                dismissPanel={dismissPanel}
                createToast={createToast}
                setIsFormValid={setIsFormValid}
                isFormValid={isFormValid}
                selectedFooterData={isOpen?.data}
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
              PrimaryBtnOnclick={() => deleteSelectedFooter(confirmationPopup.dataItem)}
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

export default QuoteFooter;
