import React from 'react';
import SunEditor from 'suneditor-react';
// import parser from 'html-react-parser';
import 'suneditor/dist/css/suneditor.min.css';
import {
  align,
  font,
  fontColor,
  fontSize,
  formatBlock,
  hiliteColor,
  horizontalRule,
  lineHeight,
  list,
  paragraphStyle,
  table,
  template,
  textStyle,
  image,
  link
} from 'suneditor/src/plugins';
import './richtextboxeditor.scss';


const RichTextEditor = ({
  value,
 onChange = () => { }, 
//  defaultValue = undefined
 }: any) => {

    // const editorRef = React.useRef<any>(null);
    const [editorValue, setEditorValue] = React.useState(value); 

    // const valueRef = React.useRef();  
    // React.useEffect(() => {
    //   valueRef.current = value;
    // });

    React.useEffect(() => {
      setEditorValue(value)
    }, [value])
        
  return (
    <><div>    
   <SunEditor
        //defaultValue={defaultValue}    
        setContents={editorValue}
        autoFocus={true}
        lang="en"
        setOptions={{
          showPathLabel: false,
          minHeight: '50vh',
          maxHeight: '50vh',
          placeholder: 'Enter your text here!!!',
          plugins: [
            align,
            font,
            fontColor,
            fontSize,
            formatBlock,
            hiliteColor,
            horizontalRule,
            lineHeight,
            list,
            paragraphStyle,
            table,
            template,
            textStyle,
            image,
            link
          ],
          buttonList: [
            ['undo', 'redo'],
            ['font', 'fontSize', 'formatBlock'],
            ['paragraphStyle'],
            [
              'bold',
              'underline',
              'italic',
              'strike',
              'subscript',
              'superscript'
            ],
            ['fontColor', 'hiliteColor'],
            ['removeFormat'],
            '/',
            ['outdent', 'indent'],
            ['align', 'horizontalRule', 'list', 'lineHeight'],
            ['table', 'link', 'image']
          ],
          formats: ['p', 'div', 'h1', 'h2', 'h3', 'h4', 'h5', 'h6'],
          font: [
            'Arial',
            'Calibri',
            'Comic Sans',
            'Courier',
            'Garamond',
            'Georgia',
            'Impact',
            'Lucida Console',
            'Palatino Linotype',
            'Segoe UI',
            'Tahoma',
            'Times New Roman',
            'Trebuchet MS'
          ]
        }}             
        onChange={onChange}
        // ref={editorRef}        
      />           
      
    </div>
    </>

  );
};



export default RichTextEditor;