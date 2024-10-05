import { checkValidationError } from '../../../../utils/Validators';
import {
  Checkbox,
  Col,
  Label,
  PhoneInput,
  Row,
  SwitchToggle,
  TextField,
} from '../../../CommonComponent';
import Address from '../../Address';
import './companysetting.scss';

interface IProps {
  domainIdentifierList: any;
  tempList: any;
  removeDomainIdentifier: any;
  onClickAddDomainIdentifier: any;
  handleOnChangeAddDomainIdentifier: any;
  collectData: any;
  formData: any;
  collectBusinessAddress: any;
  collectMailingAddress: any;
  businessAddress: any;
  mailingAddress: any;
}
const CompanySetting = ({
  domainIdentifierList,
  tempList,
  removeDomainIdentifier,
  onClickAddDomainIdentifier,
  handleOnChangeAddDomainIdentifier,
  collectData,
  formData,
  collectBusinessAddress,
  collectMailingAddress,
  businessAddress,
  mailingAddress,
}: IProps) => {
  const domainIdentifierUI = () => {
    return (
      <div className="add-domainIdentifier-block">
        <Row>
          <Col xs={12} sm={12} md={5} lg={5} className="mb-3 pe-0 pe-md-3 me-3 me-md-0">
            <Label required>Domain Identifier</Label>
            <div className="d-flex">
              <TextField
                placeholder="Enter Domain Identifier"
                value={tempList}
                onChange={handleOnChangeAddDomainIdentifier}
              />
              <div className="plusicon" title="Add" onClick={onClickAddDomainIdentifier}>
                <span className="fa fa-plus"></span>
              </div>
            </div>
          </Col>
        </Row>
        <Row>
          <Col xs={12} sm={12} md={5} lg={5} className="mb-2 pe-0">
            {domainIdentifierList?.length ? (
              <ul className="domainIdentifier-list-block">
                {domainIdentifierList?.map((item: any, index: any) => (
                  <li key={index} className={'todo-list__list-item'}>
                    <span className="item" title={item}>
                      {item}
                    </span>
                    <span
                      className="fa fa-trash-o delete-icon"
                      title="Delete"
                      onClick={() => removeDomainIdentifier(index)}
                    ></span>
                  </li>
                ))}
              </ul>
            ) : (
              ''
            )}
          </Col>
        </Row>
      </div>
    );
  };
  return (
    <>
      <Row>
        <Col xs={12} sm={12} md={4} lg={4} className="mb-3">
          <Label required>Company Name</Label>
          {/* Please add 'error-filed' className for textfield if error message is visible. */}
          <TextField
            placeholder="Enter Company Name"
            onChange={(e: any) =>
              collectData('companyDetailsVM', 'companyName', e.target.value)
            }
            {...checkValidationError(
              'required',
              formData?.companyDetailsVM?.companyName,
              undefined,
              'Company Name is Required'
            )}
          />
        </Col>
        <Col xs={12} sm={12} md={4} lg={4} className="mb-3">
          <Label required>Company Email</Label>
          {/* Please add 'error-filed' className for textfield if error message is visible. */}
          <TextField
            placeholder="Enter Company Email"
            onChange={(e: any) =>
              collectData('companyDetailsVM', 'companyEmail', e.target.value)
            }
            {...checkValidationError(
              'required&email',
              formData?.companyDetailsVM?.companyEmail,
              undefined,
              'Company Email is Required'
            )}
          />
        </Col>
        <Col xs={12} sm={12} md={4} lg={4} className="mb-3">
          <Label required>Company Phone</Label>
          {/* Please add 'error-filed' className for textfield if error message is visible. */}
          {/* <PhoneInput value={value} onChange={onChangePhoneInput} /> */}
          <PhoneInput
            {...checkValidationError(
              'required',
              formData?.companyDetailsVM?.companyPhone,
              undefined,
              'Company Phone is Required'
            )}
            value={formData?.companyDetailsVM?.companyPhone}
            onChange={(val: any, obj: any) => {
              collectData('companyDetailsVM', 'companyPhone', {
                companyPhone: val,
                countryCode: obj?.country?.dialCode,
              });
            }}
          />
        </Col>
        <Col xs={12} sm={12} md={4} lg={4} className="mb-3">
          <Label required>Company Website</Label>
          {/* Please add 'error-filed' className for textfield if error message is visible. */}
          <TextField
            placeholder="Enter Company Website"
            onChange={(e: any) =>
              collectData('companyDetailsVM', 'companyWebsite', e.target.value)
            }
            {...checkValidationError(
              'required&url',
              formData?.companyDetailsVM?.companyWebsite,
              undefined,
              'Company Website is Required'
            )}
          />
        </Col>
        <Col xs={12} sm={12} md={4} lg={4} className="mb-3">
          <Label required>Code</Label>
          {/* Please add 'error-filed' className for textfield if error message is visible. */}
          <TextField
            placeholder="Enter Code"
            onChange={(e: any) =>
              collectData('companyDetailsVM', 'codeName', e.target.value)
            }
            {...checkValidationError(
              'required',
              formData?.companyDetailsVM?.codeName,
              undefined,
              'Company Code is Required'
            )}
          />
        </Col>
        <Col xs={12} sm={12} md={4} lg={4} className="mb-3">
          <Label required>Status (Active/Inactive)</Label>
          <SwitchToggle
            className="swt-status"
            defaultChecked={formData?.companyDetailsVM?.status}
            disabled={!formData?.companyId}
            onChange={(e: any, val: any) =>
              collectData('companyDetailsVM', 'status', val)
            }
          />
        </Col>
      </Row>
      <p className="nested-heading-companytxt">Business Address</p>
      <Address collectData={collectBusinessAddress} formData={businessAddress} />
      <Row>
        <Col xs={12} sm={12} md={12} lg={12}>
          <p className="nested-heading-companytxt pull-left">Mailing Address</p>
          <div className="address-checkbox">
            <Checkbox
              label="Same as Business Address"
              className="mb-md-0"
              onChange={(e: any, val: any) =>
                collectData('companyDetailsVM', 'sameAsBA', val)
              }
            />
          </div>
        </Col>
      </Row>
      <Address
        required={false}
        collectData={collectMailingAddress}
        formData={mailingAddress}
      />
      <p className="nested-heading-companytxt">Domain Identifier</p>
      {domainIdentifierUI()}
      <p className="nested-heading-companytxt">Contact Information</p>
      <Row>
        <Col xs={12} sm={12} md={4} lg={4} className="mb-3">
          <Label>Contact Person</Label>
          {/* Please add 'error-filed' className for textfield if error message is visible. */}
          <TextField
            placeholder="Enter Contact Person"
            onChange={(e: any) =>
              collectData(
                'companyDetailsVM',
                'contactName',
                e.target.value,
                'contactInformation'
              )
            }
            // {...(checkValidationError('required', formData?.companyDetailsVM?.contactInformation?.contactName, undefined, "Contact Person is Required"))}
          />
        </Col>
        <Col xs={12} sm={12} md={4} lg={4} className="mb-3">
          <Label>Contact Email</Label>
          {/* Please add 'error-filed' className for textfield if error message is visible. */}
          <TextField
            placeholder="Enter Contact Email"
            onChange={(e: any) =>
              collectData(
                'companyDetailsVM',
                'contactEmail',
                e.target.value,
                'contactInformation'
              )
            }
            // {...(checkValidationError('required&email', formData?.companyDetailsVM?.contactInformation?.contactEmail, undefined, "Contact Email is Required"))}
          />
        </Col>
        <Col xs={12} sm={12} md={4} lg={4} className="mb-3">
          <Label>Company Number</Label>
          {/* Please add 'error-filed' className for textfield if error message is visible. */}
          <PhoneInput
            // {...(checkValidationError('required', formData?.companyDetailsVM?.contactInformation?.phoneNumber, undefined, "Phone Number is Required"))}
            value={formData?.companyDetailsVM?.contactInformation?.phoneNumber}
            onChange={(val: any, obj: any) => {
              collectData(
                'companyDetailsVM',
                'phoneNumber',
                { phoneNumber: val, countryCode: obj?.country?.dialCode },
                'contactInformation'
              );
            }}
          />
        </Col>
      </Row>
    </>
  );
};

export default CompanySetting;
