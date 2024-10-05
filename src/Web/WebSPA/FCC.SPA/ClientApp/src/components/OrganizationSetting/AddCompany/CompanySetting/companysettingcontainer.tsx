import { useEffect, useState } from 'react';
import CompanySetting from './companysetting';

const CompanySettingContainer = ({ collectData, formData }: any) => {
  const [domainIdentifierList, setDomainIdentifierList] = useState<any>([]);
  const [tempList, setTempList] = useState<any>('');
  const [businessAddress, setBusinessAddress] = useState<any>();
  const [mailingAddress, setMailingAddress] = useState<any>();

  const collectBusinessAddress = (key: any, val: any) => {
    setBusinessAddress({
      ...businessAddress,
      [key]: val,
    });
  };
  const collectMailingAddress = (key: any, val: any) => {
    setMailingAddress({
      ...mailingAddress,
      [key]: val,
    });
  };

  useEffect(() => {
    collectData('companyDetailsVM', 'businessAddress', businessAddress);
  }, [businessAddress]);

  useEffect(() => {
    collectData('companyDetailsVM', 'mailingAddress', mailingAddress);
  }, [mailingAddress]);

  useEffect(() => {
    collectData('companyDetailsVM', 'domainIdentifier', domainIdentifierList);
  }, [domainIdentifierList]);

  const handleOnChangeAddDomainIdentifier = (e: any) => {
    let currentName = e.target.value;
    setTempList(currentName);
  };

  const onClickAddDomainIdentifier = (e: any) => {
    const templistarray: any = [];
    setTempList(templistarray);
    domainIdentifierList.push(tempList);
  };

  const removeDomainIdentifier = (itemIndex: any) => {
    const arr: any = JSON.parse(JSON.stringify(domainIdentifierList));
    arr.splice(itemIndex, 1);
    setDomainIdentifierList(arr);
  };

  return (
    <CompanySetting
      domainIdentifierList={domainIdentifierList}
      tempList={tempList}
      removeDomainIdentifier={removeDomainIdentifier}
      onClickAddDomainIdentifier={onClickAddDomainIdentifier}
      handleOnChangeAddDomainIdentifier={handleOnChangeAddDomainIdentifier}
      collectData={collectData}
      formData={formData}
      collectBusinessAddress={collectBusinessAddress}
      collectMailingAddress={collectMailingAddress}
      businessAddress={businessAddress}
      mailingAddress={mailingAddress}
    />
  );
};

export default CompanySettingContainer;
