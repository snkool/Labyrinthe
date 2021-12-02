using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TelaExtrasController : MonoBehaviour
{
    //Tela Extras
    public GameObject telaExtras;
    public GameObject Op1;
    public GameObject Op2;
    public GameObject Op3;

    public void Extras()
    {
        Op1.SetActive(false);
        Op2.SetActive(false);
        Op3.SetActive(false);
        telaExtras.SetActive(true);
    }

    public void BackMenu()
    {
        Op1.SetActive(true);
        Op2.SetActive(true);
        Op3.SetActive(true);
        telaExtras.SetActive(false);
    }
}
