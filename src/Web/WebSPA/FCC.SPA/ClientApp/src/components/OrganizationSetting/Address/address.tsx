import { checkValidationError } from '../../../utils/Validators';
import { Col, Dropdown, Label, Row, TextField } from '../../CommonComponent';

interface IProps {
  countryOptions: any;
  stateOptions: any;
  collectData: any;
  required: any;
  formData: any;
  sameAsAddress?: any;
}

const Address = ({
  countryOptions,
  stateOptions,
  collectData,
  required = true,
  formData,
  sameAsAddress,
}: IProps) => {
  return (
    <Row>
      <Col xs={12} sm={12} md={4} lg={4} className="mb-3">
        <Label required={required}>Address 1</Label>
        {/* Please add 'error-filed' className for textfield if error message is visible. */}
        <TextField
          disabled={sameAsAddress}
          placeholder="Enter Address 1"
          onChange={(e: any) => collectData('address1', e.target.value)}
          value={formData?.address1}
          {...(required &&
            checkValidationError(
              'required',
              formData?.address1,
              undefined,
              'Address 1 is Required'
            ))}
        />
      </Col>
      <Col xs={12} sm={12} md={4} lg={4} className="mb-3">
        <Label>Address 2</Label>
        <TextField
          disabled={sameAsAddress}
          placeholder="Enter Address 2"
          value={formData?.address2}
          onChange={(e: any) => collectData('address2', e.target.value)}
          // {...(required &&
          //   checkValidationError(
          //     'required',
          //     formData?.address2,
          //     undefined,
          //     'Address 2 is Required'
          //   ))}
        />
      </Col>
      <Col xs={12} sm={12} md={4} lg={4} className="mb-3">
        <Label required={required}>Country</Label>
        <Dropdown
          isDisabled={sameAsAddress}
          value={formData?.country}
          isClearable
          isSearchable
          options={countryOptions}
          onChange={(e: any) => collectData('country', e)}
          {...(required &&
            checkValidationError(
              'required',
              formData?.country,
              undefined,
              'Country is Required'
            ))}
        />
      </Col>
      <Col xs={12} sm={12} md={4} lg={4} className="mb-3">
        <Label required={required}>State</Label>
        <Dropdown
          isDisabled={sameAsAddress}
          options={stateOptions}
          value={formData?.state}
          isClearable
          isSearchable
          onChange={(e: any) => collectData('state', e)}
          {...(required &&
            checkValidationError(
              'required',
              formData?.state,
              undefined,
              'State is Required'
            ))}
        />
      </Col>
      <Col xs={12} sm={12} md={4} lg={4} className="mb-3">
        <Label required={required}>City</Label>
        <TextField
          disabled={sameAsAddress}
          placeholder="Enter City"
          value={formData?.city}
          onChange={(e: any) => collectData('city', e.target.value)}
          {...(required &&
            checkValidationError(
              'required',
              formData?.city,
              undefined,
              'City is Required'
            ))}
        />
      </Col>
      <Col xs={12} sm={12} md={4} lg={4} className="mb-3">
        <Label required={required}>Zip</Label>
        <TextField
          disabled={sameAsAddress}
          placeholder="Enter Zip"
          value={formData?.zip}
          onChange={(e: any) => collectData('zip', e.target.value)}
          {...(required &&
            checkValidationError(
              'required',
              formData?.zip,
              undefined,
              'Zip is Required'
            ))}
        />
      </Col>
    </Row>
  );
};

export default Address;
