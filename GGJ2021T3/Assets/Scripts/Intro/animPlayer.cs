using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animPlayer : MonoBehaviour
{
  public Animator m_animator;
  float currentSpeed;
  public float Speed=1;
  Vector3 m_direction = Vector3.right;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    m_animator.SetFloat("speed", currentSpeed);
      transform.position = transform.position + m_direction * currentSpeed * Time.deltaTime; 
    }

  public void startMove()
  {
    currentSpeed = Speed;
  }


}
