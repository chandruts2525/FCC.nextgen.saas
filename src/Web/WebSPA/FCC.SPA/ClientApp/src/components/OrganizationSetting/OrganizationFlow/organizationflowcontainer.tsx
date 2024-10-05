import OrganizationFlow from './organizationflow';
import { dashboardIcon } from '../../../assets/images';
import { useEffect, useRef, useState } from 'react';

const OrganizationFlowContainer = ({
  openPanel,
  openPanelRegion,
  openPanelYard,
  openPanelDepartment,
  setAction,
  setConfirmationPopup,
  isOptionsVisible,
  formData,
}: any) => {
  const [OpenContext, setOpenContext] = useState(false);
  const [PosX, setPosX] = useState();
  const [PosY, setPosY] = useState();

  const [SelectedOrganization, setSelectedOrganization] = useState<any>([]);
  const [SelectedCompany, setSelectedCompany] = useState<any>([]);
  const [SelectedChildCompany, setSelectedChildCompany] = useState<any>([]);

  const [CurrentId, setCurrentId] = useState<string>('');
  const [CurrentCompanyId, setCurrentCompanyId] = useState<string>('');
  const [CurrentChildCompanyId, setCurrentChildCompanyId] = useState<string>('');
  const [CurrentFlag, setCurrentFlag] = useState<string>('');
  const [GroupStatus, setGroupStatus] = useState<boolean>(false);

  const [companyStateData, setcompanyStateData] = useState([]);
  const [groupcompanyStateData, setgroupcompanyStateData] = useState([]);
  const [RegionsStateData, setRegionsStateData] = useState([]);
  const [companyStateChildData, setcompanyStateChildData] = useState([]);
  const [commonListData, setcommonListData] = useState([]);
  const [contextmenuData, setcontextmenuData] = useState<any>([]);

  const quicklinkRef = useRef<any>(null);

  const atleastOneCompany = true;

  const contectMneuData = () => {
    if (atleastOneCompany) {
      return [
        {
          id: 'ID02765',
          flag: 'Companies',
          title: 'Add Companies',
          onClick: openPanel,
        },
        {
          id: 'ID02000',
          flag: 'Regions',
          title: 'Add Regions',
          onClick: openPanelRegion,
        },
        {
          id: 'ID02668',
          flag: 'Yards',
          title: 'Add Yards',
          onClick: openPanelYard,
        },
        {
          id: 'ID02225',
          flag: 'Department',
          title: 'Add Department',
          onClick: openPanelDepartment,
        },
        {
          id: 'ID02889',
          flag: 'Edit',
          title: 'Edit',
          onClick: () => {
            setAction('EditOrganization');
          },
        },
        {
          id: 'ID02335',
          flag: 'Delete',
          title: 'Delete',
          onClick: () => {
            setAction('DeleteOrganization');
            setConfirmationPopup(true);
          },
        },
      ];
    } else {
      return [
        {
          id: 'ID02765',
          flag: 'Companies',
          title: 'Add Companies',
          onClick: openPanel,
        },
        {
          id: 'ID02889',
          flag: 'Edit',
          title: 'Edit',
          onClick: () => {
            setAction('EditOrganization');
          },
        },
        {
          id: 'ID02335',
          flag: 'Delete',
          title: 'Delete',
          onClick: () => {
            setAction('DeleteOrganization');
            setConfirmationPopup(true);
          },
        },
      ];
    }
  };

  const companycontectMneuData: any = [
    {
      id: 'ID02000',
      flag: 'Regions',
      title: 'Add Regions',
      onClick: openPanelRegion,
    },
    {
      id: 'ID02668',
      flag: 'Yards',
      title: 'Add Yards',
      onClick: openPanelYard,
    },
    {
      id: 'ID02225',
      flag: 'Department',
      title: 'Add Department',
      onClick: openPanelDepartment,
    },
    {
      id: 'ID02889',
      flag: 'Edit',
      title: 'Edit',
    },
    {
      id: 'ID02335',
      flag: 'Delete',
      title: 'Delete',
    },
  ];

  const OrganizationListData: any = [
    {
      id: 'COID001',
      title: 'Companies',
      flag: 'Companies',
      total: 5,
      expand: false,
      group: false,
      child: [
        {
          id: '1',
          title: 'JMS Crane and Rigging, LLC111',
          email: 'Jmscraneamdrigging@llc.com',
          address: 'Billings, Unit8d States, MT - 59101',
          expand: true,
          regions: 'US',
          child: [
            {
              id: 'CCID02765',
              flag: 'Department',
              title: 'Department',
              total: 12,
              expand: true,
              child: [
                {
                  id: 'CCCID02265',
                  title: 'Bare Rental',
                },
                {
                  id: 'CCCID020100',
                  title: 'Crane',
                },
              ],
            },
            {
              id: 'CCID02000',
              flag: 'Regions',
              title: 'Regions',
              total: 7,
              expand: false,
              child: [
                {
                  id: 'CCID02658',
                  title: 'Dispatch',
                },
                {
                  id: 'CCCID02668',
                  title: 'Safety',
                },
                {
                  id: 'CCCID02669',
                  title: 'Fleet Trucks',
                },
              ],
            },
            {
              id: 'CCID02668',
              flag: 'Yards',
              title: 'Yards',
              total: 10,
              expand: false,
              child: [
                {
                  id: 'CCCID02265',
                  title: 'Bare Rental',
                },
                {
                  id: 'CCCID020100',
                  title: 'Crane',
                },
                {
                  id: 'CCID02658',
                  title: 'Dispatch',
                },
                {
                  id: 'CCCID02668',
                  title: 'Safety',
                },
                {
                  id: 'CCCID02669',
                  title: 'Fleet Trucks',
                },
              ],
            },
          ],
        },
        {
          id: '2',
          title: 'JMS Crane and Rigging, LLC2',
          email: 'Jmscraneamdrigging@llc.com',
          address: 'Billings, United States, MT - 59101',
          expand: false,
          regions: 'US',
          child: [],
        },
        {
          id: '3',
          title: 'JMS Crane and Rigging, LLC3',
          email: 'Jmscraneamdrigging@llc.com',
          address: 'Billings, United States, MT - 59101',
          expand: false,
          regions: 'US',
          child: [],
        },
        {
          id: '4',
          title: 'JMS Crane and Rigging, LLC4',
          email: 'Jmscraneamdrigging@llc.com',
          address: 'Billings, United States, MT - 59101',
          expand: false,
          regions: 'India',
          child: [],
        },
        {
          id: '5',
          title: 'JMS Crane and Rigging, LLC5',
          email: 'Jmscraneamdrigging@llc.com',
          address: 'Billings, United States, MT - 59101',
          expand: false,
          regions: 'India',
          child: [],
        },
      ],
      groupchild: [
        {
          id: 'GRPCP1',
          title: 'India',
          child: [
            {
              id: '1',
              title: 'JMS Crane and Rigging, LLC111',
              email: 'Jmscraneamdrigging@llc.com',
              address: 'Billings, Unit8d States, MT - 59101',
              expand: true,
              regions: 'US',
              child: [
                {
                  id: 'CCID02765',
                  flag: 'Department',
                  title: 'Department',
                  total: 12,
                  expand: true,
                  child: [
                    {
                      id: 'CCCID02265',
                      title: 'Bare Rental',
                    },
                    {
                      id: 'CCCID020100',
                      title: 'Crane',
                    },
                  ],
                },
                {
                  id: 'CCID02000',
                  flag: 'Regions',
                  title: 'Regions',
                  total: 7,
                  expand: false,
                  child: [
                    {
                      id: 'CCID02658',
                      title: 'Dispatch',
                    },
                    {
                      id: 'CCCID02668',
                      title: 'Safety',
                    },
                    {
                      id: 'CCCID02669',
                      title: 'Fleet Trucks',
                    },
                  ],
                },
                {
                  id: 'CCID02668',
                  flag: 'Yards',
                  title: 'Yards',
                  total: 10,
                  expand: false,
                  child: [
                    {
                      id: 'CCCID02265',
                      title: 'Bare Rental',
                    },
                    {
                      id: 'CCCID020100',
                      title: 'Crane',
                    },
                    {
                      id: 'CCID02658',
                      title: 'Dispatch',
                    },
                    {
                      id: 'CCCID02668',
                      title: 'Safety',
                    },
                    {
                      id: 'CCCID02669',
                      title: 'Fleet Trucks',
                    },
                  ],
                },
              ],
            },
            {
              id: '2',
              title: 'JMS Crane and Rigging, LLC2',
              email: 'Jmscraneamdrigging@llc.com',
              address: 'Billings, United States, MT - 59101',
              expand: false,
              regions: 'US',
              child: [],
            },
            {
              id: '3',
              title: 'JMS Crane and Rigging, LLC3',
              email: 'Jmscraneamdrigging@llc.com',
              address: 'Billings, United States, MT - 59101',
              expand: false,
              regions: 'US',
              child: [],
            },
            {
              id: '4',
              title: 'JMS Crane and Rigging, LLC4',
              email: 'Jmscraneamdrigging@llc.com',
              address: 'Billings, United States, MT - 59101',
              expand: false,
              regions: 'India',
              child: [],
            },
            {
              id: '5',
              title: 'JMS Crane and Rigging, LLC5',
              email: 'Jmscraneamdrigging@llc.com',
              address: 'Billings, United States, MT - 59101',
              expand: false,
              regions: 'India',
              child: [],
            },
          ],
        },
        {
          id: 'GRPCM2',
          title: 'US',
          child: [
            {
              id: '1',
              title: 'JMS Crane and Rigging, LLC111',
              email: 'Jmscraneamdrigging@llc.com',
              address: 'Billings, Unit8d States, MT - 59101',
              expand: true,
              child: [
                {
                  id: 'CCID02765',
                  flag: 'Department',
                  title: 'Department',
                  total: 12,
                  expand: true,
                  child: [
                    {
                      id: 'CCCID02265',
                      title: 'Bare Rental',
                    },
                    {
                      id: 'CCCID020100',
                      title: 'Crane',
                    },
                  ],
                },
                {
                  id: 'CCID02000',
                  flag: 'Regions',
                  title: 'Regions',
                  total: 7,
                  expand: false,
                  child: [
                    {
                      id: 'CCID02658',
                      title: 'Dispatch',
                    },
                    {
                      id: 'CCCID02668',
                      title: 'Safety',
                    },
                    {
                      id: 'CCCID02669',
                      title: 'Fleet Trucks',
                    },
                  ],
                },
                {
                  id: 'CCID02668',
                  flag: 'Yards',
                  title: 'Yards',
                  total: 10,
                  expand: false,
                  child: [
                    {
                      id: 'CCCID02265',
                      title: 'Bare Rental',
                    },
                    {
                      id: 'CCCID020100',
                      title: 'Crane',
                    },
                    {
                      id: 'CCID02658',
                      title: 'Dispatch',
                    },
                    {
                      id: 'CCCID02668',
                      title: 'Safety',
                    },
                    {
                      id: 'CCCID02669',
                      title: 'Fleet Trucks',
                    },
                  ],
                },
              ],
            },
            {
              id: '2',
              title: 'JMS Crane and Rigging, LLC2',
              email: 'Jmscraneamdrigging@llc.com',
              address: 'Billings, United States, MT - 59101',
              expand: false,
              child: [],
            },
            {
              id: '3',
              title: 'JMS Crane and Rigging, LLC3',
              email: 'Jmscraneamdrigging@llc.com',
              address: 'Billings, United States, MT - 59101',
              expand: false,
              child: [],
            },
          ],
        },
      ],
    },
    {
      id: 'REID002',
      title: 'Regions',
      flag: 'Regions',
      total: 3,
      expand: false,
      child: [
        {
          id: 'REG001',
          title: 'India',
          child: [
            {
              id: 'CCCID022651',
              title: 'Yards 1',
            },
            {
              id: 'CCCID0201002',
              title: 'Yards 2',
            },
            {
              id: 'CCID026583',
              title: 'Yards 3',
            },
          ],
        },
        {
          id: 'REG002',
          title: 'Us',
          child: [
            {
              id: 'CCCID0226514',
              title: 'Yards 4',
            },
            {
              id: 'CCCID02010025',
              title: 'Yards 5',
            },
            {
              id: 'CCID0265836',
              title: 'Yards 6',
            },
          ],
        },
      ],
    },
    {
      id: 'ID02668',
      title: 'Yards',
      flag: 'Yards',
      total: 6,
      expand: false,
      child: [
        {
          id: 'CCCID022651',
          title: 'Yards 1',
        },
        {
          id: 'CCCID0201002',
          title: 'Yards 2',
        },
        {
          id: 'CCID026583',
          title: 'Yards 3',
        },
        {
          id: 'CCCID026684',
          title: 'Yards 4',
        },
        {
          id: 'CCCID026695',
          title: 'Yards 5',
        },
      ],
    },
    {
      id: 'ID02225',
      title: 'Add Department',
      flag: 'Department',
      total: 3,
      expand: false,
      child: [
        {
          id: 'Depart22651',
          title: 'Department 1',
        },
        {
          id: 'Depart201002',
          title: 'Department 2',
        },
        {
          id: 'CCID026583',
          title: 'Department 3',
        },
        {
          id: 'Depart26684',
          title: 'Department 4',
        },
        {
          id: 'Depart26695',
          title: 'Department 5',
        },
      ],
    },
  ];

  const handleClickOutside = (event: { target: any }) => {
    if (
      quicklinkRef.current &&
      !quicklinkRef.current.contains(event.target) &&
      event.target.getAttribute('data-attribute') != 'refContextClick'
    ) {
      setOpenContext(false);
    }
  };

  useEffect(() => {
    document.addEventListener('click', handleClickOutside, false);
    return () => {
      document.removeEventListener('click', handleClickOutside, false);
    };
  }, []);

  const ContextMenuClick = (event: any, pram: any) => {
    event.preventDefault();
    const clientX = event.clientX;
    const clientY = event.clientY;
    setOpenContext(true);
    setPosX(clientX);
    setPosY(clientY);

    if (pram === 'Organization') {
      //setcontextmenuData([...contectMneuData]);
      setcontextmenuData(() => {
        return contectMneuData();
      });
    } else if (pram === 'Companies') {
      setcontextmenuData(() => [...companycontectMneuData]);
    } else {
      setcontextmenuData(() => [
        {
          id: 'ID02889',
          flag: 'Edit',
          title: 'Edit',
        },
        {
          id: 'ID02335',
          flag: 'Delete',
          title: 'Delete',
        },
      ]);
    }
  };

  const OrganizationListArrowClick = (
    flag: any,
    data: any,
    group: any,
    groupChild: any,
    currentData: any
  ) => {
    let getID = currentData.id;

    setCurrentId(getID);
    setCurrentFlag(flag);
    setGroupStatus(group);

    setCurrentCompanyId('');
    setCurrentChildCompanyId('');
    setSelectedCompany([]);
    setSelectedChildCompany([]);
    if (SelectedOrganization.includes(getID)) {
      setSelectedOrganization(SelectedOrganization.filter((item: any) => item !== getID));
    } else {
      setSelectedOrganization([getID]);
    }
    if (flag === 'Companies') {
      setcompanyStateData(data);
    }
    if (flag === 'Regions') {
      setRegionsStateData(data);
    }
    if (flag === 'Yards' || flag === 'Department') {
      setcommonListData(data);
    }
    if (flag === 'Companies' && group === true) {
      setgroupcompanyStateData(groupChild);
    }
  };

  const CompanyArrowClick = (flag: any, data: any, currentData: any) => {
    let getId = currentData.id;
    setCurrentCompanyId(getId);
    if (SelectedCompany.includes(getId)) {
      setSelectedCompany(SelectedCompany.filter((item: any) => item !== getId));
    } else {
      setSelectedCompany([getId]);
    }

    setCurrentChildCompanyId('');
    setSelectedChildCompany([]);
    setcompanyStateChildData(data);
  };

  const CompanyChildArrowClick = (flag: any, data: any, currentData: any) => {
    let getId = currentData.id;
    setCurrentChildCompanyId(getId);

    if (SelectedChildCompany.includes(getId)) {
      setSelectedChildCompany(SelectedChildCompany.filter((item: any) => item !== getId));
    } else {
      setSelectedChildCompany([getId]);
    }
    setcommonListData(data);
  };

  return (
    <OrganizationFlow
      contectMneuData={contectMneuData}
      companycontectMneuData={companycontectMneuData}
      OrganizationListData={OrganizationListData}
      OpenContext={OpenContext}
      PosX={PosX}
      PosY={PosY}
      SelectedOrganization={SelectedOrganization}
      SelectedCompany={SelectedCompany}
      SelectedChildCompany={SelectedChildCompany}
      CurrentId={CurrentId}
      CurrentCompanyId={CurrentCompanyId}
      setCurrentCompanyId={setCurrentCompanyId}
      CurrentChildCompanyId={CurrentChildCompanyId}
      CurrentFlag={CurrentFlag}
      GroupStatus={GroupStatus}
      companyStateData={companyStateData}
      groupcompanyStateData={groupcompanyStateData}
      RegionsStateData={RegionsStateData}
      companyStateChildData={companyStateChildData}
      commonListData={commonListData}
      contextmenuData={contextmenuData}
      quicklinkRef={quicklinkRef}
      ContextMenuClick={ContextMenuClick}
      OrganizationListArrowClick={OrganizationListArrowClick}
      CompanyArrowClick={CompanyArrowClick}
      CompanyChildArrowClick={CompanyChildArrowClick}
      isOptionsVisible={isOptionsVisible}
      formData={formData}
    />
  );
};

export default OrganizationFlowContainer;
