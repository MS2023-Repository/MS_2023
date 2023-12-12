using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(WeatherManager))]
public class WeatherManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        WeatherManager manager = (WeatherManager)target;

        if (GUI.changed)
        {
            manager.UpdateMaterials();
        }
    }
}