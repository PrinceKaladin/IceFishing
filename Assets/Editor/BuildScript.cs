using UnityEditor;
using UnityEditor.Build.Reporting; 
using UnityEngine;
using System.IO;

public class BuildScript
{
    public static void PerformBuild()
    {
        // Список сцен
        string[] scenes = {
            "Assets/Scenes/settings.unity",
            "Assets/Scenes/gameplay.unity",
            "Assets/Scenes/lose.unity",
            "Assets/Scenes/win.unity",
            "Assets/Scenes/menu.unity",
            "Assets/Scenes/howtoplay.unity"
        };

        // Папка для билдов
        string buildFolder = "build/Android";
        Directory.CreateDirectory(buildFolder);

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

        Debug.Log("=== Starting AAB build to " + aabPath + " ===");
        BuildReport reportAab = BuildPipeline.BuildPlayer(options);
        if (reportAab.summary.result == BuildResult.Succeeded)
        {
            Debug.Log("AAB build succeeded!");
        }
        else
        {
            Debug.LogError("AAB build failed! Total errors: " + reportAab.summary.totalErrors);
            // Не прерываем выполнение — продолжим пытаться собрать APK
        }

        // 2. Сборка .APK (для теста)
        EditorUserBuildSettings.buildAppBundle = false;
        options.locationPathName = apkPath;

        Debug.Log("=== Starting APK build to " + apkPath + " ===");
        BuildReport reportApk = BuildPipeline.BuildPlayer(options);
        if (reportApk.summary.result == BuildResult.Succeeded)
        {
            Debug.Log("APK build succeeded!");
        }
        else
        {
            Debug.LogError("APK build failed! Total errors: " + reportApk.summary.totalErrors);
        }

        Debug.Log("=== Build script finished ===");
    }
}
