using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorX : MonoBehaviour  // ���� �ʴ� ���� �÷��̾� ��ü�� �浹�ص� �ı� ó�� �ϰ���� �����Ƿ� ������ ����
{
    private void OnTriggerEnter2D(Collider2D collision)
    { 
        if (collision.CompareTag("Player"))  // ���� �ε��� ������Ʈ�� �±װ� "Player"�̸�
        {
            Destroy(gameObject);  // ��� �ε��� ���� ó��
        }
    }
}
