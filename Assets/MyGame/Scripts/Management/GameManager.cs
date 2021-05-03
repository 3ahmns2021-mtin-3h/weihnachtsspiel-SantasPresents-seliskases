using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Canvas canvasObject;
    public GameObject weihnachtsmann;
    public Sack sack;

    public static InvokeMenu playedLevel;

    private void Awake()
    {
        SetCanvas();
        SetWeihnachtsmann();
        SetSack();

        currentWeihnachtsmann.transform.SetParent(canvas.transform);
    }

    public void EndGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void SetSack()
    {
        currentSack = sack;
    }
    public static Sack currentSack;

    public void SetWeihnachtsmann()
    {
        currentWeihnachtsmann = weihnachtsmann;
    }
    public static GameObject currentWeihnachtsmann;

    private void SetCanvas()
    {
        canvas = canvasObject;
    }
    public static Canvas canvas;
}
