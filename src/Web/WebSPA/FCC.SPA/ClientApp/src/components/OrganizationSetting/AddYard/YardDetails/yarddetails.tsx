import {
  alphaNumericOnly,
  alphaNumericOnlyOnPaste,
  alphaNumericOnlyWithSpace,
  alphaNumericOnlyWithSpaceOnPaste,
  checkValidationError,
} from '../../../../utils/Validators';
import {
  Checkbox,
  Col,
  Dropdown,
  Label,
  PhoneInput,
  Row,
  SwitchToggle,
  TextField,
} from '../../../CommonComponent';
import Address from '../../Address';
import TermsQuoteFooter from '../../TermsQuoteFooter';

interface IProps {
  dftlOptions?: any;
  collectData?: any;
  formData?: any;
  collectPhysicalAddress?: any;
  collectMailingAddress?: any;
  physicalAddress?: any;
  mailingAddress?: any;
  handleSameAsAddress?: any;
  quoteFooterOptions?: any;
  quoteTermConditionOptions?: any;
  statePhysicalAddress?: any;
  stateMailingAddress?: any;
  sameAsAddress?: any;
  setSameAsAddress?: any;
}
const YardDetails = ({
  dftlOptions,
  collectData,
  formData,
  collectPhysicalAddress,
  collectMailingAddress,
  physicalAddress,
  mailingAddress,
  handleSameAsAddress,
  quoteFooterOptions,
  quoteTermConditionOptions,
  statePhysicalAddress,
  stateMailingAddress,
  sameAsAddress,
  setSameAsAddress,
}: IProps) => {
  return (
    <>
      <Row>
        <Col xs={12} sm={12} md={4} lg={4} className="mb-3">
          <Label required>Code</Label>
          {/* Please add 'error-filed' className for textfield if error message is visible. */}
          <TextField
            placeholder="Enter Code"
            onKeyPress={(e: any) => alphaNumericOnly(e)}
            onPaste={(e: any) => {
              alphaNumericOnlyOnPaste(e);
            }}
            value={formData?.yardDetailsVM?.businessEntityCode}
            onChange={(e: any) =>
              collectData('yardDetailsVM', 'businessEntityCode', e.target.value)
            }
            {...checkValidationError(
              'required',
              formData?.yardDetailsVM?.businessEntityCode,
              undefined,
              'Code is Required'
            )}
          />
        </Col>
        <Col xs={12} sm={12} md={4} lg={4} className="mb-3">
          <Label required>Yard Name</Label>
          <TextField
            placeholder="Enter Yard Name"
            onKeyPress={(e: any) => alphaNumericOnlyWithSpace(e)}
            onPaste={(e: any) => {
              alphaNumericOnlyWithSpaceOnPaste(e);
            }}
            value={formData?.yardDetailsVM?.businessEntityName}
            onChange={(e: any) =>
              collectData('yardDetailsVM', 'businessEntityName', e.target.value)
            }
            {...checkValidationError(
              'required',
              formData?.yardDetailsVM?.businessEntityName,
              undefined,
              'Yard Name is Required'
            )}
          />
        </Col>
        <Col xs={12} sm={12} md={4} lg={4} className="mb-3">
          <Label>DFLT GL Distr</Label>
          <Dropdown
            options={dftlOptions}
            value={formData?.yardDetailsVM?.glDistributionCode}
            onChange={(e: any) => collectData('yardDetailsVM', 'glDistributionCode', e)}
            isClearable
          />
        </Col>
        <Col xs={12} sm={12} md={4} lg={4} className="mb-3">
          <Label required>Status (Active/Inactive)</Label>
          <SwitchToggle
            disabled
            className="swt-status"
            checked={formData?.yardDetailsVM?.status}
            onChange={(e: any, val: any) => collectData('yardDetailsVM', 'status', val)}
          />
        </Col>
      </Row>
      <p className="nested-heading-companytxt">Physical Address</p>
      <Address
        collectData={collectPhysicalAddress}
        stateOptions={statePhysicalAddress}
        formData={formData?.yardDetailsVM?.physicalAddress}
      />
      <Row>
        <Col xs={12} sm={12} md={4} lg={4} className="mb-3">
          <Label required>Phone</Label>
          <PhoneInput
            value={formData?.yardDetailsVM?.physicalAddress?.addressPhone?.companyPhone}
            {...checkValidationError(
              'required',
              formData?.yardDetailsVM?.physicalAddress?.addressPhone?.companyPhone,
              undefined,
              'Company Phone is Required'
            )}
            onChange={(val: any, obj: any) => {
              collectPhysicalAddress('addressPhone', {
                companyPhone: val,
                countryCode: obj?.country?.dialCode,
              });
              //   collectData('yardDetailsVM', 'addressPhone', {
              //     companyPhone: val,
              //     countryCode: obj?.country?.dialCode,
              //   });
            }}
          />
        </Col>
      </Row>
      <Row>
        <Col xs={12} sm={12} md={12} lg={12}>
          <p className="nested-heading-companytxt pull-left">Mailing Address</p>
          <div className="address-checkbox">
            <Checkbox
              label="Same as Physical Address"
              className="mb-md-0"
              checked={sameAsAddress}
              onChange={(e: any, val: any) => {
                // collectData('yardDetailsVM', 'sameAs', val);
                setSameAsAddress(val);
                handleSameAsAddress(val);
              }}
            />
          </div>
        </Col>
      </Row>
      <Address
        sameAsAddress={sameAsAddress}
        required={false}
        collectData={collectMailingAddress}
        stateOptions={stateMailingAddress}
        formData={formData?.yardDetailsVM?.mailingAddress}
      />
      <Row>
        <Col xs={12} sm={12} md={4} lg={4} className="mb-3">
          <Label>Phone</Label>
          <PhoneInput
            disabled={sameAsAddress}
            value={formData?.yardDetailsVM?.mailingAddress?.addressPhone?.companyPhone}
            onChange={(val: any, obj: any) => {
              collectMailingAddress('addressPhone', {
                companyPhone: val,
                countryCode: obj?.country?.dialCode,
              });
              //   collectData('yardDetailsVM', 'addressPhone', {
              //     phoneNumber: val,
              //     countryCode: obj?.country?.dialCode,
              //   });
            }}
          />
        </Col>
      </Row>
      <p className="nested-heading-companytxt">T&Cs and Footers</p>
      <TermsQuoteFooter
        collectData={(name: any, value: any) => collectData('yardDetailsVM', name, value)}
        formData={formData}
        quoteFooterOptions={quoteFooterOptions}
        quoteTermConditionOptions={quoteTermConditionOptions}
      />
    </>
  );
};

export default YardDetails;
