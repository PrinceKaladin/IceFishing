using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;
using System.IO;

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

        // Сохраняем файлы прямо в корень проекта (CM_BUILD_DIR)
        string aabPath = "IceFishing.aab";
        string apkPath = "IceFishing.apk";

        BuildPlayerOptions options = new BuildPlayerOptions
        {
            scenes = scenes,
            target = BuildTarget.Android,
            options = BuildOptions.None
        };

        // 1. Сборка .AAB
        EditorUserBuildSettings.buildAppBundle = true;
        options.locationPathName = aabPath;

        Debug.Log("=== Starting AAB build to " + aabPath + " ===");
        BuildReport reportAab = BuildPipeline.BuildPlayer(options);
        if (reportAab.summary.result == BuildResult.Succeeded)
        {
            Debug.Log("AAB build succeeded! File: " + aabPath);
        }
        else
        {
            Debug.LogError("AAB build failed!");
        }

        // 2. Сборка .APK
        EditorUserBuildSettings.buildAppBundle = false;
        options.locationPathName = apkPath;

        Debug.Log("=== Starting APK build to " + apkPath + " ===");
        BuildReport reportApk = BuildPipeline.BuildPlayer(options);
        if (reportApk.summary.result == BuildResult.Succeeded)
        {
            Debug.Log("APK build succeeded! File: " + apkPath);
        }
        else
        {
            Debug.LogError("APK build failed!");
        }

        Debug.Log("=== Build script finished ===");
    }
}
