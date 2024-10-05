import {
    Col,
    Dropdown,
    Label,
    Row,
    TextField,
    SecondaryButton,
    PrimaryButton,
    MultiSelectDropdown,
} from '../../CommonComponent';
import './adduser.scss';
import { checkValidationError } from '../../../utils/Validators';

interface IProps {
    roleOptions: any;
    yardOptions: any;
    departmentOptions: any;
    companyOptions: any;
    employeeOptions: any;
    statusOptions: any;
    formData: any;
    setFormData: any;
    isFormValid: boolean;
    dismissPanel: any;
    handleSubmit: any;
    error: any;
    userId: any;
}

const AddUser = ({
    roleOptions,
    yardOptions,
    departmentOptions,
    companyOptions,
    employeeOptions,
    statusOptions,
    formData,
    setFormData,
    userId,
    isFormValid,
    dismissPanel,
    handleSubmit,
    error,
}: IProps) => {
    const collectData = (name: string, val: any, length: any = null) => {
        if (length && val.length <= length) {
            setFormData({
                ...formData,
                [name]: val,
            });
        } else if (!length) {
            setFormData({
                ...formData,
                [name]: val,
            });
        }
    };

    const getError = (previousError: any) => {
        if (previousError?.errorMessage) {
            return previousError;
        }
        return error ? { errorMessage: error } : {};
    };

    return (<>
        <div className="FCC_SlideContent-wrapper add-user-container">
            <p className="nested-heading-txt">User Details</p>
            <Row className="userdetails mb-3 mt-2 mx-0">
                <Col xs={12} sm={12} md={6} lg={4} className={'mb-3'}>
                    <Label required>Login Email</Label>
                    {/* Please add 'error-filed' className for textfield if error message is visible. */}
                    <TextField
                        name="loginEmail"
                        disabled={userId}
                        placeholder="Enter"
                        onChange={(e: any) => collectData('loginEmail', e?.target?.value, 100)}
                        value={formData?.loginEmail}
                        {...getError(checkValidationError('required&email', formData?.loginEmail))}
                    />
                </Col>
                <Col xs={12} sm={12} md={6} lg={4} className={'mb-3'}>
                    <Label required>Last Name</Label>
                    {/* Please add 'error-filed' className for textfield if error message is visible. */}
                    <TextField
                        name="lastName"
                        placeholder="Enter"
                        onChange={(e: any) => collectData('lastName', e?.target?.value, 75)}
                        value={formData?.lastName}
                        {...checkValidationError('required', formData?.lastName)}
                    />
                </Col>
                <Col xs={12} sm={12} md={6} lg={4} className={'mb-3'}>
                    <Label required>First Name</Label>
                    {/* Please add 'error-filed' className for textfield if error message is visible. */}
                    <TextField
                        name="firstName"
                        placeholder="Enter"
                        onChange={(e: any) => collectData('firstName', e?.target?.value, 75)}
                        value={formData?.firstName}
                        {...checkValidationError('required', formData?.firstName)}
                    />
                </Col>
                <Col xs={12} sm={12} md={6} lg={4} className={'mb-3'}>
                    <Label required>Role</Label>
                    <Dropdown
                        options={roleOptions}
                        onChange={(item: any) => collectData('role', item)}
                        value={formData?.role}
                        {...checkValidationError('required', formData?.role, 'dropdown')}
                    />
                </Col>
                <Col xs={12} sm={12} md={6} lg={4} className={'mb-3'}>
                    <Label>Status</Label>
                    <Dropdown
                        options={statusOptions}
                        onChange={(item: any) => collectData('status', item)}
                        value={formData?.status}
                    />
                </Col>
                <Col xs={12} sm={12} md={6} lg={4} className={'mb-3'}>
                    <Label>Employee</Label>
                    <Dropdown
                        options={employeeOptions}
                        onChange={(item: any) => collectData('employee', item)}
                        value={formData?.employee}
                    />
                </Col>
                <Col xs={12} sm={12} md={6} lg={4} className={'mb-3'}>
                    <Label>Maximum # Registered Atom Devices</Label>
                    <TextField
                        placeholder="Enter"
                        type="number"
                        onChange={(e: any) => collectData('maxRegDevices', e?.target?.value)}
                        value={formData?.maxRegDevices}
                        {...checkValidationError('maximum', formData?.maxRegDevices)}
                    />
                </Col>
            </Row>
            <p className="nested-heading-txt">User Permissions</p>
            <Row className="userdetails mt-2 mx-0">
                <Col xs={12} sm={12} md={6} lg={4} className={'mb-3'}>
                    <Label required>Company(ies)</Label>
                    <MultiSelectDropdown
                        value={formData?.companies || []}
                        onChange={(item: any) => collectData('companies', item)}
                        options={companyOptions}
                        hasSelectAll={false}
                        {...checkValidationError('required', formData?.companies, 'dropdown')}
                    />
                </Col>
                <Col xs={12} sm={12} md={6} lg={4} className={'mb-3'}>
                    <Label required>Department(s)</Label>
                    <MultiSelectDropdown
                        value={formData?.departments || []}
                        onChange={(item: any) => collectData('departments', item)}
                        options={departmentOptions}
                        hasSelectAll={false}
                        {...checkValidationError('required', formData?.departments, 'dropdown')}
                    />
                </Col>
                <Col xs={12} sm={12} md={6} lg={4} className={'mb-3'}>
                    <Label required>Yard(s)</Label>
                    <MultiSelectDropdown
                        value={formData?.yards || []}
                        onChange={(item: any) => collectData('yards', item)}
                        options={yardOptions}
                        hasSelectAll={false}
                        {...checkValidationError('required', formData?.yards, 'dropdown')}
                    />
                </Col>
            </Row>
        </div>
        <div className="FCC_slide-footer">
            <SecondaryButton text="Cancel" onClick={() => dismissPanel(false)} />
            <PrimaryButton text="Save" onClick={handleSubmit} disabled={!isFormValid} />
        </div>
    </>);
};

export default AddUser;
