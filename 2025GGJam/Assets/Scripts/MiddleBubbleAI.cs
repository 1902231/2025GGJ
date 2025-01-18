using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiddleBubbleAI : MonoBehaviour
{
    private Collider2D bubble;
    private Transform bubble_Transform;
    private float timer;


    public Transform player_Transform;
    public Rigidbody2D player_Rb;
    public float explode_speed;
    public float explode_waitTime;

    public void Start()
    {
        bubble = GetComponent<Collider2D>();
        bubble_Transform = GetComponent<Transform>();

    }
    public void Update()
    {
        Destroy();
        Explode();
        isTouchPlayer();
    }

    public void Explode()
    {
        
        if (isTouchPlayer())
        {
            Vector3 dir = (player_Transform.position - bubble_Transform.position).normalized;
            player_Rb.AddForce(dir * explode_speed, ForceMode2D.Impulse);
            Destroy(this.gameObject);
        }
        


        
    }

    public bool isTouchPlayer()
    {
        if (bubble.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void Destroy()
    {
        timer += Time.deltaTime;
        if (timer > explode_waitTime)
        {
            Destroy(this.gameObject);

        }
    }
}
