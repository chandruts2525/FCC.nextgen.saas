import * as React from "react";
import './treeview.scss'
import {
  TreeView,
  TreeViewExpandChangeEvent,
  TreeViewCheckChangeEvent,
  TreeViewItemClickEvent,
  processTreeViewItems,
  handleTreeViewCheckChange,
} from "@progress/kendo-react-treeview";
import Theme from '../../../utils/theme'

interface IProps {
    treeData:any,
    checkboxes:boolean,
    customRendering?:any
}
  
const TreeViewComponent = ({treeData,checkboxes,customRendering}:IProps) => {

  const [check, setCheck] = React.useState<any>([]);
  const [expand, setExpand] = React.useState({
    ids: [""],
    idField: "text",
  });
  const [select, setSelect] = React.useState([""]);

  const onItemClick = (event: TreeViewItemClickEvent) => {
    setSelect([event.itemHierarchicalIndex]);
  };

  const onExpandChange = (event: TreeViewExpandChangeEvent) => {
    let ids = expand.ids.slice();
    const index = ids.indexOf(event.item.text);

    index === -1 ? ids.push(event.item.text) : ids.splice(index, 1);
    setExpand({ ids, idField: "text" });
  };

  const onCheckChange = (event: TreeViewCheckChangeEvent) => {
    const settings = {
      singleMode: false,
      checkChildren: true,
      checkParents: true,
    };
    setCheck(handleTreeViewCheckChange(event, check, treeData, settings));
  };

  return (
    <div style={Theme as React.CSSProperties}>
    <TreeView
      data={processTreeViewItems(treeData, {
        select: select,
        check: check,
        expand: expand,
      })}
      expandIcons={true}
      onExpandChange={onExpandChange}
      aria-multiselectable={true}
      onItemClick={onItemClick}
      checkboxes={checkboxes}
      onCheckChange={onCheckChange}      
      item={customRendering} 
    />
    </div>
  );
};

export default TreeViewComponent