using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transiciones : MonoBehaviour
{
    private Animator transicionAnim;

    void Start()
    {
        transicionAnim = GetComponent<Animator>();
    }
    public void LoadScene (string scene)
    {
        StartCoroutine(Transiciona(scene));
    }
    IEnumerator Transiciona(string scene)
    {
        transicionAnim.SetTrigger("Salida");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(scene);
    
    }
}
