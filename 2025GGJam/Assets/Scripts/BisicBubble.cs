using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BisicBubble
{
    public int mass;
    public Collider2D bubble;
    public Transform bubble_Transform;
    public Transform player_Transform;
    public Rigidbody2D player_Rb;
    public float explode_speed;



    public void Explode() 
    { 
        if(isTouchPlayer())
        {
            Vector3 dir = (player_Transform.position - bubble_Transform.position).normalized;
            player_Rb.velocity = dir * explode_speed;
        }
    }

    public bool isTouchPlayer()
    {
        if(bubble.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void UpDate()
    {
        Explode();
    }
}
