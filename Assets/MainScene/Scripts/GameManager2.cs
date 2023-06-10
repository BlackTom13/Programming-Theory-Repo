using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class GameManager2 : MonoBehaviour
{
    [SerializeField] int score = 0;
    public int dronLives = 3;
    public int playerLives = 3;
    public bool isGameOver = false;

    public TextMeshProUGUI dronLivesText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;

    private PlayerLivesSlider playerLivesSlider;

    private PlayerController2 playerController2Script;
    public int countOfEnemies;
    private SpawnManager2 spawnManagerScript;
    void Start()
    {
        spawnManagerScript = GameObject.Find("SpawnManager").GetComponent<SpawnManager2>();
        countOfEnemies = spawnManagerScript.amountOfEnemiesInWave;
        Cursor.visible = false;
        //Debug.Log("Lives: " + lives);
        dronLivesText.text = "Dron Lives: /n" + dronLives;
        scoreText.text = "Score: " + score;
        playerLivesSlider = GameObject.Find("Player").GetComponent<PlayerLivesSlider>();
        playerController2Script = GameObject.Find("Player").GetComponent<PlayerController2>();
    }

    // Update is called once per frame
    void Update()
    {
        dronLivesText.text = "Dron Lives: " + dronLives;
        scoreText.text = "Score: " + score;
    }
    public void AddPlayerLives(int value)
    {
        playerLives += value;
        playerLivesSlider.HitOnPlayer(value);
        if (playerLives <= 0)
        {
            isGameOver = true;
            playerController2Script.DieAnimation();
            gameOverText.text = "Game Over You died";
            gameOverText.gameObject.SetActive(true);
        }
    }
    public void AddDroneLives(int value)
    {
        dronLives += value;
        if (dronLives <= 0)
        {
            gameOverText.text = "Game Over Drone Destroyed";
            gameOverText.gameObject.SetActive(true);
            dronLives = 0;
        }
    }

    public void AddScore(int value)
    {
        score += value;
    }

    public void EnemyDied()
    {
        countOfEnemies--;
        //Debug.Log(countOfEnemies);
        if (countOfEnemies == 0)
        {
            gameOverText.text = "Wave Defeated";
            gameOverText.gameObject.SetActive(true);
        }
    }
}
