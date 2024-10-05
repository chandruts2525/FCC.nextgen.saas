import { NavMenu, SearchBox } from "../../../components/CommonComponent";
import "./setting.scss";

interface IProps {
  navItemList?: any;
  backButtonClick?: any;
}

const Setting = ({ navItemList, backButtonClick }: IProps) => {
  return (
    <div className="setting-container">
      <div className="search-block-nav">
        <SearchBox placeholder="Search by" />
      </div>
      <div className="back-nav-button" onClick={backButtonClick}>
        <span className="fa fa-angle-left"></span>
        <p className="heading-txt">Settings</p>
      </div>
      <NavMenu navItemList={navItemList} />
    </div>
  );
};

export default Setting;
