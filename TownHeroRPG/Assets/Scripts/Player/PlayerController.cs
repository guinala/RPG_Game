using UnityEngine;
using UnityEngine.InputSystem;
using FMOD.Studio;

public class PlayerController : MonoBehaviour
{
    [Header("Configuration")]
    public float speed;

    [Header("Dependencies")]
    public Rigidbody2D rigidbody;


    // Private
    private Vector2 _movementInput;

    //Audio
    private EventInstance _footsteps;
    
    
    //Enable or disable controls
    private bool controlsEnabled = true;


    public void EnableControls()
    {
        controlsEnabled = true;
    }
    public void DisableControls()
    {
        controlsEnabled = false;
        _movementInput.x = 0;
        _movementInput.y = 0;
    }
    
    private void Start()
    {
        _footsteps = AudioManager.instance.CreateInstance(FMODEvents.instance.footsteps);
    }
    

    private void FixedUpdate()
    {
        rigidbody.velocity = _movementInput * speed;
        UpdateSound();
    }

    public void OnMovement(InputAction.CallbackContext value)
    {
        if(controlsEnabled)
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

    public void UpdateSound()
    {
        if(rigidbody.velocity.magnitude > 0.1f)
        {
           PLAYBACK_STATE state;
            _footsteps.getPlaybackState(out state);

            if (state.Equals(PLAYBACK_STATE.STOPPED))
            {
                _footsteps.start();
            }
        }
        //otherwise, stop the sound
        else
        {
            _footsteps.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }
       

    }

}
