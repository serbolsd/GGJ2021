using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AllAnim : MonoBehaviour
{
  public GameObject player;
  public GameObject flame;
  public GameObject bright;
  public Transform pointToArrive;
  public Transform pointToArriveFlame;

  public float playerSpeed = 1;
  public float flameSpeed = 1;

  public bool part1 = true;
  public bool part2 = false;
  public bool part2Init = false;
  public bool finish = false;
  public float dist = 0;
  public float mag = 0;
  public float elsapseTime = 0;
  public float timeToPart2 = 1.5f;
  public float timeToScale = 40;

  bool aredyLoad = false;
  // Start is called before the first frame update
  void Start()
  {

    player.GetComponent<Animator>().SetFloat("speed", 1);
  }

  // Update is called once per frame
  void Update()
  {
    if (part1)
    {
      movePlayer();
    }
    if (!part1 && !part2 &&!finish)
    {
      elsapseTime += Time.deltaTime;
      if (elsapseTime>=timeToPart2)
      {
        part2 = true;
        part2Init = true;
        elsapseTime = 0;
      }
    }
    if (part2)
    {
      getFlame();
    }
    if (finish)
    {
      elsapseTime += Time.deltaTime;
      if (elsapseTime>=2)
      {
        bright.transform.localScale += new Vector3(timeToScale * Time.deltaTime, timeToScale * Time.deltaTime,0);
      }
      if (elsapseTime >= 5&&!aredyLoad)
      {
        aredyLoad = true;
        FindObjectOfType<Transiciones>().LoadScene("Credits");
      }
    }
  }

  void movePlayer()
  {
    Vector3 vec = player.transform.position + (Vector3.right * playerSpeed * Time.deltaTime);

    dist = (pointToArrive.position - player.transform.position).x;
    mag = (Vector3.right * playerSpeed * Time.deltaTime).x;
    if (mag > dist)
    {

      player.GetComponent<Animator>().SetFloat("speed", 0);
      player.transform.position = player.transform.position + Vector3.right*dist;
      part1 = false;

    }
    player.transform.position = vec;
  }

  void getFlame()
  {
    if (part2Init)
    {
      player.GetComponent<Animator>().SetBool("win", true);
      part2Init = false;
    }
    else
    {
      player.GetComponent<Animator>().SetBool("win", false);
    }

    Vector3 dir = pointToArriveFlame.position - flame.transform.position;
    Vector3 vec = flame.transform.position + (dir.normalized * flameSpeed * Time.deltaTime);
    dist = (pointToArriveFlame.position - flame.transform.position).magnitude;
    mag = (Vector3.right * flameSpeed * Time.deltaTime).magnitude;
    if (mag > dist)
    {
      flame.transform.position = flame.transform.position + Vector3.right * dist;
      finish = true;
    }
    flame.transform.position = vec;
  }
  
}
