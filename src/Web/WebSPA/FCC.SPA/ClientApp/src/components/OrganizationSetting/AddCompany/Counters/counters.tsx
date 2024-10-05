import { Col, Label, Row, TextField } from '../../../CommonComponent';

interface IProps {
  collectData: any;
  formData: any;
}

const Counters = ({ collectData, formData }: IProps) => {
  return (
    <Row>
      <Col xs={12} sm={12} md={4} lg={4} className="mb-3">
        <Label>Quote Number</Label>
        <TextField
          placeholder="Enter Quote Number"
          type="number"
          value={formData?.countersVM?.quoteNumber}
          onChange={(e: any) => collectData('countersVM', 'quoteNumber', e.target.value)}
        />
      </Col>
      <Col xs={12} sm={12} md={4} lg={4} className="mb-3">
        <Label>Job Number</Label>
        <TextField
          placeholder="Enter Job Number"
          type="number"
          value={formData?.countersVM?.jobNumber}
          onChange={(e: any) => collectData('countersVM', 'jobNumber', e.target.value)}
        />
      </Col>
      <Col xs={12} sm={12} md={4} lg={4} className="mb-3">
        <Label>PO Request Number</Label>
        <TextField
          placeholder="Enter PO Request Number"
          type="number"
          value={formData?.countersVM?.poRequestNumber}
          onChange={(e: any) =>
            collectData('countersVM', 'poRequestNumber', e.target.value)
          }
        />
      </Col>
      <Col xs={12} sm={12} md={4} lg={4} className="mb-3">
        <Label>PO Number</Label>
        <TextField
          placeholder="Enter PO Number"
          type="number"
          value={formData?.countersVM?.poNumber}
          onChange={(e: any) => collectData('countersVM', 'poNumber', e.target.value)}
        />
      </Col>
      <Col xs={12} sm={12} md={4} lg={4} className="mb-3">
        <Label>Receiver Number</Label>
        <TextField
          placeholder="Enter Receiver Number"
          type="number"
          value={formData?.countersVM?.receiverNumber}
          onChange={(e: any) =>
            collectData('countersVM', 'receiverNumber', e.target.value)
          }
        />
      </Col>
      <Col xs={12} sm={12} md={4} lg={4} className="mb-3">
        <Label>Invoice Number</Label>
        <TextField
          placeholder="Enter Invoice Number"
          type="number"
          value={formData?.countersVM?.invoiceNumber}
          onChange={(e: any) =>
            collectData('countersVM', 'invoiceNumber', e.target.value)
          }
        />
      </Col>
      <Col xs={12} sm={12} md={4} lg={4} className="mb-3">
        <Label>Order Number</Label>
        <TextField
          placeholder="Enter Order Number"
          type="number"
          value={formData?.countersVM?.orderNumber}
          onChange={(e: any) => collectData('countersVM', 'orderNumber', e.target.value)}
        />
      </Col>
      <Col xs={12} sm={12} md={4} lg={4} className="mb-3">
        <Label>Recurring Billing Number</Label>
        <TextField
          placeholder="Enter Recurring Billing Number"
          type="number"
          value={formData?.countersVM?.recurringBillingNumber}
          onChange={(e: any) =>
            collectData('countersVM', 'recurringBillingNumber', e.target.value)
          }
        />
      </Col>
      <Col xs={12} sm={12} md={4} lg={4} className="mb-3">
        <Label>Bill Number</Label>
        <TextField
          placeholder="Enter Bill Number"
          type="number"
          value={formData?.countersVM?.billingNumber}
          onChange={(e: any) =>
            collectData('countersVM', 'billingNumber', e.target.value)
          }
        />
      </Col>
      <Col xs={12} sm={12} md={4} lg={4} className="mb-3">
        <Label>Shipping Number</Label>
        <TextField
          placeholder="Enter Shipping Number"
          type="number"
          value={formData?.countersVM?.shippingNumber}
          onChange={(e: any) =>
            collectData('countersVM', 'shippingNumber', e.target.value)
          }
        />
      </Col>
      <Col xs={12} sm={12} md={4} lg={4} className="mb-3">
        <Label>WO Number</Label>
        <TextField
          placeholder="Enter WO Number"
          type="number"
          value={formData?.countersVM?.woNumber}
          onChange={(e: any) => collectData('countersVM', 'woNumber', e.target.value)}
        />
      </Col>
    </Row>
  );
};

export default Counters;
