using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameOverScreenController GameOverScreen;

    public void GameOver()
    {
        GameOverScreen.Setup();
    }
}
