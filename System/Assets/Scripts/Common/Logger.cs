using System.Diagnostics;
using UnityEngine;

//1. �߰����� ���� ǥ�� (ex.Ÿ�ӽ�����)
//2. ��ÿ� ���带 ���� �α� ����
public static class Logger
{
    //�Ϲ� �α� �޽����� ���, DEV_VER ���Ǻ� �����Ͽ����� ����
    [Conditional("DEV_VER")]
    public static void Log(string msg)
    {
        UnityEngine.Debug.LogFormat("[{0}] {1}", System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"),msg);
    }

    // ��� �α� �޽����� ���, DEV_VER ���Ǻ� �����Ͽ����� ����
    [Conditional("DEV_VER")]
    public static void LogWarning(string msg)
    {
        UnityEngine.Debug.LogWarningFormat("[{0}] {1}", System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), msg);
    }

    //���� �α� �޽����� ���, �׻� ����
    public static void LogError(string msg)
    {
        UnityEngine.Debug.LogErrorFormat("[{0}] {1}", System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), msg);
    }
}
