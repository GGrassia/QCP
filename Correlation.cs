// This class contains the file extensions corresponding to a folder name and the custom folder.
namespace QCP
{
    // Instances of this class are created by deserializing the settings json.
    public class Correlation
    {
        // Generic folder name.
        public string Name { get; set; }

        // List of extensions corresponding to the folders
        public string Extensions { get; set; }

        // Custom folder path, if present
        public string Folder { get; set; }
    }
}
