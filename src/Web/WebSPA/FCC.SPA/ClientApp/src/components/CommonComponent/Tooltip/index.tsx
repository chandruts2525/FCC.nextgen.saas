import React from "react";
import { Tooltip } from "@softura/fluentuipackage";
import './index.scss'
interface IProps {
    content?:string,
    id?:any,
    tooltipProps?:any,
    children?:any,
    className?:any,
    onClick?:any,
    maxWidth?:any
}
const TooltipComponent = (props:IProps) => {

    return <Tooltip  
    content={props?.content}
    id={props?.id}
    tooltipProps={props?.tooltipProps}
    onClick ={props?.onClick}
    maxWidth={props?.maxWidth}
    className = {props.className}>
        {props.children}
    </Tooltip>
}

export default TooltipComponent