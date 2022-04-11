using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Image gameOverBG;
    public bool isGameOver = false;

    public void GameOver()
    {
        if (isGameOver == false)
        {
            isGameOver = true;
            gameOverBG.gameObject.SetActive(true);
            Debug.Log("Pet is dead! GAME OVER");
        }
    }
}
