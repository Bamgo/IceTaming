using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHPViewer : MonoBehaviour
{
    private EnemyHP enemyHP;
    private Slider hpSlider;

    public void Setup(EnemyHP enemyHP)
    {
        this.enemyHP = enemyHP;
        hpSlider = GetComponent<Slider>();
    }

    private void Update()
    {
        hpSlider.value = enemyHP.CurrentHP / enemyHP.MaxHP;  // Slider 값을 현재 체력/최대 체력 만큼 채워진 채로 뵈게 함
    }
}
