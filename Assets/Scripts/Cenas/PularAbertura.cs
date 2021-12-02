using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PularAbertura : MonoBehaviour
{
    // Efeitos de transicao e tela de loading
    public GameObject fadeInOut;
    public GameObject tela;
    public Animator transicao;
    public Animator BotaoPular;
    public GameObject iconeMinotauro;

    public string nomeCena;

    // Trilha sonora
    public AudioSource TrilhaSonora;
    private bool VerificaAudio = false;

    public void PularIntro()
    {
        BotaoPular.SetTrigger("SomeBotao");
        StartCoroutine(Pular());
    }

    IEnumerator Pular()
    {
        tela.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        transicao.SetTrigger("Play");
        VerificaAudio = true;
        iconeMinotauro.SetActive(true);
        yield return new WaitForSeconds(6);

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
