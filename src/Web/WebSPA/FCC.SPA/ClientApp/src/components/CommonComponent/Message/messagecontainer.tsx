import React from 'react'
import MessageAlertcomp from './message';

const MessageContainer = (props: any) => {
  return (
    <div>
      <MessageAlertcomp {...props} />
    </div>
  )
}

export default MessageContainer