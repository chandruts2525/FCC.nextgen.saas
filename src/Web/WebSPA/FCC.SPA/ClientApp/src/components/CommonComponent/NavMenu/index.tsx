import {Nav} from '@softura/fluentuipackage'
import './navmenu.scss'

const NavMenu = ({navItemList}:any) =>{
    return(
        <Nav groups={navItemList}/>
    )
}

export default NavMenu;