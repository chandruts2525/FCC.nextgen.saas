import { PhoneInput } from 'react-international-phone';
import { PhoneNumberUtil } from 'google-libphonenumber';
import 'react-international-phone/style.css';
import './index.scss';

const PhoneInputBox = ({
  defaultCountry,
  value,
  onChange,
  placeholder,
  errorMessage,
  disabled
}: any) => {
  const phoneUtil = PhoneNumberUtil.getInstance();

  const isPhoneValid = (phone: string) => {
    try {
      if (phone !== undefined) {
        return phoneUtil.isValidNumber(phoneUtil.parseAndKeepRawInput(phone));
      }
      return true;
    } catch (error) {
      return false;
    }
  };

  return (
    <>
      <PhoneInput
        defaultCountry={defaultCountry}
        value={value}
        onChange={onChange}
        placeholder={placeholder}
        className={errorMessage ? 'error-filed' : ''}
        disabled={disabled}
      />
      {errorMessage && <div style={{ color: 'red' }}>{errorMessage}</div>}
      {/* {(!isPhoneValid(value) && !errorMessage) && <div style={{ color: 'red' }}>Phone Number is not valid</div>} */}
    </>
  );
};

export default PhoneInputBox;
