using System;
using System.Collections.Generic;
using DTO;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class BubbleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject bubblePrefab;
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
    [SerializeField]
    private List<GameObjectIntPair> gameObjectIntPairs = new List<GameObjectIntPair>();
    private List<GameObject> probabilityArray = new List<GameObject>();
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
        startVector = startPosition.transform.position;
        endVector = endPosition.transform.position;
        PopulateProbabilityArray();
    }

    private void Start()
    {
        cellSize = (endVector.x - startVector.x) / cellsAmount;
        SetUpvectorSpawn();
    }

    private void SetUpvectorSpawn()
    {
        for (int i = 0; i < cellsAmount; i++)
        {
            VectorPositionBubbles.Add(startVector);
            startVector.x += cellSize;
        }
    }

    // Update is called once per frame
    private void Update()
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
            int numeroCasuale = Random.Range(0, probabilityArray.Count);
            if (evenCycle)
            {
                if (i % 2 == 0)
                {
                    GameObject bubble = probabilityArray[numeroCasuale];
                    Instantiate(bubble);
                    bubble.transform.position = VectorPositionBubbles[i];
                }
            }
            else
            {
                if (i % 2 != 0)
                {
                    GameObject bubble = probabilityArray[numeroCasuale];
                    Instantiate(bubble);
                    bubble.transform.position = VectorPositionBubbles[i];
                }
            }
        }
    }

    void PopulateProbabilityArray()
    {
        foreach (var bolla in gameObjectIntPairs)
        {
            for (int i = 0; i < bolla.probability; i++)
            {
                probabilityArray.Add(bolla.gameObject);
            }
        }
    }
}
