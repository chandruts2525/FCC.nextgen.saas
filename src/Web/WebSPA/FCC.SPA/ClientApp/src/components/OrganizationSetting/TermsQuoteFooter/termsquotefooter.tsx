import { Col, Dropdown, Label, Row } from '../../CommonComponent';

interface IProps {
  quoteFooterOptions: any;
  termsConditionsOptions: any;
  collectData: any;
  formData: any;
}

const QuoteFooter = ({
  quoteFooterOptions,
  termsConditionsOptions,
  collectData,
  formData,
}: IProps) => {
  return (
    <Row>
      <Col xs={12} sm={12} md={4} lg={4} className="mb-1">
        <Label required>Quote Terms & Conditions</Label>
        <Dropdown
          options={termsConditionsOptions}
          isSearchable
          value={formData?.yardDetailsVM?.termAndCondition}
          onChange={(e: any) => collectData('termAndCondition', e)}
        />
      </Col>
      <Col xs={12} sm={12} md={4} lg={4} className="mb-1">
        <Label required>Quote Footer</Label>
        <Dropdown
          isSearchable
          options={quoteFooterOptions}
          value={formData?.yardDetailsVM?.quoteFooter}
          onChange={(e: any) => collectData('quoteFooter', e)}
        />
      </Col>
    </Row>
  );
};

export default QuoteFooter;
