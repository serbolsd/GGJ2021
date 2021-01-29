using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the movement of a character.
/// </summary>
public class Movemient : MonoBehaviour
{
  const float DEFAULT_MAX_VELOCITY = 45.0f;

  const float MAX_SPEED = 125.0f;

  const float MIN_SPEED = 50.0f;

  const float MAX_ACCELERATION = 10.0f;

  const float DEFAULT_JUMP_FORCE = 20.0f;

  const float CURRENT_GRAVITY_SCALE = 2.5f;

  public Rigidbody2D body;

  public Vector2 direction;

  /** Controls how fast the character can go */
  public float currentMaxVelocity;

  /** The running speed of the character*/
  public float runningSpeed;

  /** Controls how high the character jumps*/
  public float jumpingForce;

  /** The active acceleration of the character*/
  public float acceleration;

  /** Controls how long it takes for the character to reach max speed*/
  public float secondsUntilMaxSpeed = 0.5f;


  private void Start()
  {
    direction = new Vector3();
    direction = Vector3.right;

    if (runningSpeed < MIN_SPEED)
    {
      runningSpeed = MIN_SPEED;
    }

    if (jumpingForce < DEFAULT_JUMP_FORCE)
    {
      jumpingForce = DEFAULT_JUMP_FORCE;
    }

    if(currentMaxVelocity < DEFAULT_MAX_VELOCITY)
    {
      currentMaxVelocity = DEFAULT_MAX_VELOCITY;
    }
    acceleration = 1.0f;

    body = GetComponent<Rigidbody2D>();
    body.gravityScale = CURRENT_GRAVITY_SCALE;

    Debug.Assert(body != null);
    Debug.Assert(secondsUntilMaxSpeed > float.Epsilon);
  }

  // Update is called once per frame
  void Update()
  {
    Vector2 result = handleInput(); 

    direction = Vector2.zero;

    body.velocity += result;

    bool isTooMuchVelocity = body.velocity.magnitude > currentMaxVelocity;

    if (isTooMuchVelocity)
    {
      Vector2 normalizedVelocity = body.velocity.normalized;
      body.velocity = normalizedVelocity * currentMaxVelocity;
    }
  }

  private Vector2
  handleInput()
  {
    Vector2 result = Vector2.zero;
    if (Input.GetKey(KeyCode.D))
    {
      direction = Vector2.right;
      result += calculateMovingDistance();
    }
    if (Input.GetKey(KeyCode.A))
    {
      direction = Vector2.left;
      result += calculateMovingDistance();
    }
    if (Input.GetKeyDown(KeyCode.W))
    {
      //direction += Vector2.up;
      result += calculateJumpingMovment();
    }

    return result;
  }

  private Vector2
  calculateMovingDistance()
  {
    return direction * runningSpeed * Time.smoothDeltaTime;
  }

  private Vector2
  calculateJumpingMovment()
  {
    return jumpingForce * Vector2.up;
  }

  private float
  calculateAcceleration()
  {
    float delta = Mathf.Abs(MAX_SPEED - MIN_SPEED);
    return delta * (1 / secondsUntilMaxSpeed) * Time.smoothDeltaTime;
  }

}
