import { Col, Dropdown, Label, PrimaryButton, Row, SecondaryButton, SwitchToggle, TextField } from "../../CommonComponent"

interface IProps {
    dismissPanelDepartment:any,
    glDistibutorsCode:any,
    companyNameOptions:any
}
const AddDepartment = ({dismissPanelDepartment,glDistibutorsCode,companyNameOptions}:IProps) => {
    return(<>
        <div className="FCC_SlideContent-wrapper">
            <Row>
                <Col xs={12} sm={12} md={12} lg={12} className="mb-3">
                    <Label required>Company Name</Label>
                    <Dropdown options={companyNameOptions}/>
                </Col>
                <Col xs={12} sm={12} md={12} lg={12} className="mb-3">
                    <Label required>Department Code</Label>
                    <TextField placeholder="Enter Department Code"/>
                </Col>
                <Col xs={12} sm={12} md={12} lg={12} className="mb-3">
                    <Label required>Department Name</Label>
                    <TextField placeholder="Enter Department Name"/>
                </Col>
                <Col xs={12} sm={12} md={12} lg={12} className="mb-3">
                    <Label>DFLT GL Distr</Label>
                    <Dropdown options={glDistibutorsCode}/>
                </Col>
                <Col xs={12} sm={12} md={12} lg={12} className="mb-3">
                    <Label>Status (Active/inactive)</Label>
                    <SwitchToggle/>
                </Col>
            </Row>
        </div>
         <div className='FCC_slide-footer'>
             <SecondaryButton text='Cancel' onClick={() => dismissPanelDepartment(false)} />
             <PrimaryButton text='Save' onClick={() => dismissPanelDepartment(false)}  />
         </div>
    </>)
}

export default AddDepartment