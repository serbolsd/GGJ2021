using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FireToNextLevel : MonoBehaviour
{
  public string levelName;
  public Animator transition;

  private void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.tag == "Player")
    {
      Invoke("changeScene", 0.8f);
      GetComponent<SpriteRenderer>().enabled = false;
      transition.SetBool("Salida",true);
    }
  }
  public void changeScene()
  {
    
      SceneManager.LoadScene(levelName);
  }
}
