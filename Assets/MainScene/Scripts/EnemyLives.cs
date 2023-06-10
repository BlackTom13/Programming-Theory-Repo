using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyLives : MonoBehaviour
{
    public Slider enemyLivesSlider;
    public int amountToBeDead;

    private int currentLivesAmount = 0;
    private GameManager2 gameManager;
    
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager2>();
        enemyLivesSlider.maxValue = amountToBeDead;
        enemyLivesSlider.value = 0;
        enemyLivesSlider.fillRect.gameObject.SetActive(false);
    }

    public void ShootEnemy(int amount)
    {
        currentLivesAmount += amount;
        enemyLivesSlider.fillRect.gameObject.SetActive(true);
        enemyLivesSlider.value = currentLivesAmount;
        gameManager.AddScore(1);
        if (currentLivesAmount == amountToBeDead)
        {
            gameManager.EnemyDied();
            Destroy(gameObject, 0.3f);
        }
    }
}
