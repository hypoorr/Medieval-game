using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Balls : MonoBehaviour
{
    public GameObject ballRef;

    void SpawnBall()
    {
        GameObject spawnedBall = Instantiate(ballRef, transform);
        spawnedBall.transform.position = new Vector3(Random.Range(-30,30), 25, Random.Range(-30,30));
        spawnedBall.SetActive(true);
        StartCoroutine(DestroyBall(spawnedBall));

    }
    IEnumerator DestroyBall(GameObject ball)
    {
        yield return new WaitForSeconds(5f);
        Destroy(ball);
    }
    void FixedUpdate()
    {
        SpawnBall();
    }
}
