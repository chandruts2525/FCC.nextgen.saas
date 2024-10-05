import * as React from "react";
import './index.scss';
import {editpen_White, save_White, close_White} from '../../../assets/images';

export const MyCommandCell = (props: any) => {

  const { dataItem, handlePopupChange, actionEnum, id } = props;
  const inEdit = dataItem[props.editField];
  const isNewItem = dataItem[id] === undefined;
  // console.log(deleteConfirmationPopup,'deleteConfirmationPopup')

  return (<> {inEdit ? (
    <td className="k-command-cell text-center">
      <button
        className="editbtn k-button k-grid-save-command"
        onClick={() =>
          isNewItem ? props.add(dataItem) : props.update(dataItem)
        }
      >
        <img src={save_White} alt="save" className="IconImg" title='Save'/>
        {/* {isNewItem ? "Add" : "Update"} */}
      </button>
      <button
        className="editbtn k-button k-grid-cancel-command"
        onClick={() => {
          handlePopupChange(true, actionEnum.discard, dataItem)
          // isNewItem ? props.discard(dataItem) : props.cancel(dataItem)
        }
        }
      >
        <span className="fa fa-close IconImg" title='Cancel'></span>
        {/* <img src={close_White} alt="cancel" className="IconImg" /> */}
        {/* {isNewItem ? "Discard" : "Cancel"} */}
      </button>
    </td >
  ) : (
    <td className="k-command-cell text-center">
      <button
        className="editbtn k-primary k-button k-grid-edit-command"
        onClick={() => props.edit(dataItem)}
      >
        <img src={editpen_White} alt="edit" className="IconImg" title='Edit'/>
        {/* Edit */}
      </button>
      <button className="editbtn k-button k-grid-cancel-command" onClick={() => {
        // eslint-disable-next-line no-restricted-globals
        // confirm("Confirm deleting: " + dataItem.ProductName) &&
        handlePopupChange(true, actionEnum.delete, dataItem)
        // props.remove(dataItem)
      }}>
        <img src={close_White} alt="cancel" className="IconImg" title='Delete' />
      </button>
    </td>
  )
  }

  </>
  )
}