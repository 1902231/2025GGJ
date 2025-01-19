using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using SwordFrames;
using Unity.VisualScripting;

public class MiddleBubbleAI : MonoBehaviour
{
    private Collider2D bubble;
    private Transform bubble_Transform;
    private Rigidbody2D bigBubble_Rb;
    private float timer;
    
    public Collider2D bigBubble;
    public Transform player_Transform;
    public Rigidbody2D player_Rb;
    public float explode_speed;
    public float explode_waitTime;
    public float range;

    private AudioSource deathSound;
    
    public void Awake()
    {
        player_Transform = GameObject.Find("Pizza").transform;
        player_Rb = GameObject.Find("Pizza").GetComponent<Rigidbody2D>();
    }
    public void Start()
    {
        bubble = GetComponent<Collider2D>();
        bubble_Transform = GetComponent<Transform>();
        deathSound = GetComponent<AudioSource>();
    }
    public void Update()
    {
        Destroy();
        Explode();
        isTouchPlayer();
        isTouchBubble();
    }

    public void Explode()
    {
        
        if (isTouchPlayer())
        {            
            player_Rb.velocity = new Vector3(player_Rb.velocity.x, explode_speed, 0);
            
            Destroy(this.gameObject);
        }

    }

    public bool isTouchBubble()
    {
        if (bubble.IsTouchingLayers(LayerMask.GetMask("BigBubble")))
        {
            return true;
        }
        else
        {
            return false;
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
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, range);
            foreach (Collider2D collider in colliders)
            {
                if(collider.CompareTag("BigBubble"))
                { 
                    Rigidbody2D big_Rb = collider.gameObject.GetComponent<Rigidbody2D>();
                    Vector3 dir = collider.gameObject.transform.position - transform.position;
                    collider.gameObject.GetComponent<BigBubbleAI>().isFly = true;
                    big_Rb.velocity = dir.normalized * explode_speed;
                }
            }
            AudioSourceManager.Instance.PlaySound("泡泡爆炸");
            Destroy(this.gameObject);
        }
    }
}
