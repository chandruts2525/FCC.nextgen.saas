import { useSelector } from 'react-redux';
import Address from './address';

const AddressContainer = ({
  collectData,
  required,
  formData,
  stateOptions,
  sameAsAddress,
}: any) => {
  const countryOptions = useSelector((state: any) => state?.country?.countryOptions);

  return (
    <Address
      sameAsAddress={sameAsAddress}
      countryOptions={countryOptions}
      stateOptions={stateOptions}
      collectData={collectData}
      required={required}
      formData={formData}
    />
  );
};

export default AddressContainer;
