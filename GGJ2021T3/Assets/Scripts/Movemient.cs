using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the movement of a character.
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class Movemient : MonoBehaviour
{
  const float DEFAULT_MAX_VELOCITY = 1;

  const float MAX_SPEED = 10;

  const float MIN_SPEED = 15;

  const float MAX_ACCELERATION = 10.0f;

  const float DEFAULT_JUMP_FORCE = 15;

  const float CURRENT_GRAVITY_SCALE = 2.5f;

  static Vector2 CURRENT_GROUND_DETECTION_SIZE = new Vector2(2.0f, 1.0f);

  /** Controls the physics of the character. */
  public Rigidbody2D body;

  /** keeps track if where on the ground*/
  public GroundDetection groundDetection;


  private Bounds bounds;

  /** The direction of the character*/
  public Vector2 direction;

  /** controls how big the box that controls the ground detection is.
   */
  public Vector2 groundDetectionSize = CURRENT_GROUND_DETECTION_SIZE; 

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
    groundDetection = GetComponent<GroundDetection>();

    bounds = GetComponent<SpriteRenderer>().bounds;

    groundDetectionSize = CURRENT_GROUND_DETECTION_SIZE;

    Vector2 lowestPoint = new Vector2(bounds.center.x, bounds.center.y - (bounds.size.y * 0.5f));

    Debug.Assert(groundDetection.init(lowestPoint, groundDetectionSize) == true);
    Debug.Assert(body != null);
    Debug.Assert(secondsUntilMaxSpeed > float.Epsilon);
  }

  // Update is called once per frame
  void Update()
  {
    Vector2 result = handleInput(); 

    direction = Vector2.zero;

    body.velocity += result;

    //bool isTooMuchVelocity = body.velocity.magnitude > currentMaxVelocity;
    bool isTooMuchVelocity = Mathf.Abs(body.velocity.x) > currentMaxVelocity;

    if (isTooMuchVelocity)
    {
      Vector2 normalizedVelocity = body.velocity;//.normalized;
      //body.velocity = normalizedVelocity * currentMaxVelocity;
      if (normalizedVelocity.x<0)
      {
        body.velocity = new Vector2(-currentMaxVelocity, normalizedVelocity.y);
      }
      else
      {
        body.velocity = new Vector2(currentMaxVelocity, normalizedVelocity.y);
      }
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
    if (Input.GetKeyDown(KeyCode.W) && groundDetection.isOnGround)
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
