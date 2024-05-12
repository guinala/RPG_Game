using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class VisitBoxesStep : QuestStep
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            FinishQuestStep();
        }
    }
    
    protected override void SetQuestStepState(string state)
    {
        //No state is needed for this step
    }
}
