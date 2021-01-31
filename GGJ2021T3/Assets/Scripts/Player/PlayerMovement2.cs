using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
//[RequireComponent(typeof(PlayerShoot))]
[RequireComponent(typeof(BoxCollider2D))]
public class PlayerMovement2 : MonoBehaviour
{

  public float m_GravityScale = 2.5f;
  public float m_Gravity = 2.5f;
  
  /** Controls the physics of the character. */
  Rigidbody2D m_body; 

  /** The direction of the character*/
  public Vector2 m_direction;

  /** Controls how fast the character can go */
  public float m_MaxSpeed;

  /** The running speed of the character*/
  public float m_currentSpeed;

  /** Controls how high the character jumps*/
  public float m_jumpingForce;
  /** Controls how high the character jumps*/
  public float m_currentJumpForce;

  /** The active acceleration of the character*/
  public float m_acceleration;

  /** The active acceleration of the character*/
  float m_NormalAcceleration;

  /** The active acceleration of the character*/
  float m_JumpAcceleration;

  /** Controls how long it takes for the character to reach max speed*/
  public float m_timeToMaxSpeed = 0.5f;

  Animator m_animator;
  SpriteRenderer m_sprite;
  PlayerShoot m_shooter;
  Vector3 m_scale;

  public bool m_bGorunded = false;
  public bool m_bJumping = true;
  [SerializeField] private LayerMask m_platformsLayer;
  [SerializeField] private LayerMask m_wallsLayer;
  [SerializeField] private LayerMask m_cellingsLayer;

  BoxCollider2D m_boxCollider;
  public BoxCollider2D m_bodyCollider;
  public BoxCollider2D m_headCollider;


  float m_rightSpeed = 0;
  float m_rightTime = 0;

  float m_leftSpeed = 0;
  float m_leftTime = 0;

  public float m_jumpSpeed = 0;
  public float m_jumpTime = 0;

  public float m_waitToCheckGround = 0.2f;
  public float m_elaptseToCheckGround = 0.2f;
  // Start is called before the first frame update
  void Start()
  {
    m_body = GetComponent<Rigidbody2D>();
    m_sprite = GetComponent<SpriteRenderer>();
    m_animator = GetComponent<Animator>();
    m_shooter = GetComponent<PlayerShoot>();
    m_boxCollider = GetComponent<BoxCollider2D>();

    m_body.gravityScale = m_GravityScale;

    m_scale = transform.localScale;
    m_acceleration = 1.0f;
    m_direction = Vector3.right;
    m_NormalAcceleration = m_MaxSpeed / m_timeToMaxSpeed;
    m_JumpAcceleration = m_NormalAcceleration * 0.5f;
  }

  // Update is called once per frame
  void Update()
  {

    //m_acceleration = m_MaxSpeed / m_timeToMaxSpeed;
    if (m_shooter.m_life.m_died)
    {
      m_animator.SetBool("dead", true);
      return;
    }
    if (m_waitToCheckGround <= m_elaptseToCheckGround)
    {
      m_bGorunded = checkIsGrounded();
    }
    if (m_bGorunded)
    {
      m_jumpSpeed = 0;
      m_jumpTime = 0;
      m_currentJumpForce = 0;
      m_bJumping = false;
    }
    else
    {
      m_elaptseToCheckGround += Time.deltaTime;
      m_bJumping = true;
    }

  
    handleInput();


    float speedx = Mathf.Abs(m_body.velocity.x);
    bool isTooMuchVelocity = speedx > m_MaxSpeed;
    m_animator.SetFloat("speed", speedx);
    m_animator.SetBool("jumping", !m_bGorunded);
    m_animator.SetFloat("jumpSpeed", m_jumpSpeed);
    m_animator.SetBool("Attacking", m_shooter.shooting);
  }

  bool 
  checkIsGrounded() {
    RaycastHit2D cast = 
      Physics2D.BoxCast(m_boxCollider.bounds.center, m_boxCollider.bounds.size,0,Vector2.down,0.1f, m_wallsLayer);
    return cast.collider != null;
  }

