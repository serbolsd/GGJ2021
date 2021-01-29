using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaxHealth : MonoBehaviour
{
  public float m_healt = 10;

  private void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.tag=="Player")
    {
      FindObjectOfType<Vida_Script>().addHealt(m_healt);
      Destroy(transform.gameObject);
    }
  }
}
