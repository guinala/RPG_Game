using UnityEngine;

namespace ScriptableObjectArchitecture
{
	[System.Serializable]
	[CreateAssetMenu(
	    fileName = "MissionSOGameEvent.asset",
	    menuName = SOArchitecture_Utility.GAME_EVENT + "Mission Request",
	    order = 120)]
	public sealed class MissionSOGameEvent : GameEventBase<MissionSO>
	{
	}
}