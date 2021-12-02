using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmadilhaParede : MonoBehaviour
{

    public Animator parede;
    public Animator parede2;
    public Animator parede3;
    public Animator parede4;

    public AudioSource somParede;

    public GameObject minotauro;
    public GameObject minotauro2;

    private bool verificaArmadilha = true;

    IEnumerator OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("PlacaArmadilha"))
        {
            if(verificaArmadilha)
            {
                parede.SetBool("ParedeLevanta", true);
                parede2.SetBool("ParedeLevanta2", true);
                somParede.Play();
                verificaArmadilha = false;
                yield return new WaitForSeconds(5);
                minotauro.SetActive(true);
            }
        }

        if (other.gameObject.tag.Equals("PlacaArmadilha2"))
        {
            if (verificaArmadilha)
            {
                parede3.SetBool("ParedeLevanta", true);
                parede4.SetBool("ParedeLevanta", true);
                somParede.Play();
                verificaArmadilha = false;
                yield return new WaitForSeconds(5);
                minotauro2.SetActive(true);
            }
        }
    }

}
