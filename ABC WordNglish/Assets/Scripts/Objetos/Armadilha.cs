using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armadilha : MonoBehaviour
{
    public GameController gc;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (this.gameObject.CompareTag("Espinho"))
        {
            print("espinho");
            gc.DamagePlayer();
        }

        if (this.gameObject.CompareTag("morte"))
        {
            print("Morte");
            gc.currentLife = 0;
        }
    }
}
