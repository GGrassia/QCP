using QCP;
using System.Text.Json;
using System;
using System.IO;

namespace QCP_GUI.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public string Greeting => "Welcome to Avalonia!";

        public Startup Startup => JsonSerializer.Deserialize<Startup>(File.ReadAllText(Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory,@"..\..\..\..\", "jsons", "startup.json"))));

    }
}