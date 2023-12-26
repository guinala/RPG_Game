using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MissionUI : MonoBehaviour
{
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI information;

    public void StartMission(MissionSO mission)
    {
        ResetUI();
        SetMissionUI(mission);
    }

    private void SetMissionUI(MissionSO mission)
    {
        titleText.text = mission.title;
        information.text = mission.information;
    }

    public void ResetUI()
    {
        titleText.text = "";
        information.text = "";
    }
}
