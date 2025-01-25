using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;


/*
 * to fix the max amount of velocity
 * 
 */
namespace Bubble
{
    public class BubbleMovement : MonoBehaviour
    {
        private Rigidbody2D _rb;
        [SerializeField] private float maxSpeed = 1.0f;
        [SerializeField] private float forceAmount = 0.01f;
        [SerializeField] private float relativeJointForce = 6.3f;
        [SerializeField] private float forceAmountWithPlayer = 5f;
        [SerializeField] private float secondsToWaitWhenPLayerEnter = 0.01f;

        [SerializeField] private LayerMask whatIsPlayer;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
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
            Debug.Log(_rb.linearVelocityY);
        }
    
    
        void Move()
        {
            if(_rb.linearVelocityY < maxSpeed)
                _rb.AddForce(new Vector2(0, forceAmount), ForceMode2D.Impulse);
        
        }

        IEnumerator TimerContact()
        {
            yield return new WaitForSeconds(secondsToWaitWhenPLayerEnter);
            GetComponent<RelativeJoint2D>().maxForce = 6.3f;//valore fisso per far cadere
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
        
            if ((whatIsPlayer.value & (1 << other.gameObject.layer)) > 0)
            {
                //aggiungi forza all'insù
                gameObject.GetComponent<RelativeJoint2D>().enabled = true;
                _rb.constraints = RigidbodyConstraints2D.FreezePositionX;
                // rendere statica la bubble
                StartCoroutine(TimerContact());
            }
            else
            {
                Debug.Log(other.gameObject.name + " non è in uno dei layer specificati.");
            }
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            //aggiungi effetto che va giù la bolla di pochi secondi come se la stessimo leggermente spingendo giù
            StopCoroutine(TimerContact());
            gameObject.GetComponent<RelativeJoint2D>().enabled = false;
            _rb.constraints = RigidbodyConstraints2D.FreezePositionX;
            gameObject.GetComponent<RelativeJoint2D>().maxForce = 40;
        }
    }
}
