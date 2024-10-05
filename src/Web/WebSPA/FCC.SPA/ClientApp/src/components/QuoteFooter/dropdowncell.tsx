export const DropDownCell = (props: any) => {

  const { dataItem } = props;
  const field = props.field || "";
  const dataValue = dataItem[field] === null ? "" : dataItem[field];

  return (
    <td className="custom-grid-dropdown-cell" key={dataItem.quoteFooterID}>
     {(dataValue === 'Active' || dataValue === true) ? 'Active' : 'Inactive'}
    </td>
  );
};