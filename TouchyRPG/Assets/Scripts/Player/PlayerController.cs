using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class PlayerController : MonoBehaviour
{
    [Header("Configuration")]
    public float speed;

    [Header("Dependencies")]
    public Rigidbody2D rigidbody;


    // Private
    private Vector2 _movementInput;


    private void FixedUpdate()
    {
        rigidbody.velocity = _movementInput * speed;
    }

    public void OnMovement(InputAction.CallbackContext value)
    {
        _movementInput = value.ReadValue<Vector2>();
    }

    public void OnQuest(InputAction.CallbackContext value)
    {
        Debug.Log("Entrando en seccion critica");
     
        Debug.Log("NAH ID WIN");
        if (value.started)
        {
            GameEventManager.instance.inputEvents.QuestLogTogglePressed();
        }
        
    }

}
