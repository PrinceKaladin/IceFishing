using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;
using System;

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

        // Пути к файлам сборки
        string aabPath = "IceFishing.aab";
        string apkPath = "IceFishing.apk";

        // ⚡ Настройка Android Signing из переменных окружения
        string keystorePath = Environment.GetEnvironmentVariable("CM_KEYSTORE_PATH");
        string keystorePass = Environment.GetEnvironmentVariable("CM_KEYSTORE_PASSWORD");
        string keyAlias = Environment.GetEnvironmentVariable("CM_KEY_ALIAS");
        string keyPass = Environment.GetEnvironmentVariable("CM_KEY_PASSWORD");

        if (!string.IsNullOrEmpty(keystorePath))
        {
            PlayerSettings.Android.useCustomKeystore = true;
            PlayerSettings.Android.keystoreName = keystorePath;
            PlayerSettings.Android.keystorePass = keystorePass;
            PlayerSettings.Android.keyaliasName = keyAlias;
            PlayerSettings.Android.keyaliasPass = keyPass;

            Debug.Log("Android signing configured successfully.");
        }
        else
        {
            Debug.LogWarning("Keystore path not set. APK/AAB will be unsigned.");
        }

        BuildPlayerOptions options = new BuildPlayerOptions
        {
            scenes = scenes,
            target = BuildTarget.Android,
            options = BuildOptions.None
        };

        // === 1. Сборка .AAB ===
        EditorUserBuildSettings.buildAppBundle = true;
        options.locationPathName = aabPath;

        Debug.Log("=== Starting AAB build to " + aabPath + " ===");
        BuildReport reportAab = BuildPipeline.BuildPlayer(options);
        if (reportAab.summary.result == BuildResult.Succeeded)
            Debug.Log("AAB build succeeded! File: " + aabPath);
        else
            Debug.LogError("AAB build failed!");

        // === 2. Сборка .APK ===
        EditorUserBuildSettings.buildAppBundle = false;
        options.locationPathName = apkPath;

        Debug.Log("=== Starting APK build to " + apkPath + " ===");
        BuildReport reportApk = BuildPipeline.BuildPlayer(options);
        if (reportApk.summary.result == BuildResult.Succeeded)
            Debug.Log("APK build succeeded! File: " + apkPath);
        else
            Debug.LogError("APK build failed!");

        Debug.Log("=== Build script finished ===");
    }
}
