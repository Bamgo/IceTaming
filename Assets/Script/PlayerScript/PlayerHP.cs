using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHP : MonoBehaviour
{
    [SerializeField]
    private float maxHP = 100;  // �ִ� ü��
    private float currentHP;  // ���� ü��
    private SpriteRenderer spriteRenderer;
    private PlayerController playerController;

    // �ܺ� Ŭ�������� ���� ����
    public float MaxHP => maxHP;  // maxHP ������ ������ �� �ִ� ������Ƽ (Get�� ����)    
    
    public float CurrentHP  // CurrentHP ������ ������ �� �ִ� ������Ƽ(Set, Get ����)
    {
        set => currentHP = Mathf.Clamp(value, 0, maxHP);
        get => currentHP;
    }

    private void Awake()
    {
        currentHP = 50;  // ���� ü�� ����
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerController = GetComponent<PlayerController>();
    }

    public void TakeDamage(float damage)
    {
        currentHP -= damage;  // ���� ü���� ������ ��ŭ ����

        StopCoroutine("HitColorAnimation");
        StartCoroutine("HitColorAnimation");

        if(currentHP <= 0)  // ü���� 0���ϰ� �Ǹ� �÷��̾� ĳ���� ���
        {
            playerController.OnDie();  // OnDie() �Լ��� ȣ���Ͽ� ��� ó��
        }
    }

    private IEnumerator HitColorAnimation()
    {
        spriteRenderer.color = Color.red;  // ��ź ��, �÷��̾� ���� ���� ������

        yield return new WaitForSeconds(0.1f);  // 0.1�� ���

        spriteRenderer.color = Color.white;  // ���� �������� ����
    }
}
