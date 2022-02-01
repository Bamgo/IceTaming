using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    [SerializeField]
    private float maxHP = 4;  // 최대 체력
    private float currentHP;  // 현재 체력

    private Enemy enemy;
    private SpriteRenderer spriteRenderer;

    public float MaxHP => maxHP;  // 외부 클래스에서 확인할 수 있도록 프로퍼티 생성
    public float CurrentHP => currentHP;

    private void Awake()
    {
        currentHP = maxHP;  // 현재 체력을 최대 체력과 같게 설정
        enemy = GetComponent<Enemy>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(float damage)
    {
        currentHP -= damage;  // 현재 체력을 damage 만큼 감소

        StopCoroutine("HitColorAnimation");
        StartCoroutine("HitColorAnimation");

        if(currentHP <= 0)  // 체력이 0 이하가 되면 플레이어 캐릭터 사망
        {
            enemy.OnDie();  // 적 캐릭터 사망
        }
    }

    private IEnumerator HitColorAnimation()
    {
        spriteRenderer.color = Color.red;  // 적 색상을 빨강으로
        yield return new WaitForSeconds(0.05f);  // 0.05초 대기
        spriteRenderer.color = Color.white;  // 적 색상을 원래 색인 하양으로
    }
}
