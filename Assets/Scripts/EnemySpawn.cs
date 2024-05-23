using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField]
    private GameObject[] enemyPrefabs; // 다양한 종류의 적 프리팹을 저장할 배열
    [SerializeField]
    private float spawnTime;
    [SerializeField]
    private Transform[] wayPoints;
    
    private void Awake()
    {
        StartCoroutine(SpawnEnemy());
    }

    private IEnumerator SpawnEnemy()
    {
        while (true)
        {
            // enemyPrefabs 배열에서 랜덤하게 적 프리팹을 선택
            GameObject randomEnemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

            // 선택된 적 프리팹을 소환
            GameObject clone = Instantiate(randomEnemyPrefab);
            Enemy enemy = clone.GetComponent<Enemy>();
            
            // 적의 경로 설정
            enemy.Setup(wayPoints);

            yield return new WaitForSeconds(spawnTime);
        }
    }
}
