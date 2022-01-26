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

        int score = PlayerPrefs.GetInt("Score");  // Stage에서 저장한 점수를 불러와 score 변수에 저장
        TextScore.text = "SCORE : " + score;
    }
}
