import * as React from "react";
import { GridCellProps } from "@progress/kendo-react-grid";
import { TextField } from "../CommonComponent";
import { alphaNumericOnlyWithSpace, alphaNumericOnlyWithSpaceOnPaste } from "../../utils/Validators";

export const TextFieldCell = (props: any) => {
  const { dataItem, onChange, defaultValue, placeholder, maxLength } = props;
  const field = props.field || "";
  const dataValue = dataItem[field] === null ? "" : dataItem[field];

  return (
    <td className="custom-grid-dropdown-cell">
      {dataItem.inEdit ? (
        <TextField defaultValue={defaultValue}
          onKeyPress={(e: any) => alphaNumericOnlyWithSpace(e)}
          onPaste={(e: any) => {
            alphaNumericOnlyWithSpaceOnPaste(e)
        }}     
          placeholder={placeholder} maxLength={maxLength} onChange={(e: any) => {
            onChange && onChange(e)
          }} />
      ) : (
        dataValue
      )}
    </td>
  );
};
