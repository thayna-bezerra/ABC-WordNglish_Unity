using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockLock : MonoBehaviour
{
    public GameController KeyControl;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if(KeyControl.KeysCollected >= 1)
            {
                KeyControl.KeysCollected--;
                Destroy(this.gameObject);
            }
        }
    }
}
