using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private int damage = 10;  // 적 공격력
    [SerializeField]
    private int scorePoint = 100;  // 적 처치 시 획득 점수
    private PlayerController playerController;  // 플레이어의 score 정보에 접근하기 위함

    private void Awake()
    {
        // 현재 코드에서는 한 번만 호출하기 때문에 OnDie()에서 바로 호출해도 되지만
        // 오브젝트 풀링을 이용해 오브젝트를 재사용할 경우에는 최초 한 번만 Find를 이용하여
        // PlayerController의 정보를 저장해두고 사용하는 것이 연산에 효율적이다.

        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))  // 적에게 부딪힌 오브젝트 태그가 Player라면
        {
            collision.GetComponent<PlayerHP>().TakeDamage(damage);  // 공격력 만큼 체력 감소

            Destroy(gameObject);  // 해당 적 사망
        }
    }

    public void OnDie()
    {
        playerController.Score += scorePoint;  // 플레이어의 점수를 scorePoint 만큼 증가
        Destroy(gameObject);  // 적 오브젝트 삭제
    }
}
