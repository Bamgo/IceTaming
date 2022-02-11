using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHP : MonoBehaviour
{
    [SerializeField]
    private float maxHP = 1000;  // 최대 체력
    private float currentHP;  // 현재 체력

    private SpriteRenderer spriteRenderer;
    private Boss boss;

    public float MaxHP => maxHP;  // 외부에서 열람할 수 있도록 프로퍼티
    public float CurrentHP => currentHP;

    private void Awake()
    {
        currentHP = maxHP;  // 현재 체력을 최대 체력으로
        spriteRenderer = GetComponent<SpriteRenderer>();
        boss = GetComponent<Boss>();
    }

    public void TakeDamage(float damage)
    {
        currentHP -= damage;  // 현재 체력을 damage 만큼 감소

        StopCoroutine("HitColorAnimation");
        StartCoroutine("HitColorAnimation");

        if(currentHP <= 0)  // 체력 0 이하가 되면 보스 사망
        {
            boss.OnDie();  // 체력이 0이면 OnDie() 함수를 호출하여 죽었을 때 처리
        }
    }

    private IEnumerator HitColorAnimation()
    {
        spriteRenderer.color = new Color(230 / 255f, 230 / 255f, 250 / 255f);  // 색상 빨간색으로
        yield return new WaitForSeconds(0.05f);  // 0.05초 대기
        spriteRenderer.color = Color.white;  // 색상 원래대로
    }
}
