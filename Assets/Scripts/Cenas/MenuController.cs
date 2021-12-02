using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuController : MonoBehaviour
{
    public string nomeCena;
    public Animator transicao;
    public GameObject tela;
    public GameObject iconeMinotauro;
    public GameObject Nevoa;
    public AudioSource TrilhaSonora;



    private bool VerificaAudio = false;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ChangeScene()
    {
        StartCoroutine(LoadLevel(nomeCena));
    }

    IEnumerator LoadLevel(string nomeCena)
    {
        tela.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        transicao.SetTrigger("Play");
        VerificaAudio = true;
        Nevoa.SetActive(false);
        iconeMinotauro.SetActive(true);
        yield return new WaitForSeconds(6);

        SceneManager.LoadScene(nomeCena);
    }

    public void Sair()
    {
        Application.Quit();
    }

    private void Update()
    {
        if(VerificaAudio)
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


