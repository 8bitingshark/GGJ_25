using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * to fix the max amount of velocity
 * 
 */
public class BubbleMovent : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float _maxSpeed = 1.0f;
    [SerializeField] private float _forceAmount = 0.01f;
    [SerializeField] private float _relativeJointForce = 6.3f;
    [SerializeField] private float _secondsToWaitWhenPLayerEnter = 0.25f;
    [SerializeField] private GameObject relativeJoint2d;
    
    [SerializeField] private LayerMask whatIsPlayer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }

    //2 frames
    void FixedUpdate()
    {
        Move();
        SetJointPos();
    }

    void SetJointPos()
    {
        if(!relativeJoint2d.GetComponent<RelativeJoint2D>().enabled)
            relativeJoint2d.transform.position = transform.position;
    }
    void Move()
    {
        if(rb.linearVelocityY < _maxSpeed)
            rb.AddForce(new Vector2(0, _forceAmount), ForceMode2D.Impulse);
        
    }

    IEnumerator timerContact()
    {
        yield return new WaitForSeconds(_secondsToWaitWhenPLayerEnter);
        relativeJoint2d.GetComponent<RelativeJoint2D>().maxForce = 6.3f;//valore fisso per far cadere
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        
        if ((whatIsPlayer.value & (1 << other.gameObject.layer)) > 0)
        {
            //aggiungi forza all'insù
            relativeJoint2d.GetComponent<RelativeJoint2D>().enabled = true;
            rb.constraints = RigidbodyConstraints2D.FreezePositionX;
            // rendere statica la bubble
            StartCoroutine(timerContact());
        }
        else
        {
            Debug.Log(other.gameObject.name + " non è in uno dei layer specificati.");
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        //aggiungi effetto che va giù la bolla di pochi secondi come se la stessimo leggermente spingendo giù
        if ((whatIsPlayer.value & (1 << other.gameObject.layer)) > 0)
        {
            StopCoroutine(timerContact());
            relativeJoint2d.GetComponent<RelativeJoint2D>().enabled = false;
            rb.constraints = RigidbodyConstraints2D.FreezePositionX;
            relativeJoint2d.GetComponent<RelativeJoint2D>().maxForce = 40;
            rb.linearVelocityY = -0.1f;
        }
    }
}
