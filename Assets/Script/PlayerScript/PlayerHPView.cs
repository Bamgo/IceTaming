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

    // 더 정확한 방법으로는 이벤트를 통해 체력 정보가 바뀔 때에만 UI 정보 갱신 
    private void Update()
    {
        sliderHP.value = playerHP.CurrentHP / playerHP.MaxHP;  // Slider UI에 현재 체력 정보 업데이트
    }
}
