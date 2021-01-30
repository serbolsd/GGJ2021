using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitMap : MonoBehaviour
{
  public Transform m_limit;
  public Transform m_spawn;
  public GameObject m_player;
  // Start is called before the first frame update
  void Start()
  {
    m_player = FindObjectOfType<PlayerShoot>().gameObject;
  }

  // Update is called once per frame
  void Update()
  {
    if (m_player.transform.position.y< m_limit.position.y)
    {
      m_player.transform.position = new Vector3(m_spawn.position.x, m_spawn.position.y,0);
      FindObjectOfType<Vida_Script>().addDamage(5);
    }
  }
}
