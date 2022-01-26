using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverScoreViewer : MonoBehaviour
{
    private TextMeshProUGUI TextScore;

    private void Awake()
    {
        TextScore = GetComponent<TextMeshProUGUI>();

        int score = PlayerPrefs.GetInt("Score");  // Stage���� ������ ������ �ҷ��� score ������ ����
        TextScore.text = "SCORE : " + score;
    }
}
