using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    public Text tutoMove;
    public Text tutoJump;
    public Text tutoEnemy;
    public Text tutoLetter;

    public Image keys;
    public Image spaceBar;

    private void OnTriggerEnter2D(Collider2D collision)
    {
         if (collision.CompareTag("Player") && this.gameObject.CompareTag("Tutorial/Movement"))
         {
             tutoMove.gameObject.SetActive(true);
             keys.gameObject.SetActive(true);
         }

         if (collision.CompareTag("Player") && this.gameObject.CompareTag("Tutorial/Jump"))
         {
             tutoJump.gameObject.SetActive(true);
             spaceBar.gameObject.SetActive(true);
         }

        if (collision.CompareTag("Player") && this.gameObject.CompareTag("Tutorial/Enemy"))
        {
            tutoEnemy.gameObject.SetActive(true);
        }

        if (collision.CompareTag("Player") && this.gameObject.CompareTag("Tutorial/Letter"))
        {
            tutoLetter.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && this.gameObject.CompareTag("Tutorial/Movement"))
        {
            tutoMove.gameObject.SetActive(false);
            keys.gameObject.SetActive(false);
        }
        
        if (collision.CompareTag("Player") && this.gameObject.CompareTag("Tutorial/Jump"))
        {
            tutoJump.gameObject.SetActive(false);
            spaceBar.gameObject.SetActive(false);
        }

        if (collision.CompareTag("Player") && this.gameObject.CompareTag("Tutorial/Enemy"))
        {
            tutoEnemy.gameObject.SetActive(false);
        }

        if (collision.CompareTag("Player") && this.gameObject.CompareTag("Tutorial/Letter"))
        {
            tutoLetter.gameObject.SetActive(false);
        }

    }
}
