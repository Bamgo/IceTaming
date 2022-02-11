using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AttackType { CircleFire = 0, SingleFireToCenterPosition }

public class BossWeapon : MonoBehaviour
{
    [SerializeField]
    private GameObject projectilePrefab01;  // ������ �� �����Ǵ� �߻�ü ������
    [SerializeField]
    private GameObject projectilePrefab02;

    public void StartFiring(AttackType attackType)
    {
        StartCoroutine(attackType.ToString());  // attackType �������� �̸��� ���� �ڷ�ƾ�� ����
    }

    public void StopFiring(AttackType attackType)
    {
        StopCoroutine(attackType.ToString());  // attackType �������� �̸��� ���� �ڷ�ƾ�� ����
    }

    private IEnumerator CircleFire()
    {
        float attackRate = 0.5f;  // ���� �ֱ�
        int count = 30;  // �߻�ü ���� ����
        float intervalAngle = 360 / count;  // �߻�ü ������ ����
        float weightAngle = 0;  // ���ߵǴ� ���� (�׻� ���� ��ġ�� �߻����� �ʵ��� ����)

        while (true)  // count ������ŭ �� ���·� ����ϴ� �߻�ü ����
        {
            for (int i = 0; i < count; ++i)
            {
                GameObject clone = Instantiate(projectilePrefab01, transform.position, Quaternion.identity);  // �߻�ü ����
                float angle = weightAngle + intervalAngle * i;  // �߻�ü �̵� ����(����)
                float x = Mathf.Cos(angle * Mathf.PI / 180.0f);  // Cos(����), ���� ������ ���� ǥ���� ���� PI / 180�� ����
                float y = Mathf.Sin(angle * Mathf.PI / 180.0f);  // Sin(����), ���� ������ ���� ǥ���� ���� PI / 180�� ����
                clone.GetComponent<Movement2D>().MoveTo(new Vector2(x, y));  // �߻�ü �̵� ���� ����
            }

            weightAngle += 5;  // �߻�ü�� �����Ǵ� ���� ���� ������ ���� ����

            yield return new WaitForSeconds(attackRate);  // attackRate �ð� ��ŭ ���
        }
    }

    private IEnumerator SingleFireToCenterPosition()
    {
        Vector3 targetPosition = Vector3.zero;  // ��ǥ ��ġ(�߾�)
        float attackRate = 0.1f;  // ���� �ֱ�

        while (true)
        {
            GameObject clone = Instantiate(projectilePrefab02, transform.position, Quaternion.identity);  // �߻�ü ����
            Vector3 direction = (targetPosition - clone.transform.position).normalized;  // �̵� ����
            clone.GetComponent<Movement2D>().MoveTo(direction);  // �߻�ü �̵� ���� ����

            yield return new WaitForSeconds(attackRate);  // attackRate ��ŭ ���
        }
    }
}
