using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBomb : MonoBehaviour
{
    [SerializeField]
    private AnimationCurve curve;
    [SerializeField]
    private AudioClip bombAudio;  // ����

    private float bombDelay = 0.5f;  // ��ź �̵� �ð� (0.5�� �� ����)

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

            transform.position = Vector3.Lerp(startPosition, endPosition, curve.Evaluate(percent));  // bombDelay�� ������ �ð����� startPosition����
            // endPosition���� �̵�. �ִϸ������� curve�� ������ �׷���ó�� ó���� ������, ���������� õõ�� �̵�

            yield return null;
        }

        animator.SetTrigger("onBomb");  // �̵��� �Ϸ�� �� �ִϸ��̼� ����

        audioSource.clip = bombAudio;  // ���� ����
        audioSource.Play();
    }

    public void OnBomb()
    {
        GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");  // ���� ���ӿ��� Enemy �±׸� ���� ��� ������Ʈ ������ �����´�.
        GameObject[] undyingEnemy = GameObject.FindGameObjectsWithTag("UndyingEnemy");  // ���� ���ӿ��� UndyingEnemy �±׸� ���� ��� ������Ʈ ������ �����´�.
        
        for (int i = 0; i < enemys.Length; ++i)  // ��� �� �ı�
        {
            enemys[i].GetComponent<Enemy>().OnDie();
        }

        for (int i = 0; i < undyingEnemy.Length; ++i)  // ��� �� �״� �� �ı�
        {
            undyingEnemy[i].GetComponent<Meteor>().OnDie();
        }

        Destroy(gameObject);  // Bomb ������Ʈ ����
    }
}
