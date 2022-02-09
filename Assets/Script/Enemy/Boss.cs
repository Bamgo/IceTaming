using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BossState { MoveToAppearPoint = 0, Phase01, }

public class Boss : MonoBehaviour
{
    [SerializeField]
    private float bossAppearPoint = 2.5f;
    private BossState bossState = BossState.MoveToAppearPoint;
    private Movement2D movement2D;
    private BossWeapon bossWeapon;

    private void Awake()
    {
        movement2D = GetComponent<Movement2D>();
        bossWeapon = GetComponent<BossWeapon>();
    }

    public void ChangeState(BossState newState)
    {
        // ������ ����.ToString()�� �ϰ� �Ǹ� �������� ���ǵ� ���� �̸��� string Ÿ������ �޾ƿ��� �ȴ�.
        // ex) bossState�� ���� BossState.MoveToAppearPoint�̸� "MoveToAppearPoint"�� �޾ƿ��� ��.
        // �̸� �̿��� �������� �̸��� �ڷ�ƾ �̸��� ��ġ���� ������ ������ ���� �ڷ�ƾ �Լ� ����� ������ �� �ִ�.

        StopCoroutine(bossState.ToString());  // ������ ��� ���̴� ���� ����
        bossState = newState;  // ���� ����
        StartCoroutine(bossState.ToString());  // ���ο� ���� ���
    }

    private IEnumerator MoveToAppearPoint()
    {
        movement2D.MoveTo(Vector3.down);  // �̵����� ���� (�ڷ�ƾ ���� �� 1ȸ ȣ��)

        while (true)
        {
            if(transform.position.y <= bossAppearPoint)  // bossAppearPoint�� �����ϸ�
            {
                movement2D.MoveTo(Vector3.zero);  // �̵������� 0, 0, 0���� ������ ���ߵ��� �Ѵ�.
                ChangeState(BossState.Phase01);  // Phase01 ���·� ����
            }

            yield return null;
        }
    }

    private IEnumerator Phase01()
    {
        bossWeapon.StartFiring(AttackType.CircleFire);  // �� ������ ��� ���� ����

        while (true)
        {
            yield return null;
        }
    }
}
