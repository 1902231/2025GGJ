using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class BisicController : MonoBehaviour
{
    public float moveSpeed;
    public float jumpSpeed;
    public float jumpTime;//最好不要大于1，否则可能会长按跳跃向上的速度衰减过头变成向下的速度
    public int jTimes;
    public BoxCollider2D feet;
    public float CreateBubbleTime_Mid;
    public float CreateBubbleTime_Big;
    public GameObject MidBubble;
    public GameObject BigBubble;
    public GameObject SmlBubble;
    public float offset;
    public float offset_Big_Up;
    public float offset_Big_Right;


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
        if (hasXSpeed)
        {
            if (rb.velocity.x > 0.1f)
            {
                transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
            if (rb.velocity.x < -0.1f)
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
        // 检查是否与 Ground 物体发生碰撞
        if (feet.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            timer = 0;
            jTimes_p = jTimes;
        }
    }
    void CreateBubble()
    {
        if (Input.GetKey(KeyCode.J))
        {
            timer_CreateBubble += Time.deltaTime;
            Debug.Log(timer_CreateBubble);

        }
        if (Input.GetKeyUp(KeyCode.J))
        {
            if (timer_CreateBubble >= CreateBubbleTime_Mid && timer_CreateBubble < CreateBubbleTime_Big)
            {

                // 计算玩家右边的位置
                Vector3 rightPosition = playerTransform.position + playerTransform.right * offset;
                // 创建新的物体
                GameObject midBubble = Instantiate(MidBubble, rightPosition, Quaternion.identity);

            }
            else if (timer_CreateBubble >= CreateBubbleTime_Big)
            {
                // 计算玩家右上角的位置
                Vector3 rightPosition = playerTransform.position + playerTransform.right * offset_Big_Right+playerTransform.up*offset_Big_Up;
                // 创建新的物体
                GameObject midBubble = Instantiate(BigBubble, rightPosition, Quaternion.identity);
            }

            timer_CreateBubble = 0;

        }
    }

    


}

