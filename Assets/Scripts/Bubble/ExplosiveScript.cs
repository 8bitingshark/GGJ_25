using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ExplosiveScript : MonoBehaviour
{
    
    [SerializeField] private float time = 2f;
    [SerializeField] private Collider2D ExplosiveTrigger;
    [SerializeField] private SpriteRenderer SpriteExplosion;
    [SerializeField] private bool flagCheckExplosion = false;
    [SerializeField] private ScoringBubble ScoringBubble;
    private List<GameObject> colliders2D;
    [SerializeField] private LayerMask whatToTrigger;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ExplosiveTrigger.enabled = false;
        colliders2D = new List<GameObject>();
        Debug.Log("culo");
    }

    void Update()
    {
        Debug.Log(ScoringBubble.getState());
        if (ScoringBubble.getState() != ScoringBubble.State.None && !flagCheckExplosion)
        {
            flagCheckExplosion = true;
            StartCoroutine(timerExplosion());
        }
    }

    IEnumerator triggerActivate()
    {
        Debug.Log("TriggerActivate");
        ExplosiveTrigger.enabled = true;
        SpriteExplosion.enabled = true;
        yield return new WaitForSeconds(0.5f);
        
        for (int i = 0; i < colliders2D.Count; i++)
        {
            if(colliders2D[i] != null)
                colliders2D[i].GetComponent<ScoringBubble>().SetState(ScoringBubble.getState());
        }
        
        Destroy(gameObject);
    }

    IEnumerator timerExplosion()
    {
        Debug.Log(ScoringBubble.getState());
        yield return new WaitForSeconds(time);
        explode();
    }


    void explode()
    {
        StartCoroutine(triggerActivate());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((whatToTrigger.value & (1 << other.gameObject.layer)) > 0)
            colliders2D.Add(other.gameObject);
    }
}
