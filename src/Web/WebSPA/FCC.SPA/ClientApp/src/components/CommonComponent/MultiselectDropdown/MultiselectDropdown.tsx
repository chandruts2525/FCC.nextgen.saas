import { MultiSelect } from '@softura/customcomponent';
import './multiselectdropdown.scss';
import { idsString } from '../../SharedComponent/Common-function';

interface IMultiSelect {
  options?: any;
  value?: any;
  onChange?: any;
  closeOnChangedValue?: any;
  disabled?: any;
  disableSearch?: any;
  hasSelectAll?: any;
  errorMessage?: any;
  className?: any;
  selectAllText?: any;
}
const MultiSelectDropdown = ({
  options,
  value,
  onChange,
  closeOnChangedValue,
  disabled,
  disableSearch,
  hasSelectAll = true,
  errorMessage,
  className,
  selectAllText = 'All Selected',
}: any) => {
  const valueRenderer = (selected: typeof options) => {
    if (selected?.length === options?.length) {
      return selectAllText;
    } else if (selected?.length > 0) {
      selected.map(({ label }: any) => label);
    }
  };

  return (
    <div title={idsString(value, 'label')}>
      <MultiSelect
        className={`${errorMessage ? 'controlValidation' : className}`}
        options={options}
        labelledBy="Select"
        value={value}
        onChange={onChange}
        closeOnChangedValue={closeOnChangedValue}
        disabled={disabled}
        disableSearch={disableSearch}
        hasSelectAll={hasSelectAll}
        valueRenderer={valueRenderer}
      />
      {errorMessage && <span className="ms-TextField-errorMessage">{errorMessage}</span>}
    </div>
  );
};

export default MultiSelectDropdown;
