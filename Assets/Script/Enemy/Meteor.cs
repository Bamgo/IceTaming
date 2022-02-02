using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    [SerializeField]
    private int damage = 25;  // 적 공격력
    [SerializeField]
    private GameObject explosionPrefab;  // 폭발 효과

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))  // 적에게 부딪힌 오브젝트 태그가 Player라면
        {
            collision.GetComponent<PlayerHP>().TakeDamage(damage);  // 공격력 만큼 체력 감소
        }
    }

    public void OnDie()
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);  // 폭발 이펙트 생성
        Destroy(gameObject);  // 운석 사망
    }
}
