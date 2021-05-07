using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameObject currentWeihnachtsmann;
    public static GameObject currentSack;
    public static InvokeMenu playedLevel;
    public static int numPresentsStored;

    public Canvas canvasObject;
    public GameObject weihnachtsmann;

    private void Awake()
    {
        SetCanvas();
        SetWeihnachtsmann();
    }

    public void EndGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void SetWeihnachtsmann()
    {
        currentWeihnachtsmann = weihnachtsmann;
        currentSack = weihnachtsmann.GetComponent<WeihnachtsmannController>().sack;
    }

    private void SetCanvas()
    {
        canvas = canvasObject;
    }
    public static Canvas canvas;
}
