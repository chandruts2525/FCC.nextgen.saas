using MediatR;
using Softura.Azure.Storage.Blobs.Abstractions;
using Microsoft.Extensions.Logging;
using IAC.Service.Queries.Role;

namespace IAC.Service.Handlers.Role
{
	public class DownloadAttachmentQueryHandler : IRequestHandler<DownloadAttachmentQuery, byte[]?>
    {
        private readonly ILogger<DownloadAttachmentQueryHandler> _logger;
        private readonly IBlobStorage _blobStorage;
        private const string pathSeperator = "/";

        public DownloadAttachmentQueryHandler(ILogger<DownloadAttachmentQueryHandler> logger, IBlobStorage blobStorage)
        {
            _blobStorage = blobStorage;
            _logger = logger;
        }

        async Task<byte[]?> IRequestHandler<DownloadAttachmentQuery, byte[]?>.Handle(DownloadAttachmentQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"DownloadAttachmentQueryHandler Start");
            try
            {
                var filename = $"{request.ContainerFolderName}{pathSeperator}{Path.GetFileName(request.FileURI)}";
                if (await _blobStorage.ExistsAsync(filename, request.ContainerName, cancellationToken))
                {
                    var result = await _blobStorage.DownloadAsync(request.FileURI, false);
                    return result;
                }
                return null;
            }
            catch (Exception)
            {
                _logger.LogError($"DownloadAttachmentQueryHandler Error Getting FileByte[]");
                throw;
            }
        }
    }
}
