using UnityEngine;
using ScriptableObjectArchitecture;

public class MissionTrigger : MonoBehaviour
{
    [Header("Configuration")]
    public MissionSO mission;

    [Header("Broadcasting events")]
    public MissionSOGameEvent missionRequestEvent;

    public void TriggerMission()
    {
        this.missionRequestEvent.Raise(this.mission);
    }
}
