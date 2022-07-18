using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveControl : MonoBehaviour
{
    public float speed = 4;
    public float jump = 3;
    public float cont = 0.4f;

    public bool isJumping;
    public bool IsActive;

    public Animator Animations;

    private Rigidbody2D rb2d;
    public GameController gc;

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
            IsActive = true; //está ativo andando
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }

        else if (a < 0) //está voltando
        {
            IsActive = true; //está ativo andando
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }

        else
        {
            IsActive = false; //parado
        }
    }


    public void JumpPlayer()
    {
        if (Input.GetButtonDown("Jump") && isJumping == false) //se não estiver pulando (variável false_)
        {
            SoundControl.sounds.somPulo.Play();
            isJumping = true;  //esta pulando fica verdadeiro quando a tecla é pressionada
            rb2d.AddForce(new Vector2(0f, jump), ForceMode2D.Impulse); //física do pulo
        }
    }

    void AnimState()
    {
        if (gc.isDamage == false)
        {
            if (IsActive == true)
            {
                Animations.Play("CatWalking");
            }

            else if (IsActive == false)
            {
                Animations.Play("CatIdle");
            }

        }

        else if (gc.isDamage == true)
        {
            cont -= Time.deltaTime;
            Animations.Play("CatDamage");

            if (cont < 0) { gc.isDamage = false; }
        }
    }
}