using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using System;

public class ClaimSwordStep : QuestStep
{
    [Header("Config")]
    [SerializeField] private string _object = "";

    private void Start()
    {
        //Pendiente de guardar correctamente el estado aunque en este caso no hace falta
        string status = "Claim Your"+_object + "Sword in home";
        ChangeState("", status);
    }

    
    private void OnEnable()
    {
        GameEventManager.instance.miscEvents.onObjectCollected += ObjectCollect;
    }
    

    private void OnDisable()
    {
        GameEventManager.instance.miscEvents.onObjectCollected -= ObjectCollect;
    }

    private void ObjectCollect(String name)
    {
        if(name == "Sword")
        {
            Debug.Log("Finalizar step");
            string status = "You claimed your" + _object + ". Talk to Sensei";
            ChangeState("", status);
            FinishQuestStep();
        }
    }

    private void UpdateState()
    {
        string state = _object;
        string status = "Claim Your Sword Sword in home";
        ChangeState(state, status);
    }

    protected override void SetQuestStepState(string state)
    {
        //Pendiente de guardar correctamente el estado
        //this.object = System.Convert.ToInt32(state);
        //UpdateState();
    }


}
