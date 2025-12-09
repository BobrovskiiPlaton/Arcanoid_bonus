using System;
using UnityEngine;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class MoveBlock : MonoBehaviour
{
    private float minX = -6f;
    private float minY = 0f;
    private float maxX = 6f;
    private float maxY = 4f;
    private float minDistance = 3f;

    private Vector2 pointA;
    private Vector2 pointB;

    private bool reverseDirection = false;

    [SerializeField] private float speed = 3f;
    
    private void Start()
    {
        pointA = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        pointB = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        while(pointB == pointA && Vector2.Distance(pointA, pointB) < minDistance)
            pointB = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        transform.position = pointA;
    }

    private void FixedUpdate()
    {
        Vector2 target = !reverseDirection ? pointB : pointA;
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, target) < 0.01f)
        {
            reverseDirection = !reverseDirection;
        }
    }
}