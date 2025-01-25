using Scoring;
using UnityEngine;

public class DestroyScript : MonoBehaviour
{

    public int scoreP1 = 0;
    public int scoreP2 = 0;
    public int missedBubbles = 0;

    [SerializeField] public LayerMask whatIsBubble;
    [SerializeField] public GameObject whatIsScoreP1;
    [SerializeField] public GameObject whatIsScoreP2;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if ((whatIsBubble.value & (1 << other.gameObject.layer)) > 0)        
        {
            Debug.Log("Stato della bolla: " + other.gameObject.GetComponent<ScoringBubble>().getState());
            // Incrementa la variabile
            if (other.gameObject.GetComponent<ScoringBubble>().getState() == ScoringBubble.State.Player1)
            {
                scoreP1++;
                whatIsScoreP1.GetComponent<ScoringScript>().UpdatePoints(scoreP1);
            }
            else if (other.gameObject.GetComponent<ScoringBubble>().getState() == ScoringBubble.State.Player2)
            {
                scoreP2++;
                whatIsScoreP2.GetComponent<ScoringScript>().UpdatePoints(scoreP2);
            }
            else
            {
                missedBubbles++;
            }
        }
        
        // Distruggi l'oggetto
        Destroy(other.gameObject);

    }
}
