using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinSpawner : MonoBehaviour
{
    public GameObject[] garbage;
    public GameObject[] bomb;

    public float xBounds, yBound;

    void Start()
    {
       StartCoroutine(SpawnRandomGameObject());
    }

    IEnumerator SpawnRandomGameObject()
    {
        yield return new WaitForSeconds(Random.Range(1, 2));

        int randomGarbage = Random.Range(0, garbage.Length);
        int randomBomb = Random.Range(0, bomb.Length);

        if(Random.value <= .6f)
            Instantiate(garbage[randomGarbage],
                new Vector2(Random.Range(-xBounds, xBounds), yBound), Quaternion.identity);
        else
           Instantiate(bomb[randomBomb],
                new Vector2(Random.Range(-xBounds, xBounds), yBound), Quaternion.identity);

        StartCoroutine(SpawnRandomGameObject());

    }

}
