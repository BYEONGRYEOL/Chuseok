using System.IO;
using UnityEditor;
using UnityEngine;

public class BuildAssetBundles : MonoBehaviour
{
#if UNITY_EDITOR
    [MenuItem("AssetBundle/BuildBundles")] //메뉴로써 제작

    /* 번들 제작하는 함수 */
    public static void BuildBundles()
    {
        string assetBundleDirectory = "Assets/Resources/AssetBundles"; //번들을 저장할 폴더의 위치
        if (!Directory.Exists(assetBundleDirectory)) //폴더가 존재하지 않으면
        {
            Directory.CreateDirectory(assetBundleDirectory); //폴더 생성
        }
        BuildPipeline.BuildAssetBundles(assetBundleDirectory, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows64); //번들 제작
        AssetDatabase.Refresh(); //Asset 리프레쉬
    }
#endif
}