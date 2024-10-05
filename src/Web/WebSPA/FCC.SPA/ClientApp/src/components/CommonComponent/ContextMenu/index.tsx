import React from 'react'
import { ContextMenu } from '@softura/fluentuipackage'
import './contextmenu.scss'

const ContextMenuContainer = ({ref,onClick,href,className,items,hidden,target,onItemClick,onDismiss,children,contextClassName}:any) => {
  return (<>
      <td ref={ref} onClick={onClick} className={contextClassName}>
          {children}
      </td>
    <ContextMenu
      className = {`${className}`}
      items={items}
      hidden={!hidden}
      target={target}
      onItemClick={onItemClick}
      onDismiss={onDismiss}
    />
  </>)
}

export default ContextMenuContainer
