using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSound : MonoBehaviour
{

    public AudioClip attackClip;
    public AudioClip Clip1;
    public AudioClip Clip2;
    public AudioClip Clip3;
    public AudioClip Clip4;
    public AudioClip Clip5;
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
            audioattack.clip = Clip1;
            audioattack.Play();
        }

        if (Input.GetKeyDown("e"))
        {
            audioattack.clip = Clip2;
            audioattack.Play();
        }

        if (Input.GetKeyDown("r"))
        {
            audioattack.clip = Clip3;
            audioattack.Play();
        }

        if (Input.GetKeyDown("s"))
        {
            audioattack.clip = Clip4;
            audioattack.Play();
        }

        if (Input.GetKeyDown("t"))
        {
            audioattack.clip = Clip5;
            audioattack.Play();
        }

        if (Input.GetKeyDown("u"))
        {
            audioattack.clip = Clip6;
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
