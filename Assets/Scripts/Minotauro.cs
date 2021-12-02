using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Minotauro : MonoBehaviour
{
    public NavMeshAgent minotauro; // Variavel da classe NavMesh
    public Transform player; // Variavel para receber o objeto player
    private bool CorreAtras = false; // Variavel para decidir se o minotauro corre ou nao
    public Animator animator; // Variavel animator para configurar as animações do minotauro (IDLE - RUN)
    public string nomeCena; // Variavel que recebe o nome da cena 

    // Audio
    public AudioSource Rugido; // Audio Rugido 
    public AudioSource PegadaMinotauro; // Audio Pegadas 
    private bool VerificaAudio = true; // Variavel para garantir que os audios toquem apenas 1x

    // Enum com os 'estados' do minotauro (VAZIO, 1 trigger = AreaBusca, 2 trigger = Corre, 3 trigger = Ataca)
    private enum minotauroStatus {vazio, AreaBusca, corre, ataca} 
    private minotauroStatus sttsAtualMinotauro = 0;

    // Variaveis para setar a posição inicial do minotauro
    public Transform spawnMinotauro;
    public GameObject MoverMinotauro;
    private bool confirmaDestino = true;
    private bool iniciaVerifcaoDestino = false;
   [SerializeField] float destinoAlcancado;
    Vector3 target;

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {        
        if(other.gameObject.tag.Equals("Player"))
        {
            sttsAtualMinotauro++; // Altera o estado do minotauro a cada trigger que entra
                                  // (1. Trigger: Aréa de busca do minotauro, enquanto o player estiver nela, o minotauro vai atrás)
                                  // (2. Trigger: Aréa de visualização do minotauro, ativa a corrida)
                                  // (3. Trigger: Encostou no corpo do minotauro = GameOver)
            //Debug.Log("Status " + sttsAtualMinotauro);

            if (sttsAtualMinotauro.ToString() == "corre" || sttsAtualMinotauro.ToString() == "ataca")
            {
                // Garante que o audio toque apenas uma vez
                if (VerificaAudio)
                {
                    PegadaMinotauro.Play();
                    Rugido.Play();
                    VerificaAudio = false;
                }

                CorreAtras = true; // Ativa a corrida do minotauro

                // Encostou no jogador = muda pra tela de GameOver 
                if (sttsAtualMinotauro.ToString() == "ataca")
                {
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                    SceneManager.LoadScene(nomeCena);
                }
            }      
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            sttsAtualMinotauro--; // Ao sair do trigger, volta o Enum para a posicao anterior
            //Debug.Log("Status negativo " + sttsAtualMinotauro);

            // Se a condição for verdadeira, o minotauro desiste de perseguir o player
            if (sttsAtualMinotauro.ToString() == "vazio")
            {
                //Debug.Log("end");
                minotauro.ResetPath(); 
                CorreAtras = false;
                target = spawnMinotauro.position;
                minotauro.SetDestination(target); // Volta para o seu spawn
                confirmaDestino = true;
                iniciaVerifcaoDestino = true;
            }
        }
    }

    // Funçao responsavel por verificar se o minotauro chegou no destino dado pelo minotauro.SetDestination(target)
    IEnumerator VerificaDestino()
    {
        float distanciaTarget = Vector3.Distance(transform.position, target);
        if (distanciaTarget < destinoAlcancado && confirmaDestino == true)
        {
            //Debug.Log("Destinato alcancado");
            animator.SetBool("Run", false); // Desativa a transição para a animação de parar
            animator.SetBool("StopRun", true); // Ativa a transição para a animação de parar
            PegadaMinotauro.Stop(); // Desativa audio das pegadas    
            yield return new WaitForSeconds(1);
            MoverMinotauro.transform.rotation = Quaternion.Euler(0, 0, 0); // Ajeita a posição do minotauro no seu local    
            VerificaAudio = true;
            confirmaDestino = false;
            iniciaVerifcaoDestino = false;
        }
    }


    void Update()
    {
        if (CorreAtras)
        {
            animator.SetBool("Run", true); // Ativa a transição para a animação de correr
            animator.SetBool("StopRun", false); // Desativa a transição para a animação de parar
            minotauro.SetDestination(player.position); // Minotauro se desloca até a posiçao do player    
        }

        if (iniciaVerifcaoDestino)
        {
            StartCoroutine(VerificaDestino());
        }
    }
}

