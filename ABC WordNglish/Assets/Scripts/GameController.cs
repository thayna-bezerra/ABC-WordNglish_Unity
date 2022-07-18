using System.Collections;
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
        HUD();
        barraDeVida.value = currentLife;

        if(currentLife <= 0)
        {
            panelOver.SetActive(true);
            //Time.timeScale = 0f;
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

            Debug.Log("Colidiu com a LETRA C - " + allLetters.Count);

            SoundControl.sounds.somColectedOthers.Play();
            FoundLetters = 1;
        }

        else if (FoundLetters == 0 && collision.CompareTag("2"))
        {
            Debug.Log("DANO por tocar na letra A antes da letra C");
            DamagePlayer();
        }
        
        else if (FoundLetters == 0 && collision.CompareTag("3"))
        {
            Debug.Log("DANO por tocar na letra T antes da letra C");
            DamagePlayer();
        }

        if (collision.CompareTag("2") && FoundLetters == 1)
        {
            collision.GetComponent<LetterControl>().LetterCollected(FollowPlayer.transform, moveSpeed, turnSpeed);
            Letter1.transform.GetComponent<LetterControl>().LetterCollected(Letter2.transform, moveSpeed, turnSpeed);

            allLetters.Add(Letter2.transform);
            
            Debug.Log("Colidiu com a LETRA A -" + allLetters.Count);

            SoundControl.sounds.somColectedOthers.Play();
            FoundLetters = 2;
        }

        else if (FoundLetters == 1 && collision.CompareTag("3"))
        {
            Debug.Log("DANO por tocar na letra T antes da letra A");
            DamagePlayer();
        }

        if (collision.CompareTag("3") && FoundLetters == 2)
        {
            collision.GetComponent<LetterControl>().LetterCollected(FollowPlayer.transform, moveSpeed, turnSpeed);
            Letter2.transform.GetComponent<LetterControl>().LetterCollected(Letter3.transform, moveSpeed, turnSpeed);

            allLetters.Add(Letter3.transform);

            Debug.Log("Colidiu com a LETRA T -" + allLetters.Count);

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

}