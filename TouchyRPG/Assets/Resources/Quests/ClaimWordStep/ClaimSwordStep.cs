using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using System;

public class ClaimSwordStep : QuestStep
{
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
            FinishQuestStep();
        }
    }

    private void UpdateState()
    {
        //string state = //object.toString();
        //ChangeState(state);
    }

    protected override void SetQuestStepState(string state)
    {
        //Pendiente de guardar correctamente el estado
        //this.object = System.Convert.ToInt32(state);
        //UpdateState();
    }


}
