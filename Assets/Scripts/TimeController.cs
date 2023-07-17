using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TimeController : MonoBehaviour
{
    public bool isTimeRunning;
    public float maxTimeSeconds = 60;
    public Text timerText;
    public Text scoreText;

    private int _score;
    private float currentTimeSeconds;
    
    // Start is called before the first frame update
    void Start()
    {
        currentTimeSeconds = maxTimeSeconds;
        isTimeRunning = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isTimeRunning) return;
        
        currentTimeSeconds = Math.Clamp(currentTimeSeconds - Time.deltaTime, 0, maxTimeSeconds);
        timerText.text = currentTimeSeconds.ToString(CultureInfo.InvariantCulture);
        if (currentTimeSeconds <= 0)
        {
            isTimeRunning = false;
            SceneManager.LoadScene(0);
        }
    }

    public void Restart()
    {
        currentTimeSeconds = maxTimeSeconds;
        isTimeRunning = true;
    }

    public void AddScore(int value)
    {
        _score += value;
        scoreText.text = _score.ToString();
    }

    public int GetScore()
    {
        return _score;
    }
}
