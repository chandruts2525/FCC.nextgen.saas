import React from 'react'
import { PrimaryButton } from '@softura/fluentuipackage';
import './primarybutton.scss';

interface IProps {
  iconProps?:any,
  menuIconProps?:any,
  text?: string,
  onClick?: any,
  className?: string | number,
  imgSrc?: any,
  children?: any,
  disabled?:any,
  id?:any,
  title?: string,
  btnRef?:any
}
const PrimaryButtonContainer = ({
  text = '',
  className = '',
  imgSrc = null,
  onClick,
  menuIconProps = null,
  iconProps = null,
  children = null,
  disabled = false,
  id,
  title = undefined,
  btnRef
}:IProps) => {
  return (<>
     <PrimaryButton
        text = {text}
        className = {`primary-btn ${className}`}
        imgSrc = {imgSrc}
        onClick = {onClick}
        iconProps = {iconProps}
        menuIconProps = {menuIconProps}
        disabled={disabled}
        id={id}
        title={title}
        componentRef={btnRef}
      >
        {children}
      </PrimaryButton>
    </>
  )
}

export default PrimaryButtonContainer
