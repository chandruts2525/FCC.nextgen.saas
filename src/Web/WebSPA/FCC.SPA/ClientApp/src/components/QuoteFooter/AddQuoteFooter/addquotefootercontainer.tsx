import React, { useState, useEffect, useCallback } from 'react';
import {
  dataExists,
  idsString,
  filterOptionInArray,
  isQuillEmpty,
} from '../../SharedComponent/Common-function';
import { Loader } from '../../CommonComponent';
import AddQuoteFooter from './addquotefooter';
import { callQuoteFooterApi } from '../quotefooterapicalls';
import Constants from '../../../utils/Constants';

interface IProps {
  dismissPanel: any;
  createToast: any;
  setIsFormValid?: any;
  isFormValid: boolean;
  selectedFooterData?: any;
}

interface FormProps {
  quoteFooterName?: any;
  description?: any;
  companies?: any;
  module?: any;
  status?: any;
}

const AddQuoteFooterContainer = ({
  dismissPanel,
  createToast,
  isFormValid,
  setIsFormValid,
  selectedFooterData,
}: IProps) => {
  const [formData, setFormData] = useState<FormProps>({
    status: { value: true, label: 'Active' },
  });
  const [companyOptions, setCompanyOptions] = useState<any>([]);
  const [moduleOptions, setModuleOptions] = useState<any>([]);
  const [componentLoader, setComponentLoader] = useState<boolean>(false);
  const [isFooterNameExist, setIsFooterNameExist] = useState<any>({
    visible: false,
    message: '',
  });
  const [description, setDescription] = useState<any>('');
  const [quoteFooterId] = useState(
    selectedFooterData?.quoteFooterId ? selectedFooterData?.quoteFooterId : null
  );

  const statusOptions = [
    { value: true, label: 'Active' },
    { value: false, label: 'Inactive' },
  ];

  const files = [
    {
      filename: 'Softura Helpdesk End User Guide-15Mar2022.docx',
      createdby: 'Michael',
      size: '4KB',
    },
    {
      filename: 'Timesheets and Tasks Management (004).pptx',
      createdby: 'John',
      size: '7MB',
    },
  ];

  /**
   * get selected Quote Footer Detail
   */
  const getSelectedQuoteFooter = async () => {
    try {
      const payload = {
        endpoint: `/api/QuoteFooters/${quoteFooterId}`,
        apiType: Constants.WorkManagement,
        apiMethod: 'Get',
        payload: null,
      };
      const res: any = await callQuoteFooterApi(payload);
      if (res.status === 200) {
        if (res?.data) {
          const { quoteFooterName, status, description, company, modules } = res.data;
          const companyData: any = [];
          const moduleData: any = [];
          if (company.length > 0) {
            company.forEach((item: any) => {
              companyData.push({
                value: item.businessEntityId,
                label: item.businessEntityName,
              });
            });
          }
          if (modules.length > 0) {
            modules.forEach((item: any) => {
              moduleData.push({ value: item.moduleID, label: item.moduleName });
            });
          }

          let selectedStatus: any = null;
          statusOptions.forEach((val: any) => {
            if (status === val.value) {
              selectedStatus = val;
            }
          });
          setDescription(description);
          setFormData({
            ...formData,
            quoteFooterName: quoteFooterName,
            status: selectedStatus,
            companies: companyData.length ? companyData : [],
            module: moduleData.length ? moduleData : [],
          });
        }
      }
    } catch (err) {
      console.log('Oops something went Wrong!.');
      createToast('error', 'Something went wrong. Please try again');
    }
  };

  const stableHandler = useCallback(getSelectedQuoteFooter, [quoteFooterId]);

  useEffect(() => {
    if (quoteFooterId) {
      stableHandler();
    }
  }, [stableHandler]);

  /**
   * get company dropdown option
   */
  const getCompany = async () => {
    try {
      const payload = {
        endpoint: '/api/QuoteFooters/GetCompany',
        apiType: Constants.WorkManagement,
        apiMethod: 'Get',
        payload: null,
      };
      const res: any = await callQuoteFooterApi(payload);
      if (res.status === 200) {
        if (res.data) {
          if (res.data.length > 0) {
            const companyDropdownOption: any = [];
            res.data.forEach((element: any) => {
              companyDropdownOption.push({
                value: element.businessEntityId,
                label: element.businessEntityName,
              });
            });
            setCompanyOptions(companyDropdownOption);
          }
        }
      }
    } catch (err) {
      console.log('Oops something went Wrong!.');
      createToast('error', 'Something went wrong. Please try again');
    }
  };

  const stableHandlerCompany = useCallback(getCompany, []);

  useEffect(() => {
    stableHandlerCompany();
  }, [stableHandlerCompany]);

  /**
   * get module dropdown option
   */
  const getModule = async () => {
    try {
      const payload = {
        endpoint: '/api/QuoteFooters/GetModule',
        apiType: Constants.WorkManagement,
        apiMethod: 'Get',
        payload: null,
      };
      const res: any = await callQuoteFooterApi(payload);
      if (res.status === 200) {
        if (res.data) {
          if (res.data.length > 0) {
            const moduleDropdownOption: any = [];
            res.data.forEach((element: any) => {
              moduleDropdownOption.push({
                value: element.moduleID,
                label: element.moduleName,
              });
            });
            setModuleOptions(moduleDropdownOption);
          }
        }
      }
    } catch (err) {
      console.log('Oops something went Wrong!.');
      createToast('error', 'Something went wrong. Please try again');
    }
  };

  const stableHandlerModule = useCallback(getModule, []);

  useEffect(() => {
    stableHandlerModule();
  }, [stableHandlerModule]);

  /**
   *
   * @param name
   * @param val
   * @param length
   * collect form data and set in state
   */
  const collectData = (name: string, val: any, length: any = null) => {
    if (length && val.length <= length) {
      setFormData({
        ...formData,
        [name]: val,
      });
    } else if (!length) {
      setFormData({
        ...formData,
        [name]: val,
      });
    }
    setIsFooterNameExist({ visible: false, message: '' });
  };

  /**
   *
   * @param name
   * @param val
   * collect dropdown data
   */
  const collectDropdownData = (name: string, val: any) => {
    setFormData({
      ...formData,
      [name]: val,
    });
    setIsFooterNameExist({ visible: false, message: '' });
  };

  /**
   *
   * @param name
   * @param val
   * handle description on change
   */
  const handleDescriptionChange = (name: string, val: any) => {
    setDescription(val);
  };

  /**
   * validate form
   */
  const validateForm = () => {
    const isEmpty = isQuillEmpty(description);
    if (
      formData &&
      formData?.quoteFooterName?.length > 0 &&
      description?.length > 0 &&
      formData?.companies?.length > 0 &&
      formData?.module?.length > 0 &&
      !isEmpty
    ) {
      setIsFormValid(true);
    } else {
      setIsFormValid(false);
    }
  };

  useEffect(() => {
    validateForm();
  }, [formData, description]);

  const postQuoteFooter = async () => {
    try {
      const { quoteFooterName, status, companies, module } = formData;

      //   const { companyIds, moduleIds } = selectCompanyAndModuleIds(companies, module);
      const companyIds = idsString(companies, 'value');
      const moduleIds = idsString(module, 'value');
      const requestData = {
        quoteFootersID: 0,
        name: quoteFooterName ? quoteFooterName : null,
        description: description ? description : null,
        status: status ? status.value : null,
        companies: companyIds,
        modules: moduleIds,
        createdBy: 'Devang',
      };
      setComponentLoader(true);
      const payload = {
        endpoint: '/api/QuoteFooters',
        apiType: Constants.WorkManagement,
        apiMethod: 'Post',
        payload: requestData,
      };
      const res: any = await callQuoteFooterApi(payload);
      if (res.status === 200) {
        createToast('success', `Footer template created successfully!`);
        dismissPanel(true);
      } else if (res.status === 203) {
        setIsFooterNameExist({ visible: true, message: res.data });
      }
    } catch (error: any) {
      createToast('error', 'Something went wrong. Please try again');
    } finally {
      setComponentLoader(false);
    }
  };

  /**
   * Update Quote Footer
   */
  const putQuoteFooter = async () => {
    try {
      const { quoteFooterName, status, companies, module } = formData;

      //   const { companyIds, moduleIds } = selectCompanyAndModuleIds(companies, module);
      const companyIds = idsString(companies, 'value');
      const moduleIds = idsString(module, 'value');
      const requestData = {
        quoteFootersID: quoteFooterId,
        name: quoteFooterName ? quoteFooterName : null,
        description: description ? description : null,
        status: status ? status.value : null,
        companies: companyIds,
        modules: moduleIds,
        modifiedBy: 'Devang',
      };
      setComponentLoader(true);
      const payload = {
        endpoint: '/api/QuoteFooters',
        apiType: Constants.WorkManagement,
        apiMethod: 'Put',
        payload: requestData,
      };
      const res: any = await callQuoteFooterApi(payload);
      if (res.status === 200) {
        createToast('success', `Footer template updated successfully!`);
        dismissPanel(true);
      } else if (res.status === 203) {
        setIsFooterNameExist({ visible: true, message: res.data });
      }
    } catch (error: any) {
      createToast('error', 'Something went wrong. Please try again');
    } finally {
      setComponentLoader(false);
    }
  };

  /**
   * Submit Form
   */
  const handleSubmit = async () => {
    if (quoteFooterId) {
      putQuoteFooter();
    } else {
      postQuoteFooter();
    }
  };

  return (
    <>
      {componentLoader && (
        <div className="component-loader">
          <Loader />
        </div>
      )}
      <AddQuoteFooter
        companyOptions={companyOptions}
        statusOptions={statusOptions}
        moduleOptions={moduleOptions}
        dismissPanel={dismissPanel}
        files={files}
        formData={formData}
        collectData={collectData}
        isFooterNameExist={isFooterNameExist}
        handleDescriptionChange={handleDescriptionChange}
        description={description}
        isFormValid={isFormValid}
        collectDropdownData={collectDropdownData}
        handleSubmit={handleSubmit}
      />
    </>
  );
};

export default React.memo(AddQuoteFooterContainer);
