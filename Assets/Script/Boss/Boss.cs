using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BossState { MoveToAppearPoint = 0, Phase01, Phase02, Phase03 }

public class Boss : MonoBehaviour
{
    [SerializeField]
    private StageData stageData;
    [SerializeField]
    private GameObject explosionPrefab;
    [SerializeField]
    private PlayerController playerController;
    [SerializeField]
    private string nextSceneName;  // ���� �� �̸�
    [SerializeField]
    private float bossAppearPoint = 2.5f;
    private BossState bossState = BossState.MoveToAppearPoint;
    private Movement2D movement2D;
    private BossWeapon bossWeapon;
    private BossHP bossHP;

    private void Awake()
    {
        movement2D = GetComponent<Movement2D>();
        bossWeapon = GetComponent<BossWeapon>();
        bossHP = GetComponent<BossHP>();
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
            if(bossHP.CurrentHP <= bossHP.MaxHP * 0.7f)  // ������ ���� ü���� 70% ���ϰ� �Ǹ�
            {
                bossWeapon.StopFiring(AttackType.CircleFire);  // �� ��� ������ ���� ����
                ChangeState(BossState.Phase02);  // Phase02�� ����
            }

            yield return null;
        }
    }

    private IEnumerator Phase02()
    {
        bossWeapon.StartFiring(AttackType.SingleFireToCenterPosition);  // �÷��̾� ��ġ�� �������� ���� �߻�ü ���� ����

        Vector3 direction = Vector3.right;  // ó�� �̵� ������ ������
        movement2D.MoveTo(direction);

        while (true)
        {
            if(transform.position.x <= stageData.LimitMin.x || transform.position.x >= stageData.LimitMax.x)  // �̵� �� ���� ���� �����ϸ� �ݴ� ��������
            {
                direction *= -1;  // ���� �ٲٱ�
                movement2D.MoveTo(direction);
            }
            if (bossHP.CurrentHP <= bossHP.MaxHP * 0.5f)
            {
                bossWeapon.StopFiring(AttackType.SingleFireToCenterPosition);
                ChangeState(BossState.Phase03);
            }

            yield return null;
        }
    }

    private IEnumerator Phase03()
    {
        bossWeapon.StartFiring(AttackType.CircleFire);  // �� ���� ���� ����
        bossWeapon.StartFiring(AttackType.SingleFireToCenterPosition);  // ���� �߻� ���� ����

        Vector3 direction = Vector3.right;  // ó�� �̵� ������ ������
        movement2D.MoveTo(direction);

        while (true)
        {
            if (transform.position.x <= stageData.LimitMin.x || transform.position.x >= stageData.LimitMax.x)  // �̵� �� ���� ���� �����ϸ� �ݴ� ��������
            {
                direction *= -1;  // ���� �ٲٱ�
                movement2D.MoveTo(direction);
            }

            yield return null;
        }
    }

    public void OnDie()
    {
        GameObject clone = Instantiate(explosionPrefab, transform.position, Quaternion.identity);  // ���� �ı� ��ƼŬ ����
        clone.GetComponent<BossClear>().Setup(playerController, nextSceneName);  // ��ƼŬ ��� �Ϸ� �� �� ��ȯ�� ���� ����
        Destroy(gameObject);  // ���� ������Ʈ ����
    }
}
