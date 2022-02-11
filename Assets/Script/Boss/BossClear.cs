using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossClear : MonoBehaviour
{
    private PlayerController playerController;
    private string sceneName;

    public void Setup(PlayerController playerController, string sceneName)
    {
        this.playerController = playerController;
        this.sceneName = sceneName;
    }

    // ParticleAutoDestroy ������Ʈ���� ��ƼŬ ����� �Ϸ�Ǹ� ��ƼŬ�� �����ϱ� ������ ������Ʈ�� ������ �� ȣ��Ǵ� OnDestroy() �Լ��� �̿���
    // ����� �Ϸ� �Ǿ��� �� �ʿ��� ó���� �����Ѵ�.
    private void OnDestroy()
    {
        playerController.Score += 10000;  // ���� óġ ����
        PlayerPrefs.SetInt("Score", playerController.Score);  // �÷��̾� ȹ�� ������ "Score" Ű�� ����
        SceneManager.LoadScene(sceneName);  // sceneName���� �� ����
    }
}
