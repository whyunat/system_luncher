using System;
using UnityEngine;


//UI �����͸� ��� Ŭ����
public class BaseUIData
{
    public Action OnShow;  //UI�� ǥ�õ� �� ������ �ݹ�
    public Action OnClose; //UI�� ���� �� ������ �ݹ�
}

//��� UI�� �⺻ Ŭ����
public class BaseUI : MonoBehaviour
{
    public Animation m_UIOpenAnim; //UI ���� �ִϸ��̼�

    private Action m_OnShow;    //UI ǥ�� �� ������ �׼�
    private Action m_OnClose;   //UI �ݱ� �� ������ �׼�


    // UI �ʱ�ȭ �޼���
    public virtual void Init(Transform anchor)
    {
        Logger.Log($"{GetType()} init.");    // �ʱ�ȭ �α� ���

        m_OnShow = null;     // ǥ�� �׼� �ʱ�ȭ
        m_OnClose = null;    // �ݱ� �׼� �ʱ�ȭ

        transform.SetParent(anchor);    // �θ� Transform ����

        var rectTransform = GetComponent<RectTransform>();    // RectTransform ������Ʈ ��������
        if (!rectTransform)    // RectTransform�� ���� ���
        {
            Logger.LogError("UI does not have rectransform.");    // ���� �α� ���
            return;    // �޼��� ����
        }

        rectTransform.localPosition = new Vector3(0f, 0f, 0f);    // ���� ��ġ ����
        rectTransform.localScale = new Vector3(1f, 1f, 1f);       // ���� ������ ����
        rectTransform.offsetMin = new Vector2(0, 0);              // �ּ� ������ ����
        rectTransform.offsetMax = new Vector2(0, 0);              // �ִ� ������ ����
    }

    // UI ���� ���� �޼���
    public virtual void SetInfo(BaseUIData uiData)
    {
        Logger.Log($"{GetType()} set info.");    // ���� ���� �α� ���

        m_OnShow = uiData.OnShow;      // ǥ�� �ݹ� ����
        m_OnClose = uiData.OnClose;    // �ݱ� �ݹ� ����
    }

    // UI ǥ�� �޼���
    public virtual void ShowUI()
    {
        if (m_UIOpenAnim)    // ���� �ִϸ��̼��� �ִ� ���
        {
            m_UIOpenAnim.Play();    // �ִϸ��̼� ���
        }

        m_OnShow?.Invoke();    // ǥ�� �ݹ� ����
        m_OnShow = null;       // ǥ�� �ݹ� �ʱ�ȭ
    }

    // UI �ݱ� �޼���
    public virtual void CloseUI(bool isCloseAll = false)
    {
        if (!isCloseAll)    // ��ü �ݱⰡ �ƴ� ���
        {
            m_OnClose?.Invoke();    // �ݱ� �ݹ� ����
        }
        m_OnClose = null;    // �ݱ� �ݹ� �ʱ�ȭ

        UIManager.Instance.CloseUI(this);    // UI �Ŵ����� ���� UI �ݱ�
    }

    // �ݱ� ��ư Ŭ�� �� ����Ǵ� �޼���
    public virtual void OnClickCloseButton()
    {
        AudioManager.Instance.PlaySFX(SFX.ui_button_click);    // ��ư Ŭ�� ȿ���� ���
        CloseUI();    // UI �ݱ� ����
    }
}