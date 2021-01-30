using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waxBoss : MonoBehaviour
{
  Vector3 m_direction = Vector3.right;
  Vector3 scale;
  public float m_speed = 2;
  public float m_lifeTime = 3;

  // Start is called before the first frame update
  void Start()
  {
    Destroy(this.gameObject, m_lifeTime);
  }
  // Update is called once per frame
  void Update()
  {
    Vector3 pos = transform.position;
    pos += m_direction * m_speed * Time.deltaTime;
    transform.position = pos;
  }

  public void setDirection(Vector3 dir)
  {
    scale = transform.localScale;
    m_direction = dir;
    if (m_direction.x > 0)
    {
      transform.localScale = new Vector3(scale.x, scale.y, scale.z);
    }
    else
    {
      transform.localScale = new Vector3(-scale.x, scale.y, scale.z);
    }
  }
  private void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.tag == "Player")
    {
      FindObjectOfType<Vida_Script>().addHealt(5);
      Destroy(transform.gameObject);
    }
  }

  private void OnCollisionEnter2D(Collision2D collision)
  {
    if (collision.gameObject.tag == "Player")
    {
      FindObjectOfType<Vida_Script>().addHealt(5);
      Destroy(transform.gameObject);
    }
  }
}
