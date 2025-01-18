using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBubbleAI : MonoBehaviour
{
    public float riseSpeed;
    public float ExplodeTime;

    private Rigidbody2D rb;
    private float timer_Explode;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        timer_Explode = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Rise();
        Destroy();
    }
    void Rise()
    {
        rb.velocity = new Vector3(rb.velocity.x, riseSpeed, 0);
    }
    void Eat_Pizza()
    {

    }
    void Destroy()
    {
        timer_Explode += Time.deltaTime;

        if(timer_Explode >= ExplodeTime)
        {
            Destroy(this.gameObject);
        }
    }
}
