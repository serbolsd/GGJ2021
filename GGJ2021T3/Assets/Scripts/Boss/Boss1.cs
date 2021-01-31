using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1 : MonoBehaviour
{
  public float m_currentSpeed = 4;
  public float m_speed1 = 4;
  public float m_speed2 = 6;
  public float m_speed3 = 8;
  public List<Transform> m_pathPoints;
  public List<Transform> m_pathPoints1;
  public List<Transform> m_pathPoints2;
  public Transform m_backLeft;
  public Transform m_backRight;
  public Transform m_CurrentBack;

  public float m_impetu = 1;
  public float m_ratio = 3;
  public float m_stayTime = 0;
  public int m_indexPath = 0;
  public float m_elapseTime = 0;
  public Vector3 nextPoint;
  Vector3 m_direction;
  Vector3 scale;

  public Transform playerPos;
  public Transform spawnBulletPos;

  public GameObject waxBulletBoss;
  public GameObject ghostBullet;
  
  float ViewDir = -1;

  
  public SpriteRenderer m_mySprite;

  bool m_isPatrol = true;
  bool m_isBack = false;
  float m_timeToBack = 8;
  float m_elapseTimeToBack = 0;

  float timeToAttack;
  float elapseTimeToAttack;

  Vector2 fase1Limit = new Vector2(15,31);
  Vector2 fase2Limit = new Vector2(12,26);
  Vector2 fase3Limit = new Vector2(10,21);
  Vector2 currentLimit = new Vector2(10,20);


  Vector2 fase1Atack = new Vector2(3, 7);
  Vector2 fase2Atack = new Vector2(2, 5);
  Vector2 fase3Atack = new Vector2(1, 4);
  Vector2 currentAtack = new Vector2(10, 20);

  BossCandle m_candle;
  bossLifeBar m_life;

  public float timeToDisaper=2.0f;
  float elapseToDisaper=0.0f;
  // Start is called before the first frame update
  void Start()
  {
    m_candle = FindObjectOfType<BossCandle>();
    m_life = FindObjectOfType<bossLifeBar>();
    currentLimit = fase1Limit;
    currentAtack = fase1Atack;
    m_currentSpeed = m_speed1;
    m_pathPoints = m_pathPoints1;
    m_CurrentBack = m_backRight;
    scale = transform.localScale;
    m_mySprite = GetComponent<SpriteRenderer>();

    timeToAttack = Random.Range(3,6);
  }

  // Update is called once per frame
  void Update()
  {
    if (m_life.m_died)
    {
      elapseToDisaper += Time.deltaTime;
      GetComponent<Animator>().SetBool("died", true);

      m_candle.GetComponent<SpriteRenderer>().enabled = false;
      m_mySprite.color = new Color(1,1,1,1-1*elapseToDisaper/ timeToDisaper);
      if (elapseToDisaper > timeToDisaper)
      {
        FindObjectOfType<Transiciones>().LoadScene("FinalScene");
      }
      return;
    }
    elapseTimeToAttack += Time.deltaTime;
    if (timeToAttack< elapseTimeToAttack)
    {
      Invoke("shoot",0.50f);
      timeToAttack = Random.Range(currentAtack.x, currentAtack.y);
      GetComponent<Animator>().SetBool("Attacking", true);
      elapseTimeToAttack = 0;
      //Alredyattack = true;
    }
    else
    {
      GetComponent<Animator>().SetBool("Attacking", false);
    }
    m_elapseTimeToBack += Time.deltaTime;
    if (m_elapseTimeToBack >= m_timeToBack)
    {
      m_isPatrol = false;
      m_isBack = true;
    }
    if (m_isPatrol)
    {
      m_elapseTime += Time.deltaTime;
      m_direction = patrolCircuit();
    }
    if (m_isBack)
    {
      goBack();
    }
    checkViewDir();
    transform.position += m_direction.normalized * m_currentSpeed;
  }
  
  void checkViewDir()
  {
      if (ViewDir >= 0)
      {
        transform.localScale = new Vector3(-scale.x, scale.y, scale.z);
      }
      else
      {
        transform.localScale = new Vector3(scale.x, scale.y, scale.z);
      }
  }

  Vector3 patrolCircuit()
  {
    Vector3 position = transform.position;
    Vector3 v1 = position - m_pathPoints[m_indexPath].position;
    v1.z = 0;
    Vector3 v2;
    int nextIdex = m_indexPath + 1;
    if (m_indexPath >= m_pathPoints.Count - 1)
    {
      nextIdex = 0;

    }
    nextPoint = m_pathPoints[nextIdex].position;
    nextPoint.z = 0;
    v2 = m_pathPoints[nextIdex].position - m_pathPoints[m_indexPath].position;
    v2.z = 0;

    float distance = Vector3.Magnitude(position - nextPoint);
    if (distance <= m_ratio)
    {
      if (m_elapseTime <= m_stayTime)
      {
        //agregar que no se mueve
        return Vector3.zero;
      }
      ++m_indexPath;
      if (m_indexPath >= m_pathPoints.Count)
      {
        m_indexPath = 0;
      }
    }
    else
    {
      m_elapseTime = 0;
    }

    float proyectionInLine = Vector3.Dot(v1, v2);
    proyectionInLine /= v2.magnitude;
    if (proyectionInLine < 0)
    {
      proyectionInLine *= -1;
    }
    Vector3 pathPoint = (v2 / proyectionInLine) + m_pathPoints[m_indexPath].position;
    pathPoint.z = 0;
    Vector3 F = seek(pathPoint, m_impetu);
    F = seek(nextPoint, m_impetu);
    return F;
  }

  Vector3 seek(Vector3 posB, float impetu)
  {
    Vector3 Dir = posB - transform.position;
    Dir.Normalize();
    Vector3 F = Dir * impetu;
    return F;
  }

  public void goBack()
  {
    Vector3 Dir = m_CurrentBack.position - transform.position;
    Dir.z = 0;
    if (Dir.magnitude<0.3)
    {
      selectSide();
      m_isPatrol = true;
      m_isBack = false;
      m_elapseTime = 0;
      m_elapseTimeToBack = 0;
      m_timeToBack = Random.Range(currentLimit.x, currentLimit.y);
      return;
    }
    Dir.Normalize();
    m_direction = Dir;
  }

  void selectSide()
  {
    int size = Random.Range(0, 2);
    if (size==0)
    {
      m_pathPoints = m_pathPoints1;
      m_CurrentBack = m_backRight;
      ViewDir = -1;
    }
    else
    {
      m_pathPoints = m_pathPoints2;
      m_CurrentBack = m_backLeft;
      ViewDir = 1;
    }
    transform.position = m_CurrentBack.position;
  }

  public void setFase(int fase)
  {
    if (fase==2)
    {
      m_currentSpeed = m_speed2;
      currentLimit = fase2Limit;
      currentAtack = fase2Atack;
    }
    else if (fase==3)
    {
      m_currentSpeed = m_speed3;
      currentLimit = fase3Limit;
      currentAtack = fase3Atack;
    }
    m_isPatrol = false;
    m_isBack = true;
  }

  void shoot()
  {
    int ran = Random.Range(0, 7);
    if (ran>=5)
    {
      shootWax();
    }
    else
    {
      shootGhost();
    }
  }

  void shootWax()
  {

    var obj = Instantiate(waxBulletBoss, spawnBulletPos.position, Quaternion.identity);
    var attack = obj.GetComponent<waxBoss>();
    Vector3 direction = playerPos.position - spawnBulletPos.position;
    attack.setDirection(direction.normalized);
  }

  void shootGhost()
  {
    var obj = Instantiate(ghostBullet, spawnBulletPos.position, Quaternion.identity);
    var attack = obj.GetComponent<ghostBoss>();
    Vector3 direction = playerPos.position - spawnBulletPos.position;
    attack.setDirection(direction.normalized);
  }
}
