import React, { useRef, useEffect } from 'react';
import ReactQuill, { Quill } from 'react-quill';
import 'react-quill/dist/quill.snow.css';
import './editor.scss';
import ImageResize from 'quill-image-resize-module-react';

declare global {
  interface Window {
    Quill: any;
  }
}

const QuillRichTextEditor = ({
  value,
  onChange,
  onFocus = () => {},
  readonly = false,
}: any) => {
  const quillRef = useRef<any>(null);
  const quillStyle: any = {
    whiteSpace: 'pre-wrap',
  };
  const handleContainerClick = () => {
    const quill = quillRef?.current?.getEditor();
    // Check if the editor is empty (no content)
    const isEditorEmpty = quill?.getLength() === 1 && !quill?.getText()?.trim();
    // Set focus on the editor when it's empty and clicked
    if (isEditorEmpty) {
      quill?.focus();
    }
  };
  useEffect(() => {
    // Get the Quill container element by class name
    const quillContainer = document.querySelector('.ql-container');
    // Define the onClick event handler for the .ql-container element
    if (quillContainer) {
      // Attach the event handler to the .ql-container element
      quillContainer?.addEventListener('click', handleContainerClick);
      // Clean up the event listener when the component unmounts
      return () => {
        quillContainer?.removeEventListener('click', handleContainerClick);
      };
    }
  }, []);
  Quill.register('modules/imageResize', ImageResize);
  window.Quill = Quill;
  return (
    <>
      <div>
        <ReactQuill
          className={`${readonly ? 'disable' : ''}`}
          onFocus={onFocus}
          readOnly={readonly}
          ref={quillRef}
          theme="snow"
          value={value}
          onChange={onChange}
          style={quillStyle}
          placeholder="Add Description here"
          modules={{
            toolbar: [
              [{ size: ['small', 'large', 'huge'] }], // custom dropdown
              [{ header: [1, 2, 3, 4, 5, 6], font: [] }],
              ['bold', 'italic', 'underline', 'strike', 'blockquote'],
              [
                { list: 'ordered' },
                { list: 'bullet' },
                { indent: '-1' },
                { indent: '+1' },
              ],
              //['link', 'image'],//'video'
              [
                {
                  color: [
                    '#000000', // Black
                    '#FF0000', // Red
                    '#00FF00', // Green
                    '#0000FF', // Blue
                    '#FFFF00', // Yellow
                    '#FF00FF', // Magenta
                    '#00FFFF', // Cyan
                    '#FFA500', // Orange
                    '#800080', // Purple
                    '#008000', // Dark Green
                    '#808080', // Gray
                    '#FFC0CB', // Pink
                    '#FFD700', // Gold
                    '#A52A2A', // Brown
                    '#FF4500', // Orange-Red
                    '#800000', // Maroon
                    '#008080', // Teal
                    '#800080', // Purple
                    '#FF1493', // Deep Pink
                    '#FFC3A0', // Pastel Apricot
                    '#FF677D', // Pastel Pink
                    '#C7CEEA', // Pastel Lavender
                    '#A7E9E1', // Pastel Turquoise
                    '#FFDFD3', // Pastel Peach
                    '#FFD700', // Pastel Yellow
                    '#FFACB7', // Pastel Coral
                    '#FFB6C1', // Pastel Pink
                    '#A2C7E2', // Pastel Blue
                  ],
                },
              ],
              //[{ 'table': true }],
              ['clean'],
            ],
            clipboard: {
              // toggle to add extra line breaks when pasting HTML:
              matchVisual: false,
            },
            imageResize: {
              parchment: Quill.import('parchment'),
              modules: ['Resize', 'DisplaySize'],
              displayStyles: {
                backgroundColor: 'black',
                border: 'none',
                color: 'white',
                // other camelCase styles for size display
              },
              toolbarStyles: {
                backgroundColor: 'black',
                border: 'none',
                color: 'white',
                // other camelCase styles for size display
              },
            },
          }}
          formats={[
            'header',
            'size',
            'font',
            'bold',
            'italic',
            'underline',
            'strike',
            'blockquote',
            'list',
            'bullet',
            'indent',
            'link',
            'image',
            'color', //'video',
            'alt',
            'height',
            'width',
            'style',
            'size',
          ]}
        />
      </div>
    </>
  );
};

export default QuillRichTextEditor;
