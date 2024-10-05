using Azure.Storage.Blobs;
using Azure.Storage;
using Microsoft.Extensions.Configuration;
using Softura.Azure.Storage.Blobs;
using Softura.Azure.Storage.Blobs.Abstractions;
using Softura.Core.Extensions;
using Softura.Excel.Abstractions;
using Softura.PDF.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Storage.Blobs.Specialized;
using Azure.Storage.Sas;

namespace FCC.Core
{
    public class ExportService<T>
    {
        private readonly IExcelService _excelService;
        private readonly IBlobStorage _blobStorage;
        private readonly IConfiguration _configuration;
        private readonly IPdfService _pdfService;
        private const string pathSeperator = "/";

        public ExportService(IExcelService excelService,IBlobStorage blobStorage, IConfiguration configuration,IPdfService pdfService)
        {
            _excelService = excelService;
            _blobStorage = blobStorage;
            _configuration = configuration;
            _pdfService = pdfService;
        }
        public async Task<BlobFile> ExportURI(List<T> list, string Filetype,string Filename)
        {
            string ContainerName = _configuration.GetValue<string>("BlobContainerName");
            string ContainerFolderName = _configuration.GetValue<string>("BlobContainerExportFolderName");

            byte[] data;            
            BlobFile blobFile = new()
            {
                ContainerName = ContainerName,
                BlobAccessType = BlobAccessType.Blob
            };

            if (Filetype.ToLower() == "excel")
            {
                data = _excelService.CreateDocument(list, "sheet1");
                Filename = Filename + ".xlsx";
                blobFile.Name = $"{ContainerFolderName}{pathSeperator}{Filename}";
                blobFile.File = data;
                blobFile.MimeType = "application/vnd.ms-excel";
            }
            else if (Filetype.ToLower() == "pdf")
            {
                string html = CsvConverter<T>.ConvertListToHtmlTable(list);
                data = _pdfService.HtmlStringToPdf(html);
                Filename = Filename + ".pdf";
                blobFile.Name = $"{ContainerFolderName}{pathSeperator}{Filename}";
                blobFile.File = data;
                blobFile.MimeType = "application/pdf";
            }
            else
            {
                data = CsvConverter<T>.ConvertToCsv(list);
                Filename = Filename + ".csv";
                blobFile.Name = $"{ContainerFolderName}{pathSeperator}{Filename}";
                blobFile.File = data;
                blobFile.MimeType = "text/csv";
            }
            var result = await _blobStorage.UploadAsync(blobFile);
            return result;
        }
    }
}