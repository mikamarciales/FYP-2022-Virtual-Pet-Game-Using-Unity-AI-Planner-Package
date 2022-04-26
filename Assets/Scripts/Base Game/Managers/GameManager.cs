// GameManager class created to be used in the base game
// Manages the Game Over screen and defines boolean to check if the game is over

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace VirtualPetGame
{
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
}
