using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TMPColor : MonoBehaviour
{
    [SerializeField]
    private float lerpTime = 0.1f;
    private TextMeshProUGUI textBossWarning;

    private void Awake()
    {
        textBossWarning = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        StartCoroutine("ColorLerpLoop");
    }

    private IEnumerator ColorLerpLoop()
    {
        while (true)
        {
            yield return StartCoroutine(ColorLerp(Color.white, Color.red));  // 색상을 하양에서 빨강으로
            yield return StartCoroutine(ColorLerp(Color.red, Color.white));  // 색상을 빨강에서 하양으로
        }
    }

    private IEnumerator ColorLerp(Color startColor, Color endColor)
    {
        float currentTime = 0.0f;
        float percent = 0.0f;

        while(percent < 1)  // lerpTime 시간 동안 while() 반복문 실행
        {
            currentTime += Time.deltaTime;
            percent = currentTime / lerpTime;

            textBossWarning.color = Color.Lerp(startColor, endColor, percent);  // TextMeshPro의 색상을 startColor에서 endColor로 변경

            yield return null;
        }
    }
}
