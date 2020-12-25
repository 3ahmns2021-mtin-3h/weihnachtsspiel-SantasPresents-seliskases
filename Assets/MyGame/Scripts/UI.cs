using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI : MonoBehaviour
{
    public GameManager gameManager;
    public TextMeshProUGUI timer;
    public TextMeshProUGUI presentCounter;

    public float startTime = 60;

    private float time;

    private void Start()
    {
        time = startTime;
    }

    private void Update()
    {
        time -= Time.deltaTime;

        if(time > 0)
        {
            DisplayTime(time);
        }
        else
        {
            gameManager.EndGame();
        }       
        
        presentCounter.text = GameManager.sack.presents.ToString();               
    }

    private void DisplayTime(float _timeToDisplay)
    {
        _timeToDisplay += 1;

        float _minutes = Mathf.FloorToInt(_timeToDisplay / 60);
        float _seconds = Mathf.FloorToInt(_timeToDisplay % 60);

        timer.text = string.Format("{0:00}:{1:00}", _minutes, _seconds);
    }
}
