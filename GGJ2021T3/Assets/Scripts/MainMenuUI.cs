using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.UI;
using UnityEngine.UIElements;

public class MainMenuUI : MonoBehaviour
{
  public Canvas canvas;

  private Button playButton;
  private Button creaditsButton;
  private Button quitButton;

  private void Start()
  {
    canvas = GetComponent<Canvas>();

    Button[] buttonsArray = canvas.GetComponents<Button>();
    buttonsArray[0]
  }
}
