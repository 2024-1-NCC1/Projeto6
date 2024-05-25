using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Track : MonoBehaviour
{
    [System.Serializable]
    public class Coletavel
    {
        public string nome;
        public GameObject prefab;
        public bool isSaudavel;
    }

    public List<Coletavel> coletaveis;
    public Vector3 numberOfcoletaveis;
    public float trackLength = 162f;
    public float coletavelSpawnRangeX = 0.890f;

    void Start()
    {
        Generatecoletaveis();
    }

    void Generatecoletaveis()
    {
        int newNumberOfcoletaveis = Random.Range((int)numberOfcoletaveis.x, (int)numberOfcoletaveis.y);

        for (int i = 0; i < newNumberOfcoletaveis; i++)
        {
            // Escolhe um colecionável aleatório da lista
            Coletavel coletavelInfo = coletaveis[Random.Range(0, coletaveis.Count)];

            // Calcula a posição aleatória
            float posX = Random.Range(-1f, 1f); // Varie a posição X entre -1 e 1 (representando as pistas)
            float posZ = Random.Range(0f, trackLength); // Varie a posição Z ao longo da pista

            // Instancia o colecionável na posição correta ao longo da pista
            Vector3 spawnPosition = new Vector3(posX * coletavelSpawnRangeX, 0.5f, posZ);
            GameObject newColetavel = Instantiate(coletavelInfo.prefab, spawnPosition, Quaternion.identity, transform);

            // Adiciona o colecionável à lista de novos colecionáveis
            newColetavel.transform.parent = transform; // Define o pai como o objeto Track
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Move a pista para frente
            transform.position = new Vector3(0, 0, transform.position.z + trackLength);
            // Gera novos colecionáveis
            Generatecoletaveis();
        }
    }
}
