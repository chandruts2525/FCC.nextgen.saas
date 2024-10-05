import React from 'react'
import { Breadcrumb } from '@softura/fluentuipackage'
import './breadcrumb.scss'

interface IProps {
  items?: any,
  maxDisplayedItems?: any,
  className?: any
}

const BreadcrumContainer = ({ items, maxDisplayedItems = 5, className = '' }:IProps) => {
  return (
    <Breadcrumb
     className = {`breadcrumb ${className}`}
     items = {items}
     maxDisplayedItems = {maxDisplayedItems}
    />
  )
}

export default BreadcrumContainer
