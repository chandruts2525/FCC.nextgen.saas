import { login } from '../redux/auth/authSlice';
import store from '../redux/store/confiStore';
// import { useNavigate } from 'react-router-dom';

export default function handleError(error, { errorToast = true } = {}) {
  // let navigate = useNavigate();
  const code = (error && error.response && error.response.status) || 404;
  const message = error?.response?.data?.error || error?.message || ERROR_PLACEHOLDER;
  const err = {
    message: typeof message === 'object' ? JSON.stringify(message) : message,
    code,
  };
  const isUnauthorized =
    error?.response?.status === 401 && !error?.config?.url?.includes('login');

  if (error?.response?.status === 401) {
    const navigationPath = window.location.pathname;
    sessionStorage.setItem('navigationPath', navigationPath);
    const url = window.location.origin;
    window.location.replace(url);
    // navigate('/');
    // store.dispatch(login({ error: { status: 401 } }));
  }
  console.log(err.message);

  throw err;
}

export const ERROR_PLACEHOLDER = 'Oops! Something went wrong...';
