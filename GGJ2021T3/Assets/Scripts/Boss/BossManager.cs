using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
  // Start is called before the first frame update
  void Start()
  {
    if (FindObjectOfType<DonDestroy>())
    {
      var  objets = FindObjectsOfType<DonDestroy>();
      foreach (var item in objets)
      {
        if (item.tag!="UI")
        {
          Destroy(FindObjectOfType<DonDestroy>().gameObject);
        }
      }
    }
  }
}
