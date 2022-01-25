using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonEvent : MonoBehaviour
{
    public void SceneLoader(string sceneName)  // Button UI 오브젝트에 부착하여 사용. 버튼을 눌렀을 때 호출되는 함수들을 작성
    {
        SceneManager.LoadScene(sceneName);
    }
}
