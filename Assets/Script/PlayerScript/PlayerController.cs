using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private string nextSceneName;
    [SerializeField]
    private StageData stageData;
    [SerializeField]
    private KeyCode keyCodeAttack = KeyCode.Z;  // ���� Ű�� zŰ ����

    private Movement2D movement2D;
    private PlayerShooting weapon;
    private Animator animator;

    private float lowSpeed;  // ShiftŰ �Է� �� ���� �ӵ�
    private float applylowSpeed; // ShiftŰ �Է½� ����Ǵ� ���� �ӵ�

    private bool isDie = false;  // ��� ����

    private int score;

    public int Score
    {
        set => score = Mathf.Max(0, value);  // score ���� ������ ���� �ʵ���
        get => score;
    }

    private void Awake()
    {
        movement2D = GetComponent<Movement2D>();
        animator = GetComponent<Animator>();
        weapon = GetComponent<PlayerShooting>();
    }

    private void Update()
    {
        if(isDie == true)  // �÷��̾ ��� �ִϸ��̼� ��� ���� ������ �̵� �� ������ �Ұ����ϰ� ����
        {
            return;
        }

        // ���� Ű�� ���� �̵� ���� ����
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        movement2D.MoveTo(new Vector3(x, y, 0));

        animator.SetFloat("DirX", x);
        animator.SetFloat("DirY", y);

        if (Input.GetKeyDown(keyCodeAttack))  // ���� Ű�� ���ȳ� ���������� ���� ���� ����/����
        {
            weapon.StartFiring();
        }
        else if (Input.GetKeyUp(keyCodeAttack))
        {
            weapon.StopFiring();
        }
    }

    private void LateUpdate()
    {
        // �÷��̾� ĳ���Ͱ� ȭ�� ������ ������ ���ϵ��� ��
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, stageData.LimitMin.x, stageData.LimitMax.x),
                                         Mathf.Clamp(transform.position.y, stageData.LimitMin.y, stageData.LimitMax.y));
    }
    public void OnDie()
    {
        movement2D.MoveTo(Vector3.zero);  // �̵� ���� �ʱ�ȭ
        animator.SetTrigger("onDie");  // ��� �ִϸ��̼� ���
        Destroy(GetComponent<CircleCollider2D>());  // ���� �浹���� �ʵ��� �ݶ��̴� ����
        isDie = true;  // ��� ó��
    }

    public void OnDieEvent()  // �÷��̾� ��� �� nextSceneName���� �̵�
    {
        PlayerPrefs.SetInt("Score", score);  // ȹ���� ���� score�� ����̽��� ����
        SceneManager.LoadScene(nextSceneName);
    }
}
