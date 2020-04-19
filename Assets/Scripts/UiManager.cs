using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [SerializeField]
    private Text scoreText;
    [SerializeField]
    private Text gameOverText;
    [SerializeField]
    private Text restartGame;
    [SerializeField]
    private Image livesImg;
    [SerializeField]
    private Sprite[] liveSprites;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = "Score: " + 0;
        gameOverText.gameObject.SetActive(false);
        restartGame.gameObject.SetActive(false);
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if(gameManager == null)
        {
            Debug.LogError("GameManager not created");
        }
    }


    public void UpdateScore(int PlayerScore)
    {
        scoreText.text = "Score: " + PlayerScore; 
    }

    public void UpdateLives(int currentLives)
    {
        livesImg.sprite = liveSprites[currentLives];
    }

    public void DisplayGameOverMessage()
    {
        gameManager.GameOver();
        gameOverText.gameObject.SetActive(true);
        restartGame.gameObject.SetActive(true);
        StartCoroutine(GameOverFlicker());
    }

    IEnumerator GameOverFlicker()
    { 
        while (true)
        {
            gameOverText.text = "Game Over";
            yield return new WaitForSeconds(1);
            gameOverText.text = "";
            yield return new WaitForSeconds(1);           
        }
    }

   
}
