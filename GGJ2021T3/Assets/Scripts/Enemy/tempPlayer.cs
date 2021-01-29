using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tempPlayer : MonoBehaviour
{
  Vector3 direction;
  public float m_speed = 0.02f;
  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {
    direction = seek(Camera.main.ScreenToWorldPoint(Input.mousePosition),1);
    direction.z = 0;
    transform.position += direction.normalized *m_speed;
  }
  Vector3 seek(Vector3 posB, float impetu)
  {
    Vector3 Dir = posB - transform.position;
    Dir.Normalize();
    Vector3 F = Dir * impetu;
    return F;
  }
}
