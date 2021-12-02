using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ColetaChave : MonoBehaviour
{
    // Audio
    public AudioSource somChave;
    public AudioSource somPlanta;
    public AudioSource somPortaoTrancado;
    public AudioSource somPortaoAberto;
    public bool verificaAudio = true;

    // Objetos que vão aparecer na tela (Icones das chaves e mensagens)
    public GameObject IconeChave1;
    public GameObject IconeChave2;
    public GameObject IconeChave3;
    public GameObject IconePlanta;
    public GameObject MsgTrancado;
    public GameObject MsgAbriu;

    // Variavel para armazenar o objeto do circulo mágico
    public GameObject CirculoMagico; 

    // Animacoes
    public Animator animator; // Animacao do portao
    public Animator animatorCirculo; // Animacao do circulo magico

    // Variavel responsavel por armanezar a quantidade de chaves que o player coletou
    private int VerificaChave = 0;

    // Verifica se o jogador coletou a planta
    private bool verificaPlanta = false;

    //Transicao final
    public GameObject telaPretaFadeIn;

    // Variaveis para dar fadeOut na Trilha sonora
    public AudioSource TrilhaSonora;
    private bool VerificaAudio = false;

    IEnumerator OnTriggerEnter(Collider other)
    {
        // If para realizar a coleta da chave
        if (other.gameObject.tag.Equals("Chave"))
        {
            somChave.Play(); // Play no som da chave
            Destroy(other.gameObject); // Destroi o objeto chave

            VerificaChave++; // Coletou +1 chave

            // Ativa os icones da chave no HUD do jogador
            switch(VerificaChave)
            {
                case 1:
                    IconeChave1.SetActive(true);
                break;

                case 2:
                    IconeChave2.SetActive(true);
                break;

                case 3:
                    IconeChave3.SetActive(true);
                break;

            }
        }

        // If para realizar a coleta da planta
        if (other.gameObject.tag.Equals("Planta"))
        {
            somPlanta.Play();
            Destroy(other.gameObject); // Destroi o objeto chave
            verificaPlanta = true;
            IconePlanta.SetActive(true);
        }

        if (other.gameObject.tag.Equals("Portao"))
        {
            // Se o player nao coletar as 3 chaves, o portao permanece fechado 
            if (VerificaChave != 3)
            {
                //Debug.Log("trancado");
                somPortaoTrancado.Play();
                MsgTrancado.SetActive(true);
                yield return new WaitForSeconds(6);
                MsgTrancado.SetActive(false);
            }
            else
            {
                // Garante que o audio so vai ser executado uma vez
                if (verificaAudio)
                {
                    somPortaoAberto.Play();
                    verificaAudio = false;
                }

                animator.SetBool("Abrir", true); // Ativa a animação da porta abrindo
                yield return new WaitForSeconds(3 / 2);
                MsgAbriu.SetActive(true); // Mostra a mensagem na tela
                animatorCirculo.SetBool("AcenderCirculo", true); // Ativa a animaçao do circulo de invocaçao
                yield return new WaitForSeconds(3);
                animatorCirculo.SetBool("GirarCirculo", true); // Ativa a segunda animaçao do circulo de invocaçao
                yield return new WaitForSeconds(3);
                CirculoMagico.SetActive(true); // Ativa o objeto do circulo mágico
                //Debug.Log("abriu");
            }
        }

        if (other.gameObject.tag.Equals("CirculoMagico"))
        {
            if(verificaPlanta)
            {
                telaPretaFadeIn.SetActive(true);
                VerificaAudio = true;
                yield return new WaitForSeconds(6);
                Debug.Log("Final com a planta");

                SceneManager.LoadScene("TelaHistoria_Final_2");
            }
            else
            {
                telaPretaFadeIn.SetActive(true);
                VerificaAudio = true;
                yield return new WaitForSeconds(6);
                Debug.Log("Final sem a planta");

                SceneManager.LoadScene("TelaHistoria_Final");
            }
            
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
