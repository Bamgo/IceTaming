using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerBombCountViewer : MonoBehaviour
{
    [SerializeField]
    private PlayerShooting playerShooting;
    private TextMeshProUGUI textBombCount;

    private void Awake()
    {
        textBombCount = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        textBombCount.text = "X " + playerShooting.BombCount;
    }
}
