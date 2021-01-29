using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class View : MonoBehaviour
{
  public EnemyMovement m_body;
  public float timeToLeft = 7;
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
    var p = collision.GetComponent<tempPlayer>();
    if (p)
    {

      elapseTime = 0;
      m_body.playerPos = p.transform;
      m_body.seePlayer = true;
    }
  }
}
