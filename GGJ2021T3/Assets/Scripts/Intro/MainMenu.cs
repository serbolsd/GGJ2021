using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
  bool isPlay=false;

  public void Play()
  {
    isPlay = true;
    FindObjectOfType<Allanim>().playGame();
  }
  public void exit()
  {
    if (isPlay)
    {
      return;
    }
    Application.Quit();
  }

  public void Credits()
  {
    if (isPlay)
    {
      return;
    }
    FindObjectOfType<Transiciones>().LoadScene("Credits");
    //SceneManager.LoadScene("Credits");
  }
}
