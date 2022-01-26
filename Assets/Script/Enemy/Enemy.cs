using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private int damage = 10;  // �� ���ݷ�
    [SerializeField]
    private int scorePoint = 100;  // �� óġ �� ȹ�� ����
    private PlayerController playerController;  // �÷��̾��� score ������ �����ϱ� ����

    private void Awake()
    {
        // ���� �ڵ忡���� �� ���� ȣ���ϱ� ������ OnDie()���� �ٷ� ȣ���ص� ������
        // ������Ʈ Ǯ���� �̿��� ������Ʈ�� ������ ��쿡�� ���� �� ���� Find�� �̿��Ͽ�
        // PlayerController�� ������ �����صΰ� ����ϴ� ���� ���꿡 ȿ�����̴�.

        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))  // ������ �ε��� ������Ʈ �±װ� Player���
        {
            collision.GetComponent<PlayerHP>().TakeDamage(damage);  // ���ݷ� ��ŭ ü�� ����

            Destroy(gameObject);  // �ش� �� ���
        }
    }

    public void OnDie()
    {
        playerController.Score += scorePoint;  // �÷��̾��� ������ scorePoint ��ŭ ����
        Destroy(gameObject);  // �� ������Ʈ ����
    }
}
