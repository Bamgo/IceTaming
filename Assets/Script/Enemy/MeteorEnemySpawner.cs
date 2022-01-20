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
            float positionX = Random.Range(stageData.LimitMin.x, stageData.LimitMax.x);  // x ��ġ�� ���������� ũ�� ���� ������ ������ �� ����
            GameObject alertLineClone = Instantiate(alertLinePrefeb, new Vector3(positionX, 0, 0), Quaternion.identity);  // ��� ������Ʈ ����

            yield return new WaitForSeconds(1.0f);  // 1�ʰ� ���

            Destroy(alertLineClone);  // ���� ��� ������Ʈ ����

            Vector3 meteorPosition = new Vector3(positionX, stageData.LimitMax.y + 1.0f, 0);  // ���׿� ������Ʈ ����
            Instantiate(meteorPrefab, meteorPosition, Quaternion.identity);

            float spawnTime = Random.Range(minSpawnTime, maxSpawnTime);  // ��� �ð�

            yield return new WaitForSeconds(spawnTime);  // �ش� �ð���ŭ ��� ��, ���� ���� ����
        }
    }
}
