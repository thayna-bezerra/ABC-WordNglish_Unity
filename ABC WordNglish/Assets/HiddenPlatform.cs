using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenPlatform : MonoBehaviour
{
    public SpriteRenderer platform;

    void Iniciar()
    {
        platform = GetComponent<SpriteRenderer>();
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            platform.color = new Color(0, 0, 0, 0);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            platform.color = new Color(1, 1, 1, 1);
        }
    }
}
