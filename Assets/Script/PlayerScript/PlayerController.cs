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

    public void OnDie()  // 플레이어 사망 시 nextSceneName으로 이동
    {
        PlayerPrefs.SetInt("Score", score);  // 획득한 점수 score를 디바이스에 저장
        SceneManager.LoadScene(nextSceneName);
    }
}
