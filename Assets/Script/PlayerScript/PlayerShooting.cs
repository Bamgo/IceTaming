using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField]
    private GameObject projectilePrefab;  // 공격할 때 생성되는 발사체 프리팹
    [SerializeField]
    private float attackRate = 0.1f;  // 공격 속도
    private int attackLevel = 1;  // 공격 레벨

    [SerializeField]
    private GameObject bombPrefeb;  // 봄 프리팹
    private int bombCount = 3;  // 생성 가능한 폭탄 수

    public int BombCount => bombCount;

    private AudioSource audioSource;  // 사운드 재생 컴포넌트

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
            //Instantiate(projectilePrefab, transform.position, Quaternion.identity);  // 발사체 오브젝트 생성
            AttackByLevel();  // 공격 레벨에 따라 발사체 생성
            audioSource.Play();  // 호출되었을 때 사운드 재생

            yield return new WaitForSeconds(attackRate);  // attackRate 시간 만큼 대기
        }
    }

    private void AttackByLevel()
    {
        GameObject cloneProjectile = null;

        switch (attackLevel)
        {
            case 1:  // Level 1 : 발사체 1개 생성
                Instantiate(projectilePrefab, transform.position, Quaternion.identity);
                break;
            case 2:  // Level 2 : 전방으로 발사체 2개 생성
                Instantiate(projectilePrefab, transform.position + Vector3.left * 0.2f, Quaternion.identity);
                Instantiate(projectilePrefab, transform.position + Vector3.right * 0.2f, Quaternion.identity);
                break;
            case 3:  // Level 3 : 전방으로 발사체 1개 생성, 좌우 대각선 방향으로 발사체 각 1개씩
                Instantiate(projectilePrefab, transform.position, Quaternion.identity);  // 가운데 발사체
                cloneProjectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);  // 왼쪽 대각선 발사체
                cloneProjectile.GetComponent<Movement2D>().MoveTo(new Vector3(-0.2f, 1, 0));
                cloneProjectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);  // 오른쪽 대각선 발사체
                cloneProjectile.GetComponent<Movement2D>().MoveTo(new Vector3(0.2f, 1, 0));
                break;
        }
    }
}
