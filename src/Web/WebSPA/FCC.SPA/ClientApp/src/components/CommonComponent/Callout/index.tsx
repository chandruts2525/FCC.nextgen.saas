import { Callout } from "@softura/fluentuipackage";
interface IProps {
  children?: any;
  toggleIsCalloutVisible?: Function;
  targetId?: any;
  className?:string,
  calloutMaxHeight?:any,
  calloutWidth?:any
}
const CalloutContainer = ({
  children = null,
  toggleIsCalloutVisible,
  targetId,
  className,
  calloutMaxHeight,
  calloutWidth
}: IProps) => {
  return (
    <Callout
      targetID={targetId}
      toggleIsCalloutVisible={toggleIsCalloutVisible}
      className={className}
      calloutMaxHeight={calloutMaxHeight}
      calloutWidth={calloutWidth}
    >
      {children}
    </Callout>
  );
};

export default CalloutContainer;
