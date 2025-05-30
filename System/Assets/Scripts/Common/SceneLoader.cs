using UnityEngine;
using UnityEngine.SceneManagement;

// �� ������ �����ϴ� ������
public enum SceneType
{
    Title,  // Ÿ��Ʋ ȭ��
    Lobby,  // �κ� ȭ��
    InGame, // ���� ȭ��
}

// �� ��ȯ�� �����ϴ� �̱��� Ŭ����
public class SceneLoader : SingletonBehaviour<SceneLoader>
{
    // ������ ������ ��ȯ�ϴ� �޼���
    public void LoadScene(SceneType sceneType)
    {
        Logger.Log($"{sceneType} scene loading...");

        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneType.ToString());
    }

    // ���� ���� �ٽ� �ε��ϴ� �޼���
    public void ReloadScene()
    {
        Logger.Log($"{SceneManager.GetActiveScene().name} scene loading...");

        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public AsyncOperation LoadSceneAsync(SceneType sceneType)
    {
        Logger.Log($"{sceneType} scene async loading...");

        Time.timeScale = 1f;

        return SceneManager.LoadSceneAsync(sceneType.ToString());
    }



}