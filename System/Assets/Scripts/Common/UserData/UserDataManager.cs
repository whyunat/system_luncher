using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ����� �����͸� �����ϴ� �̱��� �Ŵ��� Ŭ����
public class UserDataManager : SingletonBehaviour<UserDataManager>
{
    // ����� �����Ͱ� �����ϴ��� ����
    public bool ExistsSavedData { get; private set; }
    // ��� ����� ������ �ν��Ͻ��� �����ϴ� �����̳�
    public List<IUserData> UserDataList { get; private set; } = new List<IUserData>();

    // �ʱ�ȭ �Լ�
    protected override void Init()
    {
        base.Init(); // �θ� Ŭ���� �ʱ�ȭ ȣ��

        // ��� ����� �����͸� UserDataList�� �߰�
        UserDataList.Add(new UserSettingsData());
        UserDataList.Add(new UserGoodsData());
    }

    // ��� ����� �����͸� �⺻������ ����
    public void SetDefaultUserData()
    {
        // ����Ʈ�� ��� �����Ϳ� ���� �ݺ�
        for (int i = 0; i < UserDataList.Count; i++)
        {
            UserDataList[i].SetDefaultData(); // �� �����͸� �⺻������ �ʱ�ȭ
        }
    }

    // ����� ������ �ε�
    public void LoadUserData()
    {
        // PlayerPrefs���� ����� ������ ���� ���� Ȯ��
        ExistsSavedData = PlayerPrefs.GetInt("ExistsSavedData") == 1 ? true : false;

        // ����� �����Ͱ� �ִ� ��쿡�� �ε�
        if (ExistsSavedData)
        {
            // ����Ʈ�� ��� �����͸� �ε�
            for (int i = 0; i < UserDataList.Count; i++)
            {
                UserDataList[i].LoadData(); // �� ������ �ε� ����
            }
        }
    }

    // ����� ������ ����
    public void SaveUserData()
    {
        bool hasSaveError = false; // ���� ���� �߻� ���� ����

        // ����Ʈ�� ��� �����͸� ���� �õ�
        for (int i = 0; i < UserDataList.Count; i++)
        {
            bool isSaveSuccess = UserDataList[i].SaveData(); // �� ������ ���� ����
            if (!isSaveSuccess) // ���� ���� ��
            {
                hasSaveError = true; // ���� �÷��� ����
            }
        }

        // ��� �����Ͱ� ���������� ����� ���
        if (!hasSaveError)
        {
            ExistsSavedData = true; // ���� ������ ���� �÷��� ����
            PlayerPrefs.SetInt("ExistsSavedData", 1); // PlayerPrefs�� ����
            PlayerPrefs.Save(); // ��������� ��ũ�� ����
        }
    }
}