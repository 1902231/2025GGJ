using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BisicController : MonoBehaviour
{
    public float moveSpeed;
    public float jumpSpeed;
    public float jumpTime;
    public int jTimes;
    public BoxCollider2D feet;
    public float CreateBubbleTime_Mid;
    public float CreateBubbleTime_Big;
    public GameObject MidBubble;
    public GameObject BigBubble;
    public GameObject SmlBubble;
    public float offset;
    private bool hasXSpeed;

    private Rigidbody2D rb;
    private float timer;
    private int jTimes_p;
    private bool isJumping;
    private float timer_CreateBubble;
    private Transform playerTransform;



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerTransform = this.transform;
        timer = 0;
        timer_CreateBubble = 0;
        jTimes_p = jTimes;
    }


    void Update()
    {
        flip();
        Move();
        Jump();
        OnGround();
        CreateBubble();
    }

    void flip()
    {
        bool hasXSpeed = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
        if(hasXSpeed)
        {
            if(rb.velocity.x > 0.1f)
            {
                transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
            if(rb.velocity.x < -0.1f)
            {
                transform.localRotation = Quaternion.Euler(0, 180, 0);
            }
        }
    }
    void Move()
    {
        rb.velocity = new Vector3(Input.GetAxis("Horizontal") * moveSpeed, rb.velocity.y, 0);
    }
    void Jump()
    {
        if (jTimes_p <= 0)
            return;
        if (Input.GetKeyDown(KeyCode.K))
        {

        }
        if (Input.GetKey(KeyCode.K))
        {
            timer += Time.deltaTime;

            if (timer > jumpTime)
            {

                timer = jumpTime;
                return;
            }
            rb.velocity = new Vector3(rb.velocity.x, jumpSpeed * (1 - timer), 0);
        }
        if (Input.GetKeyUp(KeyCode.K))
        {
            jTimes_p--;

        }
    }
    
    void OnGround()
    {
        if (feet.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            timer = 0;
            jTimes_p = jTimes;
        }
    }
    void CreateBubble()
    {
        if(Input.GetKey(KeyCode.J))
        {
            timer_CreateBubble += Time.deltaTime;
            
            if(timer_CreateBubble >= CreateBubbleTime_Mid && timer_CreateBubble < CreateBubbleTime_Big)
            {
                Vector3 rightPosition = playerTransform.position + playerTransform.right * offset;
                GameObject midBubble = Instantiate(MidBubble, rightPosition, Quaternion.identity);
                timer_CreateBubble = 0;
            }
        }
        
        if(Input.GetKeyUp(KeyCode.J))
        {
            Debug.Log(timer_CreateBubble);
            timer_CreateBubble = 0;
        }
    }
}

