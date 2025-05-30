using System.Collections.Generic;
using UnityEngine;

// BGM Ÿ�� ������
public enum BGM
{
    lobby, // �κ� BGM
    COUNT // �� BGM ����
}

// SFX Ÿ�� ������
public enum SFX
{
    chapter_clear, // é�� Ŭ���� ȿ����
    stage_clear, // �������� Ŭ���� ȿ����
    ui_button_click, // UI ��ư Ŭ�� ȿ����
    COUNT // �� SFX ����
}

// ������� �����ϴ� �̱��� �Ŵ��� Ŭ����
public class AudioManager : SingletonBehaviour<AudioManager>
{
    public Transform BGMTrs; // BGM ����� �ҽ����� �θ� Ʈ������
    public Transform SFXTrs; // SFX ����� �ҽ����� �θ� Ʈ������

    private const string AUDIO_PATH = "Audio"; // ����� ���ҽ� ���� ���

    private Dictionary<BGM, AudioSource> m_BGMPlayer = new Dictionary<BGM, AudioSource>(); // BGM �÷��̾� ��ųʸ�
    private AudioSource m_CurrBGMSource; // ���� ��� ���� BGM �ҽ�

    private Dictionary<SFX, AudioSource> m_SFXPlayer = new Dictionary<SFX, AudioSource>(); // SFX �÷��̾� ��ųʸ�

    // �ʱ�ȭ �Լ�
    protected override void Init()
    {
        base.Init(); // �θ� Ŭ���� �ʱ�ȭ ȣ��

        LoadBGMPlayer(); // BGM �÷��̾� �ε�
        LoadSFXPlayer(); // SFX �÷��̾� �ε�
    }

    // BGM �÷��̾���� �ε��ϴ� �Լ�
    private void LoadBGMPlayer()
    {
        for (int i = 0; i < (int)BGM.COUNT; i++) // ��� BGM Ÿ�Կ� ���� �ݺ�
        {
            var audioName = ((BGM)i).ToString(); // BGM �̸� ��������
            var pathStr = $"{AUDIO_PATH}/{audioName}"; // ����� ���� ��� ����
            var audioClip = Resources.Load(pathStr, typeof(AudioClip)) as AudioClip; // ����� Ŭ�� �ε�
            if (!audioClip) // ����� Ŭ���� ���� ���
            {
                Logger.LogError($"{audioName} clip does not exist."); // ���� �α� ���
                continue; // ���� ������ �ǳʶٱ�
            }

            var newGO = new GameObject(audioName); // �� ���� ������Ʈ ����
            var newAudioSource = newGO.AddComponent<AudioSource>(); // ����� �ҽ� ������Ʈ �߰�
            newAudioSource.clip = audioClip; // ����� Ŭ�� ����
            newAudioSource.loop = true; // �ݺ� ��� ����
            newAudioSource.playOnAwake = false; // �ڵ� ��� ��Ȱ��ȭ
            newGO.transform.parent = BGMTrs; // �θ� Ʈ������ ����

            m_BGMPlayer[(BGM)i] = newAudioSource; // ��ųʸ��� ����� �ҽ� �߰�
        }
    }

    // SFX �÷��̾���� �ε��ϴ� �Լ�
    private void LoadSFXPlayer()
    {
        for (int i = 0; i < (int)SFX.COUNT; i++) // ��� SFX Ÿ�Կ� ���� �ݺ�
        {
            var audioName = ((SFX)i).ToString(); // SFX �̸� ��������
            var pathStr = $"{AUDIO_PATH}/{audioName}"; // ����� ���� ��� ����
            var audioClip = Resources.Load(pathStr, typeof(AudioClip)) as AudioClip; // ����� Ŭ�� �ε�
            if (!audioClip) // ����� Ŭ���� ���� ���
            {
                Logger.LogError($"{audioName} clip does not exist."); // ���� �α� ���
                continue; // ���� ������ �ǳʶٱ�
            }

            var newGO = new GameObject(audioName); // �� ���� ������Ʈ ����
            var newAudioSource = newGO.AddComponent<AudioSource>(); // ����� �ҽ� ������Ʈ �߰�
            newAudioSource.clip = audioClip; // ����� Ŭ�� ����
            newAudioSource.loop = false; // �ݺ� ��� ��Ȱ��ȭ
            newAudioSource.playOnAwake = false; // �ڵ� ��� ��Ȱ��ȭ
            newGO.transform.parent = SFXTrs; // �θ� Ʈ������ ����

            m_SFXPlayer[(SFX)i] = newAudioSource; // ��ųʸ��� ����� �ҽ� �߰�
        }
    }

    // BGM ��� �Լ�
    public void PlayBGM(BGM bgm)
    {
        if (m_CurrBGMSource) // ���� ��� ���� BGM�� �ִ� ���
        {
            m_CurrBGMSource.Stop(); // ���� BGM ����
            m_CurrBGMSource = null; // ���� BGM �ҽ� �ʱ�ȭ
        }

        if (!m_BGMPlayer.ContainsKey(bgm)) // ��û�� BGM�� ��ųʸ��� ���� ���
        {
            Logger.LogError($"Invalid clip name. {bgm}"); // ���� �α� ���
            return; // �Լ� ����
        }

        m_CurrBGMSource = m_BGMPlayer[bgm]; // �� BGM �ҽ� ����
        m_CurrBGMSource.Play(); // BGM ��� ����
    }

    // BGM �Ͻ����� �Լ�
    public void PauseBGM()
    {
        if (m_CurrBGMSource) m_CurrBGMSource.Pause(); // ���� BGM�� ������ �Ͻ�����
    }

    // BGM ��� �簳 �Լ�
    public void ResumeBGM()
    {
        if (m_CurrBGMSource) m_CurrBGMSource.UnPause(); // ���� BGM�� ������ ��� �簳
    }

    // BGM ���� �Լ�
    public void StopBGM()
    {
        if (m_CurrBGMSource) m_CurrBGMSource.Stop(); // ���� BGM�� ������ ����
    }

    // SFX ��� �Լ�
    public void PlaySFX(SFX sfx)
    {
        if (!m_SFXPlayer.ContainsKey(sfx)) // ��û�� SFX�� ��ųʸ��� ���� ���
        {
            Logger.LogError($"Invalid clip name. ({sfx})"); // ���� �α� ���
            return; // �Լ� ����
        }

        m_SFXPlayer[sfx].Play(); // SFX ���
    }

    // ��� ����� ���Ұ� �Լ�
    public void Mute()
    {
        foreach (var audioSourceItem in m_BGMPlayer) // ��� BGM �÷��̾ ����
        {
            audioSourceItem.Value.volume = 0f; // ������ 0���� ����
        }

        foreach (var audioSourceItem in m_SFXPlayer) // ��� SFX �÷��̾ ����
        {
            audioSourceItem.Value.volume = 0f; // ������ 0���� ����
        }
    }

    // ��� ����� ���Ұ� ���� �Լ�
    public void UnMute()
    {
        foreach (var audioSourceItem in m_BGMPlayer) // ��� BGM �÷��̾ ����
        {
            audioSourceItem.Value.volume = 1f; // ������ 1�� ����
        }

        foreach (var audioSourceItem in m_SFXPlayer) // ��� SFX �÷��̾ ����
        {
            audioSourceItem.Value.volume = 1f; // ������ 1�� ����
        }
    }
}