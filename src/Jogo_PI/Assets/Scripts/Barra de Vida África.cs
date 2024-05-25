using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BarradevidaAfrica : MonoBehaviour
{
    public Slider Vida;
    public float taxaDeDecremento = 0.5f;

    void Start()
    {
        Vida.value = 100f;
    }

    void Update()
    {
        // Diminuir a vida ao longo do tempo
        Vida.value -= taxaDeDecremento * Time.deltaTime;

        if (Vida.value <= 0)
        {
            SceneManager.LoadScene("Menu");

        }
    }

    // Função para receber dano
    public void ReceberDano(int dano)
    {
        Vida.value -= dano;
    }
}
