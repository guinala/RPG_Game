using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class QuestLogButton : MonoBehaviour, ISelectHandler //Cuando se seleccione esta clase mediante eventos se llamará automaticamente
{
    private TextMeshProUGUI buttonText;
    private UnityAction onSelectAction;

    //because we are instantiating the button and it may be disabled when we instantiate it,
    //we need to manually initialize anything here
    public void Initiliaze(string displayName, UnityAction selectAction)
    {
        this.buttonText = this.GetComponentInChildren<TextMeshProUGUI>();
        this.buttonText.text = displayName;
        this.onSelectAction = selectAction;
    }

    public void OnSelect(BaseEventData eventData)
    {
        onSelectAction();
    }
}
