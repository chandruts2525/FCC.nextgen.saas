import Setting from "./setting";

interface IProps {
  setShowSettingMenu?: any
}

const SettingContainer = ({ setShowSettingMenu }: IProps) => {
  const navItemList = [
    {
      links: [
        {
          name: 'Global Settings',
          links: [
            {
              name: 'Option Feature',
              links: [
                {
                  name: 'Option Feature 1'
                },
                {
                  name: 'Option Feature 2'
                },
                {
                  name: 'Option Feature 3'
                }
              ]
            },
            {
              name: 'Custom Labels',
            },
            {
              name: 'Task Assistant',
            },
            {
              name: 'Record Numbering Convensions',
            },
            {
              name: 'Field Options',
            },
          ],
        },
        {
          name: 'Security & Compliance',
          links: [
            {
              name: 'Role Management',
              url: '/',
              target: '_self',
            },
            {
              name: 'Role Permissions',
            },
            {
              name: 'Role Permissions Mapping',
            },
            {
              name: 'User Management',
              url: '/UserListing',
              target: '_self',
            },
            {
              name: 'Audit Report',
            },
          ],
        },
        {
          name: 'Customers',
          links: [
            {
              name: 'Customer 1',
            },
            {
              name: 'Customer 2',
            },
            {
              name: 'Customer 3',
            },
            {
              name: 'Customer 4',
            },
          ],
        },
        {
          name: 'Vendors',
          links: [
            {
              name: 'Vendors 1',
            },
            {
              name: 'Vendors 2',
            },
            {
              name: 'Vendors 3',
            },
            {
              name: 'Vendors 4',
            },
          ],
        },
        {
          name: 'Organizations',
          links: [
            {
              name: 'Organizations 1',
            },
            {
              name: 'Organizations 2',
            },
            {
              name: 'Organizations 3',
            },
            {
              name: 'Organizations 4',
            },
          ],
        },
        {
          name: 'Certification',
          links: [
            {
              name: 'Certification 1',
            },
            {
              name: 'Certification 2',
            },
            {
              name: 'Certification 3',
            },
            {
              name: 'Certification 4',
            },
          ],
        },
        {
          name: 'Employees',
          links: [
            {
              name: 'Employees',
            },
            {
              name: 'Termination Reasons',
            },
            {
              name: 'Employee Types',
              url: '/EmployeeListing',
              target: '_self',
            },
            {
              name: 'Absence Reasons',
            },
          ],
        },
        {
          name: 'Resources',
          links: [
            {
              name: 'Schedule Type',
              url: '/ScheduleListing',
              target: '_self',
            },
            {
              name: 'Resource Types',
            },
            {
              name: 'Components/Tools',
            },
            {
              name: 'Units',
            },
            {
              name: 'Down Reasons',
            }
          ],
        },
        {
          name: 'System Settings',
          links: [
            {
              name: 'Industries',
            },
            {
              name: 'Burden Codes',
            },
            {
              name: 'Classifications',
            },
            {
              name: 'Counties',
            },
            {
              name: 'States',
            },
            {
              name: 'Unit of Measure (UoM)',
              url: '/UnitOfMeasure',
              target: '_self',
            },
            {
              name: 'Job Types',
              url: '/JobTypes',
              target: '_self',
            },
            {
              name: 'Countries',
            },
          ],
        },
      ],
    },
  ];

  const backButtonClick = () => {
    return (
      setShowSettingMenu(false)
    )
  }

  return (
    <Setting navItemList={navItemList} backButtonClick={backButtonClick} />
  )
}

export default SettingContainer;
