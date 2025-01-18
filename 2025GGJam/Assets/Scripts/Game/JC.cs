using UnityEngine;

public class JC : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bubble"))
        {
            Debug.Log(other.name);
            Destroy(other.gameObject);
        }
    }
}
