using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField]
    private GameObject projectilePrefab;  // 공격할 때 생성되는 발사체 프리팹
    [SerializeField]
    private float attackRate = 0.1f;  // 공격 속도

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
            Instantiate(projectilePrefab, transform.position, Quaternion.identity);  // 발사체 오브젝트 생성

            yield return new WaitForSeconds(attackRate);  // attackRate 시간 만큼 대기
        }
    }

}
