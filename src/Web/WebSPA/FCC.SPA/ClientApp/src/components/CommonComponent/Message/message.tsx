import React, { useEffect, useState } from 'react';
import { Message } from '@softura/fluentuipackage'
import "./message.scss"

interface IProps {
    text?: any,
    type?: string,
    className?: any
    dismissAlert?: any,
    show?: boolean,
    setToast?: any
}

const MessageAlert = ({ text, type, className, dismissAlert, setToast, show }: IProps) => {

    useEffect(() => {
        let timer: any = null;
        if (show) {
            timer = setTimeout(() => {
                dismissSuccess();
            }, 5000)
        }
        return () => {
            if (timer) {
                clearTimeout(timer)
            }
        };
    }, [show]);

    const dismissSuccess = () => {
        setToast({
            show: false,
            message: "",
            type: ""
        })
    }

    return (
        <>
            <Message
                text={text}
                type={type}
                truncated={false}
                isMultiline={false}
                show={show}
                onDismiss={dismissSuccess}
                className={`alert-msg ${className}`}
            />
        </>
    )
}

export default MessageAlert;

