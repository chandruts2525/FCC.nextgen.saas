import * as React from "react";
import { GridCellProps } from "@progress/kendo-react-grid";
import { TextField } from "../CommonComponent";
import { alphaNumericOnly, checkValidationError } from "../../utils/Validators";

export const TextFieldCell = (props: any) => {

  const { dataItem, onChange, defaultValue, disabled, placeholder, maxLength, onKeyPress, onPaste } = props;
  const field = props.field || "";
  const dataValue = dataItem[field] === null ? "" : dataItem[field];

  return (
    <td className="custom-grid-dropdown-cell">
      {dataItem.inEdit ? (
        // <TextField disabled={field === "code"} value={value ?? dataValue} placeholder={field} onChange={(e: any) => { setValue(e.target.value) }} />
        <TextField
          placeholder={placeholder}
          disabled={disabled}
          maxLength={maxLength}
          defaultValue={defaultValue}
          onKeyPress={onKeyPress}
          onPaste={onPaste}
          onChange={(e: any) => {
            onChange && onChange(e);
          }}
          // {...(checkValidationError('required', value))}
        />
      ) : (
        dataValue
      )}
    </td>
  );
};
