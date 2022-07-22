using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class armadilhaEspinhos : MonoBehaviour
{
    public GameController player;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.DamagePlayer();
        }
    }
}
