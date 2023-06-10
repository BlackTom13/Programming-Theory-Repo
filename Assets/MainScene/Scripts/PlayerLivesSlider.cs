using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLivesSlider : MonoBehaviour
{
    public Slider playerLivesSlider;
    private int amountToBeDead=0;
    private int currentLives = 3;
    private GameManager2 gameManager;
    void Start()
    {
        playerLivesSlider.fillRect.gameObject.SetActive(true);
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager2>();
        playerLivesSlider.maxValue = gameManager.playerLives;
        playerLivesSlider.value = gameManager.playerLives;
    }

    public void HitOnPlayer(int amount)
    {
        currentLives += amount;
        playerLivesSlider.value = currentLives;
        if (currentLives <= amountToBeDead)
        {
            playerLivesSlider.fillRect.gameObject.SetActive(false);
        }
    }
}
