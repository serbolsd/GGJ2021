using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Vida_Script : MonoBehaviour
{

  public Image BarraDeVida;

  public float VidaActual;

  public float VidaMaxima;

  public bool m_died = false;

  void Update()
  {
    BarraDeVida.fillAmount = VidaActual / VidaMaxima;
  }

  public void addDamage(float damage)
  {
    VidaActual -= damage;
    if (VidaActual<=0)
    {
      VidaActual = 0;
      m_died = true; ;
    }
  }

  public void addHealt(float healt)
  {
    VidaActual += healt;
    if (VidaActual>=100)
    {
      VidaActual = 100;
    }
  }
}
