using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField]
    private GameObject projectilePrefab;  // ������ �� �����Ǵ� �߻�ü ������
    [SerializeField]
    private float attackRate = 0.1f;  // ���� �ӵ�
    [SerializeField]
    private int maxAttackLevel = 3;  // ���� �ִ� ����
    private int attackLevel = 1;  // ���� ����

    [SerializeField]
    private GameObject bombPrefeb;  // �� ������
    private int bombCount = 3;  // ���� ������ ��ź ��

    private AudioSource audioSource;  // ���� ��� ������Ʈ
    private Movement2D movement2D;

    public int AttackLevel
    {
        set => attackLevel = Mathf.Clamp(value, 1, maxAttackLevel);  // attackLevel�� maxAttackLevel�� �Ѿ�� �ʵ��� ����
        get => attackLevel;
    }

    public int BombCount
    {
        set => bombCount = Mathf.Max(0, value);  // 0 �̻�
        get => bombCount;
    }

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        movement2D = GetComponent<Movement2D>();
    }

    public void StartFiring()
    {
        StartCoroutine("TryAttack");
    }

    public void StopFiring()
    {
        StopCoroutine("TryAttack");
    }

    public void StartBomb()
    {
        if(bombCount > 0)
        {
            bombCount--;
            Instantiate(bombPrefeb, transform.position, Quaternion.identity);
        }
    }

    private IEnumerator TryAttack()
    {
        while (true)
        {
            //Instantiate(projectilePrefab, transform.position, Quaternion.identity);  // �߻�ü ������Ʈ ����
            AttackByLevel();  // ���� ������ ���� �߻�ü ����
            audioSource.Play();  // ȣ��Ǿ��� �� ���� ���

            yield return new WaitForSeconds(attackRate);  // attackRate �ð� ��ŭ ���
        }
    }

    private void AttackByLevel()
    {
        //GameObject cloneProjectile = null;

        switch (attackLevel)
        {
            case 1:  // Level 1 : �߻�ü 1�� ����
                Instantiate(projectilePrefab, transform.position, Quaternion.identity);
                break;
            case 2:  // Level 2 : �������� �߻�ü 2�� ����
                Instantiate(projectilePrefab, transform.position + Vector3.left * 0.2f, Quaternion.identity);
                Instantiate(projectilePrefab, transform.position + Vector3.right * 0.2f, Quaternion.identity);
                break;
            case 3:  // Level 3 : �������� �߻�ü 1�� ����, �¿� �밢�� �������� �߻�ü �� 1����
                Instantiate(projectilePrefab, transform.position, Quaternion.identity);  // ��� �߻�ü
                //cloneProjectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);  // ���� �밢�� �߻�ü
                //cloneProjectile.GetComponent<Movement2D>().MoveTo(new Vector3(-0.2f, 1, 0));  // ��°���� ����
                //cloneProjectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);  // ������ �밢�� �߻�ü
                //cloneProjectile.GetComponent<Movement2D>().MoveTo(new Vector3(0.2f, 1, 0));
                Instantiate(projectilePrefab, transform.position + Vector3.left * 0.4f, Quaternion.identity);
                Instantiate(projectilePrefab, transform.position + Vector3.right * 0.4f, Quaternion.identity);
                break;
        }
    }
}
