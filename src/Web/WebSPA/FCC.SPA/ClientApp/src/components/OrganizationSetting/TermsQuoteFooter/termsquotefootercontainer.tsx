import QuoteFooter from './termsquotefooter';

interface IProps {
  collectData: any;
  formData: any;
  quoteFooterOptions: any;
  quoteTermConditionOptions: any;
}

const QuoteFooterContainer = ({
  collectData,
  formData,
  quoteFooterOptions,
  quoteTermConditionOptions,
}: IProps) => {
  return (
    <QuoteFooter
      quoteFooterOptions={quoteFooterOptions}
      termsConditionsOptions={quoteTermConditionOptions}
      collectData={collectData}
      formData={formData}
    />
  );
};

export default QuoteFooterContainer;
