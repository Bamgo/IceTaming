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
        targetTransform = target;  // Slider UI�� �Ѿƴٴ� target ����
        rectTransform = GetComponent<RectTransform>();  // RectTransform ���� ������
    }

    private void LateUpdate()
    {
        if(targetTransform == null)  // ���� �ı��Ǿ� �Ѿƴٴ� ����� ������� Slider UI�� ����
        {
            Destroy(gameObject);
            return;
        }

        // ������Ʈ�� ��ġ�� ���ŵ� ���Ŀ� Slider UI�� �Բ� ��ġ�� �����ϵ��� �ϱ� ���� LateUpdate()���� ȣ��
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(targetTransform.position);  // ������Ʈ�� ���� ��ǥ�� �������� ȭ�鿡���� ��ǥ  �� ����
        rectTransform.position = screenPosition + distance;  // ȭ�鿡���� ��ǥ + distance ��ŭ ������ ��ġ�� Slider UI�� ��ġ�� ����
    }
}
