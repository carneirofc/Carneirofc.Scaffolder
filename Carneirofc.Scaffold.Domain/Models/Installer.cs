namespace Carneirofc.Scaffold.Domain.Models
{
    public class Installer
    {
        public required string Name { get; set; }
        public required string FileName { get; set; }
        public long Size { get; set; }
    }
}