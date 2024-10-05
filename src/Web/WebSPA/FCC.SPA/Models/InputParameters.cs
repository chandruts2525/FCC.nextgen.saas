namespace FCC.SPA.Models
{
    public class InputParameters
    {
        public object? payload { get; set; }
        public string endpoint { get; set; }
        public string apiType { get; set; }
        public string apiMethod { get; set; }
        public List<IFormFile>? Files { get; set; }
        public string? ContainerFolderName { get; set; }
    }
}
