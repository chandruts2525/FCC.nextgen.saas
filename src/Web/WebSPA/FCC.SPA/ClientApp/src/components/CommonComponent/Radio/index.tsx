import { Radio } from '@softura/fluentuipackage'

interface IProps {
  options?: any,
  onChange?: Function,
  selectedKey?: any,
  id?:any,
  className?:any
}
const RadioContainer = ({
    options,
    onChange,
    selectedKey,
    id,
    className,
}:IProps) => {
  return (<Radio id={id} options={options} selectedKey={selectedKey} onChange={onChange} className={className}/>)
}

export default RadioContainer
