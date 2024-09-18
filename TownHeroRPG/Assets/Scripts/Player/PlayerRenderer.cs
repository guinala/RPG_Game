using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRenderer : MonoBehaviour
{
    [Header("Dependencies")]
    public SpriteRenderer spriteRenderer;

    public void OnMovement(InputAction.CallbackContext value)
    {
        
        Vector2 movementInput = value.ReadValue<Vector2>();
        Debug.Log("Hola buenisimas + " + movementInput);
        Debug.Log(PlayerIsLookingLeft());

        if (movementInput.x > 0f) // Moving to the right
        {
            Debug.Log("Quiero morir");
            spriteRenderer.flipX = false;
            Debug.Log("I AM THE CLOUDS");
        }
        else if (movementInput.x < 0f) // Moving to the left
        {
            Debug.Log("La vida e suna mierda");
            spriteRenderer.flipX = true;
            Debug.Log("I AM THE STORM");
        }
    }

    private bool PlayerIsLookingLeft()
    {
        Debug.Log(spriteRenderer.flipX);
        return spriteRenderer.flipX;
    }
}
