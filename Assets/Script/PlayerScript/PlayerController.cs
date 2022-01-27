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
    private KeyCode keyCodeAttack = KeyCode.Z;  // 공격 키로 z키 설정

    private Movement2D movement2D;
    private PlayerShooting weapon;
    private Animator animator;

    private float lowSpeed;  // Shift키 입력 시 감소 속도
    private float applylowSpeed; // Shift키 입력시 연산되는 감소 속도

    private bool isDie = false;  // 사망 여부

    private int score;

    public int Score
    {
        set => score = Mathf.Max(0, value);  // score 값이 음수가 되지 않도록
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
        if(isDie == true)  // 플레이어가 사망 애니메이션 재생 중일 때에는 이동 및 공격이 불가능하게 설정
        {
            return;
        }

        // 방향 키를 눌러 이동 방향 설정
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        movement2D.MoveTo(new Vector3(x, y, 0));

        animator.SetFloat("DirX", x);
        animator.SetFloat("DirY", y);

        if (Input.GetKeyDown(keyCodeAttack))  // 공격 키가 눌렸냐 떼어졌나에 따라 공격 시작/종료
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
        // 플레이어 캐릭터가 화면 밖으로 나가지 못하도록 함
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, stageData.LimitMin.x, stageData.LimitMax.x),
                                         Mathf.Clamp(transform.position.y, stageData.LimitMin.y, stageData.LimitMax.y));
    }
    public void OnDie()
    {
        movement2D.MoveTo(Vector3.zero);  // 이동 방향 초기화
        animator.SetTrigger("onDie");  // 사망 애니메이션 재생
        Destroy(GetComponent<CircleCollider2D>());  // 적과 충돌하지 않도록 콜라이더 삭제
        isDie = true;  // 사망 처리
    }

    public void OnDieEvent()  // 플레이어 사망 시 nextSceneName으로 이동
    {
        PlayerPrefs.SetInt("Score", score);  // 획득한 점수 score를 디바이스에 저장
        SceneManager.LoadScene(nextSceneName);
    }
}
