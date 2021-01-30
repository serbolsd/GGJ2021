using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bossLifeBar : MonoBehaviour
{
  BossCandle m_boss;
  public Image BarraDeVida;
  
  public float VidaMaxima;

  public bool m_died = false;
  // Start is called before the first frame update
  void Start()
  {
    m_boss = FindObjectOfType<BossCandle>();
    VidaMaxima = m_boss.m_life;
  }

  // Update is called once per frame
  void Update()
  {

    BarraDeVida.fillAmount = m_boss.m_life / VidaMaxima;
    if (m_boss.m_life==0)
    {
      m_died = true;
    }
  }
}
