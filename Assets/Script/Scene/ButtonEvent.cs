using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonEvent : MonoBehaviour
{
    public void SceneLoader(string sceneName)  // Button UI ������Ʈ�� �����Ͽ� ���. ��ư�� ������ �� ȣ��Ǵ� �Լ����� �ۼ�
    {
        SceneManager.LoadScene(sceneName);
    }
}
