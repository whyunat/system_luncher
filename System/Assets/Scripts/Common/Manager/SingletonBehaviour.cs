using UnityEngine;

// ���׸� �̱��� ������ ������ ���̽� Ŭ����
public class SingletonBehaviour<T> : MonoBehaviour where T : SingletonBehaviour<T>
{
    // �� ��ȯ �� �������� ����
    protected bool m_IsDestroyOnLoad = false;

    // �� Ŭ������ ����ƽ �ν��Ͻ� ����
    protected static T m_Instance;

    // �ν��Ͻ� ���ٿ� ������Ƽ
    public static T Instance
    {
        get { return m_Instance; }
    }

    // �ʱ�ȭ�� ���� Awake���� Init ȣ��
    private void Awake()
    {
        Init();
    }

    // �̱��� �ʱ�ȭ ����
    protected virtual void Init()
    {
        if (m_Instance == null)
        {
            m_Instance = (T)this;

            // �� ��ȯ �� �������� ����
            if (!m_IsDestroyOnLoad)
            {
                DontDestroyOnLoad(this);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // ������Ʈ �ı� �� ȣ��
    protected virtual void OnDestroy()
    {
        Dispose();
    }

    // ���� �۾� ����
    protected virtual void Dispose()
    {
        m_Instance = null;
    }
}
