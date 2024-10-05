import React, {useState} from "react";
import "./attachment.scss";
import {
    close,
    viewIcon,
    downloadIcon,
    deleteIcon,
    uploadIcon,
  } from "../../../assets/images";

interface IProps {
    accept?: string;
    multiple?: boolean;
    onChange?: any;
    closeAttachment?:any;
    className?:string;
    files?:any;
    onRemoveFile?:any;
    previewFile?: any; 
    attachmentError?: any; 
    attachmentHeading:any;
    handleDownload?:any;
}

const Attachment = ({ multiple, onChange, accept,closeAttachment,className, files, onRemoveFile, previewFile,attachmentError,attachmentHeading, handleDownload }: IProps) => {
    // const [componentLoader, setComponentLoader] = useState<boolean>(false)
    const [dragging, setDragging] = useState(false);
    const list = files;
    const handleDragEnter = (e:any) => {
        e.preventDefault();
        setDragging(true);
      };

      const handleDragLeave = () => {
        setDragging(false);
      };

      const handleDrop = (e:any) => {
        e.preventDefault();
        setDragging(false);
        const files = e.dataTransfer.files;
        onChange(files);
      };

    return (
        <div className={`attachment-block ${className}`}>
            <div className="upload-header">
                <span>{attachmentHeading}</span>
                <img
                    src={close}
                    alt="close"
                    className="close-btn"
                    onClick={closeAttachment}
                    onKeyDown={closeAttachment}
                />
            </div>
            <div className="upload-attachments">
                <div className="dragDrop">
                    <div className={`dropzone ${dragging ? 'dragging' : ''}`}
                     onDragEnter={handleDragEnter}
                     onDragOver={(e) => e.preventDefault()}
                     onDragLeave={handleDragLeave}
                     onDrop={handleDrop}
                    >
                        <div className="draggable-container">
                            <div className="header-container-attachment">
                                <div className="sub-header"><img src={uploadIcon} alt='Upoad' className="me-2"/>Drag and Drop your files here (or) <a className='grid_anchor ms-1' href='/' onClick={(e:any) => e.preventDefault(e)} onKeyDown={(e:any) => e.preventDefault(e)}>Browse Files</a></div>    
                                <div className="allowed-format-header">Allowed all files format, Upload files not more than 5MB. Upload filename should be less than 200 characters.</div>                       
                            </div>
                            <input
                                type="file"
                                className="file-browser-input"
                                name="file-browser-input"
                                multiple={multiple}
                                onChange={(e) => {
                                    onChange(e.target.files)
                                    e.target.value = ""
                                }}
                                accept={accept}
                            />
                        </div>
                    </div>
                </div>
                 {/* show this ul tag only when having any validation start */}

                 {attachmentError && <ul className="error-message-block">
                    <li>{attachmentError}</li>
                    {/* <li>Required size is 2MB.</li>
                    <li>Maximun 5 Files can be uploaded.</li> */}
                 </ul>}
                {/* show this ul tag only when having any validation end */}
                {list.length ? (
                    <div className="file-list">
                        {/* { componentLoader &&
                            <div className='component-loader'>
                           <Loader/> */}
                        <table>
                            <tr>
                                <th>Name</th>
                                <th>Created by</th>
                            </tr>
                            {list.map((file:any, index:number) => (
                                <tr key={file}>
                                  <td>{file.filename || file.fileName}</td>
                                  <td>
                                    <span>{file.createdby}</span>
                                    <div className="image-block">
                                        <div>
                                            <img src={viewIcon} onClick={() => previewFile(file)} onKeyDown={() => previewFile(file)} alt='view-icon'/>
                                        </div>
                                        <div>
                                            {/* <a href={file?.fileURI} download={file.filename}>
                                                <img src={downloadIcon} alt='download-icon'/>
                                            </a>    */}
                                            <img src={downloadIcon} alt='download-icon' onClick={() => handleDownload(file)} onKeyDown={() => handleDownload(file)}/>                                         
                                        </div>
                                        <div>
                                            <img src={deleteIcon} alt='delete-icon' onClick={() => onRemoveFile(index)} onKeyDown={() => onRemoveFile(index)}/>
                                        </div>
                                    </div>
                                  </td>
                                </tr>
                            ))}
                        </table>

                       {/* </div>
                   } */}

                    </div>
                ) : (
                    <div className="no-file">No Attachments are uploaded</div>
                )}
            </div>
        </div>
    )
}

export default Attachment