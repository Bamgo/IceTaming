using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorX : MonoBehaviour  // 죽지 않는 적은 플레이어 기체와 충돌해도 파괴 처리 하고싶지 않으므로 쓰이지 않음
{
    private void OnTriggerEnter2D(Collider2D collision)
    { 
        if (collision.CompareTag("Player"))  // 적과 부딪힌 오브젝트의 태그가 "Player"이면
        {
            Destroy(gameObject);  // 운석에 부딪혀 삭제 처리
        }
    }
}
