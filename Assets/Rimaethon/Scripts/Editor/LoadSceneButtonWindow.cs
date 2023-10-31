using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class LoadSceneButtonWindow : EditorWindow
{
    [MenuItem("Window/Load Scene Additively")]
    private static void OpenWindow()
    {
        LoadSceneButtonWindow window = GetWindow<LoadSceneButtonWindow>();
        window.titleContent = new GUIContent("Load Scene");
        window.Show();
    }

    private void OnGUI()
    {
        GUILayout.Label("Click the button below to load the Initialization scene additively.");
        
        if (GUILayout.Button("Load Scene"))
        {
            if (!EditorApplication.isPlaying)
            {
                Debug.LogError("Cannot load scene additively while in edit mode. Enter Play mode to use this feature.");
                return;
            }

            // Replace "YourScene" with the name of your scene
            EditorSceneManager.LoadSceneInPlayMode("Assets/Rimaethon/_Scenes/Initialization.unity", new LoadSceneParameters(LoadSceneMode.Additive));
            Scene activeScene = EditorSceneManager.GetActiveScene();
            EditorSceneManager.LoadScene(activeScene.path);
        }
    }
}