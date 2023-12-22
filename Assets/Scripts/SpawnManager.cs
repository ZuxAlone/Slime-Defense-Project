using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    [SerializeField] private List<Transform> spawnPlatforms;
    [SerializeField] private GameObject enemyPrefab;

    // Start is called before the first frame update
    void Start() {
        StartCoroutine("SpawnEnemies");
    }

    IEnumerator SpawnEnemies() {
        while (true) 
        {
            int randIdx = Random.Range(0, spawnPlatforms.Count);
            Instantiate(enemyPrefab, spawnPlatforms[randIdx].position, Quaternion.identity);
            yield return new WaitForSeconds(1.5f);
        }
    }
}
