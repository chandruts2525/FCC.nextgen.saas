import { messages } from '@progress/kendo-react-inputs/dist/npm/messages';
import { createSlice } from '@reduxjs/toolkit';
import type { PayloadAction } from '@reduxjs/toolkit';

export interface notificationState {
  show?: any;
  type?: string;
  message?: string;
  className?: string;
}

const initialState: notificationState = {
  show: false,
};

export const notificationSlice = createSlice({
  name: 'notification',
  initialState,
  reducers: {
    pushNotification: (state: any, action: PayloadAction<any>) => {
      return {
        // ...state,
        ...action.payload,
      };
    },
  },
});

// Action creators are generated for each case reducer function
export const { pushNotification } = notificationSlice.actions;

export default notificationSlice.reducer;
