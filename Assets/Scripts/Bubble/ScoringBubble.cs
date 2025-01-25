using UnityEngine;

public class ScoringBubble : MonoBehaviour
{
    
    [SerializeField] private LayerMask whatIsPlayer1;
    [SerializeField] private LayerMask whatIsPlayer2;
    [SerializeField] private Color colorPlayer1;
    [SerializeField] private Color colorPlayer2;

    private State state = State.None;

    public enum State
    {
        Player1,
        Player2,
        None,
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

    void SetState(State state)
    {
        switch (state)
        {
            case State.Player1:
                state = State.Player1;
                gameObject.GetComponent<SpriteRenderer>().color = colorPlayer1;
                break;
            
            case State.Player2:
                state = State.Player2;
                gameObject.GetComponent<SpriteRenderer>().color = colorPlayer2;
                break;

        }
    }
}
