import React from 'react'
import { ReactSelect } from '@softura/customcomponent'
import './dropdown.scss'

interface IProps {
  placeholder?: string,
  options?: any,
  onChange?: any,
  name?: any,
  value?: any,
  filterOptionValue?: any;
  className?: any;
  isSearchable?: boolean;
  defaultValue?: any,
  menuIsOpen?: any
  selectRef?: any,
  autoFocus?: boolean,
  isDisabled?: boolean,
  filterOption?: Function,
  blurInputOnSelect?: any,
  inputmode?: any,
  inputValue?: any,
  onInputChange?: any,
  onBlur?: any,
  menuShouldScrollIntoView?: any
  isLoading?: any,
  noOptionsMessage?: any,
  isClearable?: any,
  isMulti?: any,
  errorMessage?: any,
  isMultiSelectCheckbox?:any,
  components?:any
}

const SearchSelectBox = ({
  placeholder = 'Select',
  options,
  onChange,
  name,
  value,
  filterOptionValue = 1,
  className = "",
  isSearchable = false,
  defaultValue,
  selectRef,
  autoFocus,
  menuIsOpen,
  isDisabled,
  filterOption,
  blurInputOnSelect,
  inputmode,
  inputValue,
  onInputChange,
  onBlur,
  menuShouldScrollIntoView,
  isLoading,
  noOptionsMessage,
  isClearable,
  isMulti,
  errorMessage,
  isMultiSelectCheckbox,
  components
}: IProps) => {

  const colourStyles: any = {
    control: (baseStyles: any, state: any) => ({
      ...baseStyles,
      borderColor: state.isFocused ? 'grey !important' : '#B7DEFF !important',
    }),

    option: (styles: any, { data, isDisabled, isFocused, isSelected }: any) => {
      return {
        ...styles,
        backgroundColor: isSelected
          ? '#8BC400'
          : '',
        borderBottom: '1px solid #8BC400'
      }
    }
  }

  return (
    <>
      <ReactSelect
        placeholder={placeholder}
        options={options}
        filterOptionValue={filterOptionValue}
        onChange={onChange}
        value={value}
        name={name}
        styles={colourStyles}
        className={`search-dropdown ${errorMessage ? 'error-filed' : className}`}
        isSearchable={isSearchable}
        defaultValue={defaultValue}
        selectRef={selectRef}
        menuIsOpen={menuIsOpen}
        isDisabled={isDisabled}
        filterOption={filterOption}
        blurInputOnSelect={blurInputOnSelect}
        inputmode={inputmode}
        inputValue={inputValue}
        onInputChange={onInputChange}
        onBlur={onBlur}
        menuShouldScrollIntoView={menuShouldScrollIntoView}
        isLoading={isLoading}
        noOptionsMessage={noOptionsMessage}
        isClearable={isClearable}
        isMulti={isMulti}
        isMultiSelectCheckbox={isMultiSelectCheckbox}
        components={components}
      />
      {errorMessage && <span className="ms-TextField-errorMessage">{errorMessage}</span>}
    </>
  )
}

export default SearchSelectBox;
