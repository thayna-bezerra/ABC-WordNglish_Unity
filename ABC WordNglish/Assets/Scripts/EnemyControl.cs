using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    public float speed;
    public GameObject point1, point2;
    public Vector2 nextPos;

    public int lifeEnemy = 1;

    public bool isDying =  false;
    public Animator Animations;

    public int danoPlayer = 15;
    public float cont = 1.5f;

    private void Start()
    {
        nextPos = point2.transform.position;
    }

    void Update()
    {
        MoveEnemy();
        EnemyDead();
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
