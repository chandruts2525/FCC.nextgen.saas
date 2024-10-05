using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IAC.Domain.ViewModel.Role
{
    public class GetRoleAttachmentResponseVM
    {
        [Key]
        public int RoleAttachmentId { get; set; }
        public int RoleId { get; set; }
        public string? FileName { get; set; }
        public string? FileURI { get; set; }
        public string? Createdby { get; set; }
    }
}
