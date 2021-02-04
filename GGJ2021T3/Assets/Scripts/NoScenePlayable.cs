using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoScenePlayable : MonoBehaviour
{
  // Start is called before the first frame update
  void Start()
  {
    if (FindObjectOfType<DonDestroy>())
    {
      Destroy(FindObjectOfType<DonDestroy>().gameObject);
    }
  }
}
