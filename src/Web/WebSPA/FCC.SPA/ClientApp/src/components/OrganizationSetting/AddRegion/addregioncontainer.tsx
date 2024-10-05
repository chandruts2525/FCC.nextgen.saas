import AddRegion from "./addregion"

interface IProps {
    dismissPanelRegion:any
}
const AddRegionContainer = ({dismissPanelRegion}:IProps) => {
    const yardList = [
        {
            id:1,
            yardName:"Albany",
            isChecked:true
        },
        {
            id:2,
            yardName:"Albuqureque",
            isChecked:false
        },
        {
            id:3,
            yardName:"Austin",
            isChecked:true
        },
        {
            id:4,
            yardName:"Augusta",
            isChecked:true
        },
        {
            id:5,
            yardName:"Birmingham",
            isChecked:true
        },
        {
            id:6,
            yardName:"Beaumont",
            isChecked:true
        },
        {
            id:7,
            yardName:"Billings",
            isChecked:true
        }
    ]
    return(
        <AddRegion yardList={yardList} dismissPanelRegion={dismissPanelRegion}/>
    )
}

export default AddRegionContainer