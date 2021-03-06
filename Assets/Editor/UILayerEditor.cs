using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Unity.EditorCoroutines.Editor;

[CustomEditor(typeof(BaseSceneManager))]
public class UILayerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (!Application.isPlaying)
        {
            BaseSceneManager baseSceneManager = (BaseSceneManager)target;
            switch (baseSceneManager.displayLayer)
            {
                case BaseSceneManager.DisplayLayer.Main:
                    EditorCoroutineUtility.StartCoroutine(ChangeLayer(baseSceneManager.main), this);
                    break;
                case BaseSceneManager.DisplayLayer.Settings:
                    EditorCoroutineUtility.StartCoroutine(ChangeLayer(baseSceneManager.settings), this);
                    break;
                case BaseSceneManager.DisplayLayer.LevelSelection:
                    EditorCoroutineUtility.StartCoroutine(ChangeLayer(baseSceneManager.levelSelection), this);
                    break;
                case BaseSceneManager.DisplayLayer.LoadingScreen:
                    EditorCoroutineUtility.StartCoroutine(ChangeLayer(baseSceneManager.loadingScreen), this);
                    break;
            }
        }
    }

    private IEnumerator ChangeLayer(GameObject layer)
    {
        yield return null;
        BaseSceneManager baseSceneManager = (BaseSceneManager)target;

        if(baseSceneManager.tempLayer != null)
        {
            baseSceneManager.tempLayer.SetActive(false);
        }

        layer.SetActive(true);
        baseSceneManager.tempLayer = layer;
    }
}
