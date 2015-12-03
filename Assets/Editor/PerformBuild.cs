using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

class PerformBuild
{
    private const string BuildFolder = "Builds";

    static readonly Regex SceneNameRgx = new Regex(@"(?<=/)([^/]+)(?=.unity)", RegexOptions.IgnoreCase);
    static readonly Regex RemoveNonCharactersRgx = new Regex("[^a-zA-Z]");

	[MenuItem("Build/CommandLineBuildAndroid")]
    public static void CommandLineBuildAndroid ()
    {
        var settings = new Settings(PlayerSettings.productName) {Scenes = GetBuildScenes()};

        var args = System.Environment.GetCommandLineArgs ();
        for (var i = 0; i < args.Length; i++) {
            if (!args[i].Equals("-bversion")) continue;
            Settings.BundleVersionCode = int.Parse(args[i+1]);
            Settings.BundleVersion = args[i+1];
        }
		
        MakeBuild(settings);
    }
	
    [MenuItem("Build/Build Test Release")]
    public static void TestBuild(){
		
        var settings = new Settings("TimeAndStuffTest") {Scenes = GetBuildScenes() };

        MakeBuild(settings);

        System.Diagnostics.Process.Start (Directory.GetCurrentDirectory() + "/" + BuildFolder + "/");
    }

    [MenuItem("Build/Prototype From Open Scene")]
    public static void PrototypeBuild ()
    {

        var settings = new Settings(RemoveNonCharactersRgx.Replace(
                SceneNameRgx.Match(EditorApplication.currentScene).Value, ""))
        {
            Scenes = new[] {EditorApplication.currentScene}
        };

        Debug.Log("Building prototype with scene: " + settings.ApkName);
        MakeBuild(settings);

        System.Diagnostics.Process.Start (Directory.GetCurrentDirectory() + "/" + BuildFolder + "/");
    }

    private static string[] GetBuildScenes()
    {
        return (from e in EditorBuildSettings.scenes where e != null where e.enabled select e.path).ToArray();
    }

    private static void MakeBuild(Settings settings)
    {

        if (settings.Scenes == null || settings.Scenes.Length == 0)
        {
            settings.Reset();
            throw new UnityException("No scenes to build.");
        }

        Directory.CreateDirectory(BuildFolder);

        UnityException exc = null;
        
        try
        {
            BuildPipeline.BuildPlayer(
                settings.Scenes, 
                BuildFolder + "/" + settings.ApkName + ".apk", 
                BuildTarget.Android, BuildOptions.None);
        }
        catch (UnityException e)
        {
            exc = e;
        }

        settings.Reset();
        
        if (exc != null)
            throw exc;
    }

    private class Settings
    {
        private static bool UseApkExpansionFiles
        {
            set { PlayerSettings.Android.useAPKExpansionFiles = value; }
        }

        private string ProductName
        {
            set { PlayerSettings.productName = value;
                ApkName = value;
                PlayerSettings.bundleIdentifier = @"dk.dadiu." + value.ToLower();
            }
        }

        public static int BundleVersionCode
        {
            set { PlayerSettings.Android.bundleVersionCode = value; }
        }

        public static string BundleVersion
        {
            set { PlayerSettings.bundleVersion = value; }
        }

        public string ApkName { get; private set; }
        public string[] Scenes;

        private readonly bool _origUseApkExpansionFiles;
        private readonly string _origBundleIdentifier;
        private readonly string _origProductName;
        private readonly int _origBundleVersionCode;
        private readonly string _origBundleVersion;

        public Settings(string productName)
        {
            if(EditorApplication.currentScene != "")
                EditorApplication.SaveScene();
            _origUseApkExpansionFiles = PlayerSettings.Android.useAPKExpansionFiles;
            _origBundleVersionCode = PlayerSettings.Android.bundleVersionCode;
            _origBundleIdentifier = PlayerSettings.bundleIdentifier;
            _origProductName = PlayerSettings.productName;
            _origBundleVersion = PlayerSettings.bundleVersion;
            ProductName = productName;
            UseApkExpansionFiles = false;
			EditorUserBuildSettings.androidBuildSubtarget = MobileTextureSubtarget.DXT;
			PlayerSettings.keystorePass = "dadiu.2015.team.4";
			PlayerSettings.keyaliasPass = "dadiu.2015.team.4";
        }
        
        public void Reset()
        {
            PlayerSettings.Android.useAPKExpansionFiles = _origUseApkExpansionFiles;
            PlayerSettings.bundleIdentifier = _origBundleIdentifier;
            PlayerSettings.productName = _origProductName;
            PlayerSettings.Android.bundleVersionCode = _origBundleVersionCode;
            PlayerSettings.bundleVersion = _origBundleVersion;
            if (EditorApplication.currentScene != "")
                EditorApplication.SaveScene();
        }

    }
}