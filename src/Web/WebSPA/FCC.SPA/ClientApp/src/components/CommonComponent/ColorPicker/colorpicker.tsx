import { useState } from 'react';
import reactCSS from 'reactcss'
import { SketchPicker } from 'react-color'

const ColorPicker = () => {
    const [state, setState] = useState<any>({
        displayColorPicker: false,
        color: {
          r: '241',
          g: '112',
          b: '19',
          a: '1',
        }})

    const handleClick = () => {
        setState({ displayColorPicker: !state.displayColorPicker })
    };
    
    const handleClose = () => {
        setState({ displayColorPicker: false })
    };
    
    const handleChange = (color:any) => {
        setState({ color: color.rgb, displayColorPicker: true  })
    };

    const styles = reactCSS({
        'default': {
          color: {
            width: '36px',
            height: '14px',
            borderRadius: '2px',
            background: `rgba(${ state?.color?.r }, ${ state?.color?.g }, ${ state?.color?.b }, ${ state?.color?.a })`,
          },
          swatch: {
            padding: '5px',
            background: '#fff',
            borderRadius: '1px',
            boxShadow: '0 0 0 1px rgba(0,0,0,.1)',
            display: 'flex',
            justifyContent:'space-between',
            alignItems:'center',
            cursor: 'pointer',
            fontSize:'12px',
            fontWeight:'normal'

          },
          popover: {
            position: 'absolute',
            zIndex: '2',
          },
          cover: {
            position: 'fixed',
            top: '0px',
            right: '0px',
            bottom: '0px',
            left: '0px',
          },
        },
      });

    return(
        <div>
            <div style={ styles.swatch } onClick={handleClick }>
                rgb({state?.color?.r},{state?.color?.g},{state?.color?.b})
            <div style={ styles.color } />
            </div>
            { state.displayColorPicker ? <div style={ styles.popover }>
            <div style={ styles.cover } onClick={ handleClose }/>
            <SketchPicker color={ state.color } onChange={ handleChange } />
            </div> : null }
        </div>
    )
}

export default ColorPicker