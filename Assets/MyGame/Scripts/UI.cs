using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI : MonoBehaviour
{
    public TextMeshProUGUI timer;
    public TextMeshProUGUI presentCounter;

    private void Update()
    {
        DisplayTime(GameManager.timeRemaining);
        presentCounter.text = GameManager.numPresentsStored.ToString();
    }

    private void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float _minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float _seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timer.text = string.Format("{0:00}:{1:00}", _minutes, _seconds);
    }
}
