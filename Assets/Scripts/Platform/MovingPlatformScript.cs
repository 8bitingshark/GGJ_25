using System;
using UnityEngine;
using UnityEngine.UIElements;

public class MovingPlatformScript : MonoBehaviour
{
    [Header("Punti di riferimento")]
    public Vector2 posA;
    public Vector2 posB;

    [Header("Parametri movimento")]
    public float speed = 0.1f; // Velocità della piattaforma
    private Transform target; // Punto di destinazione attuale

    private void Update()
    {
        transform.position = Vector2.Lerp(posA, posB, Mathf.PingPong(Time.time * speed, 1.0f));
    }
}
