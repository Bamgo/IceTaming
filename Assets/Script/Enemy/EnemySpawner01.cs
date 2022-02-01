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
    private GameObject enemyHPSliderPrefab;  // 적 체력을 나타내는 Slider UI 프리팹
    [SerializeField]
    private Transform canvasTransform;  // UI를 표현하는 Canvas 오브젝트의 Transform
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
            //Instantiate(enemyPrefab, new Vector3(positionX, stageData.LimitMax.y + 1.0f, 0.0f), Quaternion.identity);  // 적 캐릭터 생성
            Vector3 position = new Vector3(positionX, stageData.LimitMax.y + 1.0f, 0.0f);  // 적 생성 위치
            GameObject enemyClone = Instantiate(enemyPrefab, position, Quaternion.identity);  // 적 캐릭터 생성
            SpawnEnemyHPSlider(enemyClone);  // 적 체력을 나타내는 Slider UI 생성 및 설정

            yield return new WaitForSeconds(spawnTime);  // spawnTime 만큼 대기
        }
    }

    private void SpawnEnemyHPSlider(GameObject enemy)
    {
        GameObject sliderClone = Instantiate(enemyHPSliderPrefab);  // 적 체력을 나타내는 Slider UI 생성
        sliderClone.transform.SetParent(canvasTransform);  // Slider UI 오브젝트를 canvas 오브젝트의 자식으로 설정(UI는 캔버스의 자식으로 설정해야 화면에 보임)

        sliderClone.transform.localScale = Vector3.one;  // 계층 설정으로 바뀐 크기를 다시 (1, 1, 1)로 설정

        sliderClone.GetComponent<SlidePositionAutoSet>().Setup(enemy.transform);  // Slider UI가 쫓아다닐 대상을 본인으로 설정
        sliderClone.GetComponent<EnemyHPViewer>().Setup(enemy.GetComponent<EnemyHP>());  // Slider UI에 자신의 체력 정보를 표시하도록 설정
    }
}
