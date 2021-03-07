using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof (Level))]
public class LevelEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Level level = (Level)target;

        for(int i = 0; i < level.colorMappings.Length; i++)
        {
            switch (level.colorMappings[i].type)
            {
                case Level.ColorToPrefab.Type.Ice:
                    level.colorMappings[i].prefab = level.icePrefab;
                    break;
                case Level.ColorToPrefab.Type.Sack:
                    level.colorMappings[i].prefab = level.sackPrefab;
                    break;
                case Level.ColorToPrefab.Type.Weihnachtsmann:
                    level.colorMappings[i].prefab = level.weihnachtsmannPrefab;
                    break;
            }
        }
    }
}
