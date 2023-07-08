using UnityEditor;
using UnityEngine;

public class TimeScaleHelper : EditorWindow
{

    [MenuItem("Tools/Utils/Time Scale")]
    private static void Init()
    {
        EditorWindow.GetWindow<TimeScaleHelper>("Time Scale");
    }

    private void OnGUI()
    {
        if(!EditorApplication.isPlaying)
        {
            GUILayout.Label("Time Scale can be updated in play mode only!");
        }
        
        EditorGUI.BeginDisabledGroup(!EditorApplication.isPlaying);
        GUILayout.BeginVertical();
        GUILayout.BeginHorizontal();
        Time.timeScale = EditorGUILayout.Slider(Time.timeScale, 0, 10);

   
        GUILayout.EndHorizontal();
        GUILayout.EndVertical();
        GUILayout.Label("Time Scale: " + Time.timeScale);
        EditorGUI.EndDisabledGroup();
        Repaint();
    }
}