using UnityEngine;

namespace ScriptableObjectArchitecture
{
	[AddComponentMenu(SOArchitecture_Utility.EVENT_LISTENER_SUBMENU + "MissionSO")]
	public sealed class MissionSOGameEventListener : BaseGameEventListener<MissionSO, MissionSOGameEvent, MissionSOUnityEvent>
	{
	}
}