using UnityEditor;
using System.IO;

public class BuildDateInjector
{
    [MenuItem("Build/Inject Build Date")]
    public static void InjectBuildDate()
    {
        var buildDate = System.DateTime.Now.ToString("dd.MM.yyyy");
        const string path = "Assets/Resources/BuildInfo.txt";
        
        File.WriteAllText(path, buildDate);
        AssetDatabase.Refresh();
    }
}