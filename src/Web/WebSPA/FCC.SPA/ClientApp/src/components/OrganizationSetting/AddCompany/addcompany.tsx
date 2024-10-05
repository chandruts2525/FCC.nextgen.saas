import { PrimaryButton, SecondaryButton, Tab } from "../../CommonComponent";
import Brand from "./Brand";
import CompanySetting from "./CompanySetting";
import Counters from "./Counters";
import DocSign from "./DocSign";
import Localization from "./Localization";
import "./addcompany.scss";

interface IProps {
    dismissPanel: any,
    collectData: any,
    formData: any,
}
const AddCompany = ({ dismissPanel, collectData, formData }: IProps) => {
    const tabItems = [
        {
            itemKey: "company-setting",
            headerText: "Company Settings",
            content: <CompanySetting collectData={collectData} formData={formData} />
        },
        {
            itemKey: "localization",
            headerText: "Localization",
            content: <Localization collectData={collectData} formData={formData} />
        },
        {
            itemKey: "counters",
            headerText: "Counters",
            content: <Counters collectData={collectData} formData={formData} />
        },
        {
            itemKey: "docu-sign",
            headerText: "DocuSign",
            content: <DocSign collectData={collectData} formData={formData} />
        },
        {
            itemKey: "brand",
            headerText: "Brand",
            content: <Brand />
        }
    ];
    return (<>
        <div className="FCC_SlideContent-wrapper">
        <Tab
            tabItems={tabItems}
        />
        </div>
        <div className='FCC_slide-footer'>
            <SecondaryButton text='Cancel' onClick={() => dismissPanel(false)} />
            <PrimaryButton text='Save' onClick={() => dismissPanel(false)} />
        </div>
    </>);
};

export default AddCompany;