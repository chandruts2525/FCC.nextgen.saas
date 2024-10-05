import React, { useCallback, useEffect } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { pushNotification } from '../../../redux/notification/notificationSlice';
import { Message } from '@softura/fluentuipackage';
import './message.scss';

function NotificationContainer() {
  const dispatch = useDispatch();
  const { show, type, message, className } = useSelector(
    (state: any) => state && state.notification
  );

  const clearDate = () => {
    if (show) {
      setTimeout(() => {
        dispatch(
          pushNotification({
            show: false,
          })
        );
      }, 5000);
    }
  };

  const stableHandler = useCallback(clearDate, [show, dispatch]);

  useEffect(() => {
    stableHandler();
  }, [stableHandler]);

  return (
    <>
      {show &&
        type === 'success' &&
        Array.isArray(message) &&
        message.length > 0 &&
        message.map((element) => (
          <Message
            text={element}
            type={type}
            truncated={false}
            isMultiline={false}
            show={show}
            onDismiss={clearDate}
            className={`alert-msg ${className}`}
          />
        ))}
      {show && type === 'success' && !Array.isArray(message) && (
        <Message
          text={message}
          type={type}
          truncated={false}
          isMultiline={false}
          show={show}
          onDismiss={clearDate}
          className={`alert-msg ${className}`}
        />
      )}
      {show && type === 'error' && (
        <Message
          text={message}
          type={type}
          truncated={false}
          isMultiline={false}
          show={show}
          onDismiss={clearDate}
          className={`alert-msg ${className}`}
        />
      )}
    </>
  );
}

export default NotificationContainer;
