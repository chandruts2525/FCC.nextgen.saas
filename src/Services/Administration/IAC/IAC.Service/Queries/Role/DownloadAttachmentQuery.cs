using MediatR;

namespace IAC.Service.Queries.Role
{
	public class DownloadAttachmentQuery : IRequest<byte[]?>
    {
        public string FileURI { get; set; }
        public string ContainerName { get; set; }
        public string ContainerFolderName { get; set; }
        public DownloadAttachmentQuery(string _fileURI, string _containerName, string containerFolderName)
        {
            FileURI = _fileURI;
            ContainerName = _containerName;
            ContainerFolderName = containerFolderName;
        }
    }
}
