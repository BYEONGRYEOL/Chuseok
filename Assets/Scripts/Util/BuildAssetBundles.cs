using System.IO;
using UnityEditor;
using UnityEngine;

public class BuildAssetBundles : MonoBehaviour
{
#if UNITY_EDITOR
    [MenuItem("AssetBundle/BuildBundles")] //�޴��ν� ����

    /* ���� �����ϴ� �Լ� */
    public static void BuildBundles()
    {
        string assetBundleDirectory = "Assets/Resources/AssetBundles"; //������ ������ ������ ��ġ
        if (!Directory.Exists(assetBundleDirectory)) //������ �������� ������
        {
            Directory.CreateDirectory(assetBundleDirectory); //���� ����
        }
        BuildPipeline.BuildAssetBundles(assetBundleDirectory, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows64); //���� ����
        AssetDatabase.Refresh(); //Asset ��������
    }
#endif
}