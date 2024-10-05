import "./leftmenu.scss";
import { logo, close, rightArrow } from "../../assets/images";
import Setting from "./Setting";
import { SearchBox } from "../CommonComponent";

interface IProps {
  leftMenuList?: any;
  handleClickMenu?: any;
  showSettingMenu?: any;
  setShowSettingMenu?: any;
  mobile?: any;
  handleClickCloseMenuMobile?: any;
  wrapperRef?:any
}
const LeftMenu = ({
  leftMenuList,
  handleClickMenu,
  showSettingMenu,
  setShowSettingMenu,
  mobile,
  handleClickCloseMenuMobile,
  wrapperRef
}: IProps) => {
  return (
    <>
      <div className="left-panel-container">
        <div className={`logo-block mb-3 mt-2 ${mobile ? "hide" : "show"}`}>
          <img src={logo} alt="Logo" />
        </div>
        <div className={mobile ? "show" : "hide"}>
          <div className={`mobile-close-menu`}>
            <p>Menu</p>
            <img src={close} alt="Logo" onClick={handleClickCloseMenuMobile} />
          </div>
        </div>
        <div
          className={`search-block-nav-mobile px-3 ${mobile ? "show" : "hide"}`}
        >
          <SearchBox placeholder="Search by" />
        </div>
        <ul className="collapsed-menu-list-block">
          {leftMenuList.map((item: any) => {
            return (
              <li
                className={item.className}
                key={item.id}
                onClick={() => handleClickMenu(item.id)}
                ref={wrapperRef}
                id={item.id}
              >
                <div className="text-block">
                  <img src={item.icon} alt={item.text} />
                  <p>{item.text}</p>
                </div>
                <img
                  src={rightArrow}
                  alt="arrow-icon"
                  className={mobile ? "show mobile-arrow-icon" : "hide"}
                />
              </li>
            );
          })}
        </ul>
        <div className={`profile-block ${mobile ? "hide" : "show"}`}>
          <p>MB</p>
        </div>
      </div>
      {showSettingMenu ? (
        <Setting setShowSettingMenu={setShowSettingMenu} />
      ) : (
        ""
      )}
    </>
  );
};

export default LeftMenu;
