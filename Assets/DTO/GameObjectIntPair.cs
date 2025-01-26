using UnityEngine;

namespace DTO
{
    [System.Serializable] // Permette la serializzazione nella finestra dell'Inspector
    public class GameObjectIntPair
    {
        public GameObject gameObject;
        public int probability;

        public GameObjectIntPair(GameObject gameObject, int value)
        {
            this.gameObject = gameObject;
            this.probability = value;
        }
    }

}