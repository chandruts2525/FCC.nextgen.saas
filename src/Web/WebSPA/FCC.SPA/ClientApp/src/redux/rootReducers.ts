import { combineReducers } from 'redux';
import authReducer from './auth/authSlice';
import countryReducer from './common/countrySlice';
import notificationReducer from './notification/notificationSlice';

const rootReducers = combineReducers({
  auth: authReducer,
  country: countryReducer,
  notification: notificationReducer,
});

export default rootReducers;
