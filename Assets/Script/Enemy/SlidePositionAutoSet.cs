using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidePositionAutoSet : MonoBehaviour
{
    [SerializeField]
    private Vector3 distance = Vector3.down * 35.0f;
    private Transform targetTransform;
    private RectTransform rectTransform;

    public void Setup(Transform target)
    {
        targetTransform = target;  // Slider UI가 쫓아다닐 target 설정
        rectTransform = GetComponent<RectTransform>();  // RectTransform 정보 얻어오기
    }

    private void LateUpdate()
    {
        if(targetTransform == null)  // 적이 파괴되어 쫓아다닐 대상이 사라지면 Slider UI도 삭제
        {
            Destroy(gameObject);
            return;
        }

        // 오브젝트의 위치가 갱신된 이후에 Slider UI도 함께 위치를 설정하도록 하기 위해 LateUpdate()에서 호출
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(targetTransform.position);  // 오브젝트의 월드 좌표를 기준으로 화면에서의 좌표  값 구함
        rectTransform.position = screenPosition + distance;  // 화면에서의 좌표 + distance 만큼 떨어진 위치를 Slider UI의 위치로 지정
    }
}
