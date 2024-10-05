import React from 'react'
import { Col } from '@softura/fluentuipackage'
import './col.scss'

interface IProps {
  className?: any,
  children?: any,
  xs?: any,
  md?: any,
  sm?: any,
  xxl?: any,
  lg?: any,
  xl?: any,
}
const ColContainer = ({ xxl, xl, lg, md, sm, xs, className = '', children }:IProps) => {
  return (
    <Col xs={xs} sm={sm} md={md} lg={lg} xl={xl} xxl={xxl} className={className}>
      {children}
    </Col>
  )
}

export default ColContainer
