using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject windowPrefab;
    private GameObject currentWindow;

    void Awake() => Instance = this;

    void Start()
    {
        SpawnWindow();
    }

    private void SpawnWindow()
    {
        currentWindow = Instantiate(windowPrefab);
        SwipeDetector.Instance.SetWindow(currentWindow.GetComponent<Window>());
    }

    public Window GetWindow()
    {
        return currentWindow.GetComponent<Window>();
    }
}
