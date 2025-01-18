using UnityEngine;

// 门类
public class Door : MonoBehaviour
{
    private Button1 buttonClass;

    void Start()
    {
        // 查找场景中的 ButtonClass 实例
        buttonClass = FindObjectOfType<Button1>();
        if (buttonClass!= null)
        {
            buttonClass.OnButtonPressed += OpenDoor;
        }
    }

    void OpenDoor(bool isPressed)
    {
        if (isPressed)
        {
            Debug.Log("Door is opening");
            transform.position += new Vector3(0, 5, 0);
        }
    }

    void OnDestroy()
    {
        if (buttonClass!= null)
        {
            buttonClass.OnButtonPressed -= OpenDoor;
        }
    }
}
