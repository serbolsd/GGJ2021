using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuButtonActions : MonoBehaviour
{
  public void quitGame()
  {
    Debug.Log("quitting the game");
    Application.Quit();
  }

  // Update is called once per frame
  void Update()
  {

  }
}
