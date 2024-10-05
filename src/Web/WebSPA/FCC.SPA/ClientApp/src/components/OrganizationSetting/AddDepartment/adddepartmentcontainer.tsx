import AddDepartment from "./adddepartment"

interface IProps {
    dismissPanelDepartment:any
}
const AddDepartmentContainer = ({dismissPanelDepartment}:IProps) => {
    const glDistibutorsCode = [
        { value: 'GL Distributor 45', label: 'GL Distributor 45'},
        { value: 'GL Distributor 87', label: 'GL Distributor 87'},
        { value: 'GL Distributor 98', label: 'GL Distributor 98'}
    ]
    const companyNameOptions = [
        { value: 'JMS Crane and Rigging LLC111', label: 'JMS Crane and Rigging LLC111'},
        { value: 'JMS Crane and Rigging LLC2', label: 'JMS Crane and Rigging LLC2'},
        { value: 'JMS Crane and Rigging LL12', label: 'JMS Crane and Rigging LLC12'}
    ]
    return(
        <AddDepartment 
            dismissPanelDepartment={dismissPanelDepartment} 
            glDistibutorsCode={glDistibutorsCode}
            companyNameOptions={companyNameOptions}
        />
    )
}

export default AddDepartmentContainer