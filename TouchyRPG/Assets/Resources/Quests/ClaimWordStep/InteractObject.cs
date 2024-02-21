using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class InteractObject : MonoBehaviour
{
    private Collider2D _collider;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void InteractObjectSword()
    {
        Destroy(this.gameObject);
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
