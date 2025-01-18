using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed;
    private void Awake()
    {
        target = GameObject.Find("Pizza").transform;
    }
    void FixedUpdate()
    {
       if(target != null)
        {
            if(transform.position !=target.position)
            {
                Vector3 targetPos = target.position;
                transform.position = Vector3.Lerp(transform.position, targetPos, smoothSpeed);
            }
        }
    }
}
