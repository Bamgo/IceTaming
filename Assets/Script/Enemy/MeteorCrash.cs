using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorCrash : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    { 
        if (collision.CompareTag("Player"))  // ���� �ε��� ������Ʈ�� �±װ� "Player"�̸�
        {
            Destroy(gameObject);  // ��� �ε��� ���� ó��
        }
    }
}
