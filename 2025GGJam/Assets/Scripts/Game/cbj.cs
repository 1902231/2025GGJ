using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cbj : MonoBehaviour
{
    public float force = 10;
    public Vector2 forceVector = Vector2.left;
    public bool cfjOpen = false;
    private Rigidbody2D targetRigidbody;

    private void OnTriggerEnter2D(Collider2D other)
    {
        GiveWind(other);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        GiveWind(other);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Bubble")|| other.CompareTag("BigBubble"))
        {
            targetRigidbody = other.GetComponent<Rigidbody2D>();
            if (targetRigidbody!= null)
            {
                StartCoroutine(SlowDownBubble(targetRigidbody));
            }
        }
    }

    private void GiveWind(Collider2D other)
    {
        if (cfjOpen)
        {
            if (other.CompareTag("Bubble") || other.CompareTag("BigBubble"))
            {
                Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
                if (rb!= null)
                {
                    rb.AddForce(forceVector * force);
                }
                else
                {
                    Debug.Log("No Rigidbody2D found on the object");
                }
            }
            else
            {
                Debug.Log("Object is not tagged as Bubble");
            }
        }
        else
        {
            Debug.Log("cfjOpen is false");
        }
    }

    private IEnumerator SlowDownBubble(Rigidbody2D rb)
    {
        // 定义一个阻尼系数，用于控制速度减小的速度，可以根据需要调整
        float damping = 0.95f; 
        while (rb!= null && rb.velocity.magnitude > 0.1f)
        {
            if (rb == null)
            {
                yield break;
            }
            // 使用阻尼的方式逐渐减小速度
            rb.velocity *= damping; 
            yield return new WaitForFixedUpdate();
        }
        if (rb!= null)
        {
            rb.velocity = Vector2.zero;
        }
    }
}
