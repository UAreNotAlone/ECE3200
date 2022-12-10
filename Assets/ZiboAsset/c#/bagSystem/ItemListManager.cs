using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class ItemListManager : MonoBehaviour
{
    public Dictionary<string, OneTypeItem> itemNamesToType = new Dictionary<string, OneTypeItem>();
    public List<int> remainingPositionOrder = new List<int>();

    public List<OneTypeItem> itemsList = new List<OneTypeItem>();
    //Ϊ�˷��㱳��������ʱ���ٸ��£���ü���һ��add�б��(remove�б�?),�����ڱ��������������������б�
    public List<OneTypeItem> addedItemType = new List<OneTypeItem>();//
    [Header("�ֶ����ñ������͹ؿ��޹أ�")]
    public GameObject bag;
    
    // Start is called before the first frame update
    //���ش浵�е���Ʒ�б�
    public void Awake()
    {
        //���屻���ú��޷�ͨ��find�ҵ�
        //bag = GameObject.Find("bag");
        for(int i = 0; i < bag.transform.childCount; i++)
        {
            remainingPositionOrder.Add(i);
            
        }
        
        foreach(OneTypeItem item in itemsList)
        {
            remainingPositionOrder.Remove(item.itemOrder);
        }
        //��ʼʱ���౳��λ��
        Debug.Log("bag have children" + bag.transform.childCount);
        //����������Ʒ����
        foreach(OneTypeItem item in itemsList)
        {
            itemNamesToType.Add(item.ItemName,item);
        }


    }
    public void LoadItemList()
    {
        //�ݻ�ԭ���б�
        //���ô浵�ַ��б��ȡItemScriptableObject in resourcess
    }
    //���item ������һ��ʵ������Item����������һ��scriptableObject(OneTypeItem)�������б�,�浵ʱ�ǵö�ȡ����б�
    public void AddItem(Item item)//ʵ������item����
    {
        foreach(string Iname in itemNamesToType.Keys)
        {
            Debug.Log(item.ItemName+""+Iname);
            if(item.ItemName == Iname)
            {
                Debug.Log("same name");
                itemNamesToType[item.ItemName].itemNumber += 1;
                return;
            }//��������Ʒ�ڱ������Ѿ�����ʱ����Ʒ������һ��ͬʱʣ���λ��nametype��Ӧ��ϵ����
        }
        if (remainingPositionOrder.Count >= 1)//���п�λ��û�и�itemʱ,
        {
            
            var level = ScriptableObject.CreateInstance<OneTypeItem>();
            level.ItemName = item.ItemName;
            level.itemPrefabs = item.thisPrefab;//ע��ֻ�����prefab���Ը�ֵ��asset
            level.itemNumber = 1;
            level.itemOrder = remainingPositionOrder[0];
            remainingPositionOrder.Remove(remainingPositionOrder[0]);
           
            itemNamesToType.Add(level.ItemName, level);
            
            itemsList.Add(level);
            addedItemType.Add(level);//�������б�

            AssetDatabase.CreateAsset(level, @"Assets/Resources/ItemInBag/" + level.ItemName + ".asset");//�ڴ����·���д�����Դ
            AssetDatabase.SaveAssets(); //�洢��Դ
            AssetDatabase.Refresh();
        }
        else//û�п�λʱ
        {
            Debug.Log("bag is full");
        }
    }
    //�Ƴ�����
    public void RemoveItem(Item item)//���ﴫ��Ĳ�����һ��ʵ������Item prefab,�൱������þ�һ����Ʒ(�ڱ�������)
    {
        Debug.Log("exe");
        List<string> list = new List<string>(itemNamesToType.Keys);
        //���򲻲�itemType�Ƿ���itemlist�����ƺ�û������(������÷����õ��Ļ�)��
        foreach (string Iname in list)
        {
            Debug.Log(Iname + " " + item.ItemName);
            if(Iname == item.ItemName)
            {
                itemNamesToType[item.ItemName].itemNumber -= 1;
                if (itemNamesToType[item.ItemName].itemNumber <= 0)
                {
                    remainingPositionOrder.Add(itemNamesToType[item.ItemName].itemOrder);
                    remainingPositionOrder.Sort();
                    itemNamesToType.Remove(item.ItemName);
                    Destroy(item.gameObject);//ɾ������
                    string path = "Assets/Resources/ItemInBag/" + item.ItemName + ".asset";
                    
                    File.Delete(path);
                    // ע��������ݻ���Ӧ��scriptableObject,��Ҫ��ԭ�ȼ�һ��·����⣨unity�ƺ����Զ����ǣ���
                    //��ò�Ҫ������ݻ٣�����ֻ�������ݣ���bagManager����ͼ���дݻ�?
                    AssetDatabase.SaveAssets(); //ע�����
                    AssetDatabase.Refresh();
                }
            }
        }

    }
}
