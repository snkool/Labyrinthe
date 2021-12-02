using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HistoriaFinal2Controller : MonoBehaviour
{
    // Imagens do Final 
    public GameObject parte1;
    public GameObject parte2;
    public GameObject parte3;
    public GameObject parte4;


    public string nomeCena;
    private int contaTela = 0; // Contador para identificar as telas

    // Efeitos de transicao e tela de loading
    public GameObject fadeInOut;
    public GameObject tela;
    public Animator transicao;
    public GameObject iconeMinotauro;

    // Trilha sonora
    public AudioSource TrilhaSonora;
    private bool VerificaAudio = false;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ProxParte()
    {
        StartCoroutine(TrocaCena());
    }

    IEnumerator TrocaCena()
    {
        contaTela++;
        if (contaTela == 1)
        {
            fadeInOut.SetActive(true);
            yield return new WaitForSeconds(2);
            parte1.SetActive(true);
            yield return new WaitForSeconds(2);
            fadeInOut.SetActive(false);
        }
        if (contaTela == 2)
        {
            fadeInOut.SetActive(true);
            yield return new WaitForSeconds(2);
            parte2.SetActive(true);
            yield return new WaitForSeconds(2);
            fadeInOut.SetActive(false);
        }
        if (contaTela == 3)
        {
            fadeInOut.SetActive(true);
            yield return new WaitForSeconds(2);
            parte3.SetActive(true);
            yield return new WaitForSeconds(2);
            fadeInOut.SetActive(false);
        }
        if (contaTela == 4)
        {
            fadeInOut.SetActive(true);
            yield return new WaitForSeconds(2);
            parte4.SetActive(true);
            yield return new WaitForSeconds(2);
            fadeInOut.SetActive(false);
        }
        if (contaTela == 5)
        {
            tela.SetActive(true);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            VerificaAudio = true;
            transicao.SetTrigger("Play");
            VerificaAudio = true;
            iconeMinotauro.SetActive(true);
            yield return new WaitForSeconds(6);

            SceneManager.LoadScene(nomeCena);
        }
    }

    private void Update()
    {
        if (VerificaAudio)
        {
            StartCoroutine(FadeOut());
        }
    }

    private IEnumerator FadeOut()
    {
        float speed = 0.0003f;
        while (TrilhaSonora.volume < 1)
        {
            TrilhaSonora.volume -= speed;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
