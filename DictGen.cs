namespace QCP
{
    using System.Collections.Generic;

    // This static class generates the dictionary correlating extensions to generic folder names or specific custom folder.
    public static class DictGen
    {
        // This method links every extension present to the corresponding custom or generic folder
        public static Dictionary<string, string> GenerateDictionary(List<Correlation> folders, Dictionary<string, string> dict)
        {
            foreach (Correlation entry in folders)
            {
                string[] extensions = entry.Extensions.Split(',');
                if (entry.Folder == "Null" || entry.Folder == "null")
                {
                    foreach (var ext in extensions)
                    {
                        dict[ext] = entry.Name;
                    }
                }
                else
                {
                    foreach (var ext in extensions)
                    {
                        dict[ext] = entry.Folder;
                    }
                }
            }

            return dict;
        }
    }
}
