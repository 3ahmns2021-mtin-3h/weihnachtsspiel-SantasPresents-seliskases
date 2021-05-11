using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static Canvas canvas;
    public static GameObject currentWeihnachtsmann;
    public static GameObject currentSack;
    public static GameObject currentBird;
    public static InvokeMenu playedLevel;
    public static float timeRemaining;
    public static int numPresentsStored;

    public Canvas canvasObject;
    public GameObject weihnachtsmann;
    public GameObject sack;
    public float startTime;

    private void Awake()
    {
        canvas = canvasObject;
        currentWeihnachtsmann = weihnachtsmann;
        currentSack = sack;

        timeRemaining = startTime;
    }

    private void Update()
    {
        timeRemaining -= Time.deltaTime;

        if(timeRemaining < 0)
        {
            EndGame();
        }
    }

    public void EndGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
