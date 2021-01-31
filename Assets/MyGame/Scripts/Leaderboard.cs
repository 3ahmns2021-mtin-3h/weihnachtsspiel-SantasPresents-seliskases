using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Leaderboard : MonoBehaviour
{
    public TextMeshProUGUI firstPlace, secondPlace, thirdPlace;

    public void DisplayScores()
    {
        TextMeshProUGUI[] textFields = {firstPlace, secondPlace, thirdPlace};

        for (int i = 1; i <= 3; i++)
        {
            string playerName = PlayerPrefs.GetString("highScoreName" + i);
            int score = PlayerPrefs.GetInt("highScore" + i);

            textFields[i - 1].text = playerName + ": " + score;
        }
    }
}
