// System.Collections ���ӽ����̽� ���
using System.Collections;
// System.Collections.Generic ���ӽ����̽� ���
using System.Collections.Generic;
// UnityEngine ���ӽ����̽� ���
using UnityEngine;

// �̱��� ������ ��ӹ޴� �κ� ������ Ŭ����
public class LobbyManager : SingletonBehaviour<LobbyManager>
{
    // �κ� UI ��Ʈ�ѷ� ������Ƽ (�б� ����)
    public LobbyUIController LobbyUIController { get; private set; }

    // �ʱ�ȭ �޼��� �������̵�
    protected override void Init()
    {
        // �� �ε� �� �ı��ǵ��� ����
        m_IsDestroyOnLoad = true;

        // �θ� Ŭ������ �ʱ�ȭ ȣ��
        base.Init();
    }

    // Start �޼��� (���� ���� �� ȣ��)
    private void Start()
    {
        // ������ LobbyUIController ã�Ƽ� �Ҵ�
        LobbyUIController = FindObjectOfType<LobbyUIController>();
        // LobbyUIController�� ���� ���
        if (!LobbyUIController)
        {
            // ���� �α� ���
            Logger.LogError("LobbyUIController does not exist.");
            // �޼��� ����
            return;
        }

        // LobbyUIController �ʱ�ȭ
        LobbyUIController.Init();
        // �κ� ������� ���
        AudioManager.Instance.PlayBGM(BGM.lobby);
    }
}