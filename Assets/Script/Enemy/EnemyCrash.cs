using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCrash : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))  // ���� �ε��� ������Ʈ�� �±װ� "Player"�̸�
        {
            Destroy(gameObject);  // ������ �ε����� ���� ó��
        }
        else if (collision.CompareTag("PlayerShoot"))  // ���� �ε��� ������Ʈ�� �±װ� "PlayerShoot"�̸�
        {
            Destroy(gameObject);  // ������ �ε����� ���� ó��
        }
    }
}
