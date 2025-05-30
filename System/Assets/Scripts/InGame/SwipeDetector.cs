using UnityEngine;
using UnityEngine.Events;

//�������� ����
public class SwipeDetector : MonoBehaviour
{
    public static SwipeDetector Instance;
    [Header("�������� ���� ����")]
    [Tooltip("�� �� �۱�� �νĵ� �ּ� �巡�� �Ÿ� (�ȼ�)")]
    public float swipeThreshold = 1000f;

    private Vector2 lastPos;
    private float accumulatedDistance = 0f;

    public Window window; // Window ��ũ��Ʈ ����
    private Collider2D windowCollider;
    private Camera mainCamera;

    void Awake() => Instance = this;

    void Start()
    {
        if (window == null)
            window = GameManager.Instance.GetWindow();

        if (window == null)
        {
            Debug.LogError("Window�� �Ҵ���� �ʾҽ��ϴ�.");
            enabled = false;
            return;
        }

        windowCollider = window.GetComponent<Collider2D>();

        if (windowCollider == null)
        {
            Debug.LogError("Window�� Collider2D�� �����ϴ�.");
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

                // 500�ȼ����� ī��Ʈ ����
                while (accumulatedDistance >= swipeThreshold)
                {
                    accumulatedDistance -= swipeThreshold;
                    Debug.Log("SwipeClean ȣ��");
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