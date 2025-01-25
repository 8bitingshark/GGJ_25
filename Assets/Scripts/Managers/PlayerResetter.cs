using UnityEngine;

namespace Managers
{
    public class PlayerResetter : MonoBehaviour
    {
        [SerializeField] private Transform initialTransformPlayer1;
        [SerializeField] private Transform initialTransformPlayer2;
        
        [SerializeField] private LayerMask player1LayerMask;
        [SerializeField] private LayerMask player2LayerMask;
       
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (((1 << collision.gameObject.layer) & player1LayerMask) != 0)
            {
                collision.gameObject.transform.position = initialTransformPlayer1.position;
            }
            else if (((1 << collision.gameObject.layer) & player2LayerMask) != 0)
            {
                collision.gameObject.transform.position = initialTransformPlayer2.position;
            }
        }
        
        
    }
}
