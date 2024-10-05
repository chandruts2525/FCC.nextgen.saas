import React from "react";
import "./index.scss";

const Loader = () => {
  return (
    <div className="loadingbackdrop">
      <div className="lds-ripple"><div></div><div></div></div>
    </div>
  );
};
export default Loader;


 