using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField]
    private int damage = 10;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))  // 발사체에 부딪힌 오브젝트의 태그가 Player라면
        {
            collision.GetComponent<PlayerHP>().TakeDamage(damage);  // 부딪힌 플레이어 체력 감소
            Destroy(gameObject);  // 발사체 오브젝트 삭제
        }
    }
}
