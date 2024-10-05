import {TimePicker } from "@softura/fluentuipackage"
import "./index.scss";

const TimePickerComponent = ({value,onChange,increments,dateAnchor,
  showSeconds=true,allowFreeform=true,placeholder="HH:mm:ss",disabled}:any) => {

    return(
      <TimePicker
        value={value}
        onChange={onChange}
        showSeconds={showSeconds}
        allowFreeform={allowFreeform}
        autoComplete={"on"}
        increments={increments}
        dateAnchor={dateAnchor}
        placeholder={placeholder}
        disabled={disabled}
      />
    )
}

export default TimePickerComponent