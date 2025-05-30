using UnityEngine;

// ����� ���� ������ ���� Ŭ����
public class UserSettingsData : IUserData
{
    // ���� on/off ����
    public bool Sound { get; set; }

    // �⺻������ �ʱ�ȭ
    public void SetDefaultData()
    {
        Logger.Log($"{GetType()}::SetDefaultData");

        Sound = true; // ���带 �⺻������ �������� ����
    }

    // PlayerPrefs���� ���� ������ �ε�
    public bool LoadData()
    {
        Logger.Log($"{GetType()}::LoadData");

        bool result = false; // �ε� ��� ����� ����

        try
        {
            // PlayerPrefs���� ���� ������ int�� �о�ͼ� bool�� ��ȯ
            Sound = PlayerPrefs.GetInt("Sound") == 1 ? true : false;
            result = true; // �ε� ����

            Logger.Log($"Sound:{Sound}");
        }
        catch (System.Exception e)
        {
            // �ε� ���� ó��
            Logger.Log("Load failed (" + e.Message + ")");
        }

        return result; // �ε� ��� ��ȯ
    }

    // PlayerPrefs�� ���� ������ ����
    public bool SaveData()
    {
        Logger.Log($"{GetType()}::SaveData");

        bool result = false; // ���� ��� ����� ����

        try
        {
            // bool ���� int�� ��ȯ�Ͽ� PlayerPrefs�� ����
            PlayerPrefs.SetInt("Sound", Sound ? 1 : 0);
            PlayerPrefs.Save(); // ��������� ��ũ�� ����

            result = true; // ���� ����

            Logger.Log($"Sound:{Sound}");
        }
        catch (System.Exception e)
        {
            // ���� ���� ó��
            Logger.Log("Save failed (" + e.Message + ")");
        }

        return result; // ���� ��� ��ȯ
    }
}