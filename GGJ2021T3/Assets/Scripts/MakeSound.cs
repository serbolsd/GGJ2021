using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeSound : MonoBehaviour
{

  public AudioClip Clip;


  private AudioSource audioattack;
  // Start is called before the first frame update
  void Start()
  {
    audioattack = GetComponent<AudioSource>();
  }

  public void play()
  {
    audioattack.clip = Clip;
    audioattack.Play();
  }
}
