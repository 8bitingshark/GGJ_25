using System;
using Unity.VisualScripting;
using UnityEngine;

public class NeutralBubble : MonoBehaviour
{
    [SerializeField] private LayerMask otherBubbleLayer;
    private void OnCollisionEnter2D(Collision2D other)
    {
        if ((otherBubbleLayer.value & (1 << other.gameObject.layer)) > 0)
        {
            if (gameObject.GetComponent<ScoringBubble>().getState() != ScoringBubble.State.None)
            {
                if (gameObject.GetComponent<ScoringBubble>().getState() !=
                    other.gameObject.GetComponent<ScoringBubble>().getState())
                {
                    other.gameObject.GetComponent<ScoringBubble>().SetState(ScoringBubble.State.None);
                }
                    
            }
            
        }
            
    }
}
