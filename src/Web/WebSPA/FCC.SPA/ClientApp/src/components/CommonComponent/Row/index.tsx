import React from 'react'
import { Row } from '@softura/fluentuipackage'

interface IProps {
  className?: string,
  children?: any,
}
const RowContainer = ({ className = '', children }:IProps) => {
  return <Row className={className}>{children}</Row>
}

export default RowContainer
