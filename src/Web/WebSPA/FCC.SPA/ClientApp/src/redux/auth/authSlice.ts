import { createSlice } from "@reduxjs/toolkit";
import type { PayloadAction } from "@reduxjs/toolkit";

export interface AuthState {
  userDetails?: any;
}

const initialState: AuthState = {
  userDetails: {},
};

export const authSlice = createSlice({
  name: "auth",
  initialState,
  reducers: {
    login: (state: any, action: PayloadAction<any>) => {
      return {
        ...state,
        userDetails: action.payload,
      };
    },
    logout: (state: any, action: PayloadAction<any>) => ({
      ...state,
      userDetails: {},
    }),
  },
});

// Action creators are generated for each case reducer function
export const { login, logout } = authSlice.actions;

export default authSlice.reducer;
