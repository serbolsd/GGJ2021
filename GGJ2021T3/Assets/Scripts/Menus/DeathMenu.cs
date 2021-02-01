using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class DeathMenu : MonoBehaviour
{
  public bool playerIsDeath = false;

  public GameObject deathMenuUI;

  public string scenaToRestart;
  public Vida_Script m_playerLive;
  Transiciones m_transicion;
  private void Start()
  {
    //m_playerLive = FindObjectOfType<Vida_Script>();
  }
  private void Update()
  {
    if (m_playerLive.m_died)
    {
      m_transicion = FindObjectOfType<Transiciones>();
      if (m_transicion)
      {
        m_transicion.GetComponent<Image>().enabled = false;
      }
      deathMenuUI.SetActive(true);
    }
  }
  public void Restart()
  {
    // deathMenuUI.SetActive(false);
    enableTransition();
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
    enableTransition();
    FindObjectOfType<Transiciones>().LoadScene("Intro");
    Debug.Log("Exit to menu");
  }

  public void ExitGame()
  {
    enableTransition();
    Debug.Log("Quit game");
    Application.Quit();
  }

  private void enableTransition()
  {
    if (m_transicion)
    {
      m_transicion.GetComponent<Image>().enabled = true;
    }
  }
}
