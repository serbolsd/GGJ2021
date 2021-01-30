using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class DeathMenu : MonoBehaviour
{
  public static bool playerIsDeath = false;

  public GameObject deathMenuUI;

  public string scenaToRestart;
  public Vida_Script m_playerLive;
  private void Start()
  {
    m_playerLive = FindObjectOfType<Vida_Script>();
  }
  private void Update()
  {
    if (m_playerLive.m_died)
    {
      deathMenuUI.SetActive(true);
    }
  }
  public void Restart()
  {
    // deathMenuUI.SetActive(false);
    Time.timeScale = 1f;
    //playerIsDeath = false;
    FindObjectOfType<Transiciones>().LoadScene(scenaToRestart);
    Debug.Log("Restar");
  }

  void Death()
  {
    // deathMenuUI.SetActive(true);
    //Time.timeScale = 0f;
    playerIsDeath = true;
    FindObjectOfType<Transiciones>().LoadScene(scenaToRestart);
    Debug.Log("Death");

  }


  public void Exit()
  {
    FindObjectOfType<Transiciones>().LoadScene("Intro");
    Debug.Log("Exit to menu");
  }

  public void ExitGame()
  {
    Debug.Log("Quit game");
    Application.Quit();
  }
}
