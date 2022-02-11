using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHPViewer : MonoBehaviour
{
    [SerializeField]
    private BossHP bossHP;  // �ܺ� ���� �̿� ���� ��ũ��Ʈ �ҷ�����

    private Slider sliderHP;

    private void Awake()
    {
        sliderHP = GetComponent<Slider>();
    }

    private void Update()
    {
        sliderHP.value = bossHP.CurrentHP / bossHP.MaxHP;  // Slider UI�� ü�� ���� ������Ʈ (BossHP ��ũ��Ʈ�� ������Ƽ �ҷ�����)
    }
}
