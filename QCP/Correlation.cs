// This class contains the file extensions corresponding to a folder name and the custom folder.
namespace QCP
{
    public class Correlation
    {
        public string Name { get; set; }

        public string[] Extensions { get; set; }

        // Custom folder path, if present
        public string Folder { get; set; }
    }
}
