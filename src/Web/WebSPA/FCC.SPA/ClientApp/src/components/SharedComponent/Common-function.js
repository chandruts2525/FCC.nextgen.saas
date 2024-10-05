import moment from 'moment';
import * as XLSX from 'xlsx';
import { saveAs } from 'file-saver';
import {
  videoExt,
  AudioExt,
  pptExt,
  ImgExt,
  excelExt,
  zippedExt,
  textExt,
  docExt,
  PDFExt,
} from './extension_list';

export function bytesToKB(bytes) {
  if (bytes === 0) return '0 KB'; // Handle special case when size is 0 bytes.
  const kilobytes = bytes / 1024;
  return kilobytes.toFixed(2) + ' KB';
}

export function formateDateTime(datetime) {
  const newFormate = moment(datetime).format('MM/DD/YYYY hh:mm A');
  return newFormate;
}

export function dataExists(data, key, matchedItem) {
  return data.some(function (el) {
    return el[key] === matchedItem;
  });
}

export function filterOptionInArray(data, key, matchedItem) {
  return data?.filter((item) => item[key] !== matchedItem);
}

export function idsString(data, key) {
  let ids = data?.map((item) => item[key]);
  return ids.join();
}

// check object empty or not
export const isObjectEmpty = (value) => {
  return (
    Object.prototype.toString.call(value) === '[object object]' &&
    JSON.stringify(value) === '{}'
  );
};

// open pdf file in new tab
export const downloadFile = (path, filename) => {
  // Create a new link
  const anchor = document.createElement('a');
  anchor.target = '_blank';
  anchor.href = path;
  anchor.download = filename;

  // Append to the DOM
  document.body.appendChild(anchor);

  // Trigger click event
  anchor.click();

  // Remove element from DOM
  document.body.removeChild(anchor);
};

// PDF file download
export const downloadPDFFile = (path, filename) => {
  // Create a new link
  const anchor = document.createElement('a');
  anchor.setAttribute(
    'href',
    `data:text/plain;charset=utf-8, ${encodeURIComponent(path)}`
  );
  anchor.target = '_blank';
  anchor.setAttribute('download', filename);

  // Append to the DOM
  document.body.appendChild(anchor);

  // Trigger click event
  anchor.click();

  // Remove element from DOM
  document.body.removeChild(anchor);
};

export const getBlobUrl = (resData) => {
  const base64WithoutPrefix = resData.substr(''.length);
  const bytes = atob(base64WithoutPrefix);
  let length = bytes.length;
  let out = new Uint8Array(length);
  while (length--) {
    out[length] = bytes.charCodeAt(length);
  }
  return URL.createObjectURL(new Blob([out], { type: 'application/pdf' }));
};

// to get the file name
export function getFileName(file) {
  const name = file.name;
  const lastDot = name.lastIndexOf('.');
  return name.substring(0, lastDot);
}

// get file extension
export function getFileNameExt(name) {
  // const name = file.name;
  const lastDot = name.lastIndexOf('.');
  return name.substring(lastDot + 1);
}

export const base64ToArrayBuffer = (base64) => {
  const binaryString = window.atob(base64);
  const binaryLen = binaryString.length;
  const bytes = new Uint8Array(binaryLen);
  for (let i = 0; i < binaryLen; i++) {
    const ascii = binaryString.charCodeAt(i);
    bytes[i] = ascii;
  }
  return bytes;
};

export const saveByteArray = (fileName, byteData, fileType) => {
  const blob = new Blob([byteData], { type: fileType });
  const link = document.createElement('a');
  link.href = window.URL.createObjectURL(blob);
  link.download = fileName;
  link.click();
};

// get Extension for preview
export function getExtension(ext) {
  let previewType = '';
  const formateVideoExtension = [];
  const formateAudioExtension = [];

  videoExt.forEach((e) => {
    formateVideoExtension.push(`.${e.toUpperCase()}`);
  });

  AudioExt.forEach((e) => {
    formateAudioExtension.push(`.${e.toUpperCase()}`);
  });
  switch (ext) {
    case docExt.includes(ext) && ext:
      previewType = '.DOC';
      break;
    case textExt.includes(ext) && ext:
      previewType = '.TXT';
      break;
    case zippedExt.includes(ext) && ext:
      previewType = '.ZIP';
      break;
    case pptExt.includes(ext) && ext:
      previewType = '.PPT';
      break;
    case excelExt.includes(ext) && ext:
      previewType = '.XLS';
      break;
    case ImgExt.includes(ext) && ext:
      previewType = '.IMG';
      break;
    case formateAudioExtension.includes(ext) && ext:
      previewType = '.AUD';
      break;
    case formateVideoExtension.includes(ext) && ext:
      previewType = '.VID';
      break;
    case PDFExt.includes(ext) && ext:
      previewType = '.PDF';
      break;
    default:
      previewType = '';
      break;
  }
  return previewType;
}

