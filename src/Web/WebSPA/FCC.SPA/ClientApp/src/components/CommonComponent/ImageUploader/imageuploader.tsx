import { useState } from "react";
import ImageUploading, { ImageListType } from "react-images-uploading";
import "./imageuploader.scss";

const ImageUploader = () => {
    const [images, setImages] = useState([]);
    const maxNumber = 69;
  
    const onChange = (
      imageList: ImageListType,
      addUpdateIndex: number[] | undefined
    ) => {
      // data for submit
      console.log(imageList, addUpdateIndex);
      setImages(imageList as never[]);
    };
    return(
        <ImageUploading
            multiple={false}
            value={images}
            onChange={onChange}
            maxNumber={maxNumber}
        >
            {({
            imageList,
            onImageUpload,
          //  onImageRemoveAll,
           // onImageUpdate,
            onImageRemove,
            isDragging,
            dragProps
            }:any) => (
            // write your building UI
            <div className="upload__image-wrapper">
                <button
                style={isDragging ? { color: "red" } : undefined}
                onClick={onImageUpload}
                {...dragProps}
                className="upload-image"
                >{ !imageList.length ? <><a className="grid_anchor me-1">Browse</a><span>or Drag Image</span></> : ""}
                </button>
                &nbsp;
                {/* <button onClick={onImageRemoveAll}>Remove all images</button> */}
                {imageList.map((image:any, index:any) => (
                <div key={index} className="image-item">
                    <img src={image.dataURL} alt="" width="100" />
                    <div className="image-item__btn-wrapper">
                    {/* <button onClick={() => onImageUpdate(index)}>Update</button> */}
                    <button onClick={() => onImageRemove(index)}><span className="fa fa-trash-o"></span></button>
                    </div>
                </div>
                ))}
            </div>
            )}
        </ImageUploading>
    )
}

export default ImageUploader