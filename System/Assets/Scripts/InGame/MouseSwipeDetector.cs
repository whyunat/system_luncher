using UnityEngine;

public class MouseSwipeDetector : MonoBehaviour
{
    Vector2 startPos;
    Vector2 endPos;

    void Update()
    {
        // Ŭ�� ����
        if (Input.GetMouseButtonDown(0))
        {
            startPos = Input.mousePosition;
        }

        // Ŭ�� ��
        if (Input.GetMouseButtonUp(0))
        {
            endPos = Input.mousePosition;
            Vector2 swipe = endPos - startPos;

            if (swipe.magnitude > 50f) // �������� �ּ� �Ÿ� ����
            {
                if (Mathf.Abs(swipe.x) > Mathf.Abs(swipe.y))
                {
                    if (swipe.x > 0)
                        Debug.Log("������ ��������");
                    else
                        Debug.Log("���� ��������");
                }
                else
                {
                    if (swipe.y > 0)
                        Debug.Log("���� ��������");
                    else
                        Debug.Log("�Ʒ��� ��������");
                }
            }
        }
    }

}
