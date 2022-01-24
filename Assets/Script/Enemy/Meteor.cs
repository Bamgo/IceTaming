using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    [SerializeField]
    private int damage = 25;  // �� ���ݷ�

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))  // ������ �ε��� ������Ʈ �±װ� Player���
        {
            collision.GetComponent<PlayerHP>().TakeDamage(damage);  // ���ݷ� ��ŭ ü�� ����
        }
    }
}
