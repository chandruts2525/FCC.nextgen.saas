import * as React from "react";
import {
  ListBox,
  ListBoxToolbar,
  processListBoxData,  
  ListBoxItemClickEvent,
  ListBoxToolbarClickEvent  
} from "@progress/kendo-react-listbox";
import { Checkbox } from "@progress/kendo-react-inputs";
import './accumulator.scss'
import Search from "../Searchbox";
import {dataExists} from '../../SharedComponent/Common-function';
 

const MyCustomItem = (props: any) => {
  let { dataItem, selected, ...others } = props;  
  return (
    <li {...others} className={`${props.className} ${props.dataItem?.isActive ? "" : "user-inactive"}`}>
      <div className="checkboxContainer">
        <div className="checkboxDiv">
        <Checkbox disabled={props.dataItem?.isActive ? false : true} checked={selected === true && props.dataItem?.isActive ? true : false}/>
        </div>
        <div className="contenttextDiv">
        <p className="header-txt">{props.dataItem.userName}</p>
        
        <p className="subheading-txt">{props?.dataItem?.userRole}</p>
        </div>
      </div>
    </li>
  );
};

const SELECTED_FIELD = "selected";

const Accumulator = ({ sourceListHeading, targetListHeading, handleMove, userDataMapping }: any) => {

  const [state, setState] = React.useState<any>({
    unMappedUser: userDataMapping?.unMappedData,
    mappedUser: userDataMapping?.mappedData,
    draggedItem: {},
  });

  const [originalState, setOriginalState] = React.useState<any>({
    unMappedUser: userDataMapping?.unMappedData,
    mappedUser: userDataMapping?.mappedData,
    draggedItem: {},
  });
  const [userListSearch, setUserListSearch] = React.useState<any>('');
  const [assignUserListSearch, setAssignUserListSearch] = React.useState<any>('');

  React.useEffect(() => {  
    if(!(userListSearch || assignUserListSearch)){         
    setOriginalState({
      unMappedUser: userDataMapping?.unMappedData,
      mappedUser: userDataMapping?.mappedData,
      draggedItem: {},
    })   
  }
  }, [userDataMapping]) 

  React.useEffect(() => {   
    setState({
      unMappedUser: userDataMapping?.unMappedData,
      mappedUser: userDataMapping?.mappedData,
      draggedItem: {},
    })   
  }, [userDataMapping])    

  const handleItemClick = (event: any, data: any, connectedData: any) => {   
    if(event?.dataItem?.isActive){
    const finalState = {
      ...state,
      [data]: state[data].map((item: any) => {
        if (item.userId === event.dataItem.userId) {
          item[SELECTED_FIELD] = !item[SELECTED_FIELD];
        // } else if (!event.nativeEvent.ctrlKey) {
        //   item[SELECTED_FIELD] = false;
        }
        return item;
      }),
      [connectedData]: state[connectedData].map((item: any) => {
        item[SELECTED_FIELD] = false;
        return item;
      }),
    }
    setState(finalState);      
    handleMove(finalState)
  }
  };

  const handleToolBarClick = (e: ListBoxToolbarClickEvent) => {   
    let toolName = e.toolName || "";
    let result = processListBoxData(
      state.unMappedUser,
      state.mappedUser,
      toolName,
      SELECTED_FIELD
    );    
    if(result && result?.listBoxTwoData && toolName === "transferAllTo"){
     
      const currentActiveUserList = result?.listBoxTwoData.filter((item:any) => item.isActive === true)
      // const currentInActiveUserList = result?.listBoxTwoData.filter((item:any) => item.isActive !== true)

      const duplicateMappedData =  state?.mappedUser && state?.mappedUser.length > 0 ? state?.mappedUser.filter((item:any) => item.isActive !== true) : [];  
      const duplicateUnMappedData =  state?.unMappedUser && state?.unMappedUser.length > 0 ? state?.unMappedUser.filter((item:any) => item.isActive !== true) : [];  
      const list1 = [...currentActiveUserList, ...duplicateMappedData]; 
         
      result = {    
        ...result,    
        listBoxOneData: duplicateUnMappedData,
        listBoxTwoData: list1,
      }         
    }
    else if(result && result?.listBoxOneData && toolName === "transferAllFrom"){     
      const currentActiveUserList = result?.listBoxOneData.filter((item:any) => item.isActive)
      // const currentInActiveUserList = result?.listBoxOneData.filter((item:any) => item.isActive !== true)                
      const duplicateMappedData =  state?.mappedUser && state?.mappedUser.length > 0 ? state?.mappedUser.filter((item:any) => item.isActive !== true) : [];  
      const duplicateUnMappedData =  state?.unMappedUser && state?.unMappedUser.length > 0 ? state?.unMappedUser.filter((item:any) => item.isActive !== true) : [];  
      const list1 = [...currentActiveUserList, ...duplicateUnMappedData];             
      result = {
        ...result,
        listBoxOneData: list1,
        listBoxTwoData: duplicateMappedData,
      }     
    }
    else if(result && result?.listBoxTwoData && toolName === "transferTo"){
      const sortedData = result?.listBoxTwoData.sort((a,b) => (a.selected > b.selected) ? -1 : ((b.selected > a.selected) ? 0 : 1))     
      result = {
        ...result,   
        listBoxTwoData: sortedData,
      }
    }
    else if(result && result?.listBoxOneData && toolName === "transferFrom"){
      const sortedData = result?.listBoxOneData.sort((a,b) => (a.selected > b.selected) ? -1 : ((b.selected > a.selected) ? 0 : 1))      
      result = {
        ...result,   
        listBoxOneData: sortedData,
      }
    }
    const finalState = {
      ...state,
      unMappedUser: result.listBoxOneData,
      mappedUser: result.listBoxTwoData,
    }
    setState(finalState);    

    //===============change original state on user assign or unassign
    let resultForOriginal = processListBoxData(
      originalState.unMappedUser,
      originalState.mappedUser,
      toolName,
      SELECTED_FIELD
    );
    if(resultForOriginal && resultForOriginal?.listBoxTwoData && toolName === "transferAllTo"){
     
      const currentActiveUserList = resultForOriginal?.listBoxTwoData.filter((item:any) => item.isActive === true)
      // const currentInActiveUserList = resultForOriginal?.listBoxTwoData.filter((item:any) => item.isActive !== true)

      const duplicateMappedData =  state?.mappedUser && state?.mappedUser.length > 0 ? state?.mappedUser.filter((item:any) => item.isActive !== true) : [];  
      const duplicateUnMappedData =  state?.unMappedUser && state?.unMappedUser.length > 0 ? state?.unMappedUser.filter((item:any) => item.isActive !== true) : [];  
      const list1 = [...currentActiveUserList, ...duplicateMappedData]; 
          
      resultForOriginal = {
        // ...result,
        listBoxOneData: duplicateUnMappedData,
        listBoxTwoData: list1,
      }         
    }
    else if(resultForOriginal && resultForOriginal?.listBoxOneData && toolName === "transferAllFrom"){     
      const currentActiveUserList = resultForOriginal?.listBoxOneData.filter((item:any) => item.isActive)
      // const currentInActiveUserList = resultForOriginal?.listBoxOneData.filter((item:any) => item.isActive !== true)                 
      const duplicateMappedData =  state?.mappedUser && state?.mappedUser.length > 0 ? state?.mappedUser.filter((item:any) => item.isActive !== true) : [];  
      const duplicateUnMappedData =  state?.unMappedUser && state?.unMappedUser.length > 0 ? state?.unMappedUser.filter((item:any) => item.isActive !== true) : [];  
      const list1 = [...currentActiveUserList, ...duplicateUnMappedData];             
      resultForOriginal = {
        ...resultForOriginal,
        listBoxOneData: list1,
        listBoxTwoData: duplicateMappedData,
      }      
    }
    else if(resultForOriginal && resultForOriginal?.listBoxTwoData && toolName === "transferTo"){
      const sortedData = resultForOriginal?.listBoxTwoData.sort((a,b) => (a.selected > b.selected) ? -1 : ((b.selected > a.selected) ? 0 : 1))
      resultForOriginal = {
        ...resultForOriginal,   
        listBoxTwoData: sortedData,
      }
    }
    else if(resultForOriginal && resultForOriginal?.listBoxOneData && toolName === "transferFrom"){
      const sortedData = resultForOriginal?.listBoxOneData.sort((a,b) => (a.selected > b.selected) ? -1 : ((b.selected > a.selected) ? 0 : 1))
      resultForOriginal = {
        ...resultForOriginal,   
        listBoxOneData: sortedData,
      }
    }
    const originalStateNew = {
      ...originalState,
      unMappedUser: resultForOriginal.listBoxOneData,
      mappedUser: resultForOriginal.listBoxTwoData,
    }
    setOriginalState(originalStateNew);

    if(toolName === "transferAllTo"){
      const tempMappedData: any = [];      
      if(finalState && finalState?.mappedUser && finalState?.mappedUser.length > 0){
      finalState?.mappedUser.forEach((element:any) => {
        // tempMappedData.push({...element, selected: element?.isActive ? true : false})
        tempMappedData.push({...element, selected: dataExists(state?.unMappedUser, 'roleId', element?.roleId) && element?.isActive ? true : false})
      });
    }
    handleMove({
      ...finalState,      
      mappedUser: tempMappedData,
    })  
    }
    
    else if(toolName === "transferAllFrom"){   
      const tempUnMappedData: any = [];
      if(finalState && finalState?.unMappedUser && finalState?.unMappedUser.length > 0){
      finalState?.unMappedUser.forEach((element:any) => {        
        // tempUnMappedData.push({...element, selected: element?.isActive ? true : false})
        tempUnMappedData.push({...element, selected: dataExists(state?.mappedUser, 'roleId', element?.roleId) && element?.isActive ? true : false})
      });
    }
    handleMove({
      ...finalState,      
      unMappedUser: tempUnMappedData,
    })  
    }
    else{
      handleMove(finalState)
    }      
    
  };  

  const onChangeSearch = (e: any, type: string) => {
    if(e){
      if (type === "userList" && originalState && originalState.unMappedUser) {
        const filterData = originalState?.unMappedUser.filter((item: any) => item.userName.toLowerCase().indexOf(e?.target?.value.toLowerCase()) !== -1);
        setState({
          ...state,
          unMappedUser: filterData,
        })
        setUserListSearch(e.target.value)
      }
      if (type === "AssignedUserList" && originalState && originalState.mappedUser) {
        const filterData = originalState?.mappedUser.filter((item: any) => item.userName.toLowerCase().indexOf(e?.target?.value.toLowerCase()) !== -1);
        setState({
          ...state,
          mappedUser: filterData,
        })
        setAssignUserListSearch(e.target.value)
      }
    }
  }

  const onClear = (type: string) => {
    if (type === "userList") {
      setState({
        ...state,
        unMappedUser: originalState?.unMappedUser,
      })
      setUserListSearch('');
    }
    if (type === "AssignedUserList") {
      setState({
        ...state,
        mappedUser: originalState?.mappedUser,
      })
      setAssignUserListSearch('');
    }
  }

  //   const handleDragStart = (e: ListBoxDragEvent) => {
  //     setState({
  //       ...state,
  //       draggedItem: e.dataItem,
  //     });
  //   };  

  return (
    <div className="container accumulator-container">
      <div className="row justify-content-center">
        <div className="col">
          <h4 className="mb-2">{sourceListHeading}</h4>
          <Search placeholder="Search User"  className='source-searchbox' onChange={(e: any) => {onChangeSearch(e, "userList"); setUserListSearch(e?.target?.value)}} onClear={() => onClear("userList")} />
          <ListBox
            textField="userName"
            style={{ height: 300, width: "100%", textTransform: "capitalize" }}
            data={state.unMappedUser}
            selectedField={SELECTED_FIELD}
            onItemClick={(e: ListBoxItemClickEvent) =>
              handleItemClick(e, "unMappedUser", "mappedUser")
            }
            // onDragStart={handleDragStart}
            // onDrop={handleDrop}
            item={MyCustomItem}
            toolbar={() => {
              return (
                <ListBoxToolbar
                  tools={[
                    // "moveUp",
                    // "moveDown",
                    "transferTo",
                    "transferFrom",
                    "transferAllTo",
                    "transferAllFrom",
                    // "remove",
                  ]}
                data={state.unMappedUser}                 
                  dataConnected={state.mappedUser}
                  onToolClick={handleToolBarClick}
                />
              );
            }}
          />
        </div>
        <div className="col px-md-0 mt-md-0 mt-5">
          <h4 className="mb-2">{targetListHeading}</h4>
          <Search placeholder="Search User" onChange={(e: any) => {onChangeSearch(e, "AssignedUserList"); setAssignUserListSearch(e?.target?.value)}} onClear={() => onClear("AssignedUserList")} />
          <ListBox
            textField="ProductName"
            style={{ height: 300, width: "100%", textTransform: "capitalize" }}
            data={state.mappedUser}
            selectedField={SELECTED_FIELD}
            onItemClick={(e: ListBoxItemClickEvent) =>
              handleItemClick(e, "mappedUser", "unMappedUser")
            }
            // onDragStart={handleDragStart}
            // onDrop={handleDrop}
            item={MyCustomItem}
          />
        </div>
      </div>
    </div>
  );
};

export default Accumulator
