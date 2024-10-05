import { useState, useEffect, useCallback } from 'react';
import { useDispatch } from 'react-redux';
import AddYard from './addyard';
import Constants from '../../../utils/Constants';
import { callOrganizationApi } from '../organizationsettingapicalls';
import { Loader } from '../../CommonComponent';
import { pushNotification } from '../../../redux/notification/notificationSlice';

interface IProps {
  dismissPanelYard: any;
}
const AddYardContainer = ({ dismissPanelYard }: any) => {
  const [formData, setFormData] = useState<any>({ yardDetailsVM: { status: true } });
  const [componentLoader, setComponentLoader] = useState<boolean>(false);
  const [isFormValid, setIsFormValid] = useState<boolean>(false);
  // const [yardId] = useState(selectedYardData?.yardId ? selectedYardData?.yardId : null);
  // const [parentBusinessEntityId] = useState(selectedYardData?.parentBusinessEntityId ? selectedYardData?.parentBusinessEntityId : null);
  const [yardId] = useState(231);
  const [parentBusinessEntityId] = useState(10);
  const dispatch = useDispatch();

  const collectData = (parentKey: any, key: any, val: any, subKey: any) => {
    const data = {
      ...formData,

      [parentKey]: {
        ...formData?.[parentKey],

        ...(subKey
          ? {
              [subKey]: {
                ...formData?.[parentKey]?.[subKey],
                ...(key.toLowerCase().includes('phone') ? val : { [key]: val }),
              },
            }
          : {
              ...(key.toLowerCase().includes('phone') ? val : { [key]: val }),
            }),
      },
    };
    setFormData(data);
  };

  // const getYard = async () => {
  //   try {
  //     const payload = {
  //       endpoint: `/api/v1/Yard/${yardId}`,
  //       apiType: Constants.OrganizationManagement,
  //       apiMethod: 'Get',
  //       payload: null,
  //     };
  //     const res: any = await callOrganizationApi(payload);
  //     if (res.status === 200) {
  //       if (res.data) {
  //         const data = res.data;
  //         let tempTermAndCondition: any = null;
  //         let tempQuoteFooter: any = null;
  //         if (data?.term.filter((item: any) => item.termTypeID === 2)?.length > 0) {
  //           tempTermAndCondition = data?.term.filter(
  //             (item: any) => item.termTypeID === 2
  //           )[0];
  //         }
  //         if (data?.term.filter((item: any) => item.termTypeID === 1)?.length > 0) {
  //           tempQuoteFooter = data?.term.filter((item: any) => item.termTypeID === 1)[0];
  //         }
  //         let newFormData = { yardDetailsVM: { status: true } };
  //         newFormData = {
  //           ...newFormData,
  //           ['yardDetailsVM']: {
  //             ...formData?.['yardDetailsVM'],
  //             businessEntityCode: data?.businessEntityCode,
  //             businessEntityName: data?.businessEntityName,
  //             status: data?.status,
  //             businessEntityTypeId: data?.businessEntityTypeId,
  //             parentBusinessEntityId: data?.parentBusinessEntityId,
  //             glDistributionCode: {
  //               generalLedgerAccountCode: data?.glDistributionCode,
  //               value: data?.generalLedgerId,
  //             },
  //             physicalAddress:
  //               data?.addresses.filter((item: any) => item.addressTypeId === 3)?.length >
  //               0
  //                 ? data?.addresses.filter((item: any) => item.addressTypeId === 3)[0]
  //                 : [],
  //             mailingAddress:
  //               data?.addresses.filter((item: any) => item.addressTypeId === 2)?.length >
  //               0
  //                 ? data?.addresses.filter((item: any) => item.addressTypeId === 2)[0]
  //                 : [],
  //             termAndCondition: tempTermAndCondition
  //               ? {
  //                   value: tempTermAndCondition.termID,
  //                   label: tempTermAndCondition.termName,
  //                 }
  //               : null,
  //             quoteFooter: tempQuoteFooter
  //               ? { value: tempQuoteFooter.termID, label: tempQuoteFooter.termName }
  //               : null,
  //           },
  //         };
  //         console.log('response&&&', res.data);
  //       }
  //     }
  //   } catch (err) {
  //     console.log('Oops something went Wrong!.');
  //     dispatch(
  //       pushNotification({
  //         show: true,
  //         type: 'error',
  //         message: `Something went wrong. Please try again`,
  //       })
  //     );
  //   }
  // };

  // const stableHandler = useCallback(getYard, [yardId]);

  // useEffect(() => {
  //   if (yardId) {
  //     stableHandler();
  //   }
  // }, [stableHandler]);

  const addYard = async () => {
    try {
      const yardDetailTabData = formData?.yardDetailsVM;
      const counterTabData = formData?.countersVM;
      const finalAddress: any = [];
      const termFooter: any = [];
      const counters: any = [];

      // address array build
      finalAddress.push({
        addressId: 0,
        addressTypeId: 3,
        phoneNumberTypeId: 3,
        addressLine1: yardDetailTabData?.physicalAddress?.address1
          ? yardDetailTabData?.physicalAddress?.address1
          : '',
        addressLine2: yardDetailTabData?.physicalAddress?.address2
          ? yardDetailTabData?.physicalAddress?.address2
          : '',
        city: yardDetailTabData?.physicalAddress?.city
          ? yardDetailTabData?.physicalAddress?.city
          : '',
        stateProvinceId: yardDetailTabData?.physicalAddress?.state?.stateProvinceId
          ? yardDetailTabData?.physicalAddress?.state?.stateProvinceId
          : '',
        stateProvinceName: yardDetailTabData?.physicalAddress?.state?.label
          ? yardDetailTabData?.physicalAddress?.state?.label
          : '',
        stateProvinceCode: yardDetailTabData?.physicalAddress?.state?.value
          ? yardDetailTabData?.physicalAddress?.state?.value
          : '',
        postalCode: yardDetailTabData?.physicalAddress?.zip
          ? yardDetailTabData?.physicalAddress?.zip
          : '',
        phoneCountryCode: yardDetailTabData?.physicalAddress?.addressPhone?.countryCode
          ? yardDetailTabData?.physicalAddress?.addressPhone?.countryCode
          : '',
        phoneNumber: yardDetailTabData?.physicalAddress?.addressPhone?.companyPhone
          ? yardDetailTabData?.physicalAddress?.addressPhone?.companyPhone
          : '',
        countryCode: yardDetailTabData?.physicalAddress?.country?.value
          ? yardDetailTabData?.physicalAddress?.country?.value
          : '',
        countryName: yardDetailTabData?.physicalAddress?.country?.label
          ? yardDetailTabData?.physicalAddress?.country?.label
          : '',
      });
      finalAddress.push({
        addressId: 0,
        addressTypeId: 2,
        phoneNumberTypeId: 2,
        addressLine1: yardDetailTabData?.mailingAddress?.address1
          ? yardDetailTabData?.mailingAddress?.address1
          : '',
        addressLine2: yardDetailTabData?.mailingAddress?.address2
          ? yardDetailTabData?.mailingAddress?.address2
          : '',
        city: yardDetailTabData?.mailingAddress?.city
          ? yardDetailTabData?.mailingAddress?.city
          : '',
        stateProvinceId: yardDetailTabData?.mailingAddress?.state?.stateProvinceId
          ? yardDetailTabData?.mailingAddress?.state?.stateProvinceId
          : '',
        stateProvinceName: yardDetailTabData?.mailingAddress?.state?.label
          ? yardDetailTabData?.mailingAddress?.state?.label
          : '',
        stateProvinceCode: yardDetailTabData?.mailingAddress?.state?.value
          ? yardDetailTabData?.mailingAddress?.state?.value
          : '',
        postalCode: yardDetailTabData?.mailingAddress?.zip
          ? yardDetailTabData?.mailingAddress?.zip
          : '',
        phoneCountryCode: yardDetailTabData?.mailingAddress?.addressPhone?.countryCode
          ? yardDetailTabData?.mailingAddress?.addressPhone?.countryCode
          : '',
        phoneNumber: yardDetailTabData?.mailingAddress?.addressPhone?.companyPhone
          ? yardDetailTabData?.mailingAddress?.addressPhone?.companyPhone
          : '',
        countryCode: yardDetailTabData?.mailingAddress?.country?.value
          ? yardDetailTabData?.mailingAddress?.country?.value
          : '',
        countryName: yardDetailTabData?.mailingAddress?.country?.label
          ? yardDetailTabData?.mailingAddress?.country?.label
          : '',
      });

      // Quote Team array build
      termFooter.push({
        termID: yardDetailTabData?.termAndCondition?.value,
      });
      termFooter.push({
        termID: yardDetailTabData?.quoteFooter?.value,
      });

      // counter formate setup
      if (counterTabData?.quoteNumber) {
        counters.push({
          counterCategoryId: 1,
          counterValue: counterTabData?.quoteNumber,
        });
      }
      if (counterTabData?.jobNumber) {
        counters.push({
          counterCategoryId: 2,
          counterValue: counterTabData?.jobNumber,
        });
      }
      if (counterTabData?.poRequestNumber) {
        counters.push({
          counterCategoryId: 3,
          counterValue: counterTabData?.poRequestNumber,
        });
      }
      if (counterTabData?.poNumber) {
        counters.push({
          counterCategoryId: 4,
          counterValue: counterTabData?.poNumber,
        });
      }
      if (counterTabData?.receiverNumber) {
        counters.push({
          counterCategoryId: 5,
          counterValue: counterTabData?.receiverNumber,
        });
      }
      if (counterTabData?.invoiceNumber) {
        counters.push({
          counterCategoryId: 6,
          counterValue: counterTabData?.invoiceNumber,
        });
      }
      if (counterTabData?.orderNumber) {
        counters.push({
          counterCategoryId: 7,
          counterValue: counterTabData?.orderNumber,
        });
      }
      if (counterTabData?.recurringBillingNumber) {
        counters.push({
          counterCategoryId: 8,
          counterValue: counterTabData?.recurringBillingNumber,
        });
      }
      if (counterTabData?.billingNumber) {
        counters.push({
          counterCategoryId: 9,
          counterValue: counterTabData?.billingNumber,
        });
      }
      if (counterTabData?.shippingNumber) {
        counters.push({
          counterCategoryId: 10,
          counterValue: counterTabData?.shippingNumber,
        });
      }
      if (counterTabData?.woNumber) {
        counters.push({
          counterCategoryId: 11,
          counterValue: counterTabData?.woNumber,
        });
      }

      const requestData = {
        businessEntityId: 0,
        businessEntityTypeId: 6,
        businessEntityCode: yardDetailTabData?.businessEntityCode
          ? yardDetailTabData?.businessEntityCode
          : '',
        businessEntityName: yardDetailTabData?.businessEntityName
          ? yardDetailTabData?.businessEntityName
          : '',
        glDistributionCode: yardDetailTabData?.glDistributionCode
          ?.generalLedgerAccountCode
          ? yardDetailTabData?.glDistributionCode?.generalLedgerAccountCode
          : '',
        status: yardDetailTabData?.status,
        parentBusinessEntityId: parentBusinessEntityId,
        generalLedgerId: yardDetailTabData?.glDistributionCode?.value
          ? yardDetailTabData?.glDistributionCode?.value
          : 0,
        addresses: finalAddress,
        businessEntityTerms: termFooter,
        businessEntityCounters: counters,
      };
      setComponentLoader(true);
      const payload = {
        endpoint: `/api/v1/Yard`,
        apiType: Constants.OrganizationManagement,
        apiMethod: 'Post',
        payload: requestData,
      };
      const res: any = await callOrganizationApi(payload);
      if (res.status === 200) {
        dispatch(
          pushNotification({
            show: true,
            type: 'success',
            message: `Yard created successfully!`,
          })
        );
        dismissPanelYard(false);
      }
      // else if (res.status === 203) {
      //   setIsRoleExist({ visible: true, message: res.data });
      // }
    } catch (error: any) {
      // if (error.code === 403) {
      //   setIsRoleExist({ visible: true, message: 'Role Already Exists.' });
      // }
      // createToast('error', 'Something went wrong. Please try again');
      dispatch(
        pushNotification({
          show: true,
          type: 'error',
          message: `Something went wrong. Please try again`,
        })
      );
    } finally {
      setComponentLoader(false);
    }
  };

  const validateForm = () => {
    console.log(formData);
    const yardDetailTabData = formData?.yardDetailsVM;
    const counterTabData = formData?.countersVM;
    if (
      yardDetailTabData?.businessEntityCode?.length > 0 &&
      yardDetailTabData?.businessEntityName?.length > 0 &&
      yardDetailTabData?.physicalAddress?.address1?.length > 0 &&
      yardDetailTabData?.physicalAddress?.city?.length > 0 &&
      yardDetailTabData?.physicalAddress?.zip?.length > 0 &&
      yardDetailTabData?.physicalAddress?.country?.value &&
      yardDetailTabData?.physicalAddress?.state?.value &&
      yardDetailTabData?.physicalAddress?.addressPhone?.companyPhone &&
      yardDetailTabData?.termAndCondition?.value &&
      yardDetailTabData?.quoteFooter?.value
    ) {
      setIsFormValid(true);
    } else {
      setIsFormValid(false);
    }
  };

  useEffect(() => {
    validateForm();
  }, [formData]);

  useEffect(() => {
    console.log('formData&&&&', formData);
  }, [formData]);

  return (
    <>
      {componentLoader && (
        <div className="component-loader">
          <Loader />
        </div>
      )}
      <AddYard
        dismissPanelYard={dismissPanelYard}
        collectData={collectData}
        formData={formData}
        saveYard={addYard}
        isFormValid={isFormValid}
      />
    </>
  );
};

export default AddYardContainer;
