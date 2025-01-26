using System;
using UnityEngine;
using UnityEngine.UIElements;

public class MovingPlatformScript : MonoBehaviour
{
    [Header("Punti di riferimento")]
    public Transform pointA; // Primo punto
    public Transform pointB; // Secondo punto
    public Vector2 posA;
    public Vector2 posB;

    [Header("Parametri movimento")]
    public float speed = 0.1f; // Velocità della piattaforma
    private Transform target; // Punto di destinazione attuale
    
    
    private void Start()
    {
        // Inizia muovendosi verso il primo punto
        target = pointB;
        posA = pointA.position;
        posB = pointB.position;
    }

    private void Update()
    {
        transform.position = Vector2.Lerp(posA, posB, Mathf.PingPong(Time.time * speed, 1.0f));
    }
}
