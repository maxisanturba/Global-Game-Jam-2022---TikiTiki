using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeutralRockCounter : MonoBehaviour
{
    public GameController gameController;

    private void OnTriggerEnter(Collider other)
    {
        if (gameController.gameOver) return;
        if (other.CompareTag("BlueRock"))
        {
            gameController.bluePlayerPoints += .5f;
        }
        if (other.CompareTag("RedRock"))
        {
            gameController.redPlayerPoints += .5f;
        }
    }
}