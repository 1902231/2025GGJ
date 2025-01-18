using UnityEngine;

// 按钮类
public class Button1 : MonoBehaviour
{
    public bool isButtonPressed = false;
    public delegate void ButtonPressedHandler(bool isPressed);
    public event ButtonPressedHandler OnButtonPressed;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other)
        {
            PressButton();
        }
    }
    
    public void PressButton()
    {
        isButtonPressed = true;
        OnButtonPressed?.Invoke(isButtonPressed);
        Debug.Log("Button Pressed");
    }
}
