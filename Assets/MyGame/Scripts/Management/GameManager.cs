using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Canvas canvasObject;

    public static InvokeMenu playedLevel;

    private void Awake()
    {
        SetCanvas();

        SpawnSystem.useBird = playedLevel.level.bird;
        LevelGenerator.GenerateLevel(playedLevel.level);

        weihnachtsmann.transform.SetParent(canvas.transform);
    }

    public void EndGame()
    {
        if(sack.presents > playedLevel.level.highScore)
        {
            playedLevel.level.highScore = sack.presents;
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("MainScene");
    }

    public static void SetSack(Sack currentSack)
    {
        sack = currentSack;
    }
    public static Sack sack;

    public static void SetWeihnachtsmann(GameObject currrentWeihnachtsmann)
    {
        weihnachtsmann = currrentWeihnachtsmann;
    }
    public static GameObject weihnachtsmann;

    private void SetCanvas()
    {
        canvas = canvasObject;
    }
    public static Canvas canvas;
}
