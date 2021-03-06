using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(InvokeMenu))]
public class InvokeMenuEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        InvokeMenu invokeMenu = (InvokeMenu)target;

        if (invokeMenu.layer == InvokeMenu.CurrentLayer.LoadingScreen)
        {
            invokeMenu.level = EditorGUILayout.ObjectField("LevelField", invokeMenu.level, typeof(Level), true)
                as Level;
        }
    }
}
