using IAC.Domain.ViewModel.Role;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace IAC.Service.Commands.Role
{
    public class CreateAttachmentCommand : IRequest<List<CreateAttachmentResponseVM>>
    {
        public List<IFormFile> File { get; set; }
        public string ContainerName { get; set; }
        public string ContainerFolderName { get; set; }

        public CreateAttachmentCommand(List<IFormFile> _file, string _containerName, string _containerFolderName)
        {
            File = _file;
            ContainerName = _containerName;
            ContainerFolderName = _containerFolderName;
        }
    }
}
