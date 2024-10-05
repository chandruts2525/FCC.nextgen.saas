import React from 'react';
import { Col, Dropdown, Label, Row, TextField, SecondaryButton, PrimaryButton, Tooltip } from "../../CommonComponent";
import './addunitmeasure.scss';
import InfoIcon from '../../../assets/images/Info.svg';
import { alphaNumericOnly, alphaNumericOnlyOnPaste, alphaNumericOnlyWithSpace, alphaNumericOnlyWithSpaceOnPaste, checkValidationError } from '../../../utils/Validators';

interface IProps {
  statusOptions: any,
  formData: any,
  collectData: any,
  measureTypeOptions: any,
  dismissPanel: any,
  handleSubmit: any,
  isFormValid: any,
  action: any;
}

const AddUnitMeasureComp = ({
  collectData,
  formData,
  statusOptions,
  measureTypeOptions,
  dismissPanel,
  handleSubmit,
  isFormValid,
  action
}: IProps) => {



  return (<>
    <div className='FCC_SlideContent-wrapper addUnitMeasure-container'>
      <Row>
        <Col xs={12} className={'mb-3'}>
          <Label required className='addUnitLabel'>Code </Label><span><img src={InfoIcon} alt='info' /></span>
          <TextField name='code' placeholder='Enter Code' onChange={(e: any) => collectData('code', e?.target?.value, 5)}
            value={formData?.code}
            onKeyPress={(e: any) => alphaNumericOnly(e)}
            onPaste={(e: any) => {
              alphaNumericOnlyOnPaste(e);
            }}
            disabled={action === "Edit"}
            {...(checkValidationError('required', formData?.code))} />
        </Col>

        <Col xs={12} className={'mb-3'}>
          <Label required className='addUnitLabel'>Name</Label><span><img src={InfoIcon} alt='info' /></span>
          <TextField name='name' placeholder='Enter Name' onChange={(e: any) => collectData('name', formData?.name?.length > 0 ? e?.target?.value : (e?.target?.value)?.trim(), 20)}
            value={formData?.name}
            onKeyPress={(e: any) => alphaNumericOnlyWithSpace(e)}
            onPaste={(e: any) => {
              alphaNumericOnlyWithSpaceOnPaste(e);
            }}
            {...(checkValidationError('required', (formData?.name)?.trim()))} />
        </Col>

        <Col xs={12} className={'mb-3'}>
          <Label className='addUnitLabel'>Measure Type</Label><span><img src={InfoIcon} alt='info' /></span>
          <Dropdown options={measureTypeOptions} onChange={(item: any) => {
            if (item && !formData?.conversionFactor) {
              formData.conversionFactor = '';
            }
            collectData('measureType', item);
          }}
            value={formData?.measureType}
          />
        </Col>

        <Col xs={12} className={'mb-3'}>
          <Label required={formData?.measureType} className='addUnitLabel'>Conversion Factor</Label><span><img src={InfoIcon} alt='info' /></span>
          <TextField name='conversionFactor' placeholder='Enter Conversion Factor' onChange={(e: any) => collectData('conversionFactor', e?.target?.value, 4)}
            value={formData?.conversionFactor}
            disabled={!formData?.measureType}
            {...(checkValidationError('required&numeric', formData?.conversionFactor))}
          />
        </Col>

        <Col xs={12} className={'mb-3'}>
          <Label className='addUnitLabel'>Status</Label><span><img src={InfoIcon} alt='info' /></span>
          <Dropdown options={statusOptions} onChange={(item: any) => collectData('status', item)}
            value={formData?.status}
            {...(checkValidationError('required', formData?.status, 'dropdown'))}
          />
        </Col>
      </Row>
    </div>
    <div className='FCC_slide-footer'>
        <SecondaryButton text='Cancel' onClick={() => dismissPanel(false)} />
        <PrimaryButton text='Save' onClick={handleSubmit} disabled={!isFormValid} />
    </div>
    </>
  );
};

export default AddUnitMeasureComp;