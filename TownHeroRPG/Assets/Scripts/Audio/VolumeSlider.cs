using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    public enum VolumeType
    {
        MASTER,
        MUSIC,
        SFX,
        AMBIENCE
    }

    [Header("Volume Type")]
    [SerializeField] private VolumeType volumeType;

    private Slider slider;

    private void Awake()
    {
        slider = this.GetComponentInChildren<Slider>();
    }

    private void Update()
    {
        switch(volumeType)
        {
            case VolumeType.MASTER:
                slider.value = AudioManager.instance.masterVolume;
                break;
            case VolumeType.MUSIC:
                slider.value = AudioManager.instance.musicVolume;
                break;
            case VolumeType.SFX:
                slider.value = AudioManager.instance.sfxVolume;
                break;
            case VolumeType.AMBIENCE:
                slider.value = AudioManager.instance.ambientVolume;
                break;
            default:
                Debug.Log("Volume Type not found");
                break;
        }
    }

    public void OnSliderValueChanged()
    {
        switch(volumeType)
        {
            case VolumeType.MASTER:
                AudioManager.instance.masterVolume = slider.value;
                break;
            case VolumeType.MUSIC:
                AudioManager.instance.musicVolume = slider.value;
                break;
            case VolumeType.SFX:
                AudioManager.instance.sfxVolume = slider.value;
                break;
            case VolumeType.AMBIENCE:
                AudioManager.instance.ambientVolume = slider.value;
                break;
            default:
                Debug.Log("Volume Type not found");
                break;
        }
    }
}
