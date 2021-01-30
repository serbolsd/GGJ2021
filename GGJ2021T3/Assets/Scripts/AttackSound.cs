using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSound : MonoBehaviour
{

    public AudioClip attackClip;
    public AudioClip deathClip;
    public AudioClip jumpClip;
    public AudioClip victoryClip;
    public AudioClip damgeGhost;
    public AudioClip ghost;
    public AudioClip Clip6;
    public AudioClip Clip7;
    public AudioClip Clip8;
    public AudioClip Clip9;
    public AudioClip Clip10;


    private AudioSource audioattack;
    // Start is called before the first frame update
    void Start()
    {
        audioattack = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            audioattack.clip = attackClip;
            audioattack.Play();
        }

        if (Input.GetKeyDown("q"))
        {
            audioattack.clip = deathClip;
            audioattack.Play();
        }

        if (Input.GetKeyDown("e"))
        {
            audioattack.clip = jumpClip;
            audioattack.Play();
        }

        if (Input.GetKeyDown("r"))
        {
            audioattack.clip = victoryClip;
            audioattack.Play();
        }


        if (Input.GetKeyDown("t"))
        {
            audioattack.clip = damgeGhost;
            audioattack.Play();
        }

        if (Input.GetKeyDown("u"))
        {
            audioattack.clip = ghost;
            audioattack.Play();
        }

        if (Input.GetKeyDown("x"))
        {
            audioattack.clip = Clip7;
            audioattack.Play();
        }

        if (Input.GetKeyDown("z"))
        {
            audioattack.clip = Clip8;
            audioattack.Play();
        }

        if (Input.GetKeyDown("k"))
        {
            audioattack.clip = Clip9;
            audioattack.Play();
        }

        if (Input.GetKeyDown("l"))
        {
            audioattack.clip = Clip10;
            audioattack.Play();
        }
    }
}
