using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField]
    private int damage = 10;  // 적 공격력

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))  // 적에게 부딪힌 오브젝트 태그가 Player라면
        {
            collision.GetComponent<PlayerHP>().TakeDamage(damage);  // 공격력 만큼 체력 감소

            Destroy(gameObject);  // 해당 적 사망
        }
    }
}
