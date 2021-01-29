using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Allanim : MonoBehaviour
{
  public Transform[] pathFirePos;
  public Transform[] pathGhostPos;
  public Transform[] pathGhost2;

  public GameObject ghost;
  public GameObject fire;
  public GameObject Menu;

  private LTSpline visualizePath;

  public bool play = false;

  bool ghostAredy = false;
  bool fireAredy = false;

  Vector3[] pathfire;
  Vector3[] pathGhost;
  float elapseTime = 0;

  public float startToWalk = 6;

  public animPlayer m_player;

  public bool moveButtons = false;

  public float speedToQuitButtons = 2;
  // Start is called before the first frame update
  void Start()
  {
    pathfire = new Vector3[pathFirePos.Length];
    for (int i = 0; i < pathFirePos.Length; i++)
    {
      pathfire[i] = pathFirePos[i].position;
    }

    pathGhost = new Vector3[pathGhostPos.Length];
    for (int i = 0; i < pathGhostPos.Length; i++)
    {
      pathGhost[i] = pathGhostPos[i].position;
    }
  }

  // Update is called once per frame
  void Update()
  {
    //if (Input.GetKeyDown(KeyCode.Space))
    //{
    //  
    //}
    if (moveButtons)
    {
      Menu.transform.position = Menu.transform.position + Vector3.up * speedToQuitButtons * Time.deltaTime;
    }
    if (play)
    {
      elapseTime += Time.deltaTime;
      if (!ghostAredy)
      {
        animGhost();
        ghostAredy = true;
      }
      if (elapseTime>=2.5)
      {
        if (!fireAredy)
        {
          animFire();
          fireAredy = true;
        }

      }
      if (elapseTime >= startToWalk)
      {
        m_player.startMove();
      }
    }
  }

  void animFire()
  {
    LeanTween.moveSpline(fire, pathfire, 5).setOrientToPath2d(true).setSpeed(4f);
    LeanTween.moveSpline(ghost, pathfire, 5).setOrientToPath2d(true).setSpeed(4f);
  }

  void animGhost()
  {
    LeanTween.moveSpline(ghost, pathGhost, 2.0f).setOrientToPath2d(true).setSpeed(4f);
  }

  public void playGame()
  {
    Invoke("playAnimation",3);
    moveButtons = true;
  }

  public void playAnimation()
  {
    play = true;
  }
}
