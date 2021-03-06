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
                ChangeLayer(BaseSceneManager.instance.loadingScreen);
                LoadLevel(currLayer);
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

    private void LoadLevel(InvokeMenu currLevel)
    {
        SpawnSystem.useBird = currLevel.level.bird;
        //LevelGenerator.GenerateLevel(currLevel.level);

        SceneManager.LoadScene(1 + SceneManager.GetActiveScene().buildIndex);
    }
}
