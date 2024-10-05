import React, { useRef, useEffect, useState } from 'react';
import { email, location } from '../../../assets/images';
import './organizationflow.scss';
import { motion } from 'framer-motion';

interface IProps {
  contectMneuData: any,
  companycontectMneuData: any,
  OrganizationListData: any,
  OpenContext: any,
  PosX: any,
  PosY: any;
  SelectedOrganization: any;
  SelectedCompany: any;
  SelectedChildCompany: any;
  CurrentId: any;
  CurrentCompanyId: any;
  setCurrentCompanyId: any;
  CurrentChildCompanyId: any;
  CurrentFlag: any;
  GroupStatus: any;
  companyStateData: any;
  groupcompanyStateData: any;
  RegionsStateData: any;
  companyStateChildData: any;
  commonListData: any;
  contextmenuData: any;
  quicklinkRef: any,
  ContextMenuClick: any;
  OrganizationListArrowClick: any;
  CompanyArrowClick: any;
  CompanyChildArrowClick: any;
  isOptionsVisible: any,
  formData: any,
}

const ContactmenuSwitch: React.FC<any> = ({ option }) => {
  let content;
  switch (option) {
    case 'Companies':
      content = <span className="FCC_organization-caption red">C</span>;
      break;
    case 'Regions':
      content = <span className="FCC_organization-caption yellow">R</span>;
      break;
    case 'Yards':
      content = <span className="FCC_organization-caption brown">Y</span>;
      break;
    default:
      content = <span className="FCC_organization-caption purple">D</span>;
  }
  return <span>{content}</span>;
};

