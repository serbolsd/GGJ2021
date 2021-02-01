using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCandle : MonoBehaviour
{
  public float m_life=10;

  Boss1 m_boss;

  public float speedToRecover=0.2f;
  SpriteRenderer m_sprite;
  public Color m_color;

  float elapseTime;

  public void onStart()
  {
    m_boss = FindObjectOfType<Boss1>();
    m_sprite = GetComponent<SpriteRenderer>();
  }

  public void onUpdate()
  {
    if (m_sprite.color.g<1)
    {
      m_color = m_sprite.color;
      elapseTime += Time.deltaTime;
      m_sprite.color = new Color(1,1*(elapseTime/ speedToRecover), 1 * (elapseTime / speedToRecover),1);
      if (m_sprite.color.g >= 1)
      {
        m_sprite.color = Color.white;
        elapseTime = 0;
      }
    }
  }

  public void addDamage()
  {
    m_life--;
    m_sprite.color = Color.red;
    if (m_life <= 3)
    {
      m_boss.setFase(3);
    }
    else if(m_life <= 6)
    {
      m_boss.setFase(2);

    }
  }
}
