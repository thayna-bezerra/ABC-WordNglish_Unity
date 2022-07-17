using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterControl : MonoBehaviour
{
    public GameController gc;

    private Transform followTarget;
    private float moveSpeed;
    private float turnSpeed;

    public bool isCollected = false;

    private void Start()
    {
        gc.FoundLetters = 0;
    }

    public void LetterCollected(Transform followTarget, float moveSpeed, float turnSpeed)
    {
        this.followTarget = followTarget;
        this.moveSpeed = moveSpeed;
        this.turnSpeed = turnSpeed;

        isCollected = true;

        GetComponent<Collider2D>().enabled = false;
    }

    void Update()
    {
        if (isCollected == true) //quando o objeto com a tag 1/2/3 for tocado, vai chamar essa movimentção
        {
            //DIREÇÃO
            Vector3 currentPosition = transform.position;
            Vector3 moveDirection = followTarget.position - currentPosition;

            //DISTÂNCIA 
            float distanceToTarget = moveDirection.magnitude;

            if (distanceToTarget > moveSpeed)
            {
                distanceToTarget = moveSpeed;
            }

            moveDirection.Normalize();

            Vector3 target = moveDirection * distanceToTarget + currentPosition;

            transform.position = Vector3.Lerp(currentPosition, target, moveSpeed * Time.deltaTime);
        }
    }


   /* private void OnTriggerEnter2D(Collider2D collision) //sistema de coleta de letras e perca de vida caso seja a letra errada
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (this.gameObject.CompareTag("1") && gc.FoundLetters == 0)
            {
                //Destroy(this.gameObject);
                Debug.Log("encontrou letra C");
                SoundControl.sounds.somColectedOthers.Play();
                gc.FoundLetters = 1;
            }

            if (this.gameObject.CompareTag("2") && gc.FoundLetters == 1)
            {
                //Destroy(this.gameObject);
                Debug.Log("encontrou letra A");
                SoundControl.sounds.somColectedOthers.Play();
                gc.FoundLetters = 2;
            }

            if (this.gameObject.CompareTag("3") && gc.FoundLetters == 2)
            {
                //Destroy(this.gameObject);
                Debug.Log("encontrou letra T");
                SoundControl.sounds.somColectedOthers.Play();
                gc.FoundLetters = 3;
            }
        }

        else if (this.gameObject.CompareTag("1") && gc.FoundLetters == 1 || gc.FoundLetters == 2 || gc.FoundLetters == 3)
        {
            Debug.Log("perdeu vida"); gc.currentLife -= 50; gc.isDamage = true;
        }

        else if (this.gameObject.CompareTag("2") && gc.FoundLetters == 0 || gc.FoundLetters == 2 || gc.FoundLetters == 3)
        {
            Debug.Log("perdeu vida"); gc.currentLife -= 50; gc.isDamage = true;
        }

        else if (this.gameObject.CompareTag("3") && gc.FoundLetters == 0 || gc.FoundLetters == 1 || gc.FoundLetters == 3)
        {
            Debug.Log("perdeu vida"); gc.currentLife -= 50; gc.isDamage = true;
        }

    }*/
}