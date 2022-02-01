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
    private GameObject enemyHPSliderPrefab;  // �� ü���� ��Ÿ���� Slider UI ������
    [SerializeField]
    private Transform canvasTransform;  // UI�� ǥ���ϴ� Canvas ������Ʈ�� Transform
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
            //Instantiate(enemyPrefab, new Vector3(positionX, stageData.LimitMax.y + 1.0f, 0.0f), Quaternion.identity);  // �� ĳ���� ����
            Vector3 position = new Vector3(positionX, stageData.LimitMax.y + 1.0f, 0.0f);  // �� ���� ��ġ
            GameObject enemyClone = Instantiate(enemyPrefab, position, Quaternion.identity);  // �� ĳ���� ����
            SpawnEnemyHPSlider(enemyClone);  // �� ü���� ��Ÿ���� Slider UI ���� �� ����

            yield return new WaitForSeconds(spawnTime);  // spawnTime ��ŭ ���
        }
    }

    private void SpawnEnemyHPSlider(GameObject enemy)
    {
        GameObject sliderClone = Instantiate(enemyHPSliderPrefab);  // �� ü���� ��Ÿ���� Slider UI ����
        sliderClone.transform.SetParent(canvasTransform);  // Slider UI ������Ʈ�� canvas ������Ʈ�� �ڽ����� ����(UI�� ĵ������ �ڽ����� �����ؾ� ȭ�鿡 ����)

        sliderClone.transform.localScale = Vector3.one;  // ���� �������� �ٲ� ũ�⸦ �ٽ� (1, 1, 1)�� ����

        sliderClone.GetComponent<SlidePositionAutoSet>().Setup(enemy.transform);  // Slider UI�� �Ѿƴٴ� ����� �������� ����
        sliderClone.GetComponent<EnemyHPViewer>().Setup(enemy.GetComponent<EnemyHP>());  // Slider UI�� �ڽ��� ü�� ������ ǥ���ϵ��� ����
    }
}
