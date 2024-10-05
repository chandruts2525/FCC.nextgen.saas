import { Checkbox, Col, ColorPicker, ImageUploader, Label, Row } from "../../../CommonComponent"
import TermsQuoteFooter from "../../TermsQuoteFooter";
import "./brand.scss";

const Brand = () => {

    return(
       <div className="brand-tab-container">
            <p className="nested-heading-companytxt-withoutborder">Branding</p>
            <div className="branding-container mb-3">
                <div className="d-flex">
                    <Checkbox className="mb-0 mt-1 pull-left"/>     
                    <div className="pull-left">                    
                        <p><strong>Include Company Logo</strong></p>   
                        <span className="sub-text">If unchecked, e-ticket will display the company details from the Mailing Address tab.</span>                       
                    </div>  
                </div>               
                <Row>
                    <Col xs={12} sm={12} md={4} lg={4} className="mb-1">
                        <Label>Company Logo</Label>
                        <ImageUploader/>
                        <ul className="image-uploader-validators">
                            <li>Allowed image size 250*125 px.</li>
                            <li>Allowed file size 500KB.</li>
                            <li>Allowed file formats png, jpg or jpeg.</li>
                        </ul>
                    </Col>
                    <Col xs={12} sm={12} md={4} lg={4} className="mb-1">
                        <Label>Theme Color</Label>
                        <ColorPicker/>
                    </Col>
                </Row>
            </div>
            <p className="nested-heading-companytxt-withoutborder">T&Cs and Footers</p>
            <div className="footer-container">
                <TermsQuoteFooter/>
            </div>
       </div>
    )
}

export default Brand