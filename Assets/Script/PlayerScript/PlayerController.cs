using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private StageData stageData;
    private Movement2D movement2D;

    private float lowSpeed;  // ShiftŰ �Է� �� ���� �ӵ�
    private float applylowSpeed; // ShiftŰ �Է½� ����Ǵ� ���� �ӵ�
    private bool applyRunFlag = false;  // Shift Ű �Է� ����

    private Animator animator;

    private void Awake()
    {
        movement2D = GetComponent<Movement2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // ���� Ű�� ���� �̵� ���� ����
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        movement2D.MoveTo(new Vector3(x, y, 0));

        animator.SetFloat("DirX", x);
        animator.SetFloat("DirY", y);
    }

    private void LateUpdate()
    {
        // �÷��̾� ĳ���Ͱ� ȭ�� ������ ������ ���ϵ��� ��
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, stageData.LimitMin.x, stageData.LimitMax.x),
                                         Mathf.Clamp(transform.position.y, stageData.LimitMin.y, stageData.LimitMax.y));
    }
}
