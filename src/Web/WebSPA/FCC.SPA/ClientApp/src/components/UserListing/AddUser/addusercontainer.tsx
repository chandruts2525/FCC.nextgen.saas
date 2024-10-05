import { useEffect, useState } from 'react';
import AddUser from './adduser';
import { checkEmailValidation } from '../../../utils/Validators';
import { userAPI } from '../userlistingapicalls';
import { callRoleApi } from '../../RoleListing/rolelistingapicalls';
import { Loader } from '../..//CommonComponent';
import Constants from '../../../utils/Constants';

interface IProps {
  setIsFormValid?: any;
  action?: string;
  isFormValid: boolean;
  dismissPanel: any;
  selectedUserData: any;
  createToast: any;
}

interface FormProps {
  loginEmail?: any;
  lastName?: any;
  firstName?: any;
  companies?: any;
  departments?: any;
  yards?: any;
  addEntities?: any;
  deleteEntities?: any;
  status?: any;
  role?: any;
  employee?: any;
  maxRegDevices?: number;
}
const defaultFormData: FormProps = {
  status: { value: true, label: 'Active' },
};

const initValues: any = {
  companies: [
    { value: 'company 1', label: 'company 1' },
    { value: 'company 2', label: 'company 2' },
  ],
  departments: [{ value: '3', label: '1020 - BVD San Diego' }],
  yards: [{ value: 'Yard 2', label: 'Yard 2' }],
};

