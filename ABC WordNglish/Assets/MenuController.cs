using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public GameObject panelTelaInicial;
    public GameObject panelSelecaoFase;
    private void Start()
    {
        //Iniciar apenas com o panelTelaInicial ATIVO
        panelTelaInicial.SetActive(true);

        panelSelecaoFase.SetActive(false);
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
    public void levelName(string name)
    {
        SceneManager.LoadScene(name);
    }

}
