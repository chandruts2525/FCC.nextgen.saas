using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IAC.Service.Commands.Role
{
    public class DeleteAttachmentCommand : IRequest<bool>
    {
        public string FileURI { get; set; }
        public int? RoleAttachmentId { get; set; }
        public string ContainerName { get; set; }
        public string ContainerFolderName { get; set; }
        public DeleteAttachmentCommand(string _fileURI, int? _roleAttachmentId, string _containerFolderName, string _containerName)
        {
            FileURI = _fileURI;
            RoleAttachmentId = _roleAttachmentId;
            ContainerFolderName = _containerFolderName;
            ContainerName = _containerName;
        }
    }
}
