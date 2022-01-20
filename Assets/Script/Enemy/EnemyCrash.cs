using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCrash : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))  // 적과 부딪힌 오브젝트의 태그가 "Player"이면
        {
            Destroy(gameObject);  // 적에게 부딪혀서 삭제 처리
        }
        else if (collision.CompareTag("PlayerShoot"))  // 적과 부딪힌 오브젝트의 태그가 "PlayerShoot"이면
        {
            Destroy(gameObject);  // 적에게 부딪혀서 삭제 처리
        }
    }
}
