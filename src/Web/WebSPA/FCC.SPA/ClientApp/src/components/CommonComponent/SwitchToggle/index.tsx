import React from "react";
import { SwitchToggle } from "@softura/fluentuipackage";
import "./switchtoggle.scss";
import Theme from '../../../utils/theme'

interface IProps {
  label?: string;
  defaultChecked?: boolean;
  onChange?: any;
  onText?: string;
  offText?: string;
  className?: string;
  checked?: boolean;
  name?: string;
  disabled?: any;
}

const SwitchToggleContainer = ({
  label,
  defaultChecked,
  onText,
  offText,
  onChange,
  className,
  checked,
  name,
  disabled
}: IProps) => {
  return (
    <SwitchToggle
      label={label}
      defaultChecked={defaultChecked}
      onChange={onChange}
      onText={onText}
      offText={offText}
      className={className}
      checked={checked}
      name = {name}
      disabled = {disabled}
      styles={Theme as React.CSSProperties}
    />
  );
};

export default SwitchToggleContainer;
