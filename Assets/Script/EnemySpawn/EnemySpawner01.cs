using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner01 : MonoBehaviour  // �����ؼ� ������ ���� �����Ǿ� �������� ����
{
    [SerializeField]
    private StageData stageData;  // �� ������ ���� �������� ũ�� ����
    [SerializeField]
    private GameObject enemyPrefab;  // �����ؼ� ������ �� ĳ���� ������
    [SerializeField]
    private float spawnTime;  // ���� �ֱ�

    private void Awake()
    {
        StartCoroutine("SpawnEnemy");
    }

    private IEnumerator SpawnEnemy()
    {
        while (true)
        {
            float positionX = Random.Range(stageData.LimitMin.x, stageData.LimitMax.x);  // x ��ġ�� �������� ũ�� ���� ������ ������ �� ����
            Instantiate(enemyPrefab, new Vector3(positionX, stageData.LimitMax.y + 1.0f, 0.0f), Quaternion.identity);  // �� ĳ���� ����

            yield return new WaitForSeconds(spawnTime);  // spawnTime ��ŭ ���
        }
    }
}
