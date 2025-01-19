using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using SwordFrames;
using Unity.VisualScripting;

public class BigBubbleAI : MonoBehaviour
{
    public float riseSpeed;
    public float ExplodeTime;
    public Transform pizza_Transform;
    public Rigidbody2D pizza_Rb;
    public  float smoothSpeed;
    public bool isFly;

    private Rigidbody2D rb;
    private Collider2D bigBubble_Collider;
    private float timer_Explode;
    public float gravityScale;
    
    private void Awake()
    {
        isFly = false;
        pizza_Transform = GameObject.Find("Pizza").GetComponent<Transform>();
        pizza_Rb = GameObject.Find("Pizza").GetComponent<Rigidbody2D>();
        gravityScale = pizza_Rb.gravityScale;
        
    }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bigBubble_Collider = GetComponent<Collider2D>();

        timer_Explode = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Rise();
        BigBubbleDestroy();
        Eat_Pizza();
    }
    
    void Rise()
    {
        if(isFly)
        {
            return;
        }
        rb.velocity = new Vector3(rb.velocity.x, riseSpeed, 0);

    }
    void Eat_Pizza()
    {
        if(bigBubble_Collider.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            pizza_Rb.velocity = new Vector3(pizza_Rb.velocity.x, 0, 0);
                if (pizza_Transform.position != transform.position)
                {
                    Vector3 targetPos = transform.position;
                    pizza_Transform.position = Vector3.Lerp(pizza_Transform.position, targetPos, smoothSpeed);
                    pizza_Rb.gravityScale = 0;
                }
        }
    }
    public void BigBubbleDestroy()
    {
        timer_Explode += Time.deltaTime;
        if(timer_Explode >= ExplodeTime || bigBubble_Collider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            pizza_Rb.gravityScale = gravityScale;
            AudioSourceManager.Instance.PlaySound("泡泡爆炸");
            Destroy(this.gameObject);
        }
        
    }
}
