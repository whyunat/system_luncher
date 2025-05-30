using UnityEngine;
using UnityEngine.Events;

//스와이프 감지
public class SwipeDetector : MonoBehaviour
{
    public static SwipeDetector Instance;
    [Header("스와이프 감지 설정")]
    [Tooltip("한 번 닦기로 인식될 최소 드래그 거리 (픽셀)")]
    public float swipeThreshold = 1000f;

    private Vector2 lastPos;
    private float accumulatedDistance = 0f;

    public Window window; // Window 스크립트 참조
    private Collider2D windowCollider;
    private Camera mainCamera;

    void Awake() => Instance = this;

    void Start()
    {
        if (window == null)
            window = GameManager.Instance.GetWindow();

        if (window == null)
        {
            Debug.LogError("Window가 할당되지 않았습니다.");
            enabled = false;
            return;
        }

        windowCollider = window.GetComponent<Collider2D>();

        if (windowCollider == null)
        {
            Debug.LogError("Window에 Collider2D가 없습니다.");
            enabled = false;
            return;
        }
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            lastPos = Input.mousePosition;
            accumulatedDistance = 0f;
        }
        else if (Input.GetMouseButton(0))
        {
            Vector2 currentPos = Input.mousePosition;

            if (IsPointerOverWindow(currentPos))
            {
                float deltaDistance = Vector2.Distance(currentPos, lastPos);
                accumulatedDistance += deltaDistance;

                // 500픽셀마다 카운트 증가
                while (accumulatedDistance >= swipeThreshold)
                {
                    accumulatedDistance -= swipeThreshold;
                    Debug.Log("SwipeClean 호출");
                    window.SwipeClean();
                }
            }

            lastPos = currentPos;
        }
    }

    private bool IsPointerOverWindow(Vector2 screenPos)
    {
        Vector3 worldPos = mainCamera.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, mainCamera.nearClipPlane));
        Collider2D hitCollider = Physics2D.OverlapPoint(worldPos);
        bool isOver = (hitCollider == windowCollider);
        return isOver;
    }

    public void SetWindow(Window newWindow)
    {
        window = newWindow;
        windowCollider = newWindow.GetComponent<Collider2D>();
    }
}