// get FileType by FileExtension
export const getFileTypesByExtension = (ext) => {
  let fileType = '';
  const extension = ext.toUpperCase();
  switch (extension) {
    case 'PDF':
      fileType = 'application/pdf';
      break;
    case 'PNG':
      fileType = 'image/png';
      break;
    case 'JPG' || 'JPEG':
      fileType = 'image/jpeg';
      break;
    case 'PDF':
      fileType = 'application/pdf';
      break;
    case 'XLS':
      fileType = 'application/vnd.ms-excel';
      break;
    case 'XLSX':
      fileType = 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet';
      break;
    case 'DOC':
      fileType = 'application/msword';
      break;
    case 'DOCX':
      fileType =
        'application/vnd.openxmlformats-officedocument.wordprocessingml.document';
      break;
    case 'HTML' || 'HTM':
      fileType = 'text/html';
      break;
    case 'GIF':
      fileType = 'image/gif';
      break;
    case 'PPT':
      fileType = 'application/vnd.ms-powerpoint';
      break;
    case 'PPTX':
      fileType =
        'application/vnd.openxmlformats-officedocument.presentationml.presentation';
      break;
    case 'PPT':
      fileType = 'text/plain';
      break;
    case 'JSON':
      fileType = 'pplication/json';
      break;
    case 'MP3':
      fileType = 'audio/mpeg';
      break;
    case 'MP4':
      fileType = 'video/mp4';
      break;
    default:
      fileType = '';
      break;
  }
  return fileType;
};

// check empty content in rich text
export function isQuillEmpty(value = '') {
  if (value?.replace(/<(.|\n)*?>/g, '').trim().length === 0 && !value.includes('<img')) {
    return true;
  }
  return false;
}

// check string contain special character or not
export function containsSpecialChars(str) {
  const specialChars = `\`!@#$%^&*()_+\-=\[\]{};':"\\|,.<>\/?~`;
  const result = specialChars.split('').some((specialChar) => {
    if (str.includes(specialChar)) {
      return true;
    }
    return false;
  });
  return result;
}

export const csvFileName = (fileName, fileType) => {
  let name = fileName + '_';
  let date = moment().format('DD-MM-YYYY H.mm.ss');
  let fileExtension = '';
  switch (fileType) {
    case 'excel':
      fileExtension = '.xlsx';
      break;
    case 'pdf':
      fileExtension = '.pdf';
      break;
    case 'csv':
      fileExtension = '.csv';
      break;
    default:
      fileType = '';
      break;
  }
  return name + date + fileExtension;
};

/**
 *
 * @param {*} gridData
 * export CSV
 */
export const onExportCSV = (gridData, columnName, fileName) => {
  if (gridData && gridData.length > 0) {
    let dataTable = [];
    let columnHeaders = [];
    gridData.forEach((item) => {
      let excelData = {};
      columnName.forEach((colDef) => {
        if (colDef?.title != 'Action') {
          const colName = colDef?.title;
          const key = colDef?.field;
          if(['createdDate','modifiedDate','createdDateUTC','modifiedDateUTC'].includes(key)){
            excelData[colName] = item[key]? ` ${item[key]} ` :'-';
          }else{
            excelData[colName] = item[key] ?? '';
          }
          if (columnHeaders.length != Object.keys(gridData).length) {
            columnHeaders.push(colName);
          }
        }
      });
      dataTable.push(excelData);
    });
    /* make the worksheet */
    var ws = XLSX.utils.json_to_sheet(dataTable);

    /* write workbook (use type 'binary') */
    var csv = XLSX.utils.sheet_to_csv(ws);

    /* generate a download */
    const s2ab = (s) => {
      var buf = new ArrayBuffer(s.length);
      var view = new Uint8Array(buf);
      for (var i = 0; i != s.length; ++i) view[i] = s.charCodeAt(i) & 0xff;
      return buf;
    };

    saveAs(
      new Blob([s2ab(csv)], { type: 'application/octet-stream' }),
      csvFileName(fileName, 'csv')
    );
  }
};

export const encodeQuery = (params) => {
  let query = '';
  for (let d in params)
    query += encodeURIComponent(d) + '=' + encodeURIComponent(params[d]) + '&';
  return query.slice(0, -1);
};
