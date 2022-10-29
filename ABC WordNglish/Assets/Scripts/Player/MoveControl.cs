using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveControl : MonoBehaviour
{
    [Header("Propriedade do Movimento")]
    public float speed = 4;
    public float jump = 3;
    public float cont = 0.4f;

    [Space(5)]

    public bool isJumping;
    public bool isActive;

    [Header("Componentes")]
    public Animator Animations;
    public GameController gc;
    private Rigidbody2D rb2d;

    void Start()
    {
        Animations = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Movement();
        JumpPlayer();
        AnimState();
    }

    public void Movement()
    {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        transform.position += move * Time.deltaTime * speed;

        float a = Input.GetAxis("Horizontal");

        if (a > 0) //está indo
        {
            gc.dir = true;

            isActive = true; //está ativo andando
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }

        else if (a < 0) //está voltando
        {
            gc.dir = false;

            isActive = true; 
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }

        else
        {
            isActive = false; //parado
        }
    }


    public void JumpPlayer()
    {
        if (Input.GetButtonDown("Jump") && isJumping == false) //se não estiver pulando (variável false_)
        {
            isJumping = true;  //esta pulando fica verdadeiro quando a tecla é pressionada
            rb2d.AddForce(new Vector2(0f, jump), ForceMode2D.Impulse); //física do pulo

            SoundControl.sounds.somPulo.Play();
        }
    }

    void AnimState()
    {
        if (gc.isDamage == false)
        {
            if (isActive == true)
            {
                Animations.Play("CatWalking");
            }

            else if (isActive == false)
            {
                Animations.Play("CatIdle");
            }
        }

        else if (gc.isDamage == true)
        {
            cont -= Time.deltaTime;
            Animations.Play("CatDamage");

            if (cont < 0) 
                gc.isDamage = false; 
        }
    }
}