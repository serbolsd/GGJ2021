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
  
  float ViewDir = -1;

  
  public SpriteRenderer m_mySprite;

  bool m_isPatrol = true;
  bool m_isBack = false;
  float m_timeToBack = 8;
  float m_elapseTimeToBack = 0;

  // Start is called before the first frame update
  void Start()
  {
    m_currentSpeed = m_speed1;
    m_pathPoints = m_pathPoints1;
    m_CurrentBack = m_backRight;
    scale = transform.localScale;
    m_mySprite = GetComponent<SpriteRenderer>();
  }

  // Update is called once per frame
  void Update()
  {
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
      m_timeToBack = Random.Range(7.0f, 13.0f);
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
    }
    else if (fase==3)
    {
      m_currentSpeed = m_speed3;
    }
  }
}
