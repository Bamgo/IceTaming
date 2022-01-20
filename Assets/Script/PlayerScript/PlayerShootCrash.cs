using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootCrash : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))  // ���� �ε��� ������Ʈ�� �±װ� "Enemy"�̸�
        {
            Destroy(gameObject);  // �� ����
        }
        else if (collision.CompareTag("UndyingEnemy"))  // ���� �ε��� ������Ʈ�� �±װ� "UndyingEnemy"�̸�
        {
            Destroy(gameObject);  // �� ����
        }
    }
}
