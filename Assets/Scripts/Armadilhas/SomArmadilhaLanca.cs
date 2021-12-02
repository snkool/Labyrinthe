using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SomArmadilhaLanca : MonoBehaviour
{
    [SerializeField]
    private AudioSource efeitoLanca;
    public GameObject tela;
    public string nomeCena;

    private IEnumerator OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            efeitoLanca.Play();
            yield return new WaitForSeconds(1 / 2);
            tela.SetActive(true);
            yield return new WaitForSeconds(3 / 2);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            SceneManager.LoadScene(nomeCena);
        }
    }
}
