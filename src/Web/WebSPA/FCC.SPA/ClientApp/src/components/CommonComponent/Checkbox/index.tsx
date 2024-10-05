import React from 'react'
import { Checkbox } from '@softura/fluentuipackage'
import './checkbox.scss'

interface IProps {
  label?: string,
  className?: string|number,
  onChange?: any,
  defaultChecked?: boolean,
  disabled?: boolean,
  key?: any,
  checked?:any,
  title?:any
}
const CheckboxContainer = ({
  label = '',
  className = '',
  onChange,
  defaultChecked = false,
  disabled = false,
  key,
  checked,
  title
}:IProps) => {
  return <Checkbox
  className={`form-checkbox ${className}`}
  label = {label}
  onChange = {onChange}
  defaultChecked = {defaultChecked}
  disabled = {disabled}
  key={key}
  checked={checked}
  title={title}
  />
}

export default CheckboxContainer
