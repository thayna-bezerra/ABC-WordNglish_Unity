using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [Header("PAINEIS")]
    public GameObject panelTelaInicial;
    public GameObject panelSelecaoFase;

    private void Start()
    {
        ativarPanelInicial(); //Sempre iniciar com a Tela Inicial ATIVA
    }

    public void ativarSelecaoFasel()
    {
        panelTelaInicial.SetActive(false);
        panelSelecaoFase.SetActive(true);
    }

    public void ativarPanelInicial()
    {
        panelTelaInicial.SetActive(true);
        panelSelecaoFase.SetActive(false);
    }

    public void levelName(string name) //Colocar nome da cena no metodo OnClick do btn
    {
        SceneManager.LoadScene(name);
    }
}