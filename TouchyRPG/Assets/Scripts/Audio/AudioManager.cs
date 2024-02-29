using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using static UnityEngine.ParticleSystem;

public class AudioManager : MonoBehaviour
{
    [Header("Volume")]
    [Range(0, 1)]
    public float masterVolume = 1;

    [Range(0, 1)]
    public float musicVolume = 1;

    [Range(0, 1)]
    public float sfxVolume = 1;

    [Range(0, 1)]
    public float ambientVolume = 1;

    private Bus masterBus;
    private Bus musicBus;
    private Bus sfxBus;
    private Bus ambientBus;




    private List<EventInstance> eventInstances;
    private List<StudioEventEmitter> eventEmitters;

    private EventInstance _ambientInstanceEvent;
    private EventInstance _musicInstanceEvent;
    public static AudioManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Found more than one AudioManager than this");
        }

        instance = this;
        eventInstances = new List<EventInstance>();
        eventEmitters = new List<StudioEventEmitter>();

        masterBus = RuntimeManager.GetBus("bus:/");
        musicBus = RuntimeManager.GetBus("bus:/MusicGame");
        sfxBus = RuntimeManager.GetBus("bus:/SFX");
        ambientBus = RuntimeManager.GetBus("bus:/Ambience");
    }

    public void PlayOneShot(EventReference sound, Vector3 worldPos)
    {
        RuntimeManager.PlayOneShot(sound, worldPos);
    }

    private void Start()
    {
        InitializeAmbient(FMODEvents.instance.ambience);
        InitializeMusicMenu(FMODEvents.instance.musicMenu);
    }

    private void InitializeAmbient(EventReference eventReference)
    {
        _ambientInstanceEvent = CreateInstance(eventReference);
        _ambientInstanceEvent.start();
    }

    private void Update()
    {
        masterBus.setVolume(masterVolume);
        musicBus.setVolume(musicVolume);
        sfxBus.setVolume(sfxVolume);
        ambientBus.setVolume(ambientVolume);
    }

    public void setMusicArea(MusicArea Area)
    {
        FMOD.Studio.PLAYBACK_STATE playbackState;
        _musicInstanceEvent.getPlaybackState(out playbackState);

        if ((float) Area != -1)
        {
            if(playbackState == FMOD.Studio.PLAYBACK_STATE.STOPPED)
            {
                _musicInstanceEvent.start();
            }
            _musicInstanceEvent.setParameterByName("Area", (float)Area);
            
        }
                
        else
            _musicInstanceEvent.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }

    private void InitializeMusicMenu(EventReference eventReference)
    {
        _musicInstanceEvent = CreateInstance(eventReference);
        _musicInstanceEvent.start();
    }

    public void SetAmbienceParameter(string parameterName, float parameterValue)
    {
        
        Debug.Log("Cambiando parámetro a " + parameterValue);
        if(parameterName == "wind-intensity")
        {
            Debug.Log("Uso variable");
            _ambientInstanceEvent.setParameterByName(parameterName, parameterValue);
        }
           
        else
        {
            Debug.Log("No uso variable");
            _ambientInstanceEvent.setParameterByName("wind-intensity", parameterValue);
        }
            
    }

    public EventInstance CreateInstance (EventReference eventReference)
    {
       EventInstance eventInstance = RuntimeManager.CreateInstance(eventReference);
       eventInstances.Add(eventInstance);  

       return eventInstance;
    }


    public StudioEventEmitter InitializeEventEmitter(EventReference eventReference, GameObject emitterGameObject)
    {
        StudioEventEmitter emitter = emitterGameObject.AddComponent<StudioEventEmitter>();
        emitter.EventReference = eventReference;
        eventEmitters.Add(emitter);


        return emitter;
    }


    private void CleanUp()
    {
        //stop and releasy any event instance
        foreach(EventInstance evenetInstance in eventInstances)
        {
            evenetInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            evenetInstance.release();
        }

        //Stop all event emitters (si no lo hacemos irán por ahí a su bola entre escenas
        foreach(StudioEventEmitter eventEmitter in eventEmitters)
        {
            eventEmitter.Stop();
        }
    }

    private void OnDestroy()
    {
        CleanUp();
    }


}
