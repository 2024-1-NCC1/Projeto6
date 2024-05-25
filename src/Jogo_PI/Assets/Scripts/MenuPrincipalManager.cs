using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipalManager : MonoBehaviour
{
    public string nomeDoLevelDeJogo;
    public string nomeDoLevelDeJogo2;
    public GameObject painelMenuInicial;
    public GameObject painelOpcoes;
    public GameObject painelJogos;

    public void JogarAfrica()
    {
        SceneManager.LoadScene(nomeDoLevelDeJogo2);
    }

    public void JogarEuropa()
    {
        SceneManager.LoadScene(nomeDoLevelDeJogo);
    }

    public void AbrirJogos()
    {
        painelMenuInicial.SetActive(false);
        painelOpcoes.SetActive(false);
        painelJogos.SetActive(true);
    }

    public void AbrirOpcoes()
    {
        painelMenuInicial.SetActive(false);
        painelOpcoes.SetActive(true);
        painelJogos.SetActive(false);
    }

    public void FecharOpcoes()
    {
        painelMenuInicial.SetActive(true);
        painelOpcoes.SetActive(false);
        painelJogos.SetActive(false);
    }

    public void SairDoJogo()
    {
        Application.Quit();
        Debug.Log("Sair do Jogo");
    }

    public void Morte()
    {
        SceneManager.LoadScene("Menu");
        painelMenuInicial.SetActive(true);
        painelOpcoes.SetActive(false);
        painelJogos.SetActive(false);
    }
}
