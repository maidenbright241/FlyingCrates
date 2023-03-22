using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; //system for text
using UnityEngine.SceneManagement;  //library for manage scenes, we've access to scene through the code
using UnityEngine.UI; //lib-y for accessing objects of the UI

public class GameManager : MonoBehaviour

{
    public List<GameObject> targets;
    public TextMeshProUGUI scoreText;   //data type of textscore
    public TextMeshProUGUI gameOverText;
    public Button restartButton;
    public GameObject titleScreen;
    public bool isGameActive;

    private float SpawnRate = 1.0f;
    private int score;

    private IEnumerator SpawnTarget()
    {
        while(isGameActive)
        {
            yield return new WaitForSeconds(SpawnRate); //returns to start() using method 1 sec for waiting and instantiate random target
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }
    }

    public void UpdateScore(int scoreToAdd)  
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    public void GameOver()
    {
        isGameActive = false;
        restartButton.gameObject.SetActive(true);
        gameOverText.gameObject.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame(int difficulty)
    {
        SpawnRate /= difficulty;
        titleScreen.gameObject.SetActive(false); 
        scoreText.gameObject.SetActive(true);
        isGameActive = true;
        StartCoroutine(SpawnTarget());
        score = 0;
        UpdateScore(0);
    }
}
