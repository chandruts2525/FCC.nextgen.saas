import { checkValidationError } from "../../../../utils/Validators";
import { Col, Dropdown, Label, Row } from "../../../CommonComponent";

interface IProps {
    measurementTypeOptions: any,
    currencyOptions: any,
    languageOptions: any,
    timeZoneOptions: any,
    dateFormatOptions: any,
    timeFormatOptions: any;
    collectData: any,
    formData: any,
}

const Localization = ({ measurementTypeOptions, currencyOptions, languageOptions, timeZoneOptions,
    dateFormatOptions, timeFormatOptions, collectData, formData }: IProps) => {
    return (
        <Row>
            <Col xs={12} sm={12} md={4} lg={4} className="mb-3">
                <Label required>Language</Label>
                <Dropdown options={languageOptions}
                    onChange={(e: any) => collectData('companyLocalizationVM', 'language', e.value)}
                    {...(checkValidationError('required', formData?.companyLocalizationVM?.language, "dropdown", "Language is Required"))}
                />
            </Col>
            <Col xs={12} sm={12} md={4} lg={4} className="mb-3">
                <Label required>Currency</Label>
                <Dropdown options={currencyOptions}
                    onChange={(e: any) => collectData('companyLocalizationVM', 'currency', e.value)}
                    {...(checkValidationError('required', formData?.companyLocalizationVM?.currency, "dropdown", "Currency is Required"))}
                />
            </Col>
            <Col xs={12} sm={12} md={4} lg={4} className="mb-3">
                <Label>Time Zone</Label>
                <Dropdown options={timeZoneOptions} onChange={(e: any) => collectData('companyLocalizationVM', 'timeZone', e.value)} />
            </Col>
            <Col xs={12} sm={12} md={4} lg={4} className="mb-3">
                <Label>Date Format</Label>
                <Dropdown options={dateFormatOptions} onChange={(e: any) => collectData('companyLocalizationVM', 'dateFormat', e.value)} />
            </Col>
            <Col xs={12} sm={12} md={4} lg={4} className="mb-3">
                <Label>Time Format</Label>
                <Dropdown options={timeFormatOptions} onChange={(e: any) => collectData('companyLocalizationVM', 'timeFormat', e.value)} />
            </Col>
            <Col xs={12} sm={12} md={4} lg={4} className="mb-3">
                <Label required>Measurement Type</Label>
                <Dropdown options={measurementTypeOptions}
                    onChange={(e: any) => collectData('companyLocalizationVM', 'measurementTypeId', e.value)}
                    {...(checkValidationError('required', formData?.companyLocalizationVM?.measurementTypeId, "dropdown", "Measurement Type is Required"))}
                />
            </Col>
        </Row>
    );
};

export default Localization;