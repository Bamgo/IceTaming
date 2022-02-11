using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHPViewer : MonoBehaviour
{
    [SerializeField]
    private BossHP bossHP;  // 외부 변수 이용 위해 스크립트 불러오기

    private Slider sliderHP;

    private void Awake()
    {
        sliderHP = GetComponent<Slider>();
    }

    private void Update()
    {
        sliderHP.value = bossHP.CurrentHP / bossHP.MaxHP;  // Slider UI에 체력 정보 업데이트 (BossHP 스크립트의 프로퍼티 불러오기)
    }
}