const OrganizationFlow = ({
  OrganizationListData,
  OpenContext,
  PosX,
  PosY,
  SelectedOrganization,
  SelectedCompany,
  SelectedChildCompany,
  CurrentId,
  CurrentCompanyId,
  CurrentChildCompanyId,
  CurrentFlag,
  GroupStatus,
  companyStateData,
  groupcompanyStateData,
  RegionsStateData,
  companyStateChildData,
  commonListData,
  contextmenuData,
  quicklinkRef,
  ContextMenuClick,
  OrganizationListArrowClick,
  CompanyArrowClick,
  CompanyChildArrowClick,
  isOptionsVisible,
  formData,
}: IProps) => {

  const FCCorganizationContextMenu = (props: any) => {
    return (
      <motion.div
        initial={{ opacity: 0, y: -30 }}
        animate={{ opacity: 1, y: 0 }}
        exit={{ opacity: 0, y: -30 }}
        className="FCC_organization-contextMenu"
        style={{ left: props.PosX + 'px', top: props.PosY + 'px' }}
      >
        <ul>
          {props.data.map((item: any) => (
            <li key={item.id} onClick={item.onClick}>
              {item.title !== 'Edit' && item.title !== 'Delete' && (
                <ContactmenuSwitch option={item.flag} />
              )}
              <span className="FCC_organization-captionName">{item.title}</span>
            </li>
          ))}
        </ul>
      </motion.div>
    );
  };

  const CompaniesComp = (props: any) => {
    return (
      <motion.div
        initial={{ opacity: 0, x: -100 }}
        animate={{ opacity: 1, x: 0 }}
        exit={{ opacity: 0, x: -100 }}
        className="FCC_organization-companyMaster"
      >
        <ul>
          {props.data.map((item: any, index: any) => (
            <li key={item.id}>
              <div className="FCC_organization-companyBox">
                <div className="FCC_organization-companyNameWraper">
                  <span className="FCC_organization-companyName">{item?.title}</span>
                  <span className="FCC_organization-Context-dots FCC_dots"></span>
                </div>
                <div className="FCC_organization-companyemail">
                  <span className="FCC_organization-companyemailIcon"></span>
                  <span className="FCC_organization-companyemailtxt">{item?.email}</span>
                </div>
                <div className="FCC_organization-companyaddress">
                  <span className="FCC_organization-companyaddressIcon"></span>
                  <span className="FCC_organization-companyaddresstxt">
                    {item.address}
                  </span>
                </div>
                <span
                  className={`FCC_organization-arrow ${SelectedCompany.includes(item.id) ? 'open' : ''
                    }`}
                  onClick={() => CompanyArrowClick(item.flag, item.child, item)}
                ></span>
              </div>
            </li>
          ))}
        </ul>
      </motion.div>
    );
  };

  const GroupCompaniesComp = (props: any) => {
    return (
      <motion.div
        initial={{ opacity: 0, x: -100 }}
        animate={{ opacity: 1, x: 0 }}
        exit={{ opacity: 0, x: -100 }}
        className="GroupCompanyWrapper"
      >
        <ul>
          {props.data.map((item: any, index: any) => (
            <li key={item.id} className="GroupCompanyWrapperli">
              <div className="groupcompnaybox">
                <div className="groupcompnayheader">
                  <span className="groupcompanyname">{item.title}</span>
                  <span className="FCC_organization-Context-dots FCC_dots"></span>
                </div>
                <div className="groupcompnaybody">
                  <CompaniesComp data={item?.child} />
                </div>
              </div>
            </li>
          ))}
        </ul>
      </motion.div>
    );
  };

  const CompanyChildComp = (props: any) => {
    return (
      <motion.div
        initial={{ opacity: 0, x: -100 }}
        animate={{ opacity: 1, x: 0 }}
        exit={{ opacity: 0, x: -100 }}
        className="FCC_organization-companyChildMaster"
      >
        <ul>
          {props.data.map((item: any, index: boolean) => (
            <li key={item.id}>
              <div className="FCC_organization-companyChildBox">
                <span className="FCC_organization-companyChilname">
                  {item.title} ({item.total})
                </span>
                <span className="FCC_organization-Context-dots FCC_dots"></span>
                <span
                  className={`FCC_organization-arrow ${SelectedChildCompany.includes(item.id) ? 'open' : ''
                    }`}
                  onClick={() => CompanyChildArrowClick(item.flag, item.child, item)}
                ></span>
              </div>
            </li>
          ))}
        </ul>
      </motion.div>
    );
  };

  const CommonListComp = (props: any) => {
    return (
      <motion.div
        initial={{ opacity: 0, x: -100 }}
        animate={{ opacity: 1, x: 0 }}
        exit={{ opacity: 0, x: -100 }}
        className="FCC_organization-thirdChildmaster"
      >
        <ul>
          {props.data.map((item: any, index: any) => (
            <li key={item?.id}>
              <div className="FCC_organization-thirdChildbox">
                <span className="FCC_organization-thirdChild">{item?.title}</span>
                <span className="FCC_organization-Context-dots FCC_dots"></span>
              </div>
            </li>
          ))}
        </ul>
      </motion.div>
    );
  };

  const RegionsComp = (props: any) => {
    return (
      <motion.div
        initial={{ opacity: 0, x: -100 }}
        animate={{ opacity: 1, x: 0 }}
        exit={{ opacity: 0, x: -100 }}
        className="GroupCompanyWrapper"
      >
        <ul className="GroupCompanyWrapperul">
          {props.data.map((item: any, index: any) => (
            <li key={item.id} className="GroupCompanyWrapperli">
              <div className="groupcompnaybox">
                <div className="groupcompnayheader">
                  <span className="groupcompanyname">{item.title}</span>
                  <span className="FCC_organization-Context-dots FCC_dots"></span>
                </div>
                <div className="groupcompnaybody">
                  <CommonListComp data={item?.child} />
                </div>
              </div>
            </li>
          ))}
        </ul>
      </motion.div>
    );
  };

  return (
    <>
      {(formData?.businessEntityID) && <div className="FCC_organization-settingWrapper">
        <div ref={quicklinkRef}>
          {OpenContext && (
            <FCCorganizationContextMenu data={contextmenuData} PosX={PosX} PosY={PosY} />
          )}
        </div>
        <div className="FCC_organization-NameMaster">
          <div className="FCC_organization-setting">
            <span className="FCC_organization-setting-name" title={formData?.organizationName}>{formData?.organizationName}</span>
            <span
              className="FCC_organization-Context-dots FCC_dots"
              data-attribute="refContextClick"
              onClick={(event: any) => ContextMenuClick(event, 'Organization')}
            ></span>
          </div>
          {false && <div className="FCC_organization-optionlist">
            <ul>
              {OrganizationListData.map((item: any, index: number) => {
                // console.log('Array', item);
                return (
                  <li key={item.id}>
                    <ContactmenuSwitch option={item.flag} />
                    <span className="FCC_organization-captionName">
                      {item.total} {item.title}
                    </span>
                    <span
                      onClick={(event: any) => ContextMenuClick(event, item.flag)}
                      data-attribute="refContextClick"
                      className="FCC_organization-Context-dots FCC_dots"
                    ></span>
                    <span
                      className={`FCC_organization-arrow ${SelectedOrganization.includes(item?.id) ? 'open' : ''
                        }`}
                      onClick={() =>
                        OrganizationListArrowClick(
                          item.flag,
                          item.child,
                          item.group,
                          item.groupchild,
                          item
                        )
                      }
                    ></span>
                  </li>
                );
              })
              }
            </ul>
          </div>
          }
        </div>

        {SelectedOrganization.includes(CurrentId) &&
          CurrentFlag === 'Companies' &&
          GroupStatus === false ? (
          <CompaniesComp data={companyStateData} />
        ) : null}

        {SelectedOrganization.includes(CurrentId) && CurrentFlag === 'Regions' ? (
          <RegionsComp data={RegionsStateData} />
        ) : null}

        {SelectedOrganization.includes(CurrentId) && CurrentFlag === 'Yards' ? (
          <CommonListComp data={commonListData} />
        ) : null}

        {SelectedOrganization.includes(CurrentId) && CurrentFlag === 'Department' ? (
          <CommonListComp data={commonListData} />
        ) : null}

        {SelectedOrganization.includes(CurrentId) &&
          CurrentFlag === 'Companies' &&
          GroupStatus === true ? (
          <GroupCompaniesComp data={groupcompanyStateData} />
        ) : null}

        {SelectedCompany.includes(CurrentCompanyId) && CurrentFlag === 'Companies' ? (
          <CompanyChildComp data={companyStateChildData} />
        ) : null}

        {SelectedChildCompany.includes(CurrentChildCompanyId) &&
          CurrentFlag === 'Companies' ? (
          <CommonListComp data={commonListData} />
        ) : null}
      </div>
      }
    </>
  );
};

export default OrganizationFlow;
