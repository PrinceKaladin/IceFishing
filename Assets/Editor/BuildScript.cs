using UnityEditor;

public class BuildScript
{
    public static void PerformBuild()
    {
        string[] scenes = {
            "Assets/Scenes/settings.unity",
            "Assets/Scenes/gameplay.unity",
            "Assets/Scenes/lose.unity",
            "Assets/Scenes/win.unity",
            "Assets/Scenes/menu.unity",
            "Assets/Scenes/howtoplay.unity"
        };

        string buildPath = "build/Android/IceFishing.apk";
        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions
        {
            scenes = scenes,
            locationPathName = buildPath,
            target = BuildTarget.Android,
            options = BuildOptions.None
        };

        BuildPipeline.BuildPlayer(buildPlayerOptions);
    }
}

