import * as React from 'react';
import * as ReactDOM from 'react-dom';
import {
  Editor,
  EditorTools,
  EditorUtils,
  ProseMirror,
} from '@progress/kendo-react-editor';
const {
  pasteCleanup,
  sanitize,
  sanitizeClassAttr,
  sanitizeStyleAttr,
  removeAttribute,
  replaceImageSourcesFromRtf,
} = EditorUtils;

const pasteSettings = {
  convertMsLists: true,
  // stripTags: 'span|font'
  attributes: {
    class: sanitizeClassAttr,
    style: sanitizeStyleAttr,
    // keep `width`, `height` and `src` attributes
    width: () => {},
    height: () => {},
    src: () => {},
    // Removes `lang` attribute
    // lang: removeAttribute,

    // removes other (unspecified above) attributes
    '*': removeAttribute,
  },
};

const {
  Bold,
  Italic,
  Underline,
  Strikethrough,
  Subscript,
  Superscript,
  AlignLeft,
  AlignCenter,
  AlignRight,
  AlignJustify,
  Indent,
  Outdent,
  OrderedList,
  UnorderedList,
  Undo,
  Redo,
  FontSize,
  FontName,
  FormatBlock,
  Link,
  Unlink,
  InsertImage,
  ViewHtml,
  InsertTable,
  AddRowBefore,
  AddRowAfter,
  AddColumnBefore,
  AddColumnAfter,
  DeleteRow,
  DeleteColumn,
  DeleteTable,
  MergeCells,
  SplitCell,
} = EditorTools;

const KendoEditor = ({ value, onChange }: any) => {
  const onChangeEditor = (event: any) => {
    onChange(event?.html);
  };

  return (
    <Editor
      tools={[
        [Bold, Italic, Underline, Strikethrough],
        [Subscript, Superscript],
        [AlignLeft, AlignCenter, AlignRight, AlignJustify],
        [Indent, Outdent],
        [OrderedList, UnorderedList],
        FontSize,
        FontName,
        FormatBlock,
        [Undo, Redo],
        // [Link, Unlink, InsertImage, ViewHtml],
        // [InsertTable],
        // [AddRowBefore, AddRowAfter, AddColumnBefore, AddColumnAfter],
        // [DeleteRow, DeleteColumn, DeleteTable],
        // [MergeCells, SplitCell],
      ]}
      onPasteHtml={(event?: any) => {
        let html = pasteCleanup(sanitize(event?.pastedHtml), pasteSettings);

        // If the pasted HTML contains images with sources pointing to the local file system,
        // `replaceImageSourcesFromRtf` will extract the sources from the RTF and place them to images 'src' attribute in base64 format.
        if (event?.nativeEvent?.clipboardData) {
          html = replaceImageSourcesFromRtf(html, event?.nativeEvent?.clipboardData);
        }
        return html;
      }}
      contentStyle={{ height: 630 }}
      value={value}
      onChange={onChangeEditor}
    />
  );
};

export default KendoEditor;
