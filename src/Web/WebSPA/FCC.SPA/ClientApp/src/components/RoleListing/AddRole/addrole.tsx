import { useState } from "react";
import { Accumulator, Col, Dropdown, Label, Row, TextField, Attachment, SecondaryButton, PrimaryButton, DocumentPreview, MultiSelectDropdown } from "../../CommonComponent"
import { checkValidationError } from "../../../utils/Validators";
import './addrole.scss';

interface DropDownField {
    value?: string | number;
    label?: string;
    title?: string
}
interface RoleDetails {
    roleCreatedBy?: string;
    roleModifyBy?: string;
}

interface IProps {
    businessTypeOptions?: Array<DropDownField>;
    handleChangeField?: any;
    inputFields?: any;
    handleDropdownChange?: any;
    handleMove?: any;
    handleAttachment?: any;
    handleButtonClick?: any;
    visible?: boolean;
    closeAttachment?: any,
    uploadedFiles?: any;
    handleRemoveFile?: any;
    handleSubmit: any;
    isFormValid: boolean;
    dismissPanel: any;
    isRoleExist: any;
    roleId: Number | string | null | undefined;
    userDataMapping: any;
    subTypeOptions: any;
    roleUserDetails?: RoleDetails;
    attachmentError?: String;
    handleDownload?: any;
}

// const docs = [
//     { uri: require("../../../assets/preview/sample-image.jpg") },
//     { uri: require("../../../assets/preview/sample.pdf") },
//     { uri: require("../../../assets/preview/sample-ex.xlsx") },
//     { uri: "https://images.unsplash.com/photo-1594818898109-44704fb548f6" }
//   ];

const AddRole = ({ businessTypeOptions, handleChangeField, inputFields, handleDropdownChange, handleMove, handleAttachment, handleButtonClick, visible, closeAttachment, uploadedFiles, handleRemoveFile, handleSubmit, isFormValid, dismissPanel, isRoleExist, roleId, userDataMapping, subTypeOptions, roleUserDetails, attachmentError, handleDownload }: IProps) => {
    const [previewData, setPreviewData] = useState<any>({
        visible: false,
        data: []
    })

    return (<>
        <div className="FCC_SlideContent-wrapper add-role-container">
            <Row>
                <Col xs={12} sm={12} md={6} lg={6} className={'mb-3'}>
                    <Label required>Role Name</Label>
                    <TextField placeholder="Enter" onChange={(e: any) => handleChangeField(e, 100)} name="role_name" value={inputFields?.role_name} disabled={roleId ? true : false}
                        {...(isRoleExist?.visible ? { errorMessage: isRoleExist?.message } : { ...(checkValidationError('required&rolename', inputFields?.role_name)) })}
                    />
                </Col>
                <Col xs={12} sm={12} md={6} lg={6} className={'mb-3 role-attachment-btn-block'}>
                    <SecondaryButton text='Role Permissions' className='margin-top' />                    
                    <SecondaryButton text={uploadedFiles && uploadedFiles.length > 0 ? `Attachments (${uploadedFiles.length})` : 'Attachments'} className={`me-0 margin-top ${visible ? 'primary-btn' :'secondary-btn'}`} id='attachments' onClick={handleButtonClick}>
                        <span className='fa fa-angle-down'></span>
                    </SecondaryButton>
                </Col>
                {
                    visible ?
                        <Attachment className="add-role-attachment" closeAttachment={closeAttachment} files={uploadedFiles} onChange={handleAttachment} onRemoveFile={handleRemoveFile} multiple 
                        previewFile={(fileData:any) => setPreviewData({visible: true, data: []})}
                        attachmentHeading='Attachments'
                        attachmentError={attachmentError}
                        handleDownload={handleDownload}                        
                        /> : ''

                }
                <Col xs={12} sm={12} md={6} lg={6} className={'mb-3'}>
                    <Label>Type</Label>
                    <Dropdown
                        className={'mb-2'}
                        options={businessTypeOptions}
                        onChange={(e: React.ChangeEvent<HTMLInputElement>) => handleDropdownChange(e, "type")}
                        name="type"
                        value={inputFields?.type}
                    />
                </Col>
                {(inputFields?.type?.value !== 'all')
                    ?
                    <Col xs={12} sm={12} md={6} lg={6}>
                        <Label>{inputFields?.type?.label}</Label>
                        {/* <Dropdown
                            className={'mb-2'}
                            options={subTypeOptions}
                            onChange={(e: React.ChangeEvent<HTMLInputElement>) => handleDropdownChange(e, "businessSubType")}
                            name="businessSubType"
                            value={inputFields?.businessSubType}
                            placeholder={`Select ${inputFields?.type?.label}`}
                            isMulti
                            isClearable
                        /> */}
                        <MultiSelectDropdown 
                            value={inputFields?.businessSubType}
                            onChange={(e: React.ChangeEvent<HTMLInputElement>) => handleDropdownChange(e, "businessSubType")}
                            options={subTypeOptions}
                            hasSelectAll={false}
                        />
                    </Col> : <></>}
                    <Col xs={12} sm={12} md={12} lg={12} className={'mb-3'}>
                    <Row>
                        <Col xs={12} sm={12} md={6} lg={6}>
                            {roleId && roleUserDetails?.roleCreatedBy ? <span className="created-by"><b>Created by: </b>{roleUserDetails?.roleCreatedBy}</span> : ''}
                        </Col>
                        <Col xs={12} sm={12} md={6} lg={6}>
                            {roleId && roleUserDetails?.roleModifyBy ? <span className="modified-by"><b>Modified by: </b>{roleUserDetails?.roleModifyBy}</span> : ''}
                        </Col>
                    </Row>
                    </Col>              
                    <Col xs={12} sm={12} md={12} lg={12} className={'mb-3'}>
                        <Accumulator sourceListHeading='User List' userDataMapping={userDataMapping} targetListHeading={`Assigned user(s) (${userDataMapping?.mappedData?.length})`} handleMove={(data: any) => handleMove(data)} />
                    </Col>
            </Row>
            {/* {previewData?.visible && previewData?.data.length > 0 && 
            (               
              <DocumentPreview docs={previewData?.data
              }
              handleModalClose={(flag:any) => setPreviewData({visible: flag, data: []})}
              />            
            )}          */}
        </div>
         <div className='FCC_slide-footer'>
            <SecondaryButton text='Cancel' onClick={() => dismissPanel(false)} />
            <PrimaryButton text='Save' onClick={handleSubmit} disabled={!isFormValid} />
        </div>
    </>)
}

export default AddRole