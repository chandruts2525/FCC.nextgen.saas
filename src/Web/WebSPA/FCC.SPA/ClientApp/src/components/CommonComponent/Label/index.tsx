import React from 'react'
import './label.scss'

interface IProps {
  className?: string,
  children?: any,
  required?: boolean,
  id?:any,
  title?:any
}
const Label = ({ children = null, required = false , id ,title,className}:IProps) => {
  return <div className={`label ${className}`}  id={id} title={title}>
      {children}
      <>
      {
          required &&
          <span className='required'>*</span>
      }
      </>
  </div>
}

export default Label
