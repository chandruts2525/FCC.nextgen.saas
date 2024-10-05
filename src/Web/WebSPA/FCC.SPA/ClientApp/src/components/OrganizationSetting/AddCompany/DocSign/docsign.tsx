import { checkValidationError } from "../../../../utils/Validators";
import { Col, Label, Row, TextField } from "../../../CommonComponent";

const DocSign = ({ collectData, formData }: any) => {
    return (
        <Row>
            <Col xs={12} sm={12} md={4} lg={4} className="mb-3">
                <Label required>Users</Label>
                <TextField placeholder="Enter Users"
                    onChange={(e: any) => collectData('companyDocuSignVM', 'user', e.target.value)}
                    {...(checkValidationError('required', formData?.companyDocuSignVM?.user, undefined, "User is Required"))}
                />
            </Col>
            <Col xs={12} sm={12} md={4} lg={4} className="mb-3">
                <Label required>Password</Label>
                <TextField placeholder="Enter Password"
                    onChange={(e: any) => collectData('companyDocuSignVM', 'password', e.target.value)}
                    {...(checkValidationError('required', formData?.companyDocuSignVM?.password, undefined, "Password is Required"))}
                />
            </Col>
            <Col xs={12} sm={12} md={4} lg={4} className="mb-3">
                <Label required>Key</Label>
                <TextField placeholder="Enter Key"
                    onChange={(e: any) => collectData('companyDocuSignVM', 'key', e.target.value)}
                    {...(checkValidationError('required', formData?.companyDocuSignVM?.key, undefined, "Key is Required"))}
                />
            </Col>
        </Row>
    );
};

export default DocSign;