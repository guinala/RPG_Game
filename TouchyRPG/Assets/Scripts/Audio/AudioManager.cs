using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class AudioManager : MonoBehaviour
{
    private List<EventInstance> eventInstances;
    private List<StudioEventEmitter> eventEmitters;

    private EventInstance _ambientInstanceEvent;
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
    }

    public void PlayOneShot(EventReference sound, Vector3 worldPos)
    {
        RuntimeManager.PlayOneShot(sound, worldPos);
    }

    private void Start()
    {
        InitializeAmbient(FMODEvents.instance.ambience);
    }

    private void InitializeAmbient(EventReference eventReference)
    {
        _ambientInstanceEvent = CreateInstance(eventReference);
        _ambientInstanceEvent.start();
    }

    public void SetAmbienceParameter(string parameterName, float parameterValue)
    {
        
        Debug.Log("Cambiando parámetro a " + parameterValue);
        _ambientInstanceEvent.setParameterByName(parameterName, parameterValue);
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
