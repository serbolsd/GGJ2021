using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
  public float damage = 10;
  public float m_weight = 1;
  public float m_speed = 1;
  public float m_speedPersue = 6;
  public float m_speedPatrol = 4;
  public List<Transform> m_pathPoints;
  public float m_impetu=1;
  public float m_ratio = 3;
  public float m_stayTime = 0;
  public int m_indexPath = 0;
  public float m_elapseTime = 0;
  public Vector3 nextPoint;
  Vector3 m_direction;

  public float m_WanderImpetu = 1;
  public float m_wanderDisProj = 5;
  public float m_wanderRatio = 5;
  public float m_wanderAngle = 5;

  public Vector3 m_viewDir = Vector3.right;

  const float degTorad = 3.1415f / 180;

  public bool seePlayer=false;

  public Transform playerPos;

  float viewDir=0;
  float newViewDir=0;

  Vector3 scale;

  public bool m_touchedPlayer = false;

  float timeToAttacAgain = 2;
  float elapseToAttac = 10;

  float timeToContinue = 10;

  public bool isFleeze = false;
  float timeFleeze = 0;
  public float timeToDesFleeze = 4;

  public View m_myView;

  public SpriteRenderer m_mySprite;
  public void onStart()
  {
    scale = transform.localScale;
    m_mySprite = GetComponent<SpriteRenderer>();
    m_myView.onStart();
  }
  // Update is called once per frame
  public void onUpdate()
  {
    //if (Input.GetKeyDown(KeyCode.Space))
    //{
    //  freeze();
    //}
    if (isFleeze)
    {
      timeFleeze += Time.deltaTime;
      if (timeFleeze>= timeToDesFleeze)
      {
        m_mySprite.color = Color.white;
        m_myView.m_mySprite.enabled = true;
        isFleeze = false;
      }
      else
      {
        m_mySprite.color = new Color(1*(timeFleeze/ timeToDesFleeze),1,1,1);
        m_myView.m_mySprite.enabled = false;
      }
      return;
    }
    m_myView.onUpdate();
    timeToContinue += Time.deltaTime;
    if (timeToContinue>2)
    {
      if (seePlayer)
      {
        m_direction = seek(playerPos.position, 2);
        if (m_touchedPlayer)
        {
          m_direction += fleeRatio(playerPos.position, 10, 4);
        }
      }
      else
      {
        m_elapseTime += Time.deltaTime;
        m_direction = patrolCircuit();
      }
      elapseToAttac += Time.deltaTime;
      if (timeToAttacAgain <= elapseToAttac)
      {
        m_myView.m_mySprite.enabled = true;
        m_touchedPlayer = false;
      }
      else
      {
        m_myView.m_mySprite.enabled = false;
      }
      m_direction.z = 0;
      m_direction.Normalize();
      m_direction = wander();
    }
   

    float dir = Mathf.Abs(viewDir - newViewDir);


    if (dir>0.5)
    {
      if (m_direction.x >= 0)
      {
        transform.localScale = new Vector3(-scale.x, scale.y, scale.z);
      }
      else
      {
        transform.localScale = new Vector3(scale.x, scale.y, scale.z);
      }
      viewDir = newViewDir;
    }
    newViewDir = m_direction.normalized.x;
    transform.position += m_direction.normalized * m_speed; 
  }

  Vector3 patrolCircuit()
  {
    Vector3 position = transform.position;
    Vector3 v1 = position - m_pathPoints[m_indexPath].position;
    v1.z = 0;
    Vector3 v2;
    int nextIdex = m_indexPath + 1;
    if (m_indexPath >= m_pathPoints.Count-1)
    {
      nextIdex = 0;

    }
    else
    {

    }
    nextPoint = m_pathPoints[nextIdex].position;
    nextPoint.z = 0;
    v2 = m_pathPoints[nextIdex].position - m_pathPoints[m_indexPath].position;
    v2.z = 0;

    float distance = Vector3.Magnitude(position- nextPoint);
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

    float proyectionInLine = Vector3.Dot(v1,v2);
    proyectionInLine /= v2.magnitude;
    if (proyectionInLine <0)
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

  Vector3 wander()
  {
    Vector3 F;
    //if (m_direction == Vector3.zero)
    //{
    //  float x = -0.5f + (std::rand() / RAND_MAX);
    //  float y = -0.5f + (std::rand() / RAND_MAX);
    //
    //  CD::CDVector2 point = { x, y };
    //  F = seek(point, impetu);
    //  return F;
    //}

    Vector3 C = transform.position + (m_direction * m_wanderDisProj);
    C.z = 0;
    float Adir = Mathf.Atan(m_direction.y / m_direction.x);
    Adir *= (180 / 3.1415f);
    float Fdir = Adir + Random.Range(0, m_wanderAngle);
    Fdir -= m_wanderAngle / 2;
    //std::cout << Adir <<std::endl;
    Vector3 posF = new Vector3( m_wanderRatio * Mathf.Cos(Fdir * degTorad) , m_wanderRatio * Mathf.Sin(Fdir * degTorad),0 );
    posF += C;
    F = seek(posF, m_WanderImpetu);

    return F;
  }

  Vector3 fleeRatio(Vector3 PosB, float impetu, float ratio)
  {
    Vector3 Dir = transform.position - PosB;
    Dir.z = 0;
    Vector3 F = new Vector3(0, 0, 0);
    if (Dir.magnitude < ratio)
    {
      F = Dir.normalized * impetu;
    }
    return F;
  }

  private void OnTriggerEnter2D(Collider2D collision)
  {
    if (isFleeze)
    {
      return;
    }
    if (collision.tag == "Player" && timeToAttacAgain <= elapseToAttac)
    {
      FindObjectOfType<Vida_Script>().addDamage(damage);
      timeToContinue = 0;
      timeToAttacAgain = Random.Range(2, 4);
      elapseToAttac = 0;
      m_myView.m_mySprite.enabled = false;
      m_touchedPlayer = true;
    }
  }

  public void freeze()
  {
    if (!isFleeze)
    {
      isFleeze = true;
      timeFleeze = 0;
    }
  }
}
