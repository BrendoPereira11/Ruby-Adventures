using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    void Update()
    {
        float horizontal = 0.0f;

        if (Keyboard.current.leftArrowKey.isPressed)
        {
            horizontal = -1.0f;
        }
        else if (Keyboard.current.rightArrowKey.isPressed)
        {
            horizontal = 1.0f;
        }

        float vertical = 0.0f;

        if (Keyboard.current.upArrowKey.isPressed)
        {
            vertical = 1.0f;
        }
        else if (Keyboard.current.downArrowKey.isPressed)
        {
            vertical = -1.0f;
        }

        Vector2 position = transform.position;

        position.x += 0.1f * horizontal;
        position.y += 0.1f * vertical;

        transform.position = position;
    }
}