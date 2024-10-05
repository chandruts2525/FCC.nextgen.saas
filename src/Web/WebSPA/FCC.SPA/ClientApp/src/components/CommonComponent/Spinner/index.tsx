import React from "react";
import "./spinner.scss";
import { Spinner } from "@softura/fluentuipackage";

const styles = {
  root: {
    position: "fixed",
    top: 0,
    right: 0,
    zIndex: 99999999999999,
    width: "100%",
    height: "100%",
    backgroundColor: "rgba(0, 0, 0, 0.3)",
  },
};

interface IProps {
  text?: string;
}

const SpinnerContainer = ({ text, ...props }: IProps) => {
  return (
    // <div className="approval-loader">
      <Spinner styles={styles} label={text} {...props} />
    // </div>
  );
};

export default SpinnerContainer;
