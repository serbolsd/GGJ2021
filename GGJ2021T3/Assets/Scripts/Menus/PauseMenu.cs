using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{

  public bool GameIsPause = false;

  public GameObject pauseMenuUI;

  public Vida_Script m_playerLive;
  Transiciones m_transicion;
  private void Start()
  {
    m_playerLive = FindObjectOfType<Vida_Script>();
  }
  // Update is called once per frame
  void Update()
  {
    if (m_playerLive.m_died)
    {
      return;
    }
    if (Input.GetKeyDown(KeyCode.Escape))
    {
      if (GameIsPause)
      {
        Resume();
      }
      else
      {
        Pause();
      }
    }
  }

  public void Resume()
  {
    enableTransition();
    pauseMenuUI.SetActive(false);
    Time.timeScale = 1f;
    GameIsPause = false;
    Debug.Log("Resume");
  }

  void Pause()
  {
    m_transicion = FindObjectOfType<Transiciones>();
    if (m_transicion)
    {
      m_transicion.GetComponent<Image>().enabled=false;
    }
    pauseMenuUI.SetActive(true);
    Time.timeScale = 0f;
    GameIsPause = true;
    Debug.Log("Pause");

  }


  public void Exit()
  {
    enableTransition();
    FindObjectOfType<Transiciones>().LoadScene("Intro");
    Time.timeScale = 1f;
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
