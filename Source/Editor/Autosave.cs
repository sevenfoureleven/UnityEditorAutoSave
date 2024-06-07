using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

[InitializeOnLoad]
public class AutoSave
{

    //static params or some shit (I am extremely professional)
    private static float lastSaveTime;
    private static float saveInterval;
    private static bool autoSaveEnabled;
    private static bool enableStatusMessages;
    private static bool onlyRemindOnSave;
    private static string currentlySavingMessage;
    private static string savedProjectMessage;

    static AutoSave()
    {
        //handling autosave
        LoadSettings();
        EditorApplication.update += Update;
    }

    private static void Update()
    {
        //checking if autosave is enabled and/or if currently in playmode
        if(!EditorApplication.isPlaying)
        {
            if (!autoSaveEnabled) return;

            float currentTime = (float)EditorApplication.timeSinceStartup;
            if (currentTime - lastSaveTime > saveInterval)
            {
                Save();
                lastSaveTime = currentTime;
            }
        }
        else
        {
            return;
        }
    }

    //editor window, obviously.
    [MenuItem("Window/AutoSave Settings")]
    public static void ShowSettings()
    {
        AutoSaveSettings window = (AutoSaveSettings)EditorWindow.GetWindow(typeof(AutoSaveSettings));
        window.Show();
    }

    //save functin
    private static void Save()
    {
        if (enableStatusMessages && !onlyRemindOnSave)
        {
            Debug.Log(currentlySavingMessage);
        }

        EditorSceneManager.SaveOpenScenes();
        AssetDatabase.SaveAssets();

        if (enableStatusMessages)
        {
            Debug.Log(savedProjectMessage);
        }
    }

    //yes.
    private static void LoadSettings()
    {
        lastSaveTime = EditorPrefs.GetFloat("AutoSave_LastSaveTime", 0f);
        saveInterval = EditorPrefs.GetFloat("AutoSave_Interval", 300f); //defaults to a bunch'a minutes, I dunno, can't count
        autoSaveEnabled = EditorPrefs.GetBool("AutoSave_Enabled", true);
        enableStatusMessages = EditorPrefs.GetBool("AutoSave_EnableStatusMessages", true);
        onlyRemindOnSave = EditorPrefs.GetBool("AutoSave_OnlyRemindOnSave", false);
        currentlySavingMessage = EditorPrefs.GetString("AutoSave_CurrentlySavingMessage", "Auto-saving project...");
        savedProjectMessage = EditorPrefs.GetString("AutoSave_SavedProjectMessage", "Auto-save completed.");
    }

    //guess what this does
    public static void SaveSettings(float interval, bool enabled, bool statusMessages, bool remindOnSave, string savingMessage, string savedMessage)
    {
        saveInterval = interval;
        autoSaveEnabled = enabled;
        enableStatusMessages = statusMessages;
        onlyRemindOnSave = remindOnSave;
        currentlySavingMessage = savingMessage;
        savedProjectMessage = savedMessage;

        EditorPrefs.SetFloat("AutoSave_Interval", saveInterval);
        EditorPrefs.SetBool("AutoSave_Enabled", autoSaveEnabled);
        EditorPrefs.SetBool("AutoSave_EnableStatusMessages", enableStatusMessages);
        EditorPrefs.SetBool("AutoSave_OnlyRemindOnSave", onlyRemindOnSave);
        EditorPrefs.SetString("AutoSave_CurrentlySavingMessage", currentlySavingMessage);
        EditorPrefs.SetString("AutoSave_SavedProjectMessage", savedProjectMessage);
    }
}

//settings window
public class AutoSaveSettings : EditorWindow
{
    private float interval;
    private bool enabled;
    private bool statusMessages;
    private bool remindOnSave;
    private string savingMessage;
    private string savedMessage;

    //no.
    private void OnEnable()
    {
        interval = EditorPrefs.GetFloat("AutoSave_Interval", 300f);
        enabled = EditorPrefs.GetBool("AutoSave_Enabled", true);
        statusMessages = EditorPrefs.GetBool("AutoSave_EnableStatusMessages", true);
        remindOnSave = EditorPrefs.GetBool("AutoSave_OnlyRemindOnSave", false);
        savingMessage = EditorPrefs.GetString("AutoSave_CurrentlySavingMessage", "Auto-saving project...");
        savedMessage = EditorPrefs.GetString("AutoSave_SavedProjectMessage", "Auto-save completed.");
    }

    //I loved making this nonsense.
    private void OnGUI()
    {
        GUILayout.Label("AutoSave Settings", EditorStyles.boldLabel);

        enabled = EditorGUILayout.Toggle("Enable AutoSave", enabled);
        interval = EditorGUILayout.FloatField("Save Interval (s)", interval);
        statusMessages = EditorGUILayout.Toggle("Enable Status Messages", statusMessages);
        remindOnSave = EditorGUILayout.Toggle("Only Remind On Save", remindOnSave);
        savingMessage = EditorGUILayout.TextField("Saving Message", savingMessage);
        savedMessage = EditorGUILayout.TextField("Saved Message", savedMessage);

        if (GUILayout.Button("Save Settings"))
        {
            AutoSave.SaveSettings(interval, enabled, statusMessages, remindOnSave, savingMessage, savedMessage);
            Close();
        }
    }
}