using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherGroundCheck : MonoBehaviour
{
    public OtherMoveControl Jump;
    //public EnemyControl ec;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //O objeto CHÃO recebe a tag GROUND
        if (collision.gameObject.tag == "Ground")
        {
            Jump.isJumping = false; //quando o groundcheck do player colide com o chão
        }
    }
}
