import OrganizationSetting from './organizationsetting';
import { dashboardIcon } from '../../assets/images';
import { useEffect, useState } from 'react';
import { callOrganizationApi } from './organizationsettingapicalls';
import Constants from '../../utils/Constants';
import { Loader, ModalPopup } from '../CommonComponent';
import MessageContainer from '../CommonComponent/Message/messagecontainer';
import { useSelector, useDispatch } from 'react-redux';
import { setCountryOptions } from '../../redux/common/countrySlice';

const OrganizationSettingContainer = () => {
  const [select, setSelect] = useState(['']);
  const [isOpen, setIsOpen] = useState({ visible: false, data: {} });
  const [isOpenRegion, setIsOpenRegion] = useState({ visible: false, data: {} });
  const [isOpenYard, setIsOpenYard] = useState({ visible: false, data: {} });
  const [isOpenDepartment, setIsOpenDepartment] = useState({ visible: false, data: {} });
  const [isOpenGroup, setIsOpenGroup] = useState({ visible: false, data: {} });
  const [isOpenPreview, setIsOpenPreview] = useState({ visible: false, data: {} });
  const [formData, setFormData] = useState<any>();
  const [componentLoader, setComponentLoader] = useState<boolean>(false);
  const [action, setAction] = useState('');
  const [confirmationPopup, setConfirmationPopup] = useState<boolean>(false);
  const [isTextboxVisible, setIsTextboxVisible] = useState<boolean>(false);
  const [toast, setToast] = useState({
    show: false,
    message: '',
    type: '',
  });
  const createToast = (type: string, message: string) => {
    setToast({
      message,
      type,
      show: true,
    });
  };
  const breadcrumlist = [
    {
      text: (
        <p>
          <img src={dashboardIcon} alt="Home" /> Settings
        </p>
      ),
      key: 'f1',
      href: '/',
      onClick: (e: any) => {
        e.preventDefault();
      },
    },
    {
      text: 'Organization Settings',
      key: 'f2',
      href: '/',
      onClick: (e: any) => {
        e.preventDefault();
      },
    },
  ];

  const dispatch = useDispatch();
  const countryOptions = useSelector((state: any) => state?.country?.countryOptions);
  const fetchCountryOptions = async () => {
    try {
      const payload = {
        endpoint: '/api/Core/GetCountry',
        apiType: Constants.OrganizationManagement,
        apiMethod: 'Get',
      };

      const res: any = await callOrganizationApi(payload);

      if (res.status === 200) {
        const options: any = [];
        res?.data?.forEach((el: any) => {
          options.push({ value: el?.countryCode, label: el?.countryName });
        });
        dispatch(setCountryOptions(options));
      }
    } catch (e) {
      console.log('error', 'Something went wrong to get Countries.');
    }
  };

  useEffect(() => {
    console.log('countryOptions : ', countryOptions);
    if (countryOptions.length === 0) {
      fetchCountryOptions();
    }
  }, [countryOptions]);

  const collectData = (name: string, val: any) => {
    setFormData({
      ...formData,
      [name]: val,
    });
  };

  const fetchOrganizationDetails = async () => {
    try {
      setComponentLoader(true);

      const payload = {
        endpoint: '/api/v1/Organization',
        apiType: Constants.OrganizationManagement,
        apiMethod: 'Get',
      };

      const res: any = await callOrganizationApi(payload);

      if (res.status === 200) {
        if (typeof res?.data === 'string' || res?.data instanceof String) {
          setFormData(null);
          setIsTextboxVisible(true);
          setAction('');
        } else {
          setFormData({ ...res?.data, organizationName: res?.data?.businessEntityName });
          setAction('');
        }
      }
    } catch (e) {
      createToast('error', 'Something went wrong. Please try again');
    } finally {
      setComponentLoader(false);
    }
  };

  // Add Organization Name
  const onSubmit = async () => {
    try {
      if (action === 'EditOrganization') {
        setConfirmationPopup(true);
      } else {
        setComponentLoader(true);
        if (formData?.businessEntityName?.length > 0) {
          const dataObj = {
            businessEntityName: formData?.businessEntityName,
          };

          const payload = {
            endpoint: '/api/v1/Organization',
            apiType: Constants.OrganizationManagement,
            apiMethod: 'Post',
            payload: dataObj,
          };

          const res: any = await callOrganizationApi(payload);

          if (res.status === 200) {
            await fetchOrganizationDetails();
            setIsTextboxVisible(false);
            createToast('success', 'Organization created successfully.');
          }
        } else {
          createToast('error', 'Corporate/Organization Name field cannot be empty');
        }
      }
    } catch (e) {
      createToast('error', 'Something went wrong. Please try again');
    } finally {
      setComponentLoader(false);
    }
  };

  // Edit Organization Name
  const onEdit = async () => {
    setConfirmationPopup(false);
    try {
      setComponentLoader(true);
      if (formData?.businessEntityName?.length > 0) {
        const dataObj = {
          businessEntityName: formData?.businessEntityName,
          businessEntityID: formData?.businessEntityID,
        };

        const payload = {
          endpoint: '/api/v1/Organization',
          apiType: Constants.OrganizationManagement,
          apiMethod: 'Put',
          payload: dataObj,
        };

        const res: any = await callOrganizationApi(payload);

        if (res.status === 200) {
          await fetchOrganizationDetails();
          setIsTextboxVisible(false);
          createToast('success', 'Organization updated successfully.');
        }
      } else {
        createToast('error', 'Corporate/Organization Name field cannot be empty');
      }
    } catch (e) {
      createToast('error', 'Something went wrong. Please try again');
    } finally {
      setComponentLoader(false);
    }
  };

  // Delete Organization
  const onDelete = async () => {
    setConfirmationPopup(false);
    try {
      setComponentLoader(true);

      const dataObj = {
        businessEntityName: formData?.businessEntityName,
        businessEntityID: formData?.businessEntityID,
        isDeleted: true,
      };

      const payload = {
        endpoint: '/api/v1/Organization',
        apiType: Constants.OrganizationManagement,
        apiMethod: 'Put',
        payload: dataObj,
      };

      const res: any = await callOrganizationApi(payload);

      if (res.status === 200) {
        await fetchOrganizationDetails();
        setIsTextboxVisible(true);
        createToast('success', 'Organization deleted successfully.');
      }
    } catch (e) {
      createToast('error', 'Something went wrong. Please try again');
    } finally {
      setComponentLoader(false);
    }
  };

  useEffect(() => {
    // setIsTextboxVisible(true);
    fetchOrganizationDetails();
  }, []);

  useEffect(() => {
    if (action === 'DeleteOrganization') {
      setConfirmationPopup(true);
    } else if (action === 'EditOrganization') {
      setIsTextboxVisible(true);
    }
  }, [action]);

  const openPanel = (data: any) => {
    setIsOpen({ visible: true, data: data });
  };

  const dismissPanel = (isSubmit: any) => {
    setIsOpen({ visible: false, data: {} });
  };

  const openPanelGroup = (data: any) => {
    setIsOpenGroup({ visible: true, data: data });
  };

  const dismissPanelGroup = (isSubmit: any) => {
    setIsOpenGroup({ visible: false, data: {} });
  };

  const openPanelPreview = (data: any) => {
    setIsOpenPreview({ visible: true, data: data });
  };

  const dismissPanelPreview = (isSubmit: any) => {
    setIsOpenPreview({ visible: false, data: {} });
  };

  const openPanelRegion = (data: any) => {
    setIsOpenRegion({ visible: true, data: data });
  };
  const dismissPanelRegion = (isSubmit: any) => {
    setIsOpenRegion({ visible: false, data: {} });
  };

  const openPanelYard = (data: any) => {
    setIsOpenYard({ visible: true, data: data });
  };

  const dismissPanelYard = (isSubmit: any) => {
    setIsOpenYard({ visible: false, data: {} });
  };

  const openPanelDepartment = (data: any) => {
    setIsOpenDepartment({ visible: true, data: data });
  };

  const dismissPanelDepartment = (isSubmit: any) => {
    setIsOpenDepartment({ visible: false, data: {} });
  };

  const treeData = [
    {
      text: 'TNT',
      items: [
        {
          text: 'Company',
          items: [
            { text: 'Bare Dental' },
            { text: 'Crane' },
            { text: 'Dispatch' },
            { text: 'safety' },
            { text: 'Fleet Trucks' },
            { text: 'Information Technology' },
            { text: 'Maintenance' },
            { text: 'Milwright' },
            { text: 'Rigging' },
          ],
        },
        {
          text: 'Yard',
          items: [
            { text: 'Bare Dental' },
            { text: 'Crane' },
            { text: 'Dispatch' },
            { text: 'safety' },
            { text: 'Fleet Trucks' },
            { text: 'Information Technology' },
            { text: 'Maintenance' },
            { text: 'Milwright' },
            { text: 'Rigging' },
          ],
        },
        {
          text: 'Region',
          items: [
            { text: 'Bare Dental' },
            { text: 'Crane' },
            { text: 'Dispatch' },
            { text: 'safety' },
            { text: 'Fleet Trucks' },
            { text: 'Information Technology' },
            { text: 'Maintenance' },
            { text: 'Milwright' },
            { text: 'Rigging' },
          ],
        },
        {
          text: 'Department',
          items: [
            {
              text: 'Bare Dental',
              items: [
                { text: 'Bare Dental' },
                { text: 'Crane' },
                { text: 'Dispatch' },
                { text: 'safety' },
                { text: 'Fleet Trucks' },
                { text: 'Information Technology' },
                { text: 'Maintenance' },
                { text: 'Milwright' },
                { text: 'Rigging' },
              ],
            },
            { text: 'Crane' },
            { text: 'Dispatch' },
            { text: 'safety' },
            { text: 'Fleet Trucks' },
            { text: 'Information Technology' },
            { text: 'Maintenance' },
            { text: 'Milwright' },
            { text: 'Rigging' },
          ],
        },
      ],
    },
  ];

  const customTreeUI = (props: any) => {
    return (
      <>
        {IconClassName(props?.items)}
        {props.item.text}
        <span className="group-badge">Cananda</span>
      </>
    );
  };

  const is = (fileName: string, ext: string) => new RegExp(`.${ext}\$`).test(fileName);

  const IconClassName: React.FC<any> = (option) => {
    let content;
    switch (option) {
      case 'Company':
        content = <span className="FCC_organization-caption red">C</span>;
        break;
      case 'Region':
        content = <span className="FCC_organization-caption yellow">R</span>;
        break;
      case 'Yard':
        content = <span className="FCC_organization-caption brown">Y</span>;
        break;
      default:
        content = <span className="FCC_organization-caption purple">D</span>;
    }
    return <span>{content}</span>;
  };

  return (
    <>
      {componentLoader && (
        <div className="component-loader">
          <Loader />
        </div>
      )}
      <MessageContainer
        type={toast.type}
        text={toast.message}
        show={toast.show}
        setToast={setToast}
      />
      <OrganizationSetting
        breadcrumlist={breadcrumlist}
        openPanel={openPanel}
        dismissPanel={dismissPanel}
        isOpen={isOpen}
        dismissPanelRegion={dismissPanelRegion}
        isOpenRegion={isOpenRegion}
        openPanelRegion={openPanelRegion}
        isOpenYard={isOpenYard}
        openPanelYard={openPanelYard}
        dismissPanelYard={dismissPanelYard}
        isOpenDepartment={isOpenDepartment}
        openPanelDepartment={openPanelDepartment}
        dismissPanelDepartment={dismissPanelDepartment}
        openPanelGroup={openPanelGroup}
        dismissPanelGroup={dismissPanelGroup}
        isOpenGroup={isOpenGroup}
        openPanelPreview={openPanelPreview}
        dismissPanelPreview={dismissPanelPreview}
        isOpenPreview={isOpenPreview}
        treeData={treeData}
        customTreeUI={customTreeUI}
        collectData={collectData}
        formData={formData}
        onSubmit={onSubmit}
        setAction={setAction}
        action={action}
        setConfirmationPopup={setConfirmationPopup}
        isTextboxVisible={isTextboxVisible}
      />

      {confirmationPopup ? (
        <ModalPopup
          ShowModalPopupFooter={true}
          ModalPopupTitle="Confirmation"
          ModalPopupType="medium"
          FooterSecondaryBtnTxt="No"
          FooterPrimaryBtnTxt="Yes"
          closeModalPopup={() => setConfirmationPopup(false)}
          PrimaryBtnOnclick={() =>
            action === 'DeleteOrganization' ? onDelete() : onEdit()
          }
          SecondryBtnOnclick={() => setConfirmationPopup(false)}
          ModalPopupName="confirmation-popup"
        >
          {action === 'DeleteOrganization' ? (
            <p>Are you sure you want to delete Organization ?</p>
          ) : (
            <p>Are you sure you want to edit Organization ?</p>
          )}
        </ModalPopup>
      ) : (
        <></>
      )}
    </>
  );
};

export default OrganizationSettingContainer;
