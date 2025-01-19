using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class desCub : MonoBehaviour
{
    // 当进入触发器时触发的函数
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bubble") || other.CompareTag("BigBubble"))
        {
            // 获取大泡泡的 BigBubbleAI 组件
            BigBubbleAI bigBubbleAI = other.GetComponent<BigBubbleAI>();
            if (bigBubbleAI!= null)
            {
                // 先将披萨的重力设为大泡泡存储的初始重力
                bigBubbleAI.pizza_Rb.gravityScale = bigBubbleAI.gravityScale;
            }
            // 销毁自身
            Destroy(this.gameObject);
            // 销毁触发碰撞的对象（大泡泡或普通泡泡）
            Destroy(other.gameObject);
        }
    }
}
