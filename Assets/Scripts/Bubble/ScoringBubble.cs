using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoringBubble : MonoBehaviour
{
    
    [SerializeField] private LayerMask whatIsPlayer1;
    [SerializeField] private LayerMask whatIsPlayer2;
    [SerializeField] private Color colorPlayer1;
    [SerializeField] private Color colorPlayer2;
    [SerializeField] private Color neutralColor;
    [SerializeField] private GameObject pointManager;
    private Coroutine coroutine;
    [SerializeField] private Canvas canvas;
    float timer = 0;
    

    [SerializeField] private State state = State.None;

    public enum State
    {
        Player1,
        Player2,
        None,
    }

    

    private void Start()
    {
        pointManager = GameObject.Find("PointManager");
    }

    public State getState() // sta roba la usiamo per contare i punti
    {
        return state;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if ((whatIsPlayer1.value & (1 << collision.gameObject.layer)) > 0)
        {
            SetState(State.Player1);
        }else if ((whatIsPlayer2.value & (1 << collision.gameObject.layer)) > 0)
        {
            SetState(State.Player2);
        }
    }
    
    

    public void SetState(State stateTmp)
    {
        //add audio here of bubbles
        if(coroutine == null)
            coroutine = StartCoroutine(setTimerDestroy());
        switch (stateTmp)
        {
            case State.Player1:
                state = State.Player1;
                gameObject.GetComponent<SpriteRenderer>().color = colorPlayer1;
                break;
            
            case State.Player2:
                state = State.Player2;
                gameObject.GetComponent<SpriteRenderer>().color = colorPlayer2;
                break;
            
            case State.None:
                state = State.None;
                gameObject.GetComponent<SpriteRenderer>().color = neutralColor;
                break;

        }
    }

    private IEnumerator setTimerDestroy()
    {
        yield return new WaitForSeconds(4f);
        pointManager.GetComponent<DestroyScript>().assignPoint(gameObject);
    }
    
}
