using UnityEditor;
using UnityEngine;
using System.IO;

public class BuildScript
{
    public static void PerformBuild()
    {
        // Список всех сцен в проекте
        string[] scenes = {
            "Assets/Scenes/settings.unity",
            "Assets/Scenes/gameplay.unity",
            "Assets/Scenes/lose.unity",
            "Assets/Scenes/win.unity",
            "Assets/Scenes/menu.unity",
            "Assets/Scenes/howtoplay.unity"
        };

        // Путь к папке с артефактами
        string buildFolder = "build/Android";
        Directory.CreateDirectory(buildFolder); // создаём папку, если нет

        string aabPath = Path.Combine(buildFolder, "IceFishing.aab");
        string apkPath = Path.Combine(buildFolder, "IceFishing.apk");

        BuildPlayerOptions options = new BuildPlayerOptions
        {
            scenes = scenes,
            target = BuildTarget.Android,
            options = BuildOptions.None
        };

        // 1. Сборка .AAB (для Google Play)
        EditorUserBuildSettings.buildAppBundle = true;
        options.locationPathName = aabPath;

        Debug.Log("Starting AAB build...");
        BuildReport reportAab = BuildPipeline.BuildPlayer(options);
        if (reportAab.summary.result == BuildResult.Succeeded)
        {
            Debug.Log($"AAB successfully built: {aabPath}");
        }
        else
        {
            Debug.LogError("AAB build failed!");
        }

        // 2. Сборка .APK (для теста)
        EditorUserBuildSettings.buildAppBundle = false;
        options.locationPathName = apkPath;

        Debug.Log("Starting APK build...");
        BuildReport reportApk = BuildPipeline.BuildPlayer(options);
        if (reportApk.summary.result == BuildResult.Succeeded)
        {
            Debug.Log($"APK successfully built: {apkPath}");
        }
        else
        {
            Debug.LogError("APK build failed!");
        }
    }
}
