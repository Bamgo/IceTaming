using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    [SerializeField]
    private float maxHP = 4;  // �ִ� ü��
    private float currentHP;  // ���� ü��

    private Enemy enemy;
    private SpriteRenderer spriteRenderer;

    public float MaxHP => maxHP;  // �ܺ� Ŭ�������� Ȯ���� �� �ֵ��� ������Ƽ ����
    public float CurrentHP => currentHP;

    private void Awake()
    {
        currentHP = maxHP;  // ���� ü���� �ִ� ü�°� ���� ����
        enemy = GetComponent<Enemy>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(float damage)
    {
        currentHP -= damage;  // ���� ü���� damage ��ŭ ����

        StopCoroutine("HitColorAnimation");
        StartCoroutine("HitColorAnimation");

        if(currentHP <= 0)  // ü���� 0 ���ϰ� �Ǹ� �÷��̾� ĳ���� ���
        {
            enemy.OnDie();  // �� ĳ���� ���
        }
    }

    private IEnumerator HitColorAnimation()
    {
        spriteRenderer.color = Color.red;  // �� ������ ��������
        yield return new WaitForSeconds(0.05f);  // 0.05�� ���
        spriteRenderer.color = Color.white;  // �� ������ ���� ���� �Ͼ�����
    }
}
