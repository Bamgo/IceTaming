using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootCrash : MonoBehaviour
{
    [SerializeField]
    private int damage = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))  // ���� �ε��� ������Ʈ�� �±װ� "Enemy"�̸�
        {
            // collision.GetComponent<Enemy>().OnDie();  // �ε��� ������Ʈ ��� ó��
            collision.GetComponent<EnemyHP>().TakeDamage(damage);  // �ܺ� ��ũ��Ʈ EnemyHP�� TakeDamage�� damage�μ��� �̿��Ͽ� ����
            Destroy(gameObject);  // �� ����
        }
        else if (collision.CompareTag("UndyingEnemy"))  // ���� �ε��� ������Ʈ�� �±װ� "UndyingEnemy"�̸�
        {
            Destroy(gameObject);  // �� ����
        }
    }
}
