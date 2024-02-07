using UnityEngine;
using ScriptableObjectArchitecture;
using UnityEngine.Events;
using System.Collections.Generic;

public class GameStateListener_MissionReload : MonoBehaviour
{
    [Header("Listening to Events")]
    public GameStateSOGameEvent gameStateChangedEvent;

    [Header("Enabled & Disabled Shortcuts")]
    public MonoBehaviour[] components;
    public List<GameStateSO> enabledStates;
    public List<GameStateSO> disabledStates;

    private bool loading = false;

    [Header("Actions")]
    public UnityEvent loadedScene;

    private void OnEnable()
    {
        this.gameStateChangedEvent.AddListener(GameStateChanged);
    }

    private void OnDisable()
    {
        this.gameStateChangedEvent.RemoveListener(GameStateChanged);
    }

    private void GameStateChanged(GameStateSO newGameState)
    {
        InvokeShortcuts(newGameState);
        InvokeActions(newGameState);
    }

    private void InvokeShortcuts(GameStateSO newGameState)
    {
        foreach (var component in this.components)
        {
            if (this.enabledStates.Contains(newGameState))
            {
                component.enabled = true;
            }

            if (this.disabledStates.Contains(newGameState))
            {
                component.enabled = false;
            }
        }
    }

    private void InvokeActions(GameStateSO newGameState)
    {
       

        if (newGameState.stateName == "Loading")
            loading = true;

        if(loading = true && newGameState.stateName != "Loading")
        {
            this.loadedScene.Invoke();
            loading = false;
        }
        Debug.Log("EL estado de loading es: " + loading);

    }
}
