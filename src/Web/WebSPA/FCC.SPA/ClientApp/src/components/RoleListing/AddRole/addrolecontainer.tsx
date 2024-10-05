import React, { useState, useEffect, useCallback } from 'react';
import {
  formateDateTime,
  base64ToArrayBuffer,
  saveByteArray,
  getFileNameExt,
  getFileTypesByExtension,
  downloadFile,
  getFileName,
  encodeQuery,
} from '../../SharedComponent/Common-function';
import AddRole from './addrole';
import { callRoleApi } from '../rolelistingapicalls';
import { Loader } from '../../CommonComponent';
import Constants from '../../../utils/Constants';

interface IProps {
  setIsFormValid: any;
  isFormValid: boolean;
  dismissPanel: any;
  selectedRoleData: any;
  createToast: any;
}

const MAX_COUNT = 5;
const AddRoleContainer = ({
  setIsFormValid,
  isFormValid,
  dismissPanel,
  selectedRoleData,
  createToast,
}: IProps) => {
  const [inputFields, setInputField] = useState<any>({
    role_name: selectedRoleData?.roleName,
    type: { value: 'all', label: 'All' },
  });
  //const [inputFields, setInputField] = useState<any>({role_name: 'abc', type: {value: 'all', label: 'All'}});
  const [uploadedFiles, setUploadedFiles] = useState<any>([]);
  const [visible, setVisible] = useState<boolean>(false);
  const [isRoleExist, setIsRoleExist] = useState<any>({
    visible: false,
    message: '',
  });
  const [roleId] = useState(selectedRoleData?.roleId ? selectedRoleData?.roleId : null);
  //const [roleId] = useState(100);
  const [userDataMapping, setUserDataMapping] = useState({
    unMappedData: [],
    mappedData: [],
  });
  const [mappedDataStr, setMappedDataStr] = useState<any>({
    addUsersStr: '',
    deleteUsersStr: '',
    unMappedtoMappedUserStr: '',
  });
  const [businessTypeOptions, setBusinessTypeOptions] = useState<any>([
    { value: 'all', label: 'All' },
  ]);
  const [subTypeOptions, setSubTypeOptions] = useState([]);
  const [initialAssignedUsers, setInitialAssignedUsers] = useState<any>([]);
  const [inactiveAssignedUsers, setInactiveAssignedUsers] = useState<any>([]);
  const [roleUserDetails, setRoleUserDetail] = useState<any>();
  const [componentLoader, setComponentLoader] = useState<boolean>(false);
  const [attachmentError, setAttachmentError] = useState<String>('');

  /**
   * get selected Role Detail
   */
  const getRole = async () => {
    try {
      const payload = {
        endpoint: `/api/Role/${roleId}`,
        apiType: Constants.IAC,
        apiMethod: 'Get',
        payload: null,
      };
      const res: any = await callRoleApi(payload);
      // const res: any = await getRoleList(`Role/${roleId}`, {});
      if (res.status === 200) {
        if (res.data) {
          const initialAssignedUsers: any[] = res.data?.filter(
            (a: any) => a.isActive === true
          );
          const inactiveAssignedUsers: any[] = res.data?.filter(
            (a: any) => a.isActive === false
          );
          setInitialAssignedUsers(initialAssignedUsers);
          setInactiveAssignedUsers(inactiveAssignedUsers);
          let filterData = [];
          if (res.data && res.data.length > 0) {
            filterData = res.data.filter((item: any) => item.roleUserMappingID !== 0);
          }
          setUserDataMapping({
            ...userDataMapping,
            mappedData: filterData,
          });
          if (res.data && res.data.length > 0) {
            const roleInfo = res.data[0];
            setRoleUserDetail({
              roleCreatedBy:
                roleInfo?.createdBy && roleInfo?.createdDate
                  ? `${roleInfo?.createdBy} ${formateDateTime(roleInfo?.createdDate)}`
                  : '',
              roleModifyBy:
                roleInfo?.modifiedBy && roleInfo?.modifiedDate
                  ? `${roleInfo?.modifiedBy} ${formateDateTime(roleInfo?.modifiedDate)}`
                  : '',
            });
          }
          getAllUserByType({ value: 'all', label: 'All' }, [], {
            ...userDataMapping,
            mappedData: filterData,
          });
        }
      }
    } catch (err) {
      console.log('Oops something went Wrong!.');
      createToast('error', 'Something went wrong. Please try again');
    }
  };

  const stableHandler = useCallback(getRole, [roleId]);

  useEffect(() => {
    if (roleId) {
      stableHandler();
      getAttachmentByRole();
    } else {
      getAllUserByType({ value: 'all', label: 'All' }, [], userDataMapping);
    }
  }, [stableHandler]);

  /**
   * get type dropdown option
   */
  const getBusinessType = async () => {
    try {
      const payload = {
        endpoint: `/api/Role/GetBusinessType`,
        apiType: Constants.IAC,
        apiMethod: 'Get',
        payload: null,
      };
      const res: any = await callRoleApi(payload);
      // const res: any = await getRoleList("Role/GetBusinessType", {});
      if (res.status === 200) {
        if (res.data) {
          if (res.data.length > 0) {
            const typeData = res.data.filter(
              (v: any, i: any, a: any) =>
                a.findIndex(
                  (v2: any) => v2.businessEntityTypeID === v.businessEntityTypeID
                ) === i
            );
            const typeDropdownOption = [...businessTypeOptions];
            typeData.forEach((element: any) => {
              typeDropdownOption.push({
                value: element.businessEntityTypeID,
                label: element.businessEntityTypeName,
              });
            });
            setBusinessTypeOptions(typeDropdownOption);
          }
        }
      }
    } catch (err) {
      console.log('Oops something went Wrong!.');
      createToast('error', 'Something went wrong. Please try again');
    }
  };

  const stableHandlerType = useCallback(getBusinessType, []);

  useEffect(() => {
    stableHandlerType();
  }, [stableHandlerType]);

  /**
   *
   * @param typeId
   * get subType/Business SubType Option
   */
  const getBusinessByType = async (typeId: number | string) => {
    try {
      const payload = {
        endpoint: `/api/Role/GetBusinessEntityByType/${typeId}`,
        apiType: Constants.IAC,
        apiMethod: 'Get',
        payload: null,
      };
      const res: any = await callRoleApi(payload);
      // const res: any = await getRoleList(
      //   `Role/GetBusinessEntityByType/${typeId}`,
      //   {}
      // );
      if (res.status === 200) {
        if (res.data) {
          if (res.data.length > 0) {
            const subTypeDropdownOption: any = [];
            res.data.forEach((element: any) => {
              subTypeDropdownOption.push({
                value: element.businessEntityID,
                label: element.businessEntityName,
              });
            });
            setSubTypeOptions(subTypeDropdownOption);
          }
        }
      }
    } catch (err) {
      console.log('Oops something went Wrong!.');
      createToast('error', 'Something went wrong. Please try again');
    }
  };

  /**
   *
   * @param type
   * @param subType
   * @param userDataMapping
   * get All User List based on type and sub type
   */
  const getAllUserByType = async (type: any, subType: any, userDataMapping: any) => {
    try {
      let subTypeIds = '';
      if (subType && subType.length > 0) {
        subTypeIds = subType?.map((item: any) => item.value).join();
      }
      const param = {
        RoleId: roleId ? roleId : 0,
        BusinessEntityTypeIds:
          type && type?.value && type?.value !== 'all' ? type?.value.toString() : '',
        BusinessEntityIds: subTypeIds,
      };
      const url = encodeQuery(param);
      const payload = {
        endpoint: '/api/Role/GetAllUserByBusinessEntity?' + url,
        apiType: Constants.IAC,
        apiMethod: 'Get',
        payload: null,
      };
      const res: any = await callRoleApi(payload);
      // const res: any = await getRoleList(
      //   "Role/GetAllUserByBusinessEntity",
      //   param
      // );
      if (res.status === 200) {
        if (res.data) {
          if (res?.data?.length > 0) {
            setUserDataMapping({ ...userDataMapping, unMappedData: res.data });
          }
        }
      }
    } catch (err) {
      console.log('Oops something went Wrong!.');
      createToast('error', 'Something went wrong. Please try again');
    }
  };

  /**
   *
   * @param selectedData
   * delete attachment
   */
  const deletSelectedAttachment = async (selectedData: any, index: number) => {
    try {
      const params = {
        FileURI: selectedData?.fileURI,
        roleAttachmentId: selectedData?.roleAttachmentId
          ? selectedData?.roleAttachmentId
          : '',
      };
      const url = encodeQuery(params);
      const payload = {
        // endpoint: "/api/Role/DeleteAttachment?" + url,
        endpoint: `/api/Role/DeleteAttachment?FileURI=${params?.FileURI}&roleAttachmentID=${params?.roleAttachmentId}`,
        apiType: Constants.IAC,
        apiMethod: 'Delete',
        payload: null,
      };
      const res: any = await callRoleApi(payload);
      // const res: any = await deleteAttachment(`Role/DeleteAttachment`, params);
      if (res.status === 200) {
        const newFiles = [...uploadedFiles];
        newFiles.splice(index, 1);
        setUploadedFiles(newFiles);
      }
    } catch (err) {
      console.log('Oops something went Wrong!.');
      createToast('error', 'Something went wrong. Please try again');
    }
  };

  /**
   * get Role Attachment
   */
  const getAttachmentByRole = async () => {
    try {
      const payload = {
        endpoint: `/api/Role/Attachment/${roleId}`,
        apiType: Constants.IAC,
        apiMethod: 'Get',
        payload: null,
      };
      const res: any = await callRoleApi(payload);
      // const res: any = await getRoleAttachment(`Role/Attachment/${roleId}`, {});
      if (res.status === 200) {
        if (res.data) {
          setUploadedFiles(res.data);
        }
      }
    } catch (err) {
      console.log('Oops something went Wrong!.');
      createToast('error', 'Something went wrong. Please try again');
    }
  };

  const handleDownload = (selectedData: any) => {
    getDownloadAttachment(selectedData);
  };

  const getDownloadAttachment = async (selectedData: any) => {
    try {
      const params = {
        FileURI: selectedData?.fileURI,
      };
      const url = encodeQuery(params);
      const payload = {
        endpoint: '/api/Role/DownloadAttachment?' + url,
        apiType: Constants.IAC,
        apiMethod: 'Get',
        payload: null,
      };
      const res: any = await callRoleApi(payload);
      // const res: any = await getRoleAttachment(
      //   `Role/DownloadAttachment`,
      //   params
      // );
      if (res.status === 200) {
        if (res.data) {
          // downloadFile(res.data, 'selectedData?.fileName')
          const biteArray = base64ToArrayBuffer(res.data);
          const fileExtension = getFileNameExt(selectedData?.fileName);
          if (fileExtension) {
            const fileType = getFileTypesByExtension(fileExtension);
            saveByteArray(selectedData?.fileName, biteArray, fileType);
          }
          // console.log("fileExtension&&&&", fileExtension);
        }
      }
    } catch (err) {
      console.log('Oops something went Wrong!.');
      createToast('error', 'Something went wrong. Please try again');
    }
  };

  /**
   * handle click event for show attachment view
   */
  const handleButtonClick = () => {
    setVisible(!visible);
  };

  /**
   * close attachment view
   */
  const closeAttachment = () => {
    setVisible(false);
  };

  /**
   *
   * @param e
   * input handle change event
   */
  const handleChangeField = (
    e: React.ChangeEvent<HTMLInputElement>,
    length: any = null
  ) => {
    if (e.target.name === 'role_name') {
      setIsRoleExist({ visible: false, message: '' });
    }
    if (length && e.target.value.length <= length) {
      setInputField({ ...inputFields, [e.target.name]: e.target.value });
    } else if (!length) {
      setInputField({ ...inputFields, [e.target.name]: e.target.value });
    }
  };

  /**
   *
   * @param value
   * @param name
   * dropdown onchange
   */
  const handleDropdownChange = (value: any, name: string) => {
    if (name === 'type') {
      if (value.value !== 'all') {
        getBusinessByType(value.value);
      }
      setInputField({ ...inputFields, [name]: value, businessSubType: [] });
      getAllUserByType(value, [], userDataMapping);
    } else {
      setInputField({ ...inputFields, [name]: value });
      getAllUserByType(inputFields?.type, value, userDataMapping);
    }
  };

  /**
   *
   * @param data
   * client list move event
   */
  const handleMove = (data: any) => {
    setUserDataMapping({
      mappedData: data.mappedUser,
      unMappedData: data.unMappedUser,
    });
    const unMappedtoMappedUser = data?.mappedUser?.filter(
      (a: any) => inactiveAssignedUsers?.map((b: any) => b?.userId).includes(a?.userId)
    );
    let addusers = data?.mappedUser?.filter(
      (a: any) => !initialAssignedUsers?.map((b: any) => b?.userId).includes(a?.userId)
    );
    const deleteusers = initialAssignedUsers?.filter(
      (a: any) => !data?.mappedUser?.map((b: any) => b?.userId).includes(a?.userId)
    );

    addusers = addusers?.filter(function (cv: any) {
      return !unMappedtoMappedUser.find(function (e: any) {
        return e.userId === cv.userId;
      });
    });
    const addUsersStr = addusers?.map((item: any) => item.userId).join();
    const deleteUsersStr = deleteusers?.map((item: any) => item.userId).join();
    const unMappedtoMappedUserStr = unMappedtoMappedUser
      ?.map((item: any) => item.userId)
      .join();
    setMappedDataStr({
      addUsersStr: addUsersStr,
      deleteUsersStr: deleteUsersStr,
      unMappedtoMappedUserStr: unMappedtoMappedUserStr,
    });
  };

  /**
   *
   * @param files
   * handle upload file
   */
  const handleUploadFile = (files: any) => {
    setAttachmentError('');
    let fileSizeExceed: boolean = false;
    files.forEach((element: any) => {
      if (element.size > 5000000) {
        fileSizeExceed = true;
      }
    });
    if (files?.length > MAX_COUNT) {
      setAttachmentError('Maximun 5 Files can be uploaded.');
    } else if (fileSizeExceed) {
      setAttachmentError(`Required size is 5MB.`);
    } else {
      postAttachment(files);
    }
  };

  function fileExists(fileName: any) {
    return uploadedFiles.some(function (el: any) {
      return el.fileName === fileName;
    });
  }

  const newFileName = (fileName: any, count: number) => {
    const fileNameWOExtension = getFileName(fileName);
    const fileNameExtension = getFileNameExt(fileName.name);
    let isValidFileName = false;
    let additionalCount = count;
    let newFileName = '';
    while (!isValidFileName) {
      newFileName = fileNameWOExtension + '_' + additionalCount + '.' + fileNameExtension;
      let isExist = fileExists(newFileName);
      if (isExist) {
        additionalCount = additionalCount + 1;
      } else {
        isValidFileName = true;
      }
    }
    return newFileName;
  };

  const handleAttachment = (files: any) => {
    const currentFiles = Array.from(files);
    let choseFiles: any = [];
    if (currentFiles && currentFiles.length > 0) {
      currentFiles.forEach((element: any) => {
        const isExist = fileExists(element.name);
        if (isExist) {
          let name = newFileName(element, 1);
          var newFile = new File([element], name, { type: element.type });
          choseFiles.push(newFile);
        } else {
          choseFiles.push(element);
        }
      });
    } else {
      choseFiles = Array.from(files);
    }
    handleUploadFile(choseFiles);
  };

  const handleRemoveFile = (index: any) => {
    deletSelectedAttachment(uploadedFiles[index], index);
  };

  /**
   * validate form
   */
  const validateForm = () => {
    if (inputFields?.role_name?.length > 0) {
      setIsFormValid(true);
    } else {
      setIsFormValid(false);
    }
  };

  useEffect(() => {
    validateForm();
  }, [inputFields]);

  const postAttachment = async (file: any) => {
    try {
      const formData = new FormData();
      file?.forEach((element: any) => {
        formData.append('Files', element);
      });

      formData.append('apiMethod', 'Post');
      formData.append('apiType', Constants.IAC);
      formData.append('endpoint', `/api/Role/CreateAttachment`);

      const res: any = await callRoleApi(formData, true);
      // const res: any = await addAttachment("Role/CreateAttachment", formData);
      if (res.status === 200) {
        if (res?.data && res?.data.length > 0) {
          const newUploadedFile = [...uploadedFiles];
          Array.prototype.push.apply(newUploadedFile, res.data);
          setUploadedFiles(newUploadedFile);
        }
      }
    } catch (error: any) {
      console.log(error);
    }
  };

  /**
   * Add Role
   */
  const postRole = async () => {
    try {
      const requestData = {
        roleId: 0,
        roleName: inputFields
          ? inputFields?.role_name
            ? inputFields?.role_name
            : null
          : null,
        createdBy: 'Devang',
        mappedUser: mappedDataStr?.addUsersStr,
        attachments: uploadedFiles,
      };
      setComponentLoader(true);
      const payload = {
        endpoint: '/api/Role',
        apiType: Constants.IAC,
        apiMethod: 'Post',
        payload: requestData,
      };
      const res: any = await callRoleApi(payload);
      // const res: any = await addRole('Role', requestData);
      if (res.status === 200) {
        createToast('success', `Role created successfully!`);
        dismissPanel(true);
      } else if (res.status === 203) {
        setIsRoleExist({ visible: true, message: res.data });
      }
    } catch (error: any) {
      if (error.code === 403) {
        setIsRoleExist({ visible: true, message: 'Role Already Exists.' });
      }
      createToast('error', 'Something went wrong. Please try again');
    } finally {
      setComponentLoader(false);
    }
  };

  /**
   * Update Role
   */
  const putRole = async () => {
    try {
      let newAttachment: any = [];
      if (uploadedFiles.length > 0) {
        newAttachment = uploadedFiles?.filter(
          (item: any) => item.roleAttachmentId === undefined
        );
      }
      const requestData = {
        roleId: roleId,
        roleName: inputFields
          ? inputFields?.role_name
            ? inputFields?.role_name
            : null
          : null,
        modifiedBy: 'Devang',
        mappedUser: mappedDataStr?.addUsersStr,
        unMappedUser: mappedDataStr?.deleteUsersStr,
        unMappedtoMappedUser: '',
        attachments: newAttachment,
        // unMappedtoMappedUser: mappedDataStr?.unMappedtoMappedUserStr
      };
      setComponentLoader(true);
      const payload = {
        endpoint: '/api/Role',
        apiType: Constants.IAC,
        apiMethod: 'Put',
        payload: requestData,
      };
      const res: any = await callRoleApi(payload);
      // const res: any = await updateRole("Role", requestData);
      if (res.status === 200) {
        createToast('success', `Role updated successfully!`);
        dismissPanel(true);
      } else if (res.status === 203) {
        setIsRoleExist({ visible: true, message: res.data });
      }
    } catch (error: any) {
      if (error.code === 403) {
        setIsRoleExist({ visible: true, message: 'Role is Already Exist.' });
      }
      createToast('error', 'Something went wrong. Please try again');
    } finally {
      setComponentLoader(false);
    }
  };

  /**
   * Submit Form
   */
  const handleSubmit = async () => {
    if (roleId) {
      putRole();
    } else {
      postRole();
    }
  };

  return (
    <>
      {componentLoader && (
        <div className="component-loader">
          <Loader />
        </div>
      )}
      <AddRole
        businessTypeOptions={businessTypeOptions}
        subTypeOptions={subTypeOptions}
        handleChangeField={handleChangeField}
        inputFields={inputFields}
        handleDropdownChange={handleDropdownChange}
        handleMove={handleMove}
        handleAttachment={handleAttachment}
        handleButtonClick={handleButtonClick}
        visible={visible}
        closeAttachment={closeAttachment}
        uploadedFiles={uploadedFiles}
        handleRemoveFile={handleRemoveFile}
        handleSubmit={handleSubmit}
        isFormValid={isFormValid}
        dismissPanel={dismissPanel}
        isRoleExist={isRoleExist}
        roleId={roleId}
        userDataMapping={userDataMapping}
        roleUserDetails={roleUserDetails}
        attachmentError={attachmentError}
        handleDownload={handleDownload}
      />
    </>
  );
};

export default AddRoleContainer;
