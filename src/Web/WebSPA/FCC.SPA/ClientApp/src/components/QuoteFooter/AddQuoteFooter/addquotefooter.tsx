import {
  Attachment,
  Col,
  Dropdown,
  Label,
  PrimaryButton,
  Row,
  SecondaryButton,
  TextField,
  RichTextEditor,
  QuillRichTextEditor,
  MultiSelectDropdown,
  KendoEditor,
} from '../../CommonComponent';
import { alphaNumericOnlyWithSpace, alphaNumericOnlyWithSpaceOnPaste, checkValidationError } from '../../../utils/Validators';
import './addquotefooter.scss';

interface IProps {
  companyOptions: any;
  statusOptions: any;
  moduleOptions: any;
  dismissPanel: boolean;
  files: any;
  formData: any;
  collectData: any;
  isFooterNameExist: any;
  handleSubmit?: any;
  isFormValid?: any;
  handleDescriptionChange?: any;
  description?: any;
  collectDropdownData?: any;
}

const AddQuoteFooter = ({
  companyOptions,
  statusOptions,
  moduleOptions,
  dismissPanel,
  files,
  formData,
  collectData,
  isFooterNameExist,
  isFormValid,
  handleSubmit,
  handleDescriptionChange,
  description,
  collectDropdownData,
}: IProps) => {
  return (<>
    <div className="FCC_SlideContent-wrapper add-quotefooter-container">
      <Row>
        <Col xs={12} sm={12} md={6} lg={6} className={'mb-3'}>
          <Label required>Name</Label>
          <TextField
            placeholder="Enter Name"
            name="quoteFooterName"
            onKeyPress={(e: any) => alphaNumericOnlyWithSpace(e)}
            onPaste={(e: any) => {
              alphaNumericOnlyWithSpaceOnPaste(e);
            }}
            onChange={(e: any) =>
              collectData(
                'quoteFooterName',
                formData?.quoteFooterName?.length > 0
                  ? e?.target?.value
                  : e?.target?.value?.trim(),
                100
              )
            }
            value={formData?.quoteFooterName}
            {...(isFooterNameExist?.visible
              ? { errorMessage: isFooterNameExist?.message }
              : {
                ...checkValidationError('required', formData?.quoteFooterName?.trim()),
              })}
          />
        </Col>
        <Col xs={12} sm={12} md={6} lg={6} className={'mb-3'}>
          <Label required>Company(ies)</Label>
          <MultiSelectDropdown
            value={formData?.companies || []}
            onChange={(item: any) => collectDropdownData('companies', item)}
            options={companyOptions}
            name="companies"
            selectAllText="All Companies"
            {...checkValidationError('required', formData?.companies, 'dropdown')}
          />
        </Col>
        <Col xs={12} sm={12} md={6} lg={6} className={'mb-3'}>
          <Label>Status</Label>
          <Dropdown
            options={statusOptions}
            name="status"
            onChange={(item: any) => collectData('status', item)}
            value={formData?.status}
          />
        </Col>
        <Col xs={12} sm={12} md={6} lg={6} className={'mb-3'}>
          <Label required>Module(s)</Label>
          <MultiSelectDropdown
            name="module"
            value={formData?.module || []}
            onChange={(item: any) => collectDropdownData('module', item)}
            options={moduleOptions}
            selectAllText="All Modules"
            {...checkValidationError('required', formData?.module, 'dropdown')}
          />
        </Col>
        <Col xs={12} sm={12} md={12} lg={12} className={'mb-3'}>
          <Label required>Description</Label>
          <QuillRichTextEditor
            value={description}
            onChange={(e: any) => handleDescriptionChange('description', e, null)}
            {...checkValidationError('required', description)}
          />
          {/* <KendoEditor
            value={description}
            onChange={(value: any) => handleDescriptionChange('description', value, null)}
          /> */}
        </Col>
        <Col xs={12} sm={12} md={12} lg={12} className={'mb-3 px-0'}>
          <Attachment
            className="add-role-attachment"
            closeAttachment={''}
            files={files}
            multiple
            attachmentHeading="Template Upload"
          />
        </Col>
      </Row>
    </div>
    <div className="FCC_slide-footer">
      <SecondaryButton text="Cancel" onClick={dismissPanel} />
      <PrimaryButton text="Save" onClick={handleSubmit} disabled={!isFormValid} />
    </div>
  </>);
};

export default AddQuoteFooter;

