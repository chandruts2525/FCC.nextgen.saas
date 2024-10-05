import { PrimaryButton, SecondaryButton, Tab } from '../../CommonComponent';
import Counters from '../AddCompany/Counters';
import YardDetails from './YardDetails';

interface IProps {
  dismissPanelYard: any;
  collectData: any;
  formData: any;
  saveYard: any;
  isFormValid?: any;
}
const AddYard = ({
  dismissPanelYard,
  collectData,
  formData,
  saveYard,
  isFormValid,
}: IProps) => {
  const tabItems = [
    {
      itemKey: 'yard-details',
      headerText: 'Yard Details',
      content: <YardDetails collectData={collectData} formData={formData} />,
    },
    {
      itemKey: 'counters',
      headerText: 'Counters',
      content: <Counters collectData={collectData} formData={formData} />,
    },
  ];

  return (
    <>
      <div className="FCC_SlideContent-wrapper">
        <Tab tabItems={tabItems} />
      </div>
      <div className="FCC_slide-footer">
        <SecondaryButton text="Cancel" onClick={() => dismissPanelYard(false)} />
        <PrimaryButton text="Save" onClick={() => saveYard()} disabled={!isFormValid} />
      </div>
    </>
  );
};

export default AddYard;
