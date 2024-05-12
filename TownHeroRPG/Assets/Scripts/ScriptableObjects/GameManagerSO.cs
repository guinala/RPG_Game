using UnityEngine;
using ScriptableObjectArchitecture;

[CreateAssetMenu(fileName = "GameManager", menuName = "ScriptableObjects/Game Manager")]
public class GameManagerSO : ScriptableObject
{
    public GameStateSO currentState;

    [Header("Broadcasting events")]
    public GameStateSOGameEvent gameStateChanged;

    private GameStateSO previousState;

    public void SetGameState(GameStateSO gameState)
    {
        if(this.currentState != null)
        {
            this.previousState = this.currentState;
        }

        this.currentState = gameState;

        if(this.gameStateChanged != null)
        {
            this.gameStateChanged.Raise(this.currentState);
        }   

    }

    public void RestorePreviousState()
    {
        this.SetGameState(this.previousState);
    }
}
