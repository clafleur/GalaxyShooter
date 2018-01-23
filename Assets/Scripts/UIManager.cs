using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
 
    public Sprite[] livesSprites;
    public Image livesImageDisplay;
    public Text scoreText;
    public GameObject titleScreen;
    public int score;
    public bool hasGameStarted = false;

    public void UpdateLives(int currentLives)
    {
        livesImageDisplay.sprite = livesSprites[currentLives];
    }

    public void UpdateScore()
    {
        score += 10;
        scoreText.text = "Score: " + score;
    }

    public void ShowTitleScreen()
    {
        titleScreen.SetActive(true);
    }

    public void HideTitleScreen()
    {
        titleScreen.SetActive(false);
        scoreText.text = "Score: ";
    }

}
