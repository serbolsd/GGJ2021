using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSound : MonoBehaviour
{

  public AudioClip attackClip;

  private AudioSource audioattack;
  // Start is called before the first frame update
  void Start()
  {
    audioattack = GetComponent<AudioSource>();
    audioattack.clip = attackClip;
    audioattack.Play();
  }

  // Update is called once per frame
  void Update()
  {
    //if (Input.GetKeyDown("space"))
    //{
    //  audioattack.clip = attackClip;
    //  audioattack.Play();
    //}
  }
}
