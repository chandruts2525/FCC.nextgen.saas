import { useEffect, useState } from 'react';
import { Loader } from '../../CommonComponent';
import AddUnitMeasureinner from './addunitmeasure';
import { unitOfMeasureAPI } from '../unitmeasureapicalls';
import MessageContainer from '../../CommonComponent/Message/messagecontainer';
import Constants from '../../../utils/Constants';

interface IProps {
  action?: string;
  dismissPanel: any;
  selectedData: any;
  createToast: any;
}
interface FormProps {
  code?: any;
  name?: any;
  measureType?: any;
  conversionFactor?: any;
  status?: any;
  id?: any;
}

const defaultFormData: FormProps = {
  status: { value: true, label: 'Active' },
};

const statusOptions = [
  { value: true, label: 'Active' },
  { value: false, label: 'Inactive' },
];

const AddUnitMeasureContainer = ({
  action,
  dismissPanel,
  selectedData,
  createToast,
}: IProps) => {
  const [formData, setFormData] = useState(defaultFormData);
  const [measureTypeOptions, setMeasureTypeOptions] = useState([]);
  const [isFormValid, setIsFormValid] = useState(false);
  const [componentLoader, setComponentLoader] = useState<boolean>(false);
  const [addToast, setAddToast] = useState({
    show: false,
    message: '',
    type: '',
  });
  useEffect(() => {
    fetchMeasureType();
  }, [action]);

  useEffect(() => {
    if (selectedData?.unitMeasureCode && action === 'Edit') {
      console.log(selectedData);
      setFormData({
        code: selectedData?.unitMeasureCode,
        id: selectedData?.unitMeasureId,
        name: selectedData?.unitMeasureDisplayValue,
        measureType: selectedData.unitMeasureTypeId
          ? {
              value: selectedData.unitMeasureTypeId,
              label: selectedData.unitMeasureTypeDescription,
            }
          : null,
        conversionFactor: selectedData.conversionFactor,
        status: getStatus(selectedData.isActive === 'Active' ? true : false),
      });
    }
  }, [selectedData, action]);

  useEffect(() => {
    validateForm();
  }, [formData]);

  const getStatus = (status: any) => {
    return statusOptions.find((value: any) => value.value === status);
  };

  const createAddToast = (type: string, message: string) => {
    setAddToast({
      message,
      type,
      show: true,
    });
  };

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
  };

  const validateForm = () => {
    if (formData?.code?.length > 0 && formData?.name?.trim()?.length > 0) {
      if (formData?.measureType && !formData?.conversionFactor) {
        setIsFormValid(false);
      } else {
        setIsFormValid(true);
      }
    } else {
      setIsFormValid(false);
    }
  };

  const fetchMeasureType = async () => {
    try {
      const payload = {
        endpoint: `/api/UnitOfMeasure/GetMeasureType`,
        apiType: Constants.WorkManagement,
        apiMethod: 'Get',
        payload: null,
      };
      const res: any = await unitOfMeasureAPI(payload);
      if (res.status === 200) {
        if (res.data && res.data.length > 0) {
          let payload = res.data.map((item: any) => {
            return {
              value: item.unitMeasureTypeId,
              label: item.unitMeasureTypeDescription,
            };
          });
          setMeasureTypeOptions(payload);
        }
      }
    } catch (e) {
      console.log(e);
      createToast('error', 'Something went wrong. Please try again');
    }
  };

  const postUnitOfMeasure = async () => {
    try {
      const { code, name, measureType, conversionFactor, status } = formData;
      const requestData = {
        unitMeasureCode: code,
        unitMeasureName: name.trim(),
        unitMeasureTypeId: measureType?.value || null,
        conversionFactor: conversionFactor || null,
        isActive: status.value || false,
        isDelete: false,
      };
      setComponentLoader(true);
      const payload = {
        endpoint: `/api/UnitOfMeasure`,
        apiType: Constants.WorkManagement,
        apiMethod: 'Post',
        payload: requestData,
      };
      const res: any = await unitOfMeasureAPI(payload);
      if (res.status === 200) {
        createToast('success', 'Unit Of Measure created successfully!');
        dismissPanel(true);
      } else if (res.status === 203) {
        createAddToast(
          'error',
          res.data || 'Code already registered. Kindly try using other Code'
        );
      }
    } catch (error: any) {
      console.log('Something went wrong', error);
      createToast('error', 'Something went wrong. Please try again');
    } finally {
      setComponentLoader(false);
    }
  };

  const putUnitOfMeasure = async () => {
    try {
      const { code, name, measureType, conversionFactor, status, id } = formData;
      const requestData = {
        unitMeasureCode: code,
        unitMeasureId: id,
        unitMeasureName: name.trim(),
        unitMeasureTypeId: measureType?.value || null,
        conversionFactor: conversionFactor,
        isActive: status.value || false,
        isDelete: false,
      };
      setComponentLoader(true);
      const payload = {
        endpoint: `/api/UnitOfMeasure`,
        apiType: Constants.WorkManagement,
        apiMethod: 'Put',
        payload: requestData,
      };
      const res: any = await unitOfMeasureAPI(payload);
      if (res.status === 200) {
        createToast('success', 'Unit Of Measure updated successfully!');
        dismissPanel(true);
      } else if (res.status === 203) {
        createAddToast(
          'error',
          res.data || 'Name already registered. Kindly try using other code'
        );
      }
    } catch (error: any) {
      console.log(error);
      createToast('error', 'Something went wrong. Please try again');
    } finally {
      console.log('finally');
      setComponentLoader(false);
    }
  };

  const handleSubmit = async () => {
    try {
      if (action === 'Edit') {
        putUnitOfMeasure();
      } else {
        postUnitOfMeasure();
      }
    } catch (error) {
      createToast('error', 'Something went wrong. Please try again');
    }
  };

  return (
    <>
      {componentLoader && (
        <div className="component-loader">
          <Loader />
        </div>
      )}
      <MessageContainer
        type={addToast.type}
        text={addToast.message}
        show={addToast.show}
        setToast={setAddToast}
      />
      <AddUnitMeasureinner
        formData={formData}
        statusOptions={statusOptions}
        measureTypeOptions={measureTypeOptions}
        dismissPanel={dismissPanel}
        handleSubmit={handleSubmit}
        isFormValid={isFormValid}
        action={action}
        collectData={collectData}
      />
    </>
  );
};

export default AddUnitMeasureContainer;
