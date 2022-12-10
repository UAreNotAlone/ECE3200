using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class CreateAssetFile : UnityEditor.Editor
{


    const string assetsFolderName = "AssetFiles";
    

    [MenuItem("Editor/CreateSetingAsset")]
    public static void CreateAsset()
    {
        // ʵ������
        Setting asset = ScriptableObject.CreateInstance<Setting>();
        GameObject m =(GameObject) Resources.Load("Text");

        // ���ʵ���� Bullet ��Ϊ�գ�����
       
        if (!asset)
        {
            Debug.LogWarning("Bullet not found");
            return;
        }
        // �Զ�����Դ����·��
        string path = Application.dataPath + "/" + assetsFolderName + "/";
        GameObject n = Instantiate(m);
        PrefabUtility.SaveAsPrefabAsset(n, "Assets/"+"a.prefab");
        GameObject.DestroyImmediate(n);

        // �����Ŀ�ܲ�������·��������һ��
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        //������ Setting ת��Ϊ�ַ���
        //ƴ�ӱ����Զ�����Դ��.asset�� ·��
        path = string.Format("Assets/" + assetsFolderName + "/{0}.asset", (typeof(Setting).ToString()));
        // �����Զ�����Դ��ָ��·��
        AssetDatabase.CreateAsset(asset, path);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
}