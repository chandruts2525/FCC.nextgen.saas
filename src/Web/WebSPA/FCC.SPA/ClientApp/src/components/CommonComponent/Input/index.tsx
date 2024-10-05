import React from "react";
import { TextFieldComponent } from "@softura/fluentuipackage";
import "./input.scss";
import Theme from "../../../utils/theme"

interface IProps {
  label?: string;
  placeholder?: string;
  required?: boolean;
  onChange?: any;
  disabled?: boolean;
  readOnly?: boolean;
  icon?: any;
  type?: string;
  canRevealPassword?: boolean;
  errorMessage?: string;
  multiline?: boolean;
  row?: string | number;
  className?: string | number;
  resizable?: boolean;
  value?: any;
  name?: any;
  onBlur?: any;
  validateOnLoad?: any;
  onRenderInput?: any;
  autoComplete?: string;
  prefix?: any;
  onFocus?: any;
  onKeyPress?: any;
  inputRef?: any,
  onKeyUp?: any,
  onKeyDown?: any,
  onmousedown?: any,
  ontouchend?: any,
  suffix?: any,
  defaultValue?: any,
  maxLength?: any,
  onPaste?: any
}
const InputContainer = ({
  label,
  placeholder,
  required,
  onChange,
  disabled,
  readOnly,
  icon,
  type,
  canRevealPassword,
  errorMessage,
  multiline,
  row,
  className,
  resizable,
  value,
  name,
  onBlur,
  validateOnLoad,
  onRenderInput,
  autoComplete,
  prefix,
  onFocus,
  onKeyPress,
  inputRef,
  onKeyUp,
  onKeyDown,
  onmousedown,
  ontouchend,
  suffix,
  defaultValue,
  maxLength = 200,
  onPaste
}: IProps) => {
  return (
    <TextFieldComponent
      defaultValue={defaultValue}
      className={`form-input ${errorMessage ? 'error-filed' : className}`}
      label={label}
      onChange={onChange}
      required={required}
      placeholder={placeholder}
      canRevealPassword={canRevealPassword}
      errorMessage={errorMessage}
      disabled={disabled}
      readOnly={readOnly}
      icon={icon}
      type={type}
      multiline={multiline}
      row={row}
      resizable={resizable}
      value={value}
      name={name}
      onBlur={onBlur}
      onFocus={onFocus}
      validateOnLoad={validateOnLoad}
      onRenderInput={onRenderInput}
      autoComplete={autoComplete}
      prefix={prefix}
      onKeyPress={onKeyPress}
      inputRef={inputRef}
      onKeyUp={onKeyUp}
      onKeyDown={onKeyDown}
      onmousedown={onmousedown}
      ontouchend={ontouchend}
      suffix={suffix}
      maxLength={maxLength}   
      onPaste={onPaste}   
      style={Theme as React.CSSProperties}
    />
  );
};

export default InputContainer;
