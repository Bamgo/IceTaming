using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private int damage = 10;  // 적 공격력
    [SerializeField]
    private int scorePoint = 100;  // 적 처치 시 획득 점수
    [SerializeField]
    private GameObject explosionPrefab;  // 폭발 효과
    [SerializeField]
    private GameObject[] itemPrefabs;  // 적을 죽였을 때 획득 가능한 아이템 배열

    private PlayerController playerController;  // 플레이어의 score 정보에 접근하기 위함

    private void Awake()
    {
        // 현재 코드에서는 한 번만 호출하기 때문에 OnDie()에서 바로 호출해도 되지만
        // 오브젝트 풀링을 이용해 오브젝트를 재사용할 경우에는 최초 한 번만 Find를 이용하여
        // PlayerController의 정보를 저장해두고 사용하는 것이 연산에 효율적이다.

        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))  // 적에게 부딪힌 오브젝트 태그가 Player라면
        {
            collision.GetComponent<PlayerHP>().TakeDamage(damage);  // 공격력 만큼 체력 감소

            Destroy(gameObject);  // 해당 적 사망
        }
    }

    public void OnDie()
    {
        playerController.Score += scorePoint;  // 플레이어의 점수를 scorePoint 만큼 증가
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        SpawnItem();  // 일정 확률로 아이템 생성
        Destroy(gameObject);  // 적 오브젝트 삭제
    }

    private void SpawnItem()
    {
        int spawnItem = Random.Range(0, 100);  // 랜덤 확률

        if(spawnItem < 10)  // 10% 확률로 파워업 아이템
        {
            Instantiate(itemPrefabs[0], transform.position, Quaternion.identity);  // itemPrefabs 배열의 0번 프리팹을 생성
        } else if(spawnItem < 15)  // 5% 확률로 폭탄 아이템
        {
            Instantiate(itemPrefabs[1], transform.position, Quaternion.identity);
        } else if(spawnItem < 30)  // 15% 확률로 체력 회복 아이템
        {
            Instantiate(itemPrefabs[2], transform.position, Quaternion.identity);
        }
    }
}
