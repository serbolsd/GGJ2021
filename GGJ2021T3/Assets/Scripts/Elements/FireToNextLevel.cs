using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FireToNextLevel : MonoBehaviour
{
  public string levelName;

  private void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.tag=="Player")
    {
      SceneManager.LoadScene(levelName);
    }
  }
}
