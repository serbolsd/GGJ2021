using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
  public float elapseTime=0;
  bool called = false;
  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {
    elapseTime += Time.deltaTime;
    if (elapseTime>=18&&!called)
    {
      called = true;
      FindObjectOfType<Transiciones>().LoadScene("Intro");
    }
    if (Input.anyKeyDown&&!called&&elapseTime>=3)
    {
      called = true;
      FindObjectOfType<Transiciones>().LoadScene("Intro");
    }
  }
}
