using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBomb : MonoBehaviour
{
    [SerializeField]
    private AnimationCurve curve;
    [SerializeField]
    private AudioClip bombAudio;  // 사운드

    private float bombDelay = 0.5f;  // 폭탄 이동 시간 (0.5초 뒤 폭발)

    private Animator animator;
    private AudioSource audioSource;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        StartCoroutine("MoveToCenter");
    }

    private IEnumerator MoveToCenter()
    {
        Vector3 startPosition = transform.position;
        Vector3 endPosition = Vector3.zero;
        float current = 0;
        float percent = 0;

        while (percent < 1)
        {
            current += Time.deltaTime;
            percent = current / bombDelay;

            transform.position = Vector3.Lerp(startPosition, endPosition, curve.Evaluate(percent));  // bombDelay에 설정된 시간동안 startPosition에서
            // endPosition까지 이동. 애니메이터의 curve에 설정된 그래프처럼 처음엔 빠르게, 끝나갈수록 천천히 이동

            yield return null;
        }

        animator.SetTrigger("onBomb");  // 이동이 완료된 후 애니메이션 변경

        audioSource.clip = bombAudio;  // 사운드 변경
        audioSource.Play();
    }

    public void OnBomb()
    {
        GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");  // 현재 게임에서 Enemy 태그를 가진 모든 오브젝트 정보를 가져온다.
        GameObject[] undyingEnemy = GameObject.FindGameObjectsWithTag("UndyingEnemy");  // 현재 게임에서 UndyingEnemy 태그를 가진 모든 오브젝트 정보를 가져온다.
        
        for (int i = 0; i < enemys.Length; ++i)  // 모든 적 파괴
        {
            enemys[i].GetComponent<Enemy>().OnDie();
        }

        for (int i = 0; i < undyingEnemy.Length; ++i)  // 모든 안 죽는 적 파괴
        {
            undyingEnemy[i].GetComponent<Meteor>().OnDie();
        }

        Destroy(gameObject);  // Bomb 오브젝트 삭제
    }
}
