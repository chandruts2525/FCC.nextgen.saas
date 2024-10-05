import React from 'react'
import { CustomCard } from '@softura/fluentuipackage'
import './card.scss'

interface IProps {
  content?: any,
  children?: any,
  className?: string,
  title?: string | number,
  DocumentCardActivityPeople?: any,
  shouldTruncate?: any,
}
const CardContainer = ({
  children = null,
  className,
  title = '',
  DocumentCardActivityPeople = '',
  shouldTruncate = ''
}:IProps) => {
  return (
    <CustomCard
    content={children}
    className = {className}
    title = {title}
    DocumentCardActivityPeople = {DocumentCardActivityPeople}
    shouldTruncate = {shouldTruncate}
    />
  )
}

export default CardContainer
