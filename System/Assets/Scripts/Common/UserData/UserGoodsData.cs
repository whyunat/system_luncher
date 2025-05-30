using UnityEngine;

// ����� ��ȭ ������ ���� Ŭ����
public class UserGoodsData : IUserData
{
    // ���� ����
    public long Gem { get; set; }
    // ��� ����
    public long Gold { get; set; }

    // �⺻������ �ʱ�ȭ
    public void SetDefaultData()
    {
        Logger.Log($"{GetType()}::SetDefaultData");

        Gem = 0;
        Gold = 0;
    }

    // PlayerPrefs���� ������ �ε�
    public bool LoadData()
    {
        Logger.Log($"{GetType()}::LoadData");

        bool result = false;

        try
        {
            // ����� ��ȭ ������ �б�
            Gem = long.Parse(PlayerPrefs.GetString("Gem"));
            Gold = long.Parse(PlayerPrefs.GetString("Gold"));
            result = true;

            Logger.Log($"Gem:{Gem} Gold:{Gold}");
        }
        catch (System.Exception e)
        {
            // �ε� ���� ó��
            Logger.Log("Load failed (" + e.Message + ")");
        }

        return result;
    }

    // PlayerPrefs�� ������ ����
    public bool SaveData()
    {
        Logger.Log($"{GetType()}::SaveData");

        bool result = false;

        try
        {
            // ��ȭ ������ ����
            PlayerPrefs.SetString("Gem", Gem.ToString());
            PlayerPrefs.SetString("Gold", Gold.ToString());
            PlayerPrefs.Save();

            result = true;

            Logger.Log($"Gem:{Gem} Gold:{Gold}");
        }
        catch (System.Exception e)
        {
            // ���� ���� ó��
            Logger.Log("Save failed (" + e.Message + ")");
        }

        return result;
    }
}