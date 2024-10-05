import { useEffect, useState, useCallback } from 'react';
import { useDispatch } from 'react-redux';
import YardDetails from './yarddetails';
import Constants from '../../../../utils/Constants';
import { callOrganizationApi } from '../../organizationsettingapicalls';
import { pushNotification } from '../../../../redux/notification/notificationSlice';

const YardDetailsContainer = ({ collectData, formData }: any) => {
  const dispatch = useDispatch();
  const [physicalAddress, setPhysicalAddress] = useState<any>();
  const [mailingAddress, setMailingAddress] = useState<any>();
  const [generalLedgerOptions, setGeneralLedgerOptions] = useState<any>([]);
  const [quoteTermConditionOptions, setQuoteTermConditionOptions] = useState<any>([]);
  const [quoteFooterOptions, setQuoteFooterOptions] = useState<any>([]);
  const [statePhysicalAddress, setStatePhysicalAddress] = useState<any>([]);
  const [stateMailingAddress, setStateMailingAddress] = useState<any>([]);
  const [sameAsAddress, setSameAsAddress] = useState<boolean>(false);

  const getStateByCountry = async (countryCode: any, addressModule: any) => {
    try {
      const payload = {
        endpoint: `/api/Core/GetStateProvince?countryCode=${countryCode}`,
        apiType: Constants.OrganizationManagement,
        apiMethod: 'Get',
        payload: null,
      };
      const res: any = await callOrganizationApi(payload);
      if (res.status === 200) {
        if (res.data) {
          if (res.data.length > 0) {
            const stateDropdownOption: any = [];
            res.data.forEach((element: any) => {
              stateDropdownOption.push({
                value: element.stateProvinceCode,
                label: element.stateProvinceName,
                stateProvinceId: element?.stateProvinceId,
              });
            });
            if (addressModule === 'physicalAddress') {
              setStatePhysicalAddress(stateDropdownOption);
            } else if (addressModule === 'mailingAddress') {
              setStateMailingAddress(stateDropdownOption);
            }
          } else {
            if (addressModule === 'physicalAddress') {
              setStatePhysicalAddress([]);
            } else if (addressModule === 'mailingAddress') {
              setStateMailingAddress([]);
            }
          }
        }
      }
    } catch (err) {
      console.log('Oops something went Wrong!.');
      dispatch(
        pushNotification({
          show: true,
          type: 'error',
          message: `Something went wrong. Please try again`,
        })
      );
    }
  };

  /**
   * get GeneralLedger dropdown option
   */
  const getGeneralLedger = async () => {
    try {
      const payload = {
        endpoint: `/api/v1/Yard/GeneralLedger`,
        apiType: Constants.OrganizationManagement,
        apiMethod: 'Get',
        payload: null,
      };
      const res: any = await callOrganizationApi(payload);
      if (res.status === 200) {
        if (res.data) {
          if (res.data.length > 0) {
            const generalLedgerDropdownOption: any = [];
            res.data.forEach((element: any) => {
              generalLedgerDropdownOption.push({
                value: element.generalLedgerId,
                label: element.description,
                generalLedgerAccountCode: element.generalLedgerAccountCode,
              });
            });
            setGeneralLedgerOptions(generalLedgerDropdownOption);
          }
        }
      }
    } catch (err) {
      console.log('Oops something went Wrong!.');
      dispatch(
        pushNotification({
          show: true,
          type: 'error',
          message: `Something went wrong. Please try again`,
        })
      );
    }
  };

  const stableHandlerGeneralLedger = useCallback(getGeneralLedger, []);

  useEffect(() => {
    stableHandlerGeneralLedger();
  }, [stableHandlerGeneralLedger]);

  /**
   * get TermsFooter dropdown option
   */
  const getTermsFooter = async () => {
    try {
      const payload = {
        endpoint: `/api/v1/Yard/TermsFooter`,
        apiType: Constants.OrganizationManagement,
        apiMethod: 'Get',
        payload: null,
      };
      const res: any = await callOrganizationApi(payload);
      if (res.status === 200) {
        if (res.data) {
          if (res.data.length > 0) {
            const footerDropdownOption: any = [];
            const termConditionDropdownOption: any = [];
            const footerOption = res.data.filter((data: any) => data?.termTypeID === 1);
            const termConditionOption = res.data.filter(
              (data: any) => data?.termTypeID === 2
            );
            footerOption.forEach((element: any) => {
              footerDropdownOption.push({
                value: element.termID,
                label: element.termName,
              });
            });
            termConditionOption.forEach((element: any) => {
              termConditionDropdownOption.push({
                value: element.termID,
                label: element.termName,
              });
            });
            setQuoteTermConditionOptions(termConditionDropdownOption);
            setQuoteFooterOptions(footerDropdownOption);
          }
        }
      }
    } catch (err) {
      console.log('Oops something went Wrong!.');
      dispatch(
        pushNotification({
          show: true,
          type: 'error',
          message: `Something went wrong. Please try again`,
        })
      );
    }
  };

  const stableHandlerTermsFooter = useCallback(getTermsFooter, []);

  useEffect(() => {
    stableHandlerTermsFooter();
  }, [stableHandlerTermsFooter]);

  const collectPhysicalAddress = (key: any, val: any) => {
    if (key === 'country') {
      setPhysicalAddress({
        ...physicalAddress,
        [key]: val,
        ['state']: null,
      });
    } else {
      setPhysicalAddress({
        ...physicalAddress,
        [key]: val,
      });
    }
    setPhysicalAddress({
      ...physicalAddress,
      [key]: val,
    });
    // if (sameAsAddress) {
    //   setMailingAddress({
    //     ...physicalAddress,
    //     [key]: val,
    //   });
    // }
    if (key === 'country') {
      if (val) {
        getStateByCountry(val.value, 'physicalAddress');
      } else {
        setStatePhysicalAddress([]);
      }
    }
  };
  const collectMailingAddress = (key: any, val: any) => {
    if (key === 'country') {
      setMailingAddress({
        ...mailingAddress,
        [key]: val,
        ['state']: null,
      });
    } else {
      setMailingAddress({
        ...mailingAddress,
        [key]: val,
      });
    }
    if (key === 'country') {
      if (val) {
        getStateByCountry(val.value, 'mailingAddress');
      } else {
        setStateMailingAddress([]);
      }
    }
  };

  const handleSameAsAddress = (value: any) => {
    // let newPhysicalAddress = physicalAddress;
    // if (!value) {
    //   newPhysicalAddress = {
    //     companyPhone: '+1',
    //     countryCode: '1',
    //   };
    // }
    if (value) {
      setMailingAddress(physicalAddress);
    }
    // collectData('yardDetailsVM', 'mailingAddress', physicalAddress);
    if (physicalAddress?.country) {
      getStateByCountry(physicalAddress?.country?.value, 'mailingAddress');
    }
  };

  useEffect(() => {
    collectData('yardDetailsVM', 'physicalAddress', physicalAddress);
    if (sameAsAddress) {
      setTimeout(() => {
        setMailingAddress(physicalAddress);
      }, 100);
    }
  }, [physicalAddress]);

  useEffect(() => {
    collectData('yardDetailsVM', 'mailingAddress', mailingAddress);
  }, [mailingAddress]);

  return (
    <YardDetails
      dftlOptions={generalLedgerOptions}
      collectData={collectData}
      formData={formData}
      collectPhysicalAddress={collectPhysicalAddress}
      collectMailingAddress={collectMailingAddress}
      physicalAddress={physicalAddress}
      mailingAddress={mailingAddress}
      handleSameAsAddress={handleSameAsAddress}
      quoteFooterOptions={quoteFooterOptions}
      quoteTermConditionOptions={quoteTermConditionOptions}
      statePhysicalAddress={statePhysicalAddress}
      stateMailingAddress={stateMailingAddress}
      sameAsAddress={sameAsAddress}
      setSameAsAddress={setSameAsAddress}
    />
  );
};

export default YardDetailsContainer;
