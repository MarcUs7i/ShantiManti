using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using System.IO;

public class BuildDateAutomaticInjector : IPreprocessBuildWithReport
{
    public int callbackOrder => 0;

    public void OnPreprocessBuild(BuildReport report)
    {
        var buildDate = System.DateTime.Now.ToString("dd.MM.yyyy");
        const string path = "Assets/Resources/BuildInfo.txt";
        
        File.WriteAllText(path, buildDate);
        AssetDatabase.Refresh();
    }
}