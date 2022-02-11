using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AttackType { CircleFire = 0, SingleFireToCenterPosition }

public class BossWeapon : MonoBehaviour
{
    [SerializeField]
    private GameObject projectilePrefab01;  // 공격할 때 생성되는 발사체 프리팹
    [SerializeField]
    private GameObject projectilePrefab02;

    public void StartFiring(AttackType attackType)
    {
        StartCoroutine(attackType.ToString());  // attackType 열거형의 이름과 같은 코루틴을 실행
    }

    public void StopFiring(AttackType attackType)
    {
        StopCoroutine(attackType.ToString());  // attackType 열거형의 이름과 같은 코루틴을 중지
    }

    private IEnumerator CircleFire()
    {
        float attackRate = 0.5f;  // 공격 주기
        int count = 30;  // 발사체 생성 개수
        float intervalAngle = 360 / count;  // 발사체 사이의 각도
        float weightAngle = 0;  // 가중되는 각도 (항상 같은 위치로 발사하지 않도록 설정)

        while (true)  // count 개수만큼 원 형태로 방사하는 발사체 생성
        {
            for (int i = 0; i < count; ++i)
            {
                GameObject clone = Instantiate(projectilePrefab01, transform.position, Quaternion.identity);  // 발사체 생성
                float angle = weightAngle + intervalAngle * i;  // 발사체 이동 방향(각도)
                float x = Mathf.Cos(angle * Mathf.PI / 180.0f);  // Cos(각도), 라디안 각도의 각도 표현을 위해 PI / 180을 곱함
                float y = Mathf.Sin(angle * Mathf.PI / 180.0f);  // Sin(각도), 라디안 각도의 각도 표현을 위해 PI / 180을 곱함
                clone.GetComponent<Movement2D>().MoveTo(new Vector2(x, y));  // 발사체 이동 방향 결정
            }

            weightAngle += 5;  // 발사체가 생성되는 시작 각도 설정을 위한 변수

            yield return new WaitForSeconds(attackRate);  // attackRate 시간 만큼 대기
        }
    }

    private IEnumerator SingleFireToCenterPosition()
    {
        Vector3 targetPosition = Vector3.zero;  // 목표 위치(중앙)
        float attackRate = 0.1f;  // 공격 주기

        while (true)
        {
            GameObject clone = Instantiate(projectilePrefab02, transform.position, Quaternion.identity);  // 발사체 생성
            Vector3 direction = (targetPosition - clone.transform.position).normalized;  // 이동 방향
            clone.GetComponent<Movement2D>().MoveTo(direction);  // 발사체 이동 방향 설정

            yield return new WaitForSeconds(attackRate);  // attackRate 만큼 대기
        }
    }
}
