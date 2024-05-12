using UnityEngine;

namespace ScriptableObjectArchitecture
{
	[System.Serializable]
	public sealed class MissionSOReference : BaseReference<MissionSO, MissionSOVariable>
	{
	    public MissionSOReference() : base() { }
	    public MissionSOReference(MissionSO value) : base(value) { }
	}
}