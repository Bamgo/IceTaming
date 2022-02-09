using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour  // 연속해서 위에서 적이 스폰되어 내려오는 형태
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
    [SerializeField]
    private int maxEnemyCount = 100;  // 현재 스테이지의 최대 적 생성 숫자
    [SerializeField]
    private BGMController bgmController;  // 배경음악 설정
    [SerializeField]
    private MeteorEnemySpawner meteorEnemySpawner;  // 운석 적 제어
    [SerializeField]
    private GameObject textBossWarning;  // 보스 등장 텍스트 오브젝트
    [SerializeField]
    private Twinkle twinkle;  // 운석 경고선 제어(프리팹)
    [SerializeField]
    private GameObject boss;  // 보스 오브젝트

    private void Awake()
    {
        textBossWarning.SetActive(false);  // 보스 등장 텍스트 비활성화
        boss.SetActive(false);  // 보스 오브젝트 비활성화

        StartCoroutine("SpawnEnemy");
    }

    private IEnumerator SpawnEnemy()  // 적 생성 코루틴
    {
        int currentEnemyCount = 0;  // 적 생성 숫자 카운트

        while (true)
        {
            float positionX = Random.Range(stageData.LimitMin.x, stageData.LimitMax.x);  // x 위치는 스테이지 크기 범위 내에서 임의의 값 선택
            //Instantiate(enemyPrefab, new Vector3(positionX, stageData.LimitMax.y + 1.0f, 0.0f), Quaternion.identity);  // 적 캐릭터 생성
            Vector3 position = new Vector3(positionX, stageData.LimitMax.y + 1.0f, 0.0f);  // 적 생성 위치
            GameObject enemyClone = Instantiate(enemyPrefab, position, Quaternion.identity);  // 적 캐릭터 생성
            SpawnEnemyHPSlider(enemyClone);  // 적 체력을 나타내는 Slider UI 생성 및 설정

            currentEnemyCount++;  // 적 생성 숫자 증가
            if(currentEnemyCount == maxEnemyCount)  // 적 최대 숫자까지 생성 완료하면 적 생성 코루틴 중지, 보스 생성 코루틴 실행
            {
                meteorEnemySpawner.SpawnBoss();  // 안 죽는 적도 생성 코루틴 중지
                twinkle.SpawnBossTwinkle();  // 운석 경고선 코루틴 중지
                DestroyClone("Alert");  // 경고선 삭제
                StartCoroutine("SpawnBoss");
                break;
            }

            yield return new WaitForSeconds(spawnTime);  // spawnTime 만큼 대기
        }
    }

    private void SpawnEnemyHPSlider(GameObject enemy)  // 적 체력 슬라이더
    {
        GameObject sliderClone = Instantiate(enemyHPSliderPrefab);  // 적 체력을 나타내는 Slider UI 생성
        sliderClone.transform.SetParent(canvasTransform);  // Slider UI 오브젝트를 canvas 오브젝트의 자식으로 설정(UI는 캔버스의 자식으로 설정해야 화면에 보임)

        sliderClone.transform.localScale = Vector3.one;  // 계층 설정으로 바뀐 크기를 다시 (1, 1, 1)로 설정

        sliderClone.GetComponent<SlidePositionAutoSet>().Setup(enemy.transform);  // Slider UI가 쫓아다닐 대상을 본인으로 설정
        sliderClone.GetComponent<EnemyHPViewer>().Setup(enemy.GetComponent<EnemyHP>());  // Slider UI에 자신의 체력 정보를 표시하도록 설정
    }

    private IEnumerator SpawnBoss()  // 보스 코루틴
    {
        bgmController.ChangeBGM(BGMType.Boss);  // 보스 등장 BGM 설정

        textBossWarning.SetActive(true);  // 보스 등장 텍스트 활성화
        yield return new WaitForSeconds(1.0f);  // 1초 대기

        textBossWarning.SetActive(false);  // 텍스트 비활성화
        boss.SetActive(true);  // 보스 오브젝트 활성화

        boss.GetComponent<Boss>().ChangeState(BossState.MoveToAppearPoint);  // 보스의 첫 번째 상태인 '지정된 위치로 이동' 실행
    }

    public static void DestroyClone(string str)  // 태그를 이용한 Clone 오브젝트 삭제
    {
        GameObject[] clone = GameObject.FindGameObjectsWithTag(str);

        for (int i = 0; i < clone.Length; i++)
        {
            Destroy(clone[i]);
        }
    }
}
