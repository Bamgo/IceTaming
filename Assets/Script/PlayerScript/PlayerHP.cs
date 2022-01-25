using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHP : MonoBehaviour
{
    [SerializeField]
    private float maxHP = 100;  // �ִ� ü��
    private float currentHP;  // ���� ü��
    private SpriteRenderer spriteRenderer;

    // �ܺ� Ŭ�������� ���� ����
    public float MaxHP => maxHP;  // maxHP ������ ������ �� �ִ� ������Ƽ (Get�� ����)    
    public float CurrentHP => currentHP;  // currentHP ������ ������ �� �ִ� ������Ƽ (Get�� ����)

    private void Awake()
    {
        currentHP = 50;  // ���� ü�� ����
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(float damage)
    {
        currentHP -= damage;  // ���� ü���� ������ ��ŭ ����

        StopCoroutine("HitColorAnimation");
        StartCoroutine("HitColorAnimation");

        if(currentHP <= 0)  // ü���� 0���ϰ� �Ǹ� �÷��̾� ĳ���� ���
        {
            Debug.Log("Player HP : 0       You Died !!");
        }
    }

    private IEnumerator HitColorAnimation()
    {
        spriteRenderer.color = Color.red;  // ��ź ��, �÷��̾� ���� ���� ������

        yield return new WaitForSeconds(0.1f);  // 0.1�� ���

        spriteRenderer.color = Color.white;  // ���� �������� ����
    }
}