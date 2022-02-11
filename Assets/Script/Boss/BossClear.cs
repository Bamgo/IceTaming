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

    // ParticleAutoDestroy 컴포넌트에서 파티클 재생이 완료되면 파티클을 삭제하기 때문에 오브젝트가 삭제될 때 호출되는 OnDestroy() 함수를 이용해
    // 재생이 완료 되었을 때 필요한 처리를 설정한다.
    private void OnDestroy()
    {
        playerController.Score += 10000;  // 보스 처치 점수
        PlayerPrefs.SetInt("Score", playerController.Score);  // 플레이어 획득 점수를 "Score" 키에 저장
        SceneManager.LoadScene(sceneName);  // sceneName으로 씬 변경
    }
}
