using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockCounter : MonoBehaviour
{
    public int playerColor; //1 = Blue 2 = Red
    public GameController gameController;

    private void OnTriggerEnter(Collider other)
    {
        if (gameController.gameOver) return;
        switch (playerColor)
        {
            case 1:
            {
                if (other.CompareTag("BlueRock"))
                {
                    gameController.bluePlayerPoints += 1.5f;
                }

                break;
            }
            case 2:
            {
                if (other.CompareTag("RedRock"))
                {
                    gameController.redPlayerPoints += 1.5f;
                }

                break;
            }
        }
    }
}
