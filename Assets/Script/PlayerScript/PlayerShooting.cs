using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField]
    private GameObject projectilePrefab;  // ������ �� �����Ǵ� �߻�ü ������
    [SerializeField]
    private float attackRate = 0.1f;  // ���� �ӵ�
    private int attackLevel = 1;  // ���� ����

    [SerializeField]
    private GameObject bombPrefeb;  // �� ������
    private int bombCount = 3;  // ���� ������ ��ź ��

    public int BombCount => bombCount;

    private AudioSource audioSource;  // ���� ��� ������Ʈ

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
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
        GameObject cloneProjectile = null;

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
                cloneProjectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);  // ���� �밢�� �߻�ü
                cloneProjectile.GetComponent<Movement2D>().MoveTo(new Vector3(-0.2f, 1, 0));
                cloneProjectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);  // ������ �밢�� �߻�ü
                cloneProjectile.GetComponent<Movement2D>().MoveTo(new Vector3(0.2f, 1, 0));
                break;
        }
    }
}
