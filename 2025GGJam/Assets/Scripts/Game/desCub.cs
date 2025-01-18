using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class desCub : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bubble"))
        {
            Destroy(this.gameObject);
            Destroy(other.gameObject);
        }
    }
}
