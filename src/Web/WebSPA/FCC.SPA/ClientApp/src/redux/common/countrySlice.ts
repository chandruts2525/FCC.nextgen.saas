import { createSlice } from '@reduxjs/toolkit';
import type { PayloadAction } from '@reduxjs/toolkit';

export interface countryState {
  countryOptions?: any;
}

const initialState: countryState = {
  countryOptions: [],
};

export const countrySlice = createSlice({
  name: 'country',
  initialState,
  reducers: {
    setCountryOptions: (state: any, action: PayloadAction<any>) => {
      return {
        ...state,
        countryOptions: action.payload,
      };
    },
  },
});

// Action creators are generated for each case reducer function
export const { setCountryOptions } = countrySlice.actions;

export default countrySlice.reducer;
