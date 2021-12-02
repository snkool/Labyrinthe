using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnChave : MonoBehaviour
{
    // Possiveis posicoes para a chave spwanar
    public Transform Spawn1;
    public Transform Spawn2;
    public Transform Spawn3;
    public Transform Spawn4;
    public Transform Spawn5;
    public Transform Spawn6;

    // Objetos das chaves
    public GameObject chave2;
    public GameObject chave3;

    // Variavel responsavel por salvar a posicao dos spawns
    private Vector3 savedPosition;

    // Variavel que vai receber um numero aleatorio de 1-6 (referencia aos 6 spawns)
    private int num = 0;

    // Lista que vai receber os numeros referentes aos spawnpoints para a seleçao random
    List<int> list = new List<int>();

    // Variavel responsável por receber uma seed aleatoria
    private int seed = 0;

    void Start()    
    {
        // Gera uma seed aleatoria para o random
        seed = (int)System.DateTime.Now.Ticks; 
        //Debug.Log("seed " + seed);
        Random.InitState(seed);

        popularLista();
        setSpawnChave();
    }
   
    // Função responsável por setar a posição em que a chave vai spawnar
    void setSpawnChave()
    {  
        for (int i = 0; i < 2; i++)
        {
            num = RandomGenerator();
            //Debug.Log("Posicao spawn " + num);
            
            switch (num)
            {
                case 1:
                    savedPosition = Spawn1.position;
                    break;

                case 2:
                    savedPosition = Spawn2.position;
                    break;

                case 3:
                    savedPosition = Spawn3.position;
                    break;

                case 4:
                    savedPosition = Spawn4.position;
                    break;

                case 5:
                    savedPosition = Spawn5.position;
                    break;

                case 6:
                    savedPosition = Spawn6.position;
                    break;
            }

            if(i == 0)
            {
                //Debug.Log("Chave 2 " + savedPosition);
                chave2.transform.position = savedPosition;
            }
            if(i == 1)
            {
                //Debug.Log("Chave 3 " + savedPosition);
                chave3.transform.position = savedPosition;
            }
        }       
    }

    // Funçao responsavel por popular a lista
    private void popularLista()
    {
        for (int n = 1; n <= 6; n++) 
        {
            list.Add(n);
        }
    }

    // Função responsável por selecionar números inteiros aleatórios sem repetição que estão dentro da lista
    private int RandomGenerator()
    {
        
        int index = Random.Range(0, list.Count);    //  Seleciona um elemento aleatorio da lista
        int i = list[index];    //  i = o numero selecionado aleatoriamennte
        list.RemoveAt(index);   //  Remove elemento da lista

        return i;
    }
}
