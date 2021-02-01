using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transiciones : MonoBehaviour
{
  private Animator transicionAnim;

  void Start()
  {
    transicionAnim = GetComponent<Animator>();
  }

  public void LoadScene(string scene)
  {
    StartCoroutine(Transiciona(scene));
  }

  IEnumerator Transiciona(string scene)
  {
    transicionAnim.SetBool("Salida", true);
    yield return new WaitForSeconds(1.0f);
    SceneManager.LoadScene(scene);
  }

  public void enterInScene()
  {
    transicionAnim.SetBool("Salida", false);
  }
}
