using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour  // �����ؼ� ������ ���� �����Ǿ� �������� ����
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
    [SerializeField]
    private int maxEnemyCount = 100;  // ���� ���������� �ִ� �� ���� ����
    [SerializeField]
    private BGMController bgmController;  // ������� ����
    [SerializeField]
    private MeteorEnemySpawner meteorEnemySpawner;  // � �� ����
    [SerializeField]
    private GameObject textBossWarning;  // ���� ���� �ؽ�Ʈ ������Ʈ
    [SerializeField]
    private Twinkle twinkle;  // � ��� ����(������)
    [SerializeField]
    private GameObject boss;  // ���� ������Ʈ

    private void Awake()
    {
        textBossWarning.SetActive(false);  // ���� ���� �ؽ�Ʈ ��Ȱ��ȭ
        boss.SetActive(false);  // ���� ������Ʈ ��Ȱ��ȭ

        StartCoroutine("SpawnEnemy");
    }

    private IEnumerator SpawnEnemy()  // �� ���� �ڷ�ƾ
    {
        int currentEnemyCount = 0;  // �� ���� ���� ī��Ʈ

        while (true)
        {
            float positionX = Random.Range(stageData.LimitMin.x, stageData.LimitMax.x);  // x ��ġ�� �������� ũ�� ���� ������ ������ �� ����
            //Instantiate(enemyPrefab, new Vector3(positionX, stageData.LimitMax.y + 1.0f, 0.0f), Quaternion.identity);  // �� ĳ���� ����
            Vector3 position = new Vector3(positionX, stageData.LimitMax.y + 1.0f, 0.0f);  // �� ���� ��ġ
            GameObject enemyClone = Instantiate(enemyPrefab, position, Quaternion.identity);  // �� ĳ���� ����
            SpawnEnemyHPSlider(enemyClone);  // �� ü���� ��Ÿ���� Slider UI ���� �� ����

            currentEnemyCount++;  // �� ���� ���� ����
            if(currentEnemyCount == maxEnemyCount)  // �� �ִ� ���ڱ��� ���� �Ϸ��ϸ� �� ���� �ڷ�ƾ ����, ���� ���� �ڷ�ƾ ����
            {
                meteorEnemySpawner.SpawnBoss();  // �� �״� ���� ���� �ڷ�ƾ ����
                twinkle.SpawnBossTwinkle();  // � ��� �ڷ�ƾ ����
                DestroyClone("Alert");  // ��� ����
                StartCoroutine("SpawnBoss");
                break;
            }

            yield return new WaitForSeconds(spawnTime);  // spawnTime ��ŭ ���
        }
    }

    private void SpawnEnemyHPSlider(GameObject enemy)  // �� ü�� �����̴�
    {
        GameObject sliderClone = Instantiate(enemyHPSliderPrefab);  // �� ü���� ��Ÿ���� Slider UI ����
        sliderClone.transform.SetParent(canvasTransform);  // Slider UI ������Ʈ�� canvas ������Ʈ�� �ڽ����� ����(UI�� ĵ������ �ڽ����� �����ؾ� ȭ�鿡 ����)

        sliderClone.transform.localScale = Vector3.one;  // ���� �������� �ٲ� ũ�⸦ �ٽ� (1, 1, 1)�� ����

        sliderClone.GetComponent<SlidePositionAutoSet>().Setup(enemy.transform);  // Slider UI�� �Ѿƴٴ� ����� �������� ����
        sliderClone.GetComponent<EnemyHPViewer>().Setup(enemy.GetComponent<EnemyHP>());  // Slider UI�� �ڽ��� ü�� ������ ǥ���ϵ��� ����
    }

    private IEnumerator SpawnBoss()  // ���� �ڷ�ƾ
    {
        bgmController.ChangeBGM(BGMType.Boss);  // ���� ���� BGM ����

        textBossWarning.SetActive(true);  // ���� ���� �ؽ�Ʈ Ȱ��ȭ
        yield return new WaitForSeconds(1.0f);  // 1�� ���

        textBossWarning.SetActive(false);  // �ؽ�Ʈ ��Ȱ��ȭ
        boss.SetActive(true);  // ���� ������Ʈ Ȱ��ȭ

        boss.GetComponent<Boss>().ChangeState(BossState.MoveToAppearPoint);  // ������ ù ��° ������ '������ ��ġ�� �̵�' ����
    }

    public static void DestroyClone(string str)  // �±׸� �̿��� Clone ������Ʈ ����
    {
        GameObject[] clone = GameObject.FindGameObjectsWithTag(str);

        for (int i = 0; i < clone.Length; i++)
        {
            Destroy(clone[i]);
        }
    }
}
