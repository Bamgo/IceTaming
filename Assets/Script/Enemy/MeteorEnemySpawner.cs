using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorEnemySpawner : MonoBehaviour
{
    [SerializeField]
    private StageData stageData;
    [SerializeField]
    private GameObject alertLinePrefeb;
    [SerializeField]
    private GameObject meteorPrefab;
    [SerializeField]
    private float minSpawnTime = 1.0f;
    [SerializeField]
    private float maxSpawnTime = 4.0f;

    private void Awake()
    {
        StartCoroutine("SpawnMeteor");
    }

    private IEnumerator SpawnMeteor()
    {
        while (true)
        {
            float positionX = Random.Range(stageData.LimitMin.x, stageData.LimitMax.x);  // x 위치는 스테이지의 크기 범위 내에서 임의의 값 선택
            GameObject alertLineClone = Instantiate(alertLinePrefeb, new Vector3(positionX, 0, 0), Quaternion.identity);  // 경고선 오브젝트 생성

            yield return new WaitForSeconds(1.0f);  // 1초간 대기

            Destroy(alertLineClone);  // 기존 경고선 오브젝트 삭제

            Vector3 meteorPosition = new Vector3(positionX, stageData.LimitMax.y + 1.0f, 0);  // 메테오 오브젝트 생성
            Instantiate(meteorPrefab, meteorPosition, Quaternion.identity);

            float spawnTime = Random.Range(minSpawnTime, maxSpawnTime);  // 대기 시간

            yield return new WaitForSeconds(spawnTime);  // 해당 시간만큼 대기 후, 다음 로직 실행
        }
    }
}
