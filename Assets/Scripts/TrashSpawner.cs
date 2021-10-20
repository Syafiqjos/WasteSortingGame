using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashSpawner : MonoBehaviour
{
    public Transform trashParent;
    [SerializeField] private GameObject[] trashes;

    public float minSpawnPosX;
    public float maxSpawnPosX;

    [Range(0, 10)] public float extraFallSpeed;

    [Range(0, 10)] public float minInterval;
    [Range(0, 10)] public float maxInterval;

    [Range(0, 10)] public float minDiagonalSpeed;
    [Range(0, 10)] public float maxDiagonalSpeed;

    void Start()
    {
        float timeInterval = GetTimeInterval();
        StartCoroutine(SpawnCountdown(timeInterval));

        Debug.Log("Start");
    }

    IEnumerator SpawnCountdown(float timer)
    {
        yield return new WaitForSeconds(timer);

        SpawnTrash(SelectRandomTrash());

        float timeInterval = GetTimeInterval();
        StartCoroutine(SpawnCountdown(timeInterval));
    }

    private float GetTimeInterval()
    {
        return Random.Range(minInterval, maxInterval);
    }

    private float GetSpawnPosXInterval()
    {
        return Random.Range(minSpawnPosX, maxSpawnPosX);
    }

    private float GetDiagonalSpeed(float spawnPos)
    {
        float speed = Random.Range(minDiagonalSpeed, maxDiagonalSpeed);

        // If Spawn Pos in the right side then move diagonal Left
        if (spawnPos > minSpawnPosX + (maxSpawnPosX - minSpawnPosX) / 2)
        {
            speed *= -1;
        }

        return speed;
    }

    private GameObject SelectRandomTrash()
    {
        return trashes[Random.Range(0, trashes.Length)];
    }

    private void SpawnTrash(GameObject trash)
    {
        GameObject ne = Instantiate(trash, trashParent);

        float spawnPos = GetSpawnPosXInterval();

        ne.transform.position = new Vector3(spawnPos, trashParent.position.y);

        Trash tr = ne.GetComponent<Trash>();
        tr.diagonalSpeed = GetDiagonalSpeed(spawnPos);
        tr.extraFallSpeed = extraFallSpeed;

        Debug.Log("Spawn");
    }
}
