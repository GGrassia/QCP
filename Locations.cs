// This class stores the location and destination of the single files and moves them
namespace QCP
{
    public struct Locations
    {
        private string StartPath { get; set; }

        private string EndPath { get; set; }

        public Locations(string location, string destination)
        {
            StartPath = location;
            EndPath = destination;
        }

        // Move the files
        public static void TidyUp(Locations file)
        {
            System.IO.File.Move(file.StartPath, file.EndPath);
        }
    }
}
