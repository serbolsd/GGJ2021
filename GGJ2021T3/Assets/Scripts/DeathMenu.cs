using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class DeathMenu : MonoBehaviour
{
    public static bool playerIsDeath = false;

    //public GameObject deathMenuUI;

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown("m"))
        {
            if (playerIsDeath)
            {
                Restart();
            }
            else
            {
                Death();
            }
        }
    }

    public void Restart()
    {
        // deathMenuUI.SetActive(false);
        Time.timeScale = 1f;
        playerIsDeath = false;
        Debug.Log("Restar");
    }

    void Death()
    {
        // deathMenuUI.SetActive(true);
        Time.timeScale = 0f;
        playerIsDeath = true;
        Debug.Log("Death");

    }


    public void Exit()
    {
        Debug.Log("Exit to menu");

    }

    public void ExitGame()
    {
        Debug.Log("Quit game");
        Application.Quit();
    }
}
