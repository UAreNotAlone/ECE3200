using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TestScriptable : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var level = ScriptableObject.CreateInstance<OneTypeItem>();
        level.ItemName = "first";
        AssetDatabase.CreateAsset(level, @"Assets/Resources/ItemInBag/" + level.ItemName + ".asset");//�ڴ����·���д�����Դ
        AssetDatabase.SaveAssets(); //�洢��Դ
        AssetDatabase.Refresh();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
