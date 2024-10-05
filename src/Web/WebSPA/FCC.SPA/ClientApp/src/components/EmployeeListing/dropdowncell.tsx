import * as React from "react";
import { GridCellProps } from "@progress/kendo-react-grid";
import { Dropdown } from "../CommonComponent";

export const DropDownCell = (props: any) => {
  const { dataItem, onChange, defaultValue, statusOptions } = props;
  const field = props.field || "";
  const dataValue = dataItem[field] === null ? "" : dataItem[field];

  return (
    <td className="custom-grid-dropdown-cell">
      {dataItem.inEdit ? (
        <Dropdown
          options={statusOptions}
          defaultValue={defaultValue}
          onChange={(e: any) => {
            onChange && onChange(e);
          }}
        />
      ) : dataValue === "Active" || dataValue === true ? (
        "Active"
      ) : (
        "Inactive"
      )}
    </td>
  );
};
