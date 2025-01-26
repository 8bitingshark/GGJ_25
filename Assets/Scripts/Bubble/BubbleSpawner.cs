using System;
using System.Collections.Generic;
using UnityEngine;

public class BubbleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject bubbleNormalPrefab;
    [SerializeField] private GameObject bubbleExplosivePrefab;
    [SerializeField] private GameObject startPosition;
    [SerializeField] private GameObject endPosition;
    [SerializeField] private float spawnInterval = 2.5f;

    [SerializeField] private Vector2 startVector;
    [SerializeField] private Vector2 endVector;
    [SerializeField] private List<Vector2> VectorPositionBubbles = new List<Vector2>();
    private float cellSize;
    private float timer = 0;
    private bool flag = false;
    [SerializeField] private int cellsAmount;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
        startVector = startPosition.transform.position;
        endVector = endPosition.transform.position;
    }

    void Start()
    {
        cellSize = (endVector.x - startVector.x) / cellsAmount;
        SetUpvectorSpawn();
    }

    void SetUpvectorSpawn()
    {
        for (int i = 0; i < cellsAmount; i++)
        {
            VectorPositionBubbles.Add(startVector);
            startVector.x += cellSize;
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            spawnBubbleAction(flag);
            timer = 0;
            flag = !flag;
        }
    }

    void spawnBubbleAction(bool evenCycle)
    {
        for (int i = 0; i < VectorPositionBubbles.Count; i++)
        {
            if (evenCycle)
            {
                if (i % 2 == 0)
                {
                    GameObject bubble = Instantiate(bubbleNormalPrefab);
                    bubble.transform.position = VectorPositionBubbles[i];
                }
            }
            else
            {
                if (i % 2 != 0)
                {
                    GameObject bubble = Instantiate(bubbleExplosivePrefab);
                    bubble.transform.position = VectorPositionBubbles[i];
                }
            }
        }
    }
}
