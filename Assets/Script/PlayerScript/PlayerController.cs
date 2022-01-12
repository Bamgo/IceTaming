using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private StageData stageData;
    private Movement2D movement2D;

    private float lowSpeed;  // Shift키 입력 시 감소 속도
    private float applylowSpeed; // Shift키 입력시 연산되는 감소 속도
    private bool applyRunFlag = false;  // Shift 키 입력 여부

    private Animator animator;

    private void Awake()
    {
        movement2D = GetComponent<Movement2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // 방향 키를 눌러 이동 방향 설정
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        movement2D.MoveTo(new Vector3(x, y, 0));

        animator.SetFloat("DirX", x);
        animator.SetFloat("DirY", y);
    }

    private void LateUpdate()
    {
        // 플레이어 캐릭터가 화면 밖으로 나가지 못하도록 함
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, stageData.LimitMin.x, stageData.LimitMax.x),
                                         Mathf.Clamp(transform.position.y, stageData.LimitMin.y, stageData.LimitMax.y));
    }
}
