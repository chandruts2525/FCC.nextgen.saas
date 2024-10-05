import React from 'react'
import DocViewer, { DocViewerRenderers } from "@cyntler/react-doc-viewer";
import ModalPopup from "../ModalPopup";

function DocumentPreview(props) {
  const {docs, handleModalClose} = props;

  // Note: docs data pass in array formate Like below Example

  // const docs = [
  //   { uri: require("../public/file-sample_500kB.doc") },
  //   { uri: require("../public/c4611_sample_explain.pdf") },
  //   { uri: "https://images.unsplash.com/photo-1594818898109-44704fb548f6" }
  // ];

  return (
    <>    
    <ModalPopup
        ShowModalPopupFooter={true}
        ModalPopupTitle="File Preview"
        ModalPopupType="medium"
        FooterSecondaryBtnTxt="No"
        FooterPrimaryBtnTxt="Yes"
        closeModalPopup={()=>handleModalClose(false)}
        PrimaryBtnOnclick={()=>handleModalClose(false)}
        SecondryBtnOnclick={()=>handleModalClose(false)}
        ModalPopupName="confirmation-popup"
    >
      <DocViewer documents={docs} pluginRenderers={DocViewerRenderers} /> 
  </ModalPopup>           
    </>
  )
}

export default DocumentPreview