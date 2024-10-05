import DocSign from "./docsign";

const DocSignContainer = ({ collectData, formData }: any) => {
    return (
        <DocSign collectData={collectData} formData={formData} />
    );
};

export default DocSignContainer;