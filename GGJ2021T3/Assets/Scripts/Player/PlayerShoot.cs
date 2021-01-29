using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
  public Transform m_spawn;
  public GameObject bullet;
  public Vector3 m_direcrtion = Vector3.right;
  public Vida_Script m_life;

  public float m_shootDelay;
  public float m_elapseTime;
  public bool shooting = false;
  // Update is called once per frame
  private void Start()
  {
    m_elapseTime = m_shootDelay;
    m_life = FindObjectOfType<Vida_Script>();
  }
  void Update()
  {
    m_elapseTime += Time.deltaTime;
    if (m_elapseTime>=0.2)
    {
      shooting = false;
    }
    if (Input.GetKeyDown(KeyCode.Space))
    {
      if (m_elapseTime>= m_shootDelay)
      {
        shoot();
        m_elapseTime = 0;
      }
    }
  }

  public void shoot()
  {
    if (m_life.VidaActual<=20)
    {
      return;
    }
    m_life.addDamage(5);
    shooting = true;
    var obj = Instantiate(bullet, m_spawn.position, Quaternion.identity);
    var attack = obj.GetComponent<WaxBullet>();
    attack.setDirection(m_direcrtion);
  }
}