  void
  checkCelling()
  {
    RaycastHit2D cast =
       Physics2D.BoxCast(m_headCollider.bounds.center, m_headCollider.bounds.size, 0, Vector2.up, 0.1f, m_wallsLayer);
    if (cast.collider != null)
    {
      m_currentJumpForce = 0;
      m_jumpTime = 0;
    }
  }

  void
  checkWall()
  {
    RaycastHit2D cast =
      Physics2D.BoxCast(m_bodyCollider.bounds.center, m_bodyCollider.bounds.size, 0, Vector2.right, 0.1f, m_wallsLayer);
    if (cast.collider != null)
    {
      m_rightSpeed = 0;
      m_rightTime = 0;
    }
    cast =
    Physics2D.BoxCast(m_bodyCollider.bounds.center, m_bodyCollider.bounds.size, 0, Vector2.left, 0.1f, m_wallsLayer);
    if (cast.collider != null)
    {
      m_leftSpeed = 0;
      m_leftTime = 0;
    }
  }

  private void
  handleInput() {

    m_direction.x = 0;

    
    if (m_bGorunded && Input.GetKeyDown(KeyCode.W))
    {
      m_acceleration = m_JumpAcceleration;
      //m_body.velocity = Vector2.up * m_jumpingForce;
      m_currentJumpForce = m_jumpingForce;
      m_elaptseToCheckGround = 0; 
      m_bJumping = true;
      m_bGorunded = false;
      GetComponent<JumpSound>().play();
    }
    else
    {
      m_acceleration = m_NormalAcceleration;
    }
    checkCelling();
    if (m_bJumping)
    {
      calculeJump();
    }

    if (Input.GetKey(KeyCode.D)) {
      m_shooter.m_direcrtion = Vector3.right;
      transform.localScale = new Vector3(m_scale.x, m_scale.y, m_scale.z);
      m_direction += Vector2.right;
      calculateRigthSpeed();
    }
    else
    {
      calculateDesAcelerateRigthSpeed();
    }
    if (Input.GetKey(KeyCode.A)) {
      m_shooter.m_direcrtion = -Vector3.right;
      transform.localScale = new Vector3(-m_scale.x, m_scale.y, m_scale.z);
      m_direction += Vector2.left;
      calculateLeftSpeed();
    }
    else
    {
      calculateDesAcelerateLeftSpeed();
    }
    checkWall();
    m_currentSpeed = m_rightSpeed - m_leftSpeed;
    m_body.velocity = new Vector2(m_currentSpeed, m_jumpSpeed);
    //transform.position = new Vector2(transform.position.x+m_currentSpeed, transform.position.y + m_jumpSpeed);
  }

  void calculateRigthSpeed() {
    m_rightTime += Time.deltaTime;
    if (m_rightTime>= m_timeToMaxSpeed)
    {
      m_rightTime = m_timeToMaxSpeed;
    }
    m_rightSpeed = m_acceleration * m_rightTime;
  }

  void calculateDesAcelerateRigthSpeed()
  {
    m_rightTime -= Time.deltaTime;
    if (m_rightTime <= 0)
    {
      m_rightTime = 0;
    }
    m_rightSpeed = m_acceleration * m_rightTime;
  }

  void calculateLeftSpeed()
  {
    m_leftTime += Time.deltaTime;
    if (m_leftTime >= m_timeToMaxSpeed)
    {
      m_leftTime = m_timeToMaxSpeed;
    }
    m_leftSpeed = m_acceleration * m_leftTime;
  }

  void calculateDesAcelerateLeftSpeed()
  {
    m_leftTime -= Time.deltaTime;
    if (m_leftTime <= 0)
    {
      m_leftTime = 0;
    }
    m_leftSpeed = m_acceleration * m_leftTime;
  }

  void calculeJump()
  {
    m_jumpTime += Time.deltaTime;
    m_jumpSpeed = m_currentJumpForce - m_Gravity * m_jumpTime;
  }
}
