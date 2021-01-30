using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{

  public static bool GameIsPause = false;

  public GameObject pauseMenuUI;

  // Update is called once per frame
  void Update()
  {

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
    pauseMenuUI.SetActive(false);
    Time.timeScale = 1f;
    GameIsPause = false;
    Debug.Log("Resume");
  }

  void Pause()
  {
    pauseMenuUI.SetActive(true);
    Time.timeScale = 0f;
    GameIsPause = true;
    Debug.Log("Pause");

  }


  public void Exit()
  {
    FindObjectOfType<Transiciones>().LoadScene("Intro");
    Time.timeScale = 1f;
    Debug.Log("Exit to menu");

  }

  public void ExitGame()
  {
    Debug.Log("Quit game");
    Application.Quit();
  }
}
