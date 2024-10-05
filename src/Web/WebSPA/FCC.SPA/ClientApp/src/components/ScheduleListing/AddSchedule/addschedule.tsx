import React, { useEffect } from 'react'
import { Col, Dropdown, Label, Row, TextField, SecondaryButton, PrimaryButton, Tooltip, Callout } from "../../CommonComponent"
import './addschedule.scss'
import { InfoIcon } from '../../../assets/images';
import Checkbox from '../../CommonComponent/Checkbox'
import { alphaNumericOnly, checkValidationError, alphaNumericOnlyWithSpaceOnPaste, alphaNumericOnlyOnPaste, alphaNumericOnlyWithSpace } from '../../../utils/Validators';
import {containsSpecialChars} from '../../SharedComponent/Common-function';
import TooltipComponent from '../../CommonComponent/Tooltip';

const AddSchedule = ({ hideInfoTooltip, isTooltipVisible, handleInfoClick, dismissPanel, statusOptions, unitComponentOptions, formData, setFormData, onSave, onUpdate, isFormValid }: any) => {

  const collectData = (name: string, val: any) => {   
      setFormData({
        ...formData,
        [name]: val
      })   
  }  

  const onSubmit = () => {
    if (formData?.scheduleTypeId) {
      onUpdate(formData)
    } else {
      onSave(formData)
    }
  }

  return (<>
    <div className="FCC_SlideContent-wrapper add-schedule-container">
      <Row>
        <Col xs={12} className={'mb-3'}>
          <Label required className='addScheduleLabel'>Code </Label>
          <span><img src={InfoIcon} alt='info' /></span>
          <TextField name='Code' placeholder='Enter Code'
            disabled={formData?.scheduleTypeId}
            onChange={(e: any) => collectData('scheduleTypeCode', e?.target?.value)}
            value={formData?.scheduleTypeCode}
            onKeyPress={(e: any) => alphaNumericOnly(e)}
            onPaste={(e: any) => {
              alphaNumericOnlyOnPaste(e)
            }}  
            maxLength={20}
            {...(checkValidationError('required', formData?.scheduleTypeCode, undefined, "Code Required"))}
          />
        </Col>

        <Col xs={12} className={'mb-3'}>
          <Label required className='addScheduleLabel'>Name</Label><span><img src={InfoIcon} alt='info' /></span>
          <TextField name='Name' placeholder='Enter Name'
            onChange={(e: any) => collectData('scheduleTypeName', e?.target?.value)}
            value={formData?.scheduleTypeName}
            onKeyPress={(e: any) => alphaNumericOnlyWithSpace(e)}
            onPaste={(e: any) => {
              alphaNumericOnlyWithSpaceOnPaste(e)
            }}  
            maxLength={30}
            {...(checkValidationError('required', formData?.scheduleTypeName, undefined, "Name Required"))}
          />
        </Col>

        <Col xs={12} className={'mb-3'}>
          <Label className='addScheduleLabel'>Unit or Component</Label><span><img src={InfoIcon} alt='info' /></span>
          <Dropdown options={unitComponentOptions}
            onChange={(item: any) => collectData('unitOrComponent', item)}
            value={formData?.unitOrComponent}
            isClearable
          />
        </Col>

        <Col xs={12} className={'mb-3'}>
          <Label className='addScheduleLabel'>Status</Label><span><img src={InfoIcon} alt='info' /></span>
          <Dropdown options={statusOptions}
            onChange={(item: any) => collectData('isActive', item)}
            value={formData?.isActive}
          />
        </Col>

        <Col xs={12} className={'mt-2 mb-2'}>
          <div className='checkboxContainer'>
            <Checkbox onChange={(e: any) => collectData("schedulable", e?.target?.checked)} checked={formData?.schedulable} />
            <span className='checkboxText'>Schedulable</span>
          </div>
        </Col>
        <Col xs={12} className={'mb-2'}>
          <div className='checkboxContainer'>
            <Checkbox onChange={(e: any) => collectData("gbp", e?.target?.checked)} checked={formData?.gbp} />
            <span className='checkboxText'>Ground Bearing Pressure (GBP) Applies</span>
            <span className={'mb-4'}>
              <img src={InfoIcon} alt='info' id='GBP-id' onMouseEnter={handleInfoClick} onMouseLeave={hideInfoTooltip}/>
            </span>
            {
              isTooltipVisible ?
                <Callout calloutWidth={250} targetId="GBP-id" toggleIsCalloutVisible={handleInfoClick}>
                  <span className='tootip-content'>Ground Bearing Pressure (GBP) is used to indicate maximum level of support required by a unit with a point load.
                    If left blank, this field will not be used for this unit type; if a value is added, the value will be displayed on quote, e-ticket and elsewhere.
                  </span>
                </Callout> : ''
            }
          </div>
        </Col>
      </Row>
    </div>
    <div className='FCC_slide-footer'>
      <SecondaryButton text='Cancel' onClick={dismissPanel} />
      <PrimaryButton text='Save' onClick={() => onSubmit()} disabled={!isFormValid} />
    </div>
  </>);
};

export default AddSchedule;
