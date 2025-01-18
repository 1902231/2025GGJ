using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class xlSlider : MonoBehaviour
{
    public Slider slider;
    public float slvale = 1;
    
    public Transform character;
    public Vector3 offset = new Vector3(-1, 0, 0); // 滑块相对于角色的偏移量
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = character.GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D component not found on the character.");
        }
    }

    void LateUpdate()
    {
        if (character!= null && slider!= null)
        {
            // 计算滑块的新位置
            Vector3 newPosition = character.position + offset;
            // 根据角色的朝向调整偏移量
            if (rb!= null && rb.velocity.x < 0)
            {
                offset.x = Mathf.Abs(offset.x);
            }
            else if (rb!= null && rb.velocity.x > 0)
            {
                offset.x = -Mathf.Abs(offset.x);
            }
            newPosition = character.position + offset;
            slider.transform.position = newPosition;
        }
    }

    void Update()
    {
        XL();
        FlipSlider();
    }

    public void XL()
    {
        if (Input.GetKey(KeyCode.J))
        {
            slider.value += slvale * Time.deltaTime;
            slider.value = Mathf.Min(slider.value, slider.maxValue);
        }
        else
        {
            slider.value -= slvale * Time.deltaTime;
            slider.value = Mathf.Max(slider.value, 0);
        }
    }
    
    
    private void FlipSlider()
    {
        if (rb == null)
        {
            return;
        }
        bool hasXSpeed = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
        if (hasXSpeed)
        {
            if (rb.velocity.x > 0.1f)
            {
                offset.x = -Mathf.Abs(offset.x);
            }
            else if (rb.velocity.x < -0.1f)
            {
                offset.x = Mathf.Abs(offset.x);
            }
        }
    }
}
