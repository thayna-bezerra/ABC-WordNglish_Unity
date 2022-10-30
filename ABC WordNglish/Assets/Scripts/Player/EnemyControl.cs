using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    [Header("Movimentação Inimigo")]
    public float speed;
    public GameObject point1, point2;
    private Vector2 nextPos;

    [Header("Vida Inimigo")]
    public int lifeEnemy = 1;

    public bool isDying =  false;

    public int danoPlayer = 10;
    public float cont = 1.5f;
    [Space]
    public Animator Animations;

    private void Start()
    {
        nextPos = point2.transform.position;
    }

    void Update()
    {
        MoveEnemy();
    }

    public void MoveEnemy()
    {
        if (transform.position == point1.transform.position)
        {
            nextPos = point2.transform.position;
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }

        if(transform.position == point2.transform.position)
        {
            nextPos = point1.transform.position;
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }

        transform.position = Vector2.MoveTowards(transform.position, nextPos, speed);
    }

    public void EnemyDead()
    {
        if (isDying == true)
        {
            lifeEnemy = 0;

            cont -= Time.deltaTime;

            Animations.Play("EnemyDead");
            
            if (cont < 0) Destroy(this.gameObject);
        }
    }
}
