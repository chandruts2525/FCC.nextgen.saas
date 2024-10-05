import { Col, Label, MultiSelectDropdown, PrimaryButton, Row, SecondaryButton, TextField } from "../../CommonComponent"

interface IProps {
    selectedCompany:any,
    setSelectedCompany:any,
    getSelectDataCompany:any,
    dismissPanelGroup:any
}
const AddGroup = ({selectedCompany,setSelectedCompany,getSelectDataCompany,dismissPanelGroup}:IProps) => {
    return(<>
        <div className="FCC_SlideContent-wrapper">
            <Row>
                <Col xs={12} sm={12} md={12} lg={12} className='mb-3'>
                    <Label required>Group Name</Label>
                    <TextField placeholder="Enter Group Name"/>
                </Col>
                <Col xs={12} sm={12} md={12} lg={12} className='mb-3'>
                    <Label required>Company</Label>
                    <MultiSelectDropdown value={selectedCompany} onChange={setSelectedCompany} options= {getSelectDataCompany()}/>
                </Col>
                <Col xs={12} sm={12} md={12} lg={12} className='mb-3'>
                    <Label required>Yard</Label>
                    <MultiSelectDropdown value={selectedCompany} onChange={setSelectedCompany} options= {getSelectDataCompany()}/>
                </Col>
                <Col xs={12} sm={12} md={12} lg={12} className='mb-3'>
                    <Label required>Department</Label>
                    <MultiSelectDropdown value={selectedCompany} onChange={setSelectedCompany} options= {getSelectDataCompany()}/>
                </Col>
            </Row>
        </div>
        <div className='FCC_slide-footer'>
            <SecondaryButton text='Cancel' onClick={() => dismissPanelGroup(false)} />
            <PrimaryButton text='Save' onClick={() => dismissPanelGroup(false)}  />
        </div>
    </>)
}

export default AddGroup