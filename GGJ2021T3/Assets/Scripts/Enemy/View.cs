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
  private void Start()
  {
    m_mySprite = GetComponent<SpriteRenderer>();
    m_sound = GetComponent<MakeSound>();
  }
  // Update is called once per frame
  void Update()
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
    }
  }

  private void OnTriggerEnter2D(Collider2D collision)
  {
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
    }
  }
}
