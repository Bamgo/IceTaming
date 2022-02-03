using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType { PowerUp = 0, Bomb, HP, }  // 열거형으로 아이템 정의

public class Item : MonoBehaviour
{
    [SerializeField]
    private ItemType itemType;

    private Movement2D movement2D;

    private void Awake()
    {
        movement2D = GetComponent<Movement2D>();

        //float x = Random.Range(-1.0f, 1.0f);
        //float y = Random.Range(-1.0f, 1.0f);

        //movement2D.MoveTo(new Vector3(x, y, 0));  // 이동방향 정의(랜덤 이동)
        movement2D.MoveTo(new Vector3(0, -1, 0));  // 이동방향 정의(아래로만 떨어지게)
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))  // 아이템과 부딪힌 오브젝트 태그가 플레이어이면
        {
            Use(collision.gameObject);  // 아이템 획득 시 효과
            Destroy(gameObject);  // 아이템 오브젝트 삭제
        }
    }

    public void Use(GameObject player)
    {
        switch (itemType)  // 아이템 타입에 따라 어떤 작용이 일어나는지
        {
            case ItemType.PowerUp:
                player.GetComponent<PlayerShooting>().AttackLevel++;
                break;
            case ItemType.Bomb:
                player.GetComponent<PlayerShooting>().BombCount++;
                break;
            case ItemType.HP:
                player.GetComponent<PlayerHP>().CurrentHP += 5;
                break;
        }
    }
}
