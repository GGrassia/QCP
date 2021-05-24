namespace QCP
{
    using System.Collections.Generic;

    // This static class generates the dictionary correlating extensions to generic folder names or specific custom folder.
    public static class DictGen
    {
        public static Dictionary<string, string> GenerateDictionary(List<Correlation> folders, Dictionary<string, string> dict)
        {
            foreach (Correlation entry in folders)
            {
                if (entry.Folder == null)
                {
                    foreach (var ext in entry.Extensions)
                    {
                        dict[ext] = entry.Name;
                    }
                }
                else
                {
                    foreach (var ext in entry.Extensions)
                    {
                        dict[ext] = entry.Folder;
                    }
                }
            }

            return dict;
        }
    }
}
