import { useEffect, useState } from "react";
import AddCompany from "./addcompany";
interface IProps {
    dismissPanel: any;
}
const AddCompanyContainer = ({ dismissPanel }: IProps) => {
    const [formData, setFormData] = useState<any>({ companyDetailsVM: { status: true } });

    const collectData = (parentKey: any, key: any, val: any, subKey: any) => {
        const data = {
            ...formData,

            [parentKey]: {
                ...formData?.[parentKey],

                ...(subKey ? {
                    [subKey]: {
                        ...formData?.[parentKey]?.[subKey],

                        ...((key.toLowerCase()).includes('phone') ? val :
                            { [key]: val }
                        )
                    }
                } : {
                    ...((key.toLowerCase()).includes('phone') ? val :
                        { [key]: val }
                    )
                })
            }
        };
        setFormData(data);
    };

    useEffect(() => {
        console.log(formData);
    }, [formData]);

    return (
        <AddCompany dismissPanel={dismissPanel} collectData={collectData} formData={formData} />
    );
};

export default AddCompanyContainer;