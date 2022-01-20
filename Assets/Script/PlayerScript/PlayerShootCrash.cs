using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootCrash : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))  // 샷과 부딪힌 오브젝트의 태그가 "Enemy"이면
        {
            Destroy(gameObject);  // 샷 삭제
        }
        else if (collision.CompareTag("UndyingEnemy"))  // 샷과 부딪힌 오브젝트의 태그가 "UndyingEnemy"이면
        {
            Destroy(gameObject);  // 샷 삭제
        }
    }
}
