import React from 'react';
import { Searchbox } from '@softura/fluentuipackage';
import './searchbox.scss';

interface IProps {
  placeholder?: string,
  className?: string,
  onChange?: any,
  onSearch?: any,
  icon?: any,
  disabled?: boolean,
  underlined?: boolean,
  onBlur?:any,
  value?:string,
  onClear?:Function,
  componentRef?:any
}
const Search = ({
  placeholder,
  onChange,
  onSearch,
  icon,
  disabled,
  underlined,
  className,
  onBlur,
  value,
  onClear,
  componentRef
}:IProps) => {
  const cssStyles = {
    marginTop: 20,
    marginBottom: 0,
    'input::placeholder': {
      color: "black",
      fontSize: 12,
      fontFamily: 'Arial, san-serif',
      opacity: 0.5
    },
    '.ms-SearchBox-clearButton': {
      marginRight: '20px'
    }
  }
  return <div className='searchbox'>
    <Searchbox
    className = {`${cssStyles} ${className}`}
    placeholder = {placeholder}
    onChange = {onChange}
    onSearch = {onSearch}
    onBlur = {onBlur}
    icon = {icon}
    disabled = {disabled}
    underlined = {underlined}
    value={value}
    onClear={onClear}
    componentRef={componentRef}
  />
 </div>
}

export default Search
