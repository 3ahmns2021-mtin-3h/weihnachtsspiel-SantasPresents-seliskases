using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BaseSceneManager : MonoBehaviour
{
    public GameObject layerBase;
    public Slider loadingSlider;
    [HideInInspector]
    public GameObject tempLayer;

    [Header("Layers")]
    public GameObject main;
    public GameObject settings;
    public GameObject levelSelection;
    public GameObject loadingScreen;

    [Header("Custom Inspector")]
    public DisplayLayer displayLayer;

    public static BaseSceneManager instance;

    public enum DisplayLayer
    {
        Main,
        Settings,
        LevelSelection,
        LoadingScreen,
    }

    private void Start()
    {
        instance = this;
    }

    public IEnumerator LoadAsynchronously(InvokeMenu currLevel, int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        GameManager.playedLevel = currLevel;

        while (!operation.isDone)
        {
            float _progress = Mathf.Clamp01(operation.progress / 0.9f);
            loadingSlider.value = _progress;

            yield return null;
        }
    }
}
