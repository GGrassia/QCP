using System.Threading;
namespace QCP
{
    public struct FileMover
    {
        private string StartPath { get; }

        private string EndPath { get; }

        public FileMover(string location, string destination)
        {
            StartPath = location;
            EndPath = destination;
        }

        // Move the files
        public void TidyUp()
        {
            System.IO.File.Move(StartPath, EndPath);
        }
    }
}
