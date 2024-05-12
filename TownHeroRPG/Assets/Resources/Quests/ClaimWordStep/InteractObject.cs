using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using FMODUnity;

[RequireComponent(typeof(StudioEventEmitter))]
public class InteractObject : MonoBehaviour
{
    private Collider2D _collider;
    private SpriteRenderer _spriteRenderer;
    private StudioEventEmitter _studioEventEmitter;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }


    private void Start()
    {
        _studioEventEmitter = AudioManager.instance.InitializeEventEmitter(FMODEvents.instance.swordIdle, this.gameObject);
        _studioEventEmitter.Play();
    }

    public void InteractObjectSword()
    {
        Destroy(this.gameObject);
        _studioEventEmitter.Stop();
        AudioManager.instance.PlayOneShot(FMODEvents.instance.swordCollected, this.transform.position);
        GameEventManager.instance.miscEvents.ObjectCollected(gameObject.name);
        Debug.Log("Sword Collected");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Holaaaasdqwd");
        if(collision.gameObject.CompareTag("Player"))
        {
            InteractObjectSword();
        }
    }
}
