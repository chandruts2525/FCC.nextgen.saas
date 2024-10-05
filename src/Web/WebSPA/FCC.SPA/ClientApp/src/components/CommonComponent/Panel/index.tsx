import {Panel, PanelType} from '@softura/fluentuipackage';
import './panel.scss'
import Theme from '../../../utils/theme';

const Slider = ({onRenderFooterContent,className,children,isOpen,onDismiss,isFooterAtBottom,headerText,type,customWidth}:any) => {
    const panelStyle:any  = {
        content:{
            height:'calc(100vh- 110px)',
            overflow:'auto',
            '@media(min-height: 480px)': {
                height:'calc(100vh - 110px)',
            }
        },
        scrollableContent: {
            overflow:'hidden'
        },
    }
    return(
        <Panel
            style={Theme as React.CSSProperties}
            styles={panelStyle}
            onRenderFooterContent={isFooterAtBottom ? onRenderFooterContent: null }
            closeButtonAriaLabel="Close"
            className={className}
            isOpen={isOpen} 
            onDismiss={onDismiss}
            // type={PanelType.large}
            type={type ? type : PanelType.large}
            isFooterAtBottom={isFooterAtBottom}
            headerText={headerText}
            customWidth={type === PanelType.custom  ? customWidth : undefined}
            allowTouchBodyScroll={true}
            isBlocking={true}
           // overlayProps={{ className: "foo", styles: { root: { position: "relative" } } }}
        >
        {children}
        </Panel>
    )
}

export default Slider