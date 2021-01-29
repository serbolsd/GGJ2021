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

  private void Start()
  {
    m_boss = FindObjectOfType<Boss1>();
    m_sprite = GetComponent<SpriteRenderer>();
  }

  private void Update()
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
    if (m_life <= 2)
    {
      m_boss.setFase(3);
    }
    else if(m_life <= 5)
    {
      m_boss.setFase(2);

    }
  }
}
