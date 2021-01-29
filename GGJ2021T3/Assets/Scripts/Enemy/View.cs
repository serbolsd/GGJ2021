using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class View : MonoBehaviour
{
  public EnemyMovement m_body;
  float timeToLeft = 5;
  float elapseTime = 0;
  // Update is called once per frame
  void Update()
  {
    elapseTime += Time.deltaTime;
    if (elapseTime >= timeToLeft)
    {
      m_body.seePlayer = false;
    }
  }

  private void OnTriggerEnter2D(Collider2D collision)
  {
    //if (m_body.seePlayer)
    //{
    //  if (elapseTime >= timeToLeft)
    //  {
    //    m_body.seePlayer = false;
    //  }
    //  else
    //  {
    //    return;
    //  }
    //}
    var p = collision.GetComponent<tempPlayer>();
    if (p)
    {

      elapseTime = 0;
      m_body.playerPos = p.transform;
      m_body.seePlayer = true;
    }
  }
}
