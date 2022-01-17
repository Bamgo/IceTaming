using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField]
    private GameObject projectilePrefab;  // ������ �� �����Ǵ� �߻�ü ������
    [SerializeField]
    private float attackRate = 0.1f;  // ���� �ӵ�

    public void StartFiring()
    {
        StartCoroutine("TryAttack");
    }

    public void StopFiring()
    {
        StopCoroutine("TryAttack");
    }

    private IEnumerator TryAttack()
    {
        while (true)
        {
            Instantiate(projectilePrefab, transform.position, Quaternion.identity);  // �߻�ü ������Ʈ ����

            yield return new WaitForSeconds(attackRate);  // attackRate �ð� ��ŭ ���
        }
    }

}