using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public Text QtdCoins;
    public GameObject iconKey;
    public GameObject[] PanelObjetivoHUD = new GameObject[3];

    [Space(10)]

    [Header("LetterControl")]
    public int FoundLetters;
    //0 - nenhuma //1 - c // 2 - ca // 3 - cat

    public float moveSpeed;
    public float turnSpeed;

    public List<Transform> allLetters = new List<Transform>();
    
    public GameObject LetterC;
    public GameObject LetterA;
    public GameObject LetterT;
    public GameObject segueplayer;

    [Space(10)]

    [Header("Panel Control")]
    public GameObject panelWins;
    public GameObject panelOver;


    private void Start()
    {
        currentLife = maxLife;
        barraDeVida.maxValue = maxLife;

        iconKey.SetActive(false);

        panelWins.SetActive(false);
        panelOver.SetActive(false);
    }

    void Update() 
    {
        HUD();
        barraDeVida.value = currentLife;

        if(currentLife <= 0)
        {
            panelOver.SetActive(true);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("1") && FoundLetters == 0)
        {
            //Transform target = allLetters.Count == 0 ? transform : allLetters[allLetters.Count - 1];
            //collision.GetComponent<LetterControl>().LetterCollected(target, moveSpeed, turnSpeed);

            collision.GetComponent<LetterControl>().LetterCollected(segueplayer.transform, moveSpeed, turnSpeed);

            //allLetters.Add(collision.transform);
            allLetters.Add(LetterC.transform); //pos lista = 0

            allLetters[0] = LetterC.transform;

            Debug.Log("Colidiu com a LETRA C - " + allLetters.Count);

            SoundControl.sounds.somColectedOthers.Play();
            FoundLetters = 1;
        }

        if (collision.CompareTag("2") && FoundLetters == 1)
        {
            LetterC.transform.GetComponent<LetterControl>().LetterCollected(LetterA.transform, moveSpeed, turnSpeed);
            collision.GetComponent<LetterControl>().LetterCollected(segueplayer.transform, moveSpeed, turnSpeed);

            allLetters.Add(LetterA.transform);
            
            Debug.Log("Colidiu com a LETRA A -" + allLetters.Count);

            SoundControl.sounds.somColectedOthers.Play();
            FoundLetters = 2;
        }

        if (collision.CompareTag("3") && FoundLetters == 2)
        {
            collision.GetComponent<LetterControl>().LetterCollected(segueplayer.transform, moveSpeed, turnSpeed);
            LetterA.transform.GetComponent<LetterControl>().LetterCollected(LetterT.transform, moveSpeed, turnSpeed);

            allLetters.Add(LetterT.transform);

            Debug.Log("Colidiu com a LETRA T -" + allLetters.Count);

            SoundControl.sounds.somColectedOthers.Play();
            FoundLetters = 3;
        }

        //else { Debug.Log("levou dano"); } // precisa de um if dentro da condição, pq ta verificando os de baixo tbm

        if (collision.gameObject.CompareTag("Enemy"))
        { 
            if (ec.lifeEnemy == 1)
            {
                SoundControl.sounds.somDanoNoPlayer.Play();
                Debug.Log("-15 de vida");
                isDamage = true;
                currentLife -= ec.danoPlayer;
            }

            else isDamage = false;
        }

        if (collision.gameObject.CompareTag("Coin"))
        {
            Coins++;
        }
        
        if (collision.gameObject.CompareTag("Door") && FoundLetters == 3)
        {
            panelWins.SetActive(true);
        }
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
}