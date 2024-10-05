import React from 'react'
import { CustomDatePicker } from '@softura/fluentuipackage'
import './datepicker.scss'

interface IProps {
  label?: string,
  minDate?: any,
  maxDate?: any,
  className?: string,
  placeholder?: string,
  allowTextInput?: boolean,
  showMonthPickerAsOverlay?: boolean,
  isMonthPickerVisible?: boolean,
  isRequired?: boolean,
  disabled?: boolean,
  value?: any,
  onChangeDate?: any
}

const DatepickerContainer = ({
  label,
  minDate,
  maxDate,
  className,
  placeholder,
  allowTextInput,
  showMonthPickerAsOverlay,
  isMonthPickerVisible,
  isRequired,
  disabled,
  value,
  onChangeDate
}:IProps) => {
  return <CustomDatePicker
  className = {`datepicker ${className}`}
  label = {label}
  minDate = {minDate}
  maxDate = {maxDate}
  placeholder = {placeholder}
  allowTextInput = {allowTextInput}
  showMonthPickerAsOverlay = {showMonthPickerAsOverlay}
  isMonthPickerVisible = {isMonthPickerVisible}
  isRequired = {isRequired}
  disabled = {disabled}
  value = {value}
  onSelectDate = {onChangeDate}
  />
}

export default DatepickerContainer
