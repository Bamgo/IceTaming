using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorCrash : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    { 
        if (collision.CompareTag("Player"))  // 적과 부딪힌 오브젝트의 태그가 "Player"이면
        {
            Destroy(gameObject);  // 운석에 부딪혀 삭제 처리
        }
    }
}
