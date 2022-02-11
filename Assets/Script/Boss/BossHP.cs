using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHP : MonoBehaviour
{
    [SerializeField]
    private float maxHP = 1000;  // �ִ� ü��
    private float currentHP;  // ���� ü��

    private SpriteRenderer spriteRenderer;
    private Boss boss;

    public float MaxHP => maxHP;  // �ܺο��� ������ �� �ֵ��� ������Ƽ
    public float CurrentHP => currentHP;

    private void Awake()
    {
        currentHP = maxHP;  // ���� ü���� �ִ� ü������
        spriteRenderer = GetComponent<SpriteRenderer>();
        boss = GetComponent<Boss>();
    }

    public void TakeDamage(float damage)
    {
        currentHP -= damage;  // ���� ü���� damage ��ŭ ����

        StopCoroutine("HitColorAnimation");
        StartCoroutine("HitColorAnimation");

        if(currentHP <= 0)  // ü�� 0 ���ϰ� �Ǹ� ���� ���
        {
            boss.OnDie();  // ü���� 0�̸� OnDie() �Լ��� ȣ���Ͽ� �׾��� �� ó��
        }
    }

    private IEnumerator HitColorAnimation()
    {
        spriteRenderer.color = new Color(230 / 255f, 230 / 255f, 250 / 255f);  // ���� ����������
        yield return new WaitForSeconds(0.05f);  // 0.05�� ���
        spriteRenderer.color = Color.white;  // ���� �������
    }
}
