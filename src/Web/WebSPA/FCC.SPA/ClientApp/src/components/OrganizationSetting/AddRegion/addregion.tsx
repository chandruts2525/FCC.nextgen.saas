import { Checkbox, Col, Label, PrimaryButton, Row, SearchBox, SecondaryButton, SwitchToggle, TextField } from "../../CommonComponent"
import "./addregion.scss"
interface IProps {
    yardList?:any,
    dismissPanelRegion:any
}
const AddRegion = ({yardList,dismissPanelRegion}:IProps) => {
    return(<>
        <div className="FCC_SlideContent-wrapper">
            <Row className="add-region-container">
                <Col xs={12} sm={12} md={12} lg={12} className="mb-3">
                    <Label required>Region Name</Label>
                    <TextField placeholder="Enter Region Name"/>
                </Col>
                <Col xs={12} sm={12} md={12} lg={12} className="mb-3">
                    <Label required>Status (Active/Inactive)</Label>
                    <SwitchToggle className="swt-status" />
                </Col>
                <Col xs={12} sm={12} md={12} lg={12} className="mb-3">
                    <Label>Description</Label>
                    <TextField multiline resizable={false} placeholder="Enter Description"/>
                </Col>
                <Col xs={12} sm={12} md={12} lg={12} className="mb-3">
                    <Label>Yards List</Label>
                    <SearchBox placeholder="Search Yards"/>
                    <ul className="yard-list-block">
                        {
                            yardList.map((item:any)=>{
                                return(
                                    <li key={item.id}>
                                        <Checkbox className="mb-0"/>
                                        <span>{item.yardName}</span>
                                    </li>
                                )
                            })
                        }
                    </ul>
                </Col>            
            </Row>
        </div>
        <div className='FCC_slide-footer'>
            <SecondaryButton text='Cancel' onClick={() => dismissPanelRegion(false)} />
            <PrimaryButton text='Save' onClick={() => dismissPanelRegion(false)}  />
        </div>
    </>)
}

export default AddRegion