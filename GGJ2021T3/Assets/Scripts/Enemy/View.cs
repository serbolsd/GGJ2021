using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class View : MonoBehaviour
{
  public EnemyMovement m_body;
  public float timeToLeft = 7;
  float elapseTime = 0;
  MakeSound m_sound;
  public SpriteRenderer m_mySprite;
  public void onStart()
  {
    m_mySprite = GetComponent<SpriteRenderer>();
    m_sound = GetComponent<MakeSound>();
  }
  // Update is called once per frame
  public void onUpdate()
  {
    //if (m_body.isFleeze)
    //{
    //  m_mySprite.enabled = false;
    //  return;
    //}
    //else
    //{
    //  m_mySprite.enabled = true;
    //}
    elapseTime += Time.deltaTime;
    if (elapseTime >= timeToLeft)
    {
      m_mySprite.color = new Color(1,1,1,0.5f);
      m_body.seePlayer = false;
      m_body.m_speed = m_body.m_speedPatrol;
    }
  }

  private void OnTriggerEnter2D(Collider2D collision)
  {
    if (m_body.isFleeze)
    {
      return;
    }
    if (collision.tag=="Player")
    {
      if (!m_body.seePlayer)
      {
        m_sound.play();
      }
      var p = collision;
      m_mySprite.color = new Color(1,0,0,0.5f);
      elapseTime = 0;
      m_body.playerPos = p.transform;
      m_body.seePlayer = true;
      m_body.m_speed = m_body.m_speedPersue;
    }
  }
}
