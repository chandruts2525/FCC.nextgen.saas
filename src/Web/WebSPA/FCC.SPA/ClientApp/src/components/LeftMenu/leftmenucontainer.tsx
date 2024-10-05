import { useEffect, useRef, useState } from "react";
import LeftMenu from "./leftmenu";
import { useTranslation } from 'react-i18next';

import {
  CPQIcon,
  scheduleIcon,
  eticketIcon,
  invoiceIcon,
  maintenanceIcon,
  inventoryIcon,
  safetyIcon,
  biIcon,
  settingIcon,
  whatsNewIcon,
  logoutIcon,
} from '../../assets/images';
import { signoutUser } from '../UserListing/userlistingapicalls';
import { useDispatch } from 'react-redux';
import { login } from '../../redux/auth/authSlice';

const LeftMenuContainer = ({ mobile, setShowMobileMenu }: any) => {
  const { t } = useTranslation();
  const dispatch = useDispatch();

  const leftMenuList = [
    {
      id: 'cpq_menu',
      text: t('CPQtxt'),
      icon: CPQIcon,
    },
    {
      id: 'schedule_menu',
      text: t('Scheduletxt'),
      icon: scheduleIcon,
    },
    {
      id: 'eticket_menu',
      text: t('ETickettxt'),
      icon: eticketIcon,
    },
    {
      id: 'invoice_menu',
      text: t('Invoicetxt'),
      icon: invoiceIcon,
    },
    {
      id: 'maintenance_menu',
      text: t('Maintenancetxt'),
      icon: maintenanceIcon,
    },
    {
      id: 'inventory_menu',
      text: t('Inventorytxt'),
      icon: inventoryIcon,
    },
    {
      id: 'safety_menu',
      text: t('Safetytxt'),
      icon: safetyIcon,
    },
    {
      id: 'bi_menu',
      text: t('BItxt'),
      icon: biIcon,
    },
    {
      id: 'setting_menu',
      text: t('Settingtxt'),
      icon: settingIcon,
      className: 'active', // add this active class only for the current item clicked
    },
    {
      id: 'whatsnew_menu',
      text: t('WhatsNewtxt'),
      icon: whatsNewIcon,
    },
    {
      id: 'logout_menu',
      text: t('LogOut'),
      icon: logoutIcon,
    },
  ];

  const [showSettingMenu, setShowSettingMenu] = useState(false);

  const handleClickMenu = (id: any) => {
    switch (id) {
      case 'setting_menu':
        setShowSettingMenu(!showSettingMenu);
        break;
      case 'bi_menu':
        setShowSettingMenu(false);
        break;
      case 'safety_menu':
        setShowSettingMenu(false);
        break;
      case 'inventory_menu':
        setShowSettingMenu(false);
        break;
      case 'maintenance_menu':
        setShowSettingMenu(false);
        break;
      case 'invoice_menu':
        setShowSettingMenu(false);
        break;
      case 'eticket_menu':
        setShowSettingMenu(false);
        break;
      case 'schedule_menu':
        setShowSettingMenu(false);
        break;
      case 'logout_menu':
        logoutUser();
        break;
      case 'cpq_menu':
        setShowSettingMenu(false);
        break;
      default:
        break;
    }
  };

  const logoutUser = async () => {
    try {
      dispatch(login(null));
      // await signoutUser();
      const url = window.location.origin + '/api/Auth/Logout';
      window.location.replace(url);
    } catch (e) {
      console.log(e);
    }
  };

  const handleClickCloseMenuMobile = () => {
    return setShowMobileMenu(false);
  };

  const wrapperRef = useRef<any>(null);

  useEffect(() => {
    document.addEventListener("click", handleClickOutside, false);
    return () => {
      document.removeEventListener("click", handleClickOutside, false);
    };
  }, []);

  const handleClickOutside = (event: any) => {
    if (!event.target.closest('.ms-Nav.navigationBar')) {
      if (event.target.closest('#setting_menu')) {
        setShowSettingMenu(true)
      }
      else {
        setShowSettingMenu(false)
      }
    }
  }
  
  return (
    <LeftMenu
      leftMenuList={leftMenuList}
      handleClickMenu={handleClickMenu}
      showSettingMenu={showSettingMenu}
      setShowSettingMenu={setShowSettingMenu}
      mobile={mobile}
      handleClickCloseMenuMobile={handleClickCloseMenuMobile}
      wrapperRef={wrapperRef}
    />
  );
};

export default LeftMenuContainer;
