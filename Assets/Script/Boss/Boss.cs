using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BossState { MoveToAppearPoint = 0, Phase01, Phase02, Phase03 }

public class Boss : MonoBehaviour
{
    [SerializeField]
    private StageData stageData;
    [SerializeField]
    private GameObject explosionPrefab;
    [SerializeField]
    private PlayerController playerController;
    [SerializeField]
    private string nextSceneName;  // 다음 씬 이름
    [SerializeField]
    private float bossAppearPoint = 2.5f;
    private BossState bossState = BossState.MoveToAppearPoint;
    private Movement2D movement2D;
    private BossWeapon bossWeapon;
    private BossHP bossHP;

    private void Awake()
    {
        movement2D = GetComponent<Movement2D>();
        bossWeapon = GetComponent<BossWeapon>();
        bossHP = GetComponent<BossHP>();
    }

    public void ChangeState(BossState newState)
    {
        // 열거형 변수.ToString()을 하게 되면 열거형에 정의된 변수 이름을 string 타입으로 받아오게 된다.
        // ex) bossState가 현재 BossState.MoveToAppearPoint이면 "MoveToAppearPoint"를 받아오게 됨.
        // 이를 이용해 열거형의 이름과 코루틴 이름을 일치시켜 열거형 변수에 따라 코루틴 함수 재생을 제어할 수 있다.

        StopCoroutine(bossState.ToString());  // 이전에 재생 중이던 상태 종료
        bossState = newState;  // 상태 변경
        StartCoroutine(bossState.ToString());  // 새로운 상태 재생
    }

    private IEnumerator MoveToAppearPoint()
    {
        movement2D.MoveTo(Vector3.down);  // 이동방향 설정 (코루틴 실행 시 1회 호출)

        while (true)
        {
            if(transform.position.y <= bossAppearPoint)  // bossAppearPoint에 도달하면
            {
                movement2D.MoveTo(Vector3.zero);  // 이동방향을 0, 0, 0으로 설정해 멈추도록 한다.
                ChangeState(BossState.Phase01);  // Phase01 상태로 변경
            }

            yield return null;
        }
    }

    private IEnumerator Phase01()
    {
        bossWeapon.StartFiring(AttackType.CircleFire);  // 원 형태의 방사 공격 시작

        while (true)
        {
            if(bossHP.CurrentHP <= bossHP.MaxHP * 0.7f)  // 보스의 현재 체력이 70% 이하가 되면
            {
                bossWeapon.StopFiring(AttackType.CircleFire);  // 원 방사 형태의 공격 중지
                ChangeState(BossState.Phase02);  // Phase02로 변경
            }

            yield return null;
        }
    }

    private IEnumerator Phase02()
    {
        bossWeapon.StartFiring(AttackType.SingleFireToCenterPosition);  // 플레이어 위치를 기준으로 단일 발사체 공격 시작

        Vector3 direction = Vector3.right;  // 처음 이동 방향은 오른쪽
        movement2D.MoveTo(direction);

        while (true)
        {
            if(transform.position.x <= stageData.LimitMin.x || transform.position.x >= stageData.LimitMax.x)  // 이동 중 양쪽 끝에 도달하면 반대 방향으로
            {
                direction *= -1;  // 방향 바꾸기
                movement2D.MoveTo(direction);
            }
            if (bossHP.CurrentHP <= bossHP.MaxHP * 0.5f)
            {
                bossWeapon.StopFiring(AttackType.SingleFireToCenterPosition);
                ChangeState(BossState.Phase03);
            }

            yield return null;
        }
    }

    private IEnumerator Phase03()
    {
        bossWeapon.StartFiring(AttackType.CircleFire);  // 원 형태 공격 시작
        bossWeapon.StartFiring(AttackType.SingleFireToCenterPosition);  // 단일 발사 공격 시작

        Vector3 direction = Vector3.right;  // 처음 이동 방향은 오른쪽
        movement2D.MoveTo(direction);

        while (true)
        {
            if (transform.position.x <= stageData.LimitMin.x || transform.position.x >= stageData.LimitMax.x)  // 이동 중 양쪽 끝에 도달하면 반대 방향으로
            {
                direction *= -1;  // 방향 바꾸기
                movement2D.MoveTo(direction);
            }

            yield return null;
        }
    }

    public void OnDie()
    {
        GameObject clone = Instantiate(explosionPrefab, transform.position, Quaternion.identity);  // 보스 파괴 파티클 생성
        clone.GetComponent<BossClear>().Setup(playerController, nextSceneName);  // 파티클 재생 완료 후 씬 전환을 위한 설정
        Destroy(gameObject);  // 보스 오브젝트 삭제
    }
}
