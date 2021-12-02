using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class HistoriaController : MonoBehaviour
{
    // Imagens da historia
    public GameObject parte1;
    public GameObject parte2;
    public GameObject parte3;
    public GameObject parte4;
    public GameObject parte5;
    public GameObject parte6;
    public GameObject parte7;
    public GameObject parte8;
    public GameObject parte9;
    public GameObject parte10;
    public GameObject parte11;
    public GameObject parte12;
    public GameObject parte13;

    public string nomeCena;
    private int contaTela = 0; // Contador para identificar as telas

    // Efeitos de transicao e tela de loading
    public GameObject fadeInOut;
    public GameObject tela;
    public Animator transicao;
    public GameObject iconeMinotauro;
    public Animator BotaoPular;

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
        if(contaTela == 1)
        {
            fadeInOut.SetActive(true);
            yield return new WaitForSeconds(2);
            parte2.SetActive(true);
            yield return new WaitForSeconds(2);
            fadeInOut.SetActive(false);
        }
        if (contaTela == 2)
        {
            fadeInOut.SetActive(true);
            yield return new WaitForSeconds(2);
            parte3.SetActive(true);
            yield return new WaitForSeconds(2);
            fadeInOut.SetActive(false);
        }
        if (contaTela == 3)
        {
            fadeInOut.SetActive(true);
            yield return new WaitForSeconds(2);
            parte4.SetActive(true);
            yield return new WaitForSeconds(2);
            fadeInOut.SetActive(false);
        }
        if (contaTela == 4)
        {
            fadeInOut.SetActive(true);
            BotaoPular.SetTrigger("SomeBotao");
            yield return new WaitForSeconds(2);
            parte5.SetActive(true);
            yield return new WaitForSeconds(2);
            fadeInOut.SetActive(false);
        }
        if (contaTela == 5)
        {
            fadeInOut.SetActive(true);
            yield return new WaitForSeconds(2);
            parte6.SetActive(true);
            yield return new WaitForSeconds(2);
            fadeInOut.SetActive(false);
        }
        if (contaTela == 6)
        {
            fadeInOut.SetActive(true);
            yield return new WaitForSeconds(2);
            parte7.SetActive(true);
            yield return new WaitForSeconds(2);
            fadeInOut.SetActive(false);
        }
        if (contaTela == 7)
        {
            fadeInOut.SetActive(true);
            yield return new WaitForSeconds(2);
            parte8.SetActive(true);
            yield return new WaitForSeconds(2);
            fadeInOut.SetActive(false);
        }
        if (contaTela == 8)
        {
            fadeInOut.SetActive(true);
            yield return new WaitForSeconds(2);
            parte9.SetActive(true);
            yield return new WaitForSeconds(2);
            fadeInOut.SetActive(false);
        }
        if (contaTela == 9)
        {
            fadeInOut.SetActive(true);
            yield return new WaitForSeconds(2);
            parte10.SetActive(true);
            yield return new WaitForSeconds(2);
            fadeInOut.SetActive(false);
        }
        if (contaTela == 10)
        {
            fadeInOut.SetActive(true);
            yield return new WaitForSeconds(2);
            parte11.SetActive(true);
            yield return new WaitForSeconds(2);
            fadeInOut.SetActive(false);
        }
        if (contaTela == 11)
        {
            fadeInOut.SetActive(true);
            yield return new WaitForSeconds(2);
            parte12.SetActive(true);
            yield return new WaitForSeconds(2);
            fadeInOut.SetActive(false);
        }
        if (contaTela == 12)
        {
            fadeInOut.SetActive(true);
            yield return new WaitForSeconds(2);
            parte13.SetActive(true);
            yield return new WaitForSeconds(2);
            fadeInOut.SetActive(false);
        }
        if(contaTela == 13)
        {
            fadeInOut.SetActive(true);
            yield return new WaitForSeconds(1);
            tela.SetActive(true);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            transicao.SetTrigger("Play");
            VerificaAudio = true;
            iconeMinotauro.SetActive(true);
            yield return new WaitForSeconds(6);
            

            SceneManager.LoadScene(nomeCena);
        }
        //Debug.Log("Cena" + contaTela);
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