const AddUserContainer = ({
  setIsFormValid,
  action,
  isFormValid,
  dismissPanel,
  selectedUserData,
  createToast,
}: IProps) => {
  const [formData, setFormData] = useState(defaultFormData);
  const [deleteEntities, setDeleteEntities] = useState({});
  const [addEntities, setAddEntities] = useState({});
  const [error, setError] = useState('');
  const [userId] = useState(selectedUserData?.userId ? selectedUserData?.userId : null);
  const [userPermissionOptions, setUserPermissionOptions] = useState({
    company: [],
    yard: [],
    department: [],
  });
  const [employeeOptions, setEmployeeOptions] = useState([]);
  const [roleOptions, setRoleOptions] = useState([]);
  const [oldBusinessEntity, setOldBusinessEntity]: any = useState([]);
  const [componentLoader, setComponentLoader] = useState<boolean>(false);

  const statusOptions = [
    { value: true, label: 'Active' },
    { value: false, label: 'Inactive' },
  ];

  useEffect(() => {
    fetchPermissionOptions();
    fetchRoles();
    fetchEmployees();
  }, []);

  const fetchEmployees = async () => {
    try {
      const payload = {
        endpoint: `/api/User/GetBusinessEntity`,
        apiType: Constants.IAC,
        apiMethod: 'Get',
        payload: null,
      };
      const res: any = await userAPI(payload);
      if (res.status === 200) {
        if (res.data && res.data.length > 0) {
          let rolePayload = res.data.map((item: any) => {
            return {
              value: item.businessEntityID,
              label: item.businessEntityName,
            };
          });
          setEmployeeOptions(rolePayload);
        }
      }
    } catch (e) {
      console.log(e);
      createToast('error', 'Something went wrong. Please try again');
    }
  };

  const groupData = (list: any, key: any) => {
    const map: any = {
      Company: [],
      Yard: [],
      Department: [],
    };
    list.forEach((item: any) => {
      let group = item[key];
      let payload = {
        label: item.businessEntityName,
        value: item.businessEntityID,
      };
      map[group].push(payload);
    });
    return map;
  };

  const fetchRoles = async () => {
    try {
      const payload = {
        endpoint: `/api/Role/Search`,
        apiType: Constants.IAC,
        apiMethod: 'Get',
        payload: null,
      };
      const res: any = await callRoleApi(payload);
      if (res.status === 200) {
        if (
          res.data &&
          res?.data?.searchRoleFilerResponse &&
          res?.data?.searchRoleFilerResponse?.length > 0
        ) {
          let rolePayload = res?.data?.searchRoleFilerResponse.map((item: any) => {
            return {
              value: item.roleId,
              label: item.roleName,
            };
          });
          setRoleOptions(rolePayload);
        }
      }
    } catch (e) {
      console.log(e);
      createToast('error', 'Something went wrong. Please try again');
    }
  };

  const fetchPermissionOptions = async () => {
    try {
      const payload = {
        endpoint: `/api/Role/GetBusinessType`,
        apiType: Constants.IAC,
        apiMethod: 'Get',
        payload: null,
      };
      const res: any = await userAPI(payload);
      if (res.status === 200) {
        if (res.data && res.data.length > 0) {
          const grouped: any = groupData(res.data, 'businessEntityTypeName');
          setUserPermissionOptions({
            company: grouped.Company,
            yard: grouped.Yard,
            department: grouped.Department,
          });
        }
      }
    } catch (error) {
      console.log('Something went wrong', error);
      createToast('error', 'Something went wrong. Please try again');
    }
  };

  const validateForm = () => {
    console.log(formData);
    if (
      formData?.loginEmail?.length > 0 &&
      checkEmailValidation(formData?.loginEmail) &&
      formData?.lastName?.length > 0 &&
      formData?.firstName?.length > 0 &&
      formData?.companies?.length > 0 &&
      formData?.departments?.length > 0 &&
      formData?.role?.value &&
      formData?.yards?.length > 0
    ) {
      setIsFormValid(true);

      const addnewcompanies = formData?.companies.filter(
        (a: any) => !initValues?.companies?.map((b: any) => b.value).includes(a.value)
      );
      const addnewdepartments = formData?.departments.filter(
        (a: any) => !initValues?.departments?.map((b: any) => b.value).includes(a.value)
      );
      const addnewyards = formData?.yards.filter(
        (a: any) => !initValues?.yards?.map((b: any) => b.value).includes(a.value)
      );

      const deletecompanies = initValues?.companies.filter(
        (a: any) => !formData?.companies?.map((b: any) => b.value).includes(a.value)
      );
      const deletedepartments = initValues?.departments.filter(
        (a: any) => !formData?.departments?.map((b: any) => b.value).includes(a.value)
      );
      const deleteyards = initValues?.yards.filter(
        (a: any) => !formData?.yards?.map((b: any) => b.value).includes(a.value)
      );

      const addNew = [...addnewcompanies, ...addnewdepartments, ...addnewyards]
        ?.map((item: any) => item.value)
        .join();
      const deleteOld = [...deletecompanies, ...deletedepartments, ...deleteyards]
        ?.map((item: any) => item.value)
        .join();
      // const data={
      //     ...formData,
      //     NewBusinessEntityIDs:addNew,
      //     OldBusinessEntityIDs:deleteOld,
      // }

      setAddEntities(addNew);
      setDeleteEntities(deleteOld);
    } else {
      setIsFormValid(false);
    }
  };

  useEffect(() => {
    validateForm();
  }, [formData]);

  useEffect(() => {
    if (action === 'Edit') {
      fetchUser(selectedUserData?.userId);
    }
  }, [action]);

  const fetchUser = async (id: number) => {
    try {
      const payload = {
        endpoint: `/api/User/${id}`,
        apiType: Constants.IAC,
        apiMethod: 'Get',
        payload: null,
      };
      const res: any = await userAPI(payload);
      if (res.status === 200) {
        const {
          companies,
          departments,
          employeeID,
          employeeName,
          firstName,
          lastName,
          loginEmail,
          maximumATOMDevices,
          roleID,
          roleName,
          status,
          yards,
          employee,
        } = res.data;
        // dismissPanel(true)
        const yardData: any = [];
        const companyData: any = [];
        const departmentData: any = [];
        const oldEntities: any = [];

        if (companies.length > 0) {
          companies.forEach((item: any) => {
            companyData.push({
              value: item.businessEntityId,
              label: item.businessEntityName,
            });
            oldEntities.push(item.businessEntityId);
          });
        }
        if (departments.length > 0) {
          departments.forEach((item: any) => {
            departmentData.push({
              value: item.businessEntityId,
              label: item.businessEntityName,
            });
            oldEntities.push(item.businessEntityId);
          });
        }
        if (yards.length > 0) {
          yards.forEach((item: any) => {
            yardData.push({
              value: item.businessEntityId,
              label: item.businessEntityName,
            });
            oldEntities.push(item.businessEntityId);
          });
        }

        if (employee && employee.length > 0) {
          oldEntities.push(employee[0].businessEntityId);
        }

        let selectedStatus: any = null;
        statusOptions.forEach((val: any) => {
          if (status === val.value) {
            selectedStatus = val;
          }
        });

        setOldBusinessEntity(oldEntities);
        setFormData({
          ...formData,
          loginEmail,
          lastName,
          firstName,
          status: selectedStatus,
          role: { value: roleID, label: roleName },
          employee:
            employee && employee.length > 0
              ? {
                  value: employee[0].businessEntityId,
                  label: employee[0].businessEntityName,
                }
              : null,
          maxRegDevices: maximumATOMDevices,
          yards: yardData.length ? yardData : null,
          departments: departmentData.length ? departmentData : null,
          companies: companyData.length ? companyData : null,
        });
        // const {
        //     loginEmail,
        //     lastName,
        //     firstName,
        //     companies,
        //     departments,
        //     yards,
        //     addEntities,
        //     deleteEntities,
        //     status,
        //     role,
        //     employee,
        //     maxRegDevices
        // } = formData;
      }
    } catch (e) {
      console.log(e);
      createToast('error', 'Something went wrong. Please try again');
    }
  };

  const extractValue = (list: any) => {
    let val: any = [];
    list.forEach((item: any) => {
      val.push(item.value);
    });
    return val;
  };

  const postUser = async () => {
    try {
      const {
        loginEmail,
        lastName,
        firstName,
        companies,
        departments,
        yards,
        addEntities,
        deleteEntities,
        status,
        role,
        employee,
        maxRegDevices,
      } = formData;
      setError('');
      let businessEntity = '';
      const companyValue = companies.length ? extractValue(companies) : [];
      const departmentValue = departments.length ? extractValue(departments) : [];
      const yardValue = yards.length ? extractValue(yards) : [];
      businessEntity = companyValue.toString();
      businessEntity = businessEntity + ',' + departmentValue.toString();
      businessEntity = businessEntity + ',' + yardValue.toString();
      if (employee && employee?.value) {
        businessEntity = businessEntity + ',' + employee?.value.toString();
      }
      const requestData = {
        userId: 0,
        loginEmail: loginEmail,
        firstName: firstName,
        lastName: lastName,
        status: status.value || false,
        roleID: role?.value || 0,
        // "employeeID": employee?.value || 0,
        maximumATOMDevices: maxRegDevices,
        businessEntityIDs: businessEntity,
      };
      setComponentLoader(true);
      const payload = {
        endpoint: `/api/User`,
        apiType: Constants.IAC,
        apiMethod: 'Post',
        payload: requestData,
      };
      const res: any = await userAPI(payload);
      if (res.status === 200) {
        createToast('success', 'User created successfully!');
        dismissPanel(true);
      } else if (res.status === 203) {
        setError('Email ID already registered. Kindly try using other email id');
      }
    } catch (error: any) {
      console.log('Something went wrong', error);
      createToast('error', 'Something went wrong. Please try again');
    } finally {
      setComponentLoader(false);
    }
  };

  const putUser = async () => {
    try {
      const {
        loginEmail,
        lastName,
        firstName,
        companies,
        departments,
        yards,
        addEntities,
        deleteEntities,
        status,
        role,
        employee,
        maxRegDevices,
      } = formData;
      const oldIds: any = [];
      const newIds: any = [];
      const currentValues: any = [];
      companies.forEach((item: any) => {
        if (oldBusinessEntity.indexOf(item.value) < 0) {
          newIds.push(item.value);
        }
        currentValues.push(item.value);
      });
      departments.forEach((item: any) => {
        if (oldBusinessEntity.indexOf(item.value) < 0) {
          newIds.push(item.value);
        }
        currentValues.push(item.value);
      });
      yards.forEach((item: any) => {
        if (oldBusinessEntity.indexOf(item.value) < 0) {
          newIds.push(item.value);
        }
        currentValues.push(item.value);
      });
      if (employee) {
        if (oldBusinessEntity.indexOf(employee?.value) < 0) {
          newIds.push(employee?.value);
        }
        currentValues.push(employee?.value);
      }
      oldBusinessEntity.forEach((item: any) => {
        if (currentValues.indexOf(item) < 0) {
          oldIds.push(item);
        }
      });
      const requestData = {
        userId: userId,
        loginEmail: loginEmail,
        firstName: firstName,
        lastName: lastName,
        status: status.value,
        roleID: role.value,
        // "employeeID": employee.value,
        maximumATOMDevices: maxRegDevices,
        modifiedBy: 'XYZ',
        oldBusinessEntityIDs: oldIds.length ? oldIds.toString() : '',
        newBusinessEntityIDs: newIds.length ? newIds.toString() : '',
      };
      setComponentLoader(true);
      const payload = {
        endpoint: `/api/User`,
        apiType: Constants.IAC,
        apiMethod: 'Put',
        payload: requestData,
      };
      const res: any = await userAPI(payload);
      if (res.status === 200) {
        createToast('success', 'User updated successfully!');
        dismissPanel(true);
      }
    } catch (error: any) {
      console.log('Something went wrong', error);
      createToast('error', 'Something went wrong. Please try again');
    } finally {
      setComponentLoader(false);
    }
  };

  const handleSubmit = async () => {
    try {
      if (userId) {
        putUser();
      } else {
        postUser();
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
      <AddUser
        roleOptions={roleOptions}
        yardOptions={userPermissionOptions.yard}
        departmentOptions={userPermissionOptions.department}
        companyOptions={userPermissionOptions.company}
        employeeOptions={employeeOptions}
        statusOptions={statusOptions}
        formData={formData}
        userId={userId}
        setFormData={setFormData}
        handleSubmit={handleSubmit}
        isFormValid={isFormValid}
        dismissPanel={dismissPanel}
        error={error}
      />
    </>
  );
};

export default AddUserContainer;
