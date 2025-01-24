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

    enum BubbleType
    {
        Normal,
        Fall,
        Stay,
        StayStay,
    }
    private Rigidbody2D rb;

    [SerializeField] private float _forceAmount = 0.01f;
    [SerializeField] private float _relativeJointForce = 6.3f;
    [SerializeField] private float _forceAmountWithPlayer = 5f;
    [SerializeField] private float _secondsToWaitWhenPLayerEnter = 0.01f;

    [SerializeField] private LayerMask whatIsPlayer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Start()
    {
        
    }

    void Update()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
        Debug.Log(rb.linearVelocityY);
    }
    
    
    void Move()
    {
        rb.AddForce(new Vector2(0, _forceAmount), ForceMode2D.Impulse);
        
    }

    IEnumerator timerContact()
    {
        
        yield return new WaitForSeconds(_secondsToWaitWhenPLayerEnter);
        gameObject.GetComponent<RelativeJoint2D>().maxForce = 6.3f;//valore fisso per far cadere
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if ((whatIsPlayer.value & (1 << other.gameObject.layer)) > 0)
        {
            
            //aggiungi forza all'insù
            gameObject.GetComponent<RelativeJoint2D>().enabled = true;
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
        StopCoroutine(timerContact());
        gameObject.GetComponent<RelativeJoint2D>().enabled = false;
        gameObject.GetComponent<RelativeJoint2D>().maxForce = 40;
    }
}
