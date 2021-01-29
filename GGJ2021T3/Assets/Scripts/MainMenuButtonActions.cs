using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuButtonActions : MonoBehaviour
{

  static public string Nivel1 = "Nivel1";
  public void quitGame()
  {
    Debug.Log("quitting the game");
    Application.Quit();
  }
}
