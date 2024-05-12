using UnityEngine;
using UnityEngine.Events;

namespace ScriptableObjectArchitecture
{
	[System.Serializable]
	public class MissionSOEvent : UnityEvent<MissionSO> { }

	[CreateAssetMenu(
	    fileName = "MissionSOVariable.asset",
	    menuName = SOArchitecture_Utility.VARIABLE_SUBMENU + "Mission Request",
	    order = 120)]
	public class MissionSOVariable : BaseVariable<MissionSO, MissionSOEvent>
	{
	}
}