using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootCrash : MonoBehaviour
{
    [SerializeField]
    private int damage = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))  // 샷과 부딪힌 오브젝트의 태그가 "Enemy"이면
        {
            // collision.GetComponent<Enemy>().OnDie();  // 부딪힌 오브젝트 사망 처리
            collision.GetComponent<EnemyHP>().TakeDamage(damage);  // 외부 스크립트 EnemyHP의 TakeDamage를 damage인수를 이용하여 실행
            Destroy(gameObject);  // 샷 삭제
        }
        else if (collision.CompareTag("UndyingEnemy"))  // 샷과 부딪힌 오브젝트의 태그가 "UndyingEnemy"이면
        {
            Destroy(gameObject);  // 샷 삭제
        }
    }
}
