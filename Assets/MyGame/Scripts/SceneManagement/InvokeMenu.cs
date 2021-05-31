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
            case CurrentLayer.LoadingScreen:
                StartCoroutine(BaseSceneManager.instance.LoadAsynchronously(currLayer, 1));
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
}

