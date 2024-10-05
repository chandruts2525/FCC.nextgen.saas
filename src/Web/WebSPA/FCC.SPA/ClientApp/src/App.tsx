import React, { useEffect, useState } from 'react';
import { useSelector, useDispatch } from 'react-redux';
import { useNavigate } from 'react-router-dom';
import './App.scss';
import Footer from './components/Footer';
import RoutesPages from './routes';
import LeftMenu from './components/LeftMenu';
import logo from './assets/images/NexGen.svg';
import Theme from './utils/theme';
import {
  getUserDetails,
  signoutUser,
} from './components/UserListing/userlistingapicalls';
import { login } from './redux/auth/authSlice';
import NotificationContainer from './components/CommonComponent/Message/notificationContainer';
// import { useTranslation } from 'react-i18next';

const App = () => {
  const dispatch = useDispatch();
  const navigate = useNavigate();
  // const { t } = useTranslation();
  const currentUser = useSelector((state: any) => state.auth);
  const [mobile, setMobile] = useState(false);
  const [showMobileMenu, setShowMobileMenu] = useState(false);

  useEffect(() => {
    getUserDetail();
  }, []);

  const getSessionData = () => {
    return sessionStorage.getItem('navigationPath');
  };

  const clearSessionData = () => {
    sessionStorage.removeItem('navigationPath');
  };

  const navigationPath = getSessionData();
  useEffect(() => {
    if (navigationPath) {
      navigate(navigationPath);
      clearSessionData();
    }
  }, []);

  // useEffect(() => {
  //   if (currentUser && currentUser?.userDetails?.error?.status === 401) {
  //     // logoutUser();
  //     const url = window.location.origin;
  //     window.location.replace(url);
  //   }
  // }, [currentUser]);

  // const logoutUser = async () => {
  //   // await signoutUser();
  //   const url = window.location.origin + '/api/Auth/Logout';
  //   window.location.replace(url);
  // };

  const getUserDetail = async () => {
    const response: any = await getUserDetails();
    dispatch(login(response.data));
  };

  useEffect(() => {
    const resizeFun = () => {
      if (window.innerWidth < 768) {
        setMobile(true);
      } else {
        setMobile(false);
        setShowMobileMenu(true);
      }
    };
    resizeFun();
    window.addEventListener('resize', resizeFun);
    return () => window.removeEventListener('resize', resizeFun);
  }, []);

  const handleClickMenuMobile = () => {
    return setShowMobileMenu(true);
  };

  return (
    <div style={Theme as React.CSSProperties}>
      <div className={`${mobile ? 'show' : 'hide'}`}>
        <div className={`mobile-menu-block`}>
          <div onClick={handleClickMenuMobile} className="hamburger-icon">
            <span className="fa fa-bars"></span>
          </div>
          <div className="logo-block-mobile">
            <img src={logo} alt="Logo" />
            <span>
              <b>Next Gen</b>
            </span>
          </div>
          <div className="profile-block-mobile">
            <p>MB</p>
          </div>
        </div>
      </div>
      <div className="fcc-container">
        <div className={`left-panel ${showMobileMenu ? 'show' : 'hide'}`}>
          <LeftMenu mobile={mobile} setShowMobileMenu={setShowMobileMenu} />
        </div>
        <div className="right-panel">
          <div className="wrapperHDH">
            <NotificationContainer />
            <RoutesPages />
            <Footer />
          </div>
        </div>
      </div>
    </div>
  );
};

export default App;
