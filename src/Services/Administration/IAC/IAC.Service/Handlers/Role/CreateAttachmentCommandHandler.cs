using IAC.Domain.ViewModel.Role;
using IAC.Service.Commands.Role;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Softura.Azure.Storage.Blobs;
using Softura.Azure.Storage.Blobs.Abstractions;
using System.Text.RegularExpressions;

namespace IAC.Service.Handlers.Role
{
    public class CreateAttachmentCommandHandler : IRequestHandler<CreateAttachmentCommand, List<CreateAttachmentResponseVM>>
    {
        private readonly ILogger<CreateAttachmentCommandHandler> _logger;
        private readonly IBlobStorage _blobStorage;
        private const string pathSeperator = "/";
        public CreateAttachmentCommandHandler(ILogger<CreateAttachmentCommandHandler> logger, IBlobStorage blobStorage)
        {
            _logger = logger;
            _blobStorage = blobStorage;
        }

        async Task<List<CreateAttachmentResponseVM>> IRequestHandler<CreateAttachmentCommand, List<CreateAttachmentResponseVM>>.Handle(CreateAttachmentCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"FileUploadCommandHandler Start");
            var results = new BlobFile();
            List<CreateAttachmentResponseVM> fileReturnData = new();
            try
            {
                foreach (IFormFile formFile in request.File)
                {
                    using var ms = new MemoryStream();
                    await formFile.CopyToAsync(ms, cancellationToken);
                    var Filename = Path.GetFileNameWithoutExtension(Regex.Replace(formFile.FileName, "[^a-zA-Z0-9_.]+", "", RegexOptions.Compiled, TimeSpan.FromMilliseconds(100))) + Guid.NewGuid() + Path.GetExtension(formFile.FileName);
                    BlobFile blobFile = new BlobFile()
                    {
                        ContainerName = request.ContainerName,
                        Name = $"{request.ContainerFolderName}{pathSeperator}{Filename}",
                        MimeType = formFile.ContentType,
                        File = ms.ToArray(),
                        BlobAccessType = BlobAccessType.Blob
                    };
                    results = await _blobStorage.UploadAsync(blobFile);
                    fileReturnData.Add(new CreateAttachmentResponseVM { FileName = formFile.FileName, uploadFilename = results.Name, FileURI = results.Uri.ToString() });
                }

                _logger.LogInformation($"FileUploadCommandHandler END");
                return fileReturnData;
            }
            catch (Exception)
            {
                _logger.LogError($"FileUploadCommandHandler Error Uploading Blob Storage.");
                throw;
            }
        }
    }
}
