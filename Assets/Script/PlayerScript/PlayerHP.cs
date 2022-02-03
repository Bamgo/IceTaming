using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHP : MonoBehaviour
{
    [SerializeField]
    private float maxHP = 100;  // 최대 체력
    private float currentHP;  // 현재 체력
    private SpriteRenderer spriteRenderer;
    private PlayerController playerController;

    // 외부 클래스에서 참조 가능
    public float MaxHP => maxHP;  // maxHP 변수에 접근할 수 있는 프로퍼티 (Get만 가능)    
    
    public float CurrentHP  // CurrentHP 변수에 접근할 수 있는 프로퍼티(Set, Get 가능)
    {
        set => currentHP = Mathf.Clamp(value, 0, maxHP);
        get => currentHP;
    }

    private void Awake()
    {
        currentHP = 50;  // 현재 체력 설정
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerController = GetComponent<PlayerController>();
    }

    public void TakeDamage(float damage)
    {
        currentHP -= damage;  // 현재 체력을 데미지 만큼 감소

        StopCoroutine("HitColorAnimation");
        StartCoroutine("HitColorAnimation");

        if(currentHP <= 0)  // 체력이 0이하가 되면 플레이어 캐릭터 사망
        {
            playerController.OnDie();  // OnDie() 함수를 호출하여 사망 처리
        }
    }

    private IEnumerator HitColorAnimation()
    {
        spriteRenderer.color = Color.red;  // 피탄 시, 플레이어 색상 빨간 색으로

        yield return new WaitForSeconds(0.1f);  // 0.1초 대기

        spriteRenderer.color = Color.white;  // 원래 색상으로 변경
    }
}
