using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public AudioClip AndarSFX = null;
    private AudioSource mAudioSource = null;

    [SerializeField] Transform playerCamera = null;
    [SerializeField] float sensibilidadeMouse = 1.0f; // Sensibilidade do mouse
    [SerializeField] float velAndar = 4.0f; // Velocidade do Player
    [SerializeField] float gravidade = -13.0f; //  valor da Gravidade 

    // Deixam o movimento do mouse/teclado mais suaves
    [SerializeField] [Range(0.0f, 0.5f)] float moveSmoothTime = 0.3f;
    [SerializeField] [Range(0.0f, 0.5f)] float mouseSmoothTime = 0.03f;

    // Trava o cursor do mouse
    [SerializeField] bool lockCursor = true;

    float cameraPitch = 0.0f;
    float velocidadeY = 0.0f;
    CharacterController controller = null;

    // Posicao da direcao atual do player + mouse
    Vector2 currentDir = Vector2.zero;
    Vector2 currentDirVel = Vector2.zero;

    Vector2 currentMouseDelta = Vector2.zero;
    Vector2 currentMouseDeltaVel = Vector2.zero;

    // Variavel que vai ser usada para alterar a cena
    public string nomeCena;


    void Start()
    {
        controller = GetComponent<CharacterController>();
        mAudioSource = GetComponent<AudioSource>();
        // Se o mouse estiver aparecendo, ele trava o mouse e o deixa invisivel
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    void Update()
    {
        // Atualização da posição do mouse
        UpdateMouseLook();
        // Atualização da posição do teclado
        updateMovimento();
        updateSound();
    }

    // Função responsável por receber as posições do mouse
    void UpdateMouseLook()
    {
        Vector2 targetMouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        currentMouseDelta = Vector2.SmoothDamp(currentMouseDelta, targetMouseDelta, ref currentMouseDeltaVel, mouseSmoothTime);

        cameraPitch -= currentMouseDelta.y * sensibilidadeMouse;
        cameraPitch = Mathf.Clamp(cameraPitch, -90.0f,  70.0f);

        playerCamera.localEulerAngles = Vector3.right * cameraPitch;

        transform.Rotate(Vector3.up * currentMouseDelta.x * sensibilidadeMouse);
    }

    // Função responsável por receber as posições do teclado
    void updateMovimento()
    {
        Vector2 targetDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        targetDir.Normalize();

            currentDir = Vector2.SmoothDamp(currentDir, targetDir, ref currentDirVel, moveSmoothTime);

        if (controller.isGrounded)
            velocidadeY = 0.0f;

        velocidadeY += gravidade * Time.deltaTime;

        Vector3 velocidade = (transform.forward * currentDir.y + transform.right * currentDir.x) * velAndar + Vector3.up * velocidadeY;

        controller.Move(velocidade * Time.deltaTime);
    }

    void updateSound()
    {
        if (Input.GetButtonDown("Vertical"))
        {
            if (mAudioSource != null)
            {
                mAudioSource.Play();        
            }
        }

        if (Input.GetButtonUp("Vertical"))
        {
            if (mAudioSource != null)
            {
                mAudioSource.Stop();
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Parede"))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            SceneManager.LoadScene(nomeCena);            
        }
    }
}
