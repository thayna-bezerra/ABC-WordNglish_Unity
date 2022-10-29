using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectibles : MonoBehaviour
{
    public GameController gc;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (this.gameObject.tag == "KeyBlue")
            {
                gc.KeysCollected++;
            }

            Destroy(this.gameObject);
            SoundControl.sounds.somColectedCoins.Play();
        }
    }
}
