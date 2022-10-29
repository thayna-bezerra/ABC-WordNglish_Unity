using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [Header("Sistema de Vida")]
    public int maxLife = 100;
    public int currentLife;
    public Slider barraDeVida;
    public bool isDamage = false;

    [Header("Sistema de Coletáveis")]
    public int Coins;
    public int KeysCollected = 0;

    [Header("HUD")]
    public GameObject iconKey;
    public GameObject[] PanelObjetivoHUD = new GameObject[3];
    public Text QtdCoins;

    [Header("LetterControl")]
    public int FoundLetters;
    //0 - nenhuma //1 - c // 2 - ca // 3 - cat

    public float moveSpeed;
    public float turnSpeed;

    public bool dir; //false voltando // true indo
    public string currentCollectedLetters;

    public List<Transform> allLetters = new List<Transform>();
    
    public GameObject Letter1;
    public GameObject Letter2;
    public GameObject Letter3;
    public GameObject FollowPlayer;

    [Header("Panel Control")]
    public GameObject panelWins;
    public GameObject panelOver;
    public GameObject panelPause;

    [Header("Componentes")]
    public EnemyControl ec;
    public Animator Animations;

    void Start()
    {
        Time.timeScale = 1f;

        currentLife = maxLife;
        barraDeVida.maxValue = maxLife;

        iconKey.SetActive(false);

        panelWins.SetActive(false);
        panelOver.SetActive(false);
        panelPause.SetActive(false);
    }

    void Update() 
    {
        LetterOrder();
        HUD();
        barraDeVida.value = currentLife;

        if(currentLife <= 0)
        {
            panelOver.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    void LetterOrder()
    {
        /// C A -> GATO
        //   INDO    //
        if (dir == true && currentCollectedLetters == "012")
        {
            Letter2.GetComponent<LetterControl>().LetterCollected(FollowPlayer.transform, moveSpeed, turnSpeed); //Para a letra A seguir o PLAYER
            Letter1.transform.GetComponent<LetterControl>().LetterCollected(Letter2.transform, moveSpeed, turnSpeed); //Para a letra C seguir a letra A
        }

        /// GATO -> C A
        //    VOLTANDO    //
        if (dir == false && currentCollectedLetters == "012")
        {
            Letter1.GetComponent<LetterControl>().LetterCollected(FollowPlayer.transform, moveSpeed, turnSpeed); //Para a letra C seguir o PLAYER
            Letter2.transform.GetComponent<LetterControl>().LetterCollected(Letter1.transform, moveSpeed, turnSpeed); //Para a letra A seguir a letra C
        }


        if (dir == true && currentCollectedLetters == "0123")
        {
            Letter3.GetComponent<LetterControl>().LetterCollected(FollowPlayer.transform, moveSpeed, turnSpeed); //Para a letra T seguir o PLAYER
            Letter2.GetComponent<LetterControl>().LetterCollected(Letter3.transform, moveSpeed, turnSpeed); //Para a letra A seguir a letra T
            Letter1.transform.GetComponent<LetterControl>().LetterCollected(Letter2.transform, moveSpeed, turnSpeed); //Para a letra C seguir a letra A
        }

        if (dir == false && currentCollectedLetters == "0123")
        {
            Letter1.GetComponent<LetterControl>().LetterCollected(FollowPlayer.transform, moveSpeed, turnSpeed); //Para a letra C seguir o PLAYER
            Letter2.transform.GetComponent<LetterControl>().LetterCollected(Letter1.transform, moveSpeed, turnSpeed); //Para a letra A seguir a letra C
            Letter3.transform.GetComponent<LetterControl>().LetterCollected(Letter2.transform, moveSpeed, turnSpeed); //Para a letra T seguir a letra A

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        #region VERIFICAÇÃO PRIMEIRA LETRA
        if (collision.CompareTag("1") && FoundLetters == 0)
        {
            collision.GetComponent<LetterControl>().LetterCollected(FollowPlayer.transform, moveSpeed, turnSpeed);

            allLetters.Add(Letter1.transform); //pos lista = 0

            Debug.Log("Colidiu com a LETRA C / D - " + allLetters.Count);

            //Definindo novos valores para as vars de verificação
            FoundLetters = 1;
            currentCollectedLetters = "01";

            SoundControl.sounds.somColectedOthers.Play();
        }

        else if (FoundLetters == 0 && collision.CompareTag("2"))
        {
            Debug.Log("DANO por tocar na letra A / O antes da letra C / D");
            DamagePlayer();
        }
        
        else if (FoundLetters == 0 && collision.CompareTag("3"))
        {
            Debug.Log("DANO por tocar na letra T / G antes da letra C / D");
            DamagePlayer();
        }

        #endregion

        #region VERIFICAÇÃO SEGUNDA LETRA

        if (collision.CompareTag("2") && FoundLetters == 1)
        {
            allLetters.Add(Letter2.transform);
            
            Debug.Log("Colidiu com a LETRA A / O -" + allLetters.Count);

            SoundControl.sounds.somColectedOthers.Play();
            FoundLetters = 2;
            currentCollectedLetters = "012";
        }

        else if (FoundLetters == 1 && collision.CompareTag("3"))
        {
            Debug.Log("DANO por tocar na letra T / G antes da letra A / O");
            DamagePlayer();
        }

        #endregion

        #region VERIFICAÇÃO TERCEIRA LETRA

        if (collision.CompareTag("3") && FoundLetters == 2)
        {
            allLetters.Add(Letter3.transform);

            Debug.Log("Colidiu com a LETRA T / G -" + allLetters.Count);

            FoundLetters = 3;
            currentCollectedLetters = "0123";

            SoundControl.sounds.somColectedOthers.Play();
        }

        #endregion

        if (collision.gameObject.CompareTag("Enemy"))
        { 
            if (ec.lifeEnemy == 1)
            {
                Debug.Log("DANO pelo inimigo");
                DamagePlayer();
            }

            else isDamage = false;
        }

        if (collision.gameObject.CompareTag("Coin"))
            Coins++;
        
        //Para vitória do level
        if (collision.gameObject.CompareTag("Door") && FoundLetters == 3)
        {
            Time.timeScale = 0f;
            panelWins.SetActive(true);
        }
    }

    public void DamagePlayer()
    {
        isDamage = true;
        currentLife -= ec.danoPlayer;

        SoundControl.sounds.somDanoNoPlayer.Play();
    }

    public void HUD()
    {
        QtdCoins.text = Coins.ToString();

        if (KeysCollected >= 1)
        {
            iconKey.SetActive(true);
        }

        else if (KeysCollected == 0)
        {
            iconKey.SetActive(false);
        }

        switch (FoundLetters)
        {
            case 3:
                PanelObjetivoHUD[0].SetActive(false); //com uma vida desativado
                PanelObjetivoHUD[1].SetActive(false); //com duas vidas desativado
                PanelObjetivoHUD[2].SetActive(true);  //com 3 vidas
                break;

            case 2:
                PanelObjetivoHUD[0].SetActive(false);
                PanelObjetivoHUD[1].SetActive(true);
                PanelObjetivoHUD[2].SetActive(false);
                break;

            case 1:
                PanelObjetivoHUD[0].SetActive(true);
                PanelObjetivoHUD[1].SetActive(false);
                PanelObjetivoHUD[2].SetActive(false);
                break;

            default:
                PanelObjetivoHUD[0].SetActive(false);
                PanelObjetivoHUD[1].SetActive(false);
                PanelObjetivoHUD[2].SetActive(false);
                break;
        }
    }

    #region MÉTODOS PARA OS BOTÕES
    public void OnPauseBtnClicked()
    {
        panelPause.SetActive(true);
        Time.timeScale = 0f;
    }

    public void OnRestartBtnClicked()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnContinueBtnClicked()
    {
        panelPause.SetActive(false);
        Time.timeScale = 1f;
    }

    public void OnWinLevelGoTo2()
    {
        SceneManager.LoadScene("Level2");
        Time.timeScale = 1f;
    }

    public void OnWinLevelGoTo3()
    {
        SceneManager.LoadScene("Level3");
        Time.timeScale = 1f;
    }

    public void GoToHome()
    {
        SceneManager.LoadScene("_Menu");
    }
    #endregion
}