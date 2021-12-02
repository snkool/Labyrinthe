using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    public string nomeCena;
    public Animator transicao;
    public GameObject tela;
    public GameObject Nevoa;

    // Variaveis para dar fadeOut na Trilha sonora
    public AudioSource TrilhaSonora;
    private bool VerificaAudio = false;

    // Botão restart
    public void ChangeScene()
    {
        StartCoroutine(LoadLevel(nomeCena));
    }

    IEnumerator LoadLevel(string nomeCena)
    {
        tela.SetActive(true);
        VerificaAudio = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        transicao.SetTrigger("Play");
        Nevoa.SetActive(false);
        
        yield return new WaitForSeconds(1);

        SceneManager.LoadScene(nomeCena);
    }

    public void Back()
    {
        VerificaAudio = true;
        SceneManager.LoadScene(nomeCena);       
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
