using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the movement of a character.
/// </summary>
public class Movemient : MonoBehaviour
{
  const float MAX_SPEED = 500.0f;
  const float MAX_ACCELERATION = 10.0f;
  const float MIN_ACCELERATION = 0.001f;

  public Rigidbody2D body;

  public Vector3 direction;

  /** The active speed of the character*/
  public float speed; 

  /** The active acceleration of the character*/
  public float acceleration;//acceleration


  private void Start()
  {
    direction = new Vector3();
    direction = Vector3.right;
    speed = 50.0f;
    acceleration = 1.0f;
    body = GetComponent<Rigidbody2D>();
    Debug.Assert(body != null);
  }

  // Update is called once per frame
  void Update()
  {
    if (Input.GetKey(KeyCode.D))
    {
      direction = Vector2.right;
      body.velocity = calculateMovingDistance();
    }
    else if (Input.GetKey(KeyCode.A))
    {
      direction = Vector2.left;
      body.velocity = calculateMovingDistance();
    }
  }

  private Vector2
  calculateMovingDistance()
  {
    return direction * speed * Time.smoothDeltaTime;
  }

}
