using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BisicController : MonoBehaviour
{
    public float moveSpeed;
    public float jumpSpeed;
    public float jumpTime;//��ò�Ҫ����1��������᳤ܻ����Ծ���ϵ��ٶ�˥����ͷ������µ��ٶ�
    public int jTimes;
    public BoxCollider2D feet;


    private Rigidbody2D rb;
    private float timer;
    private int jTimes_p;
    private bool isJumping;



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        timer = 0;
        jTimes_p = jTimes;
    }


    void Update()
    {
        Move();
        Jump();
        OnGround();
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
            Debug.Log(timer);
            if (timer > jumpTime)
            {
                Debug.Log(111);
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
        // ����Ƿ��� Ground ���巢����ײ
        if (feet.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            timer = 0;
            jTimes_p = jTimes;
        }
    }
}

