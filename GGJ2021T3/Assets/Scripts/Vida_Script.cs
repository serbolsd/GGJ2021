using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Vida_Script : MonoBehaviour
{

    public Image BarraDeVida;

    public float VidaActual;

    public float VidaMaxima;

    void Update()
    {
        BarraDeVida.fillAmount = VidaActual / VidaMaxima;
    }
}
