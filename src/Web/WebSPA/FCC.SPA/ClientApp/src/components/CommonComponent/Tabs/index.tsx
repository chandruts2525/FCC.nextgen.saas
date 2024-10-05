import React from 'react'
import { Tab } from '@softura/fluentuipackage'
import './tabs.scss'

interface IProps {
  className?: string,
  linkFormat?: string,
  tabItems?: any,
  selectedKey?:any,
  onLinkClick?:any
}

const Tabs = ({ linkFormat, tabItems, className, selectedKey, onLinkClick }:IProps) => {

  return <div className='pivot'>
    <Tab
      className = {`custom-tab ${className}`}
      linkFormat = {linkFormat}
      tabItems = {tabItems}
      selectedKey = {selectedKey}
      onLinkClick = {onLinkClick}
    />
  </div>
}

export default Tabs
