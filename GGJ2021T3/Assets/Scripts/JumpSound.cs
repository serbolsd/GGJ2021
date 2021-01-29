using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpSound : MonoBehaviour {

    public AudioClip jumpClip;

    private AudioSource audioPlayer;

    // Start is called before the first frame update
    void Start() {
        audioPlayer = GetComponent<AudioSource>();
    }

    // Update is called once per frame
  void Update() {
    if (Input.GetKeyDown("w")) {
      audioPlayer.clip = jumpClip;
      audioPlayer.Play();
    }
  }
}
