﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public EnemyControl ec;
    public Animator Animations;

    [Header("Sistema de Vida")]
    public int maxLife = 100;
    public int currentLife;
    public Slider barraDeVida;
    public bool isDamage = false;

    [Space(10)]

    [Header("Sistema de Coletáveis")]
    public int Coins;
    [Space]
    public int KeysCollected = 0;

    [Space(10)]

    [Header("HUD")]
    public GameObject iconKey;
    public GameObject[] PanelObjetivoHUD = new GameObject[3];
    public Text QtdCoins;

    [Space(10)]

    [Header("LetterControl")]
    public int FoundLetters;
    //0 - nenhuma //1 - c // 2 - ca // 3 - cat

    public float moveSpeed;
    public float turnSpeed;

    public bool dir; //false voltando // true indo


    public List<Transform> allLetters = new List<Transform>();
    
    public GameObject Letter1;
    public GameObject Letter2;
    public GameObject Letter3;
    public GameObject FollowPlayer;

    [Space(10)]

    [Header("Panel Control")]
    public GameObject panelWins;
    public GameObject panelOver;
    public GameObject panelPause;

    void Start()
    {
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
            //Time.timeScale = 0f;
        }

    }

    void LetterOrder()
    {
        /// C A -> GATO
        //   INDO    //
        if (dir == true && FoundLetters == 1)
        {
            Letter2.GetComponent<LetterControl>().LetterCollected(FollowPlayer.transform, moveSpeed, turnSpeed); //Para a letra A seguir o PLAYER
            Letter1.transform.GetComponent<LetterControl>().LetterCollected(Letter2.transform, moveSpeed, turnSpeed); //Para a letra C seguir a letra A
        }
        /// GATO -> C A
        //    VOLTANDO    //
        if (dir == false && FoundLetters == 1)
        {
            Letter1.GetComponent<LetterControl>().LetterCollected(FollowPlayer.transform, moveSpeed, turnSpeed); //Para a letra C seguir o PLAYER
            Letter2.transform.GetComponent<LetterControl>().LetterCollected(Letter1.transform, moveSpeed, turnSpeed); //Para a letra A seguir a letra C
        }


        if (dir == true && FoundLetters == 2)
        {
            Letter3.GetComponent<LetterControl>().LetterCollected(FollowPlayer.transform, moveSpeed, turnSpeed); //Para a letra T seguir o PLAYER
            Letter2.GetComponent<LetterControl>().LetterCollected(Letter3.transform, moveSpeed, turnSpeed); //Para a letra A seguir a letra T
            Letter1.transform.GetComponent<LetterControl>().LetterCollected(Letter2.transform, moveSpeed, turnSpeed); //Para a letra C seguir a letra A
        }

        if (dir == false && FoundLetters == 2)
        {
            Letter1.GetComponent<LetterControl>().LetterCollected(FollowPlayer.transform, moveSpeed, turnSpeed); //Para a letra C seguir o PLAYER
            Letter2.transform.GetComponent<LetterControl>().LetterCollected(Letter1.transform, moveSpeed, turnSpeed); //Para a letra A seguir a letra C
            Letter3.transform.GetComponent<LetterControl>().LetterCollected(Letter2.transform, moveSpeed, turnSpeed); //Para a letra T seguir a letra A

        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("1") && FoundLetters == 0)
        {
            //Transform target = allLetters.Count == 0 ? transform : allLetters[allLetters.Count - 1];
            //collision.GetComponent<LetterControl>().LetterCollected(target, moveSpeed, turnSpeed);
            //allLetters.Add(collision.transform);
            
            collision.GetComponent<LetterControl>().LetterCollected(FollowPlayer.transform, moveSpeed, turnSpeed);

            allLetters.Add(Letter1.transform); //pos lista = 0

            Debug.Log("Colidiu com a LETRA C / D - " + allLetters.Count);

            SoundControl.sounds.somColectedOthers.Play();
            FoundLetters = 1;
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

        if (collision.CompareTag("2") && FoundLetters == 1)
        {
            //collision.GetComponent<LetterControl>().LetterCollected(FollowPlayer.transform, moveSpeed, turnSpeed);
            //Letter1.transform.GetComponent<LetterControl>().LetterCollected(Letter2.transform, moveSpeed, turnSpeed);

            allLetters.Add(Letter2.transform);
            
            Debug.Log("Colidiu com a LETRA A / O -" + allLetters.Count);

            SoundControl.sounds.somColectedOthers.Play();
            FoundLetters = 2;
        }

        else if (FoundLetters == 1 && collision.CompareTag("3"))
        {
            Debug.Log("DANO por tocar na letra T / G antes da letra A / O");
            DamagePlayer();
        }

        if (collision.CompareTag("3") && FoundLetters == 2)
        {
            //collision.GetComponent<LetterControl>().LetterCollected(FollowPlayer.transform, moveSpeed, turnSpeed);
            //Letter2.transform.GetComponent<LetterControl>().LetterCollected(Letter3.transform, moveSpeed, turnSpeed);

            allLetters.Add(Letter3.transform);

            Debug.Log("Colidiu com a LETRA T / G -" + allLetters.Count);

            SoundControl.sounds.somColectedOthers.Play();
            FoundLetters = 3;
        }

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
        {
            Coins++;
        }
        
        if (collision.gameObject.CompareTag("Door") && FoundLetters == 3)
        {
            Time.timeScale = 0f;
            panelWins.SetActive(true);
        }
    }

    public void DamagePlayer()
    {
        SoundControl.sounds.somDanoNoPlayer.Play();
        isDamage = true;
        currentLife -= ec.danoPlayer;
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

    public void OnPauseBtnClicked()
    {
        panelPause.SetActive(true);
        Time.timeScale = 0f;
    }
    public void OnRestartBtnClicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }
    public void OnContinueBtnClicked()
    {
        panelPause.SetActive(false);
        Time.timeScale = 1f;
    }
    public void OnWinLevel()
    {
        SceneManager.LoadScene("Level2");
    }

}