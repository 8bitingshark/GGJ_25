using Scoring;
using Unity.VisualScripting;
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

    public void assignPoint(GameObject other)
    {
        int value = 0;
        // Incrementa la variabile
        if (other.gameObject.CompareTag("goldenBubble"))
        {
            value = 3;
        }
        else
        {
            value = 1;
        }
        if (other.gameObject.GetComponent<ScoringBubble>().getState() == ScoringBubble.State.Player1)
        {
            Debug.Log("value p1 = "+value.ToString());
            scoreP1 += value;
            whatIsScoreP1.GetComponent<ScoringScript>().UpdatePoints(scoreP1);
        }
        else if (other.gameObject.GetComponent<ScoringBubble>().getState() == ScoringBubble.State.Player2)
        {
            Debug.Log("value p2 = "+value.ToString());
            scoreP2 += value;
            whatIsScoreP2.GetComponent<ScoringScript>().UpdatePoints(scoreP2);
        }
        else
        {
            missedBubbles++;
        }
        // Distruggi l'oggetto
        Destroy(other.gameObject);
    }
    
    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("helo");
        if ((whatIsBubble.value & (1 << other.gameObject.layer)) > 0)        
        {
            int value = 0;
            // Incrementa la variabile
            if (other.gameObject.CompareTag("goldenBubble"))
            {
                value = 3;
            }
            else
            {
                value = 1;
            }
            if (other.gameObject.GetComponent<ScoringBubble>().getState() == ScoringBubble.State.Player1)
            {
                Debug.Log("value p1 = "+value.ToString());
                scoreP1 += value;
                whatIsScoreP1.GetComponent<ScoringScript>().UpdatePoints(scoreP1);
            }
            else if (other.gameObject.GetComponent<ScoringBubble>().getState() == ScoringBubble.State.Player2)
            {
                Debug.Log("value p2 = "+value.ToString());
                scoreP2 += value;
                whatIsScoreP2.GetComponent<ScoringScript>().UpdatePoints(scoreP2);
            }
            else
            {
                missedBubbles++;
            }
            // Distruggi l'oggetto
            Destroy(other.gameObject);
        }
    }
}
