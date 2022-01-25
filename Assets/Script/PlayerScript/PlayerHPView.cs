using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHPView : MonoBehaviour
{
    [SerializeField]
    private PlayerHP playerHP;
    private Slider sliderHP;

    private void Awake() 
    {
        sliderHP = GetComponent<Slider>();
    }

    // �� ��Ȯ�� ������δ� �̺�Ʈ�� ���� ü�� ������ �ٲ� ������ UI ���� ���� 
    private void Update()
    {
        sliderHP.value = playerHP.CurrentHP / playerHP.MaxHP;  // Slider UI�� ���� ü�� ���� ������Ʈ
    }
}