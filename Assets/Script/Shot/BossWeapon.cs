using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AttackType { CircleFire = 0, }

public class BossWeapon : MonoBehaviour
{
    [SerializeField]
    private GameObject projectilePrefab;  // ������ �� �����Ǵ� �߻�ü ������

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
                GameObject clone = Instantiate(projectilePrefab, transform.position, Quaternion.identity);  // �߻�ü ����
                float angle = weightAngle + intervalAngle * i;  // �߻�ü �̵� ����(����)
                float x = Mathf.Cos(angle * Mathf.PI / 180.0f);  // Cos(����), ���� ������ ���� ǥ���� ���� PI / 180�� ����
                float y = Mathf.Sin(angle * Mathf.PI / 180.0f);  // Sin(����), ���� ������ ���� ǥ���� ���� PI / 180�� ����
                clone.GetComponent<Movement2D>().MoveTo(new Vector2(x, y));  // �߻�ü �̵� ���� ����
            }

            weightAngle += 5;  // �߻�ü�� �����Ǵ� ���� ���� ������ ���� ����

            yield return new WaitForSeconds(attackRate);  // attackRate �ð� ��ŭ ���
        }
    }
}
