using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BossState { MoveToAppearPoint = 0, Phase01, }

public class Boss : MonoBehaviour
{
    [SerializeField]
    private float bossAppearPoint = 2.5f;
    private BossState bossState = BossState.MoveToAppearPoint;
    private Movement2D movement2D;
    private BossWeapon bossWeapon;

    private void Awake()
    {
        movement2D = GetComponent<Movement2D>();
        bossWeapon = GetComponent<BossWeapon>();
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
            yield return null;
        }
    }
}
