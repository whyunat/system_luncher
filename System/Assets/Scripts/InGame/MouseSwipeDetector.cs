using UnityEngine;

public class MouseSwipeDetector : MonoBehaviour
{
    Vector2 startPos;
    Vector2 endPos;

    void Update()
    {
        // 클릭 시작
        if (Input.GetMouseButtonDown(0))
        {
            startPos = Input.mousePosition;
        }

        // 클릭 끝
        if (Input.GetMouseButtonUp(0))
        {
            endPos = Input.mousePosition;
            Vector2 swipe = endPos - startPos;

            if (swipe.magnitude > 50f) // 스와이프 최소 거리 조건
            {
                if (Mathf.Abs(swipe.x) > Mathf.Abs(swipe.y))
                {
                    if (swipe.x > 0)
                        Debug.Log("오른쪽 스와이프");
                    else
                        Debug.Log("왼쪽 스와이프");
                }
                else
                {
                    if (swipe.y > 0)
                        Debug.Log("위로 스와이프");
                    else
                        Debug.Log("아래로 스와이프");
                }
            }
        }
    }

}
