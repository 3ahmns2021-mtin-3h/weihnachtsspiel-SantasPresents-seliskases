using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InvokeMenu : MonoBehaviour
{    
    public enum CurrentLayer
    {
        Main,
        Settings,
        LevelSelection,
        LoadingScreen,
    }
    [SerializeReference]
    public CurrentLayer layer;
    [HideInInspector]
    public Level level;

    public void LayerInput(InvokeMenu currLayer)
    {
        switch (currLayer.layer)
        {
            case CurrentLayer.Main:
                ChangeLayer(BaseSceneManager.instance.main);
                break;
            case CurrentLayer.Settings:
                ChangeLayer(BaseSceneManager.instance.settings);
                break;
            case CurrentLayer.LevelSelection:
                ChangeLayer(BaseSceneManager.instance.levelSelection);
                break;
            case CurrentLayer.LoadingScreen:
                StartCoroutine(LoadAsynchronously(currLayer, 1));
                ChangeLayer(BaseSceneManager.instance.loadingScreen);
                break;
        }
    }

    private void ChangeLayer(GameObject layer)
    {
        if(BaseSceneManager.instance.tempLayer != null)
        {
            BaseSceneManager.instance.tempLayer.SetActive(false);
        }

        layer.SetActive(true);
        BaseSceneManager.instance.tempLayer = layer;
    }


    private IEnumerator LoadAsynchronously(InvokeMenu currLevel, int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        GameManager.playedLevel = currLevel;

        while (!operation.isDone)
        {
            float _progress = Mathf.Clamp01(operation.progress / 0.9f);
            BaseSceneManager.instance.loadingSlider.value = _progress;

            yield return null;
        }
    }
}

