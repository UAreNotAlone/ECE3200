using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagManager : MonoBehaviour
{
    
    public GameObject bag;
    public ItemListManager itemListManager;
    public Dictionary<int ,Transform> ItemPositionToOrders = new Dictionary<int, Transform>();//null is meaningless
    private void Awake()
    {
        bag = this.gameObject;
        List<Transform> itemPositions = new List<Transform>(bag.transform.GetComponentsInChildren<Transform>());
        
        itemPositions.Remove(bag.transform);
       for(int i = 0; i < itemPositions.Count; i++)
        {
            ItemPositionToOrders.Add(i, itemPositions[i]);//�ѱ�����λ��һ��int order��ϵ����(order��0��ʼ����)
        }
        //ע���ʼ�����Ǽ���������Ʒ
        itemListManager = GameObject.Find("PlayerItemList").GetComponent<ItemListManager>();
        foreach (OneTypeItem type in itemListManager.itemsList)
        {
            //��������
            Item obj = Instantiate(type.itemPrefabs);
            obj.transform.position = ItemPositionToOrders[type.itemOrder].transform.position;
            obj.transform.SetParent(ItemPositionToOrders[type.itemOrder].transform);
            obj.couldUsed = true;
            //���²���
            //ע��itemManager���������µ�һ����������ֻ�Ǳ�����ʾ��ͼ
        }
        itemListManager.addedItemType = new List<OneTypeItem>();//ע�������֮��������Ϊnull
        gameObject.SetActive(false);//ֻ��һ��ʼ���Զ������Լ�
    }
    public void OnEnable()//�����رձ�����ͨ����������������ʵ�ֵ�
    {
        //��ȡitemListManager����ĸ����б����£�����������б���Ϊnone
        foreach (OneTypeItem type in itemListManager.addedItemType)
        {
            //��������
            Item obj = Instantiate(type.itemPrefabs);
            obj.transform.position = ItemPositionToOrders[type.itemOrder].transform.position;
            obj.transform.SetParent(ItemPositionToOrders[type.itemOrder].transform);
            obj.couldUsed = true;
        }
        itemListManager.addedItemType = new List<OneTypeItem>();
    }
    public void UseItem(Item item)
    {
        IItemEffect[] itemEffects = item.gameObject.GetComponents<IItemEffect>();
        foreach(IItemEffect itemEffect in itemEffects)
        {
            itemEffect.ItemEffect();
        }
        itemListManager.RemoveItem(item);//��������
    }
    public void Update()
    {
        
    }
}
