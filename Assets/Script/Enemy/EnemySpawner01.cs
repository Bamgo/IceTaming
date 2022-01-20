using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner01 : MonoBehaviour  // 연속해서 위에서 적이 스폰되어 내려오는 형태
{
    [SerializeField]
    private StageData stageData;  // 적 생성을 위한 스테이지 크기 정보
    [SerializeField]
    private GameObject enemyPrefab;  // 복제해서 생성할 적 캐릭터 프리팹
    [SerializeField]
    private float spawnTime;  // 생성 주기

    private void Awake()
    {
        StartCoroutine("SpawnEnemy");
    }

    private IEnumerator SpawnEnemy()
    {
        while (true)
        {
            float positionX = Random.Range(stageData.LimitMin.x, stageData.LimitMax.x);  // x 위치는 스테이지 크기 범위 내에서 임의의 값 선택
            Instantiate(enemyPrefab, new Vector3(positionX, stageData.LimitMax.y + 1.0f, 0.0f), Quaternion.identity);  // 적 캐릭터 생성

            yield return new WaitForSeconds(spawnTime);  // spawnTime 만큼 대기
        }
    }
}
