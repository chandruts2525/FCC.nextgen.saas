import { useState } from "react"
import AddGroup from "./addgroup"

const AddGroupContainer = ({dismissPanelGroup}:any) => {
    const [selectedCompany, setSelectedCompany] = useState([])

    const getSelectDataCompany = (number = 5) => {
        return Array(number)
        .fill(undefined)
        .map((_, index) =>(
            { id: index, label: `Data-${index}`, value:`Option ${index}` }
          ));
    }

    return(
        <AddGroup 
            setSelectedCompany={setSelectedCompany}
            selectedCompany={selectedCompany}
            getSelectDataCompany={getSelectDataCompany}
            dismissPanelGroup={dismissPanelGroup}
        />
    )
}

export default AddGroupContainer