using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject[] objectsToFindPrefab;
    [SerializeField]
    public float spawnRangePos = 10.0f;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GenerateObjectsToFind());
        Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
    }

    public IEnumerator GenerateObjectsToFind()
    {
        yield return new WaitForSeconds(3f);
        Debug.Log("Nuovo oggeto da trovare");
        int randomObjectsToFind = Random.Range(0, objectsToFindPrefab.Length);
        Instantiate(objectsToFindPrefab[randomObjectsToFind], GenerateSpawnPosition(), objectsToFindPrefab[randomObjectsToFind].transform.rotation);

    }

    private Vector3 GenerateSpawnPosition()
    {
        float spawnPosX = Random.Range(-spawnRangePos, spawnRangePos);
        float spawnPosZ = Random.Range(-spawnRangePos, spawnRangePos);
        Vector3 randomPos = new Vector3(spawnPosX, 1, spawnPosZ);
        return randomPos;
    }
}
