import React from 'react'
import { SecondaryButton } from '@softura/fluentuipackage';
import './secondarybutton.scss';

interface IProps {
  id?:any,
  iconProps?:any,
  menuIconProps?:any,
  text?: string,
  onClick?: any,
  className?: string | number,
  imgSrc?: any,
  children?: any,
  disabled?:boolean,
  title?: string,
  btnRef?:any
}

const SecondaryButtonContainer = ({
  id='',
  text = '',
  className = '',
  imgSrc = null,
  onClick,
  menuIconProps = null,
  iconProps = null,
  children = null,
  disabled=false,
  title = undefined,
  btnRef
}:IProps) => {
  return (<>
      <SecondaryButton
      id={id}
        text = {text}
        className = {`secondary-btn ${className}`}
        imgSrc = {imgSrc}
        onClick = {onClick}
        iconProps = {iconProps}
        menuIconProps = {menuIconProps}
        disabled = {disabled}
        title={title}
        componentRef={btnRef}

      >
        {children}
      </SecondaryButton>
    </>
  )
}

export default SecondaryButtonContainer
