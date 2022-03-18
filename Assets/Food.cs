using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    private float _coordinateX;
    private float _coordinateY;

    private void Start()
    {
        RandomSpawnPosition();
    }
    private void RandomSpawnPosition()
    {
        _coordinateX = Mathf.Round(Random.Range(-10, 10));
        _coordinateY = Mathf.Round(Random.Range(-12, 15));

        transform.position = new Vector2(_coordinateX, _coordinateY);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Snake")
            RandomSpawnPosition();
    }
}
