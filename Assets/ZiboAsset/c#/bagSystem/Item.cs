using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour//����ϣ��cardprefab �� cyberborgҲ������Ϊ���屻�Ž�����
{
    public ItemListManager listManager;
    public bool couldUsed = false;
    [Header("�ֶ�����")]
    public Item thisPrefab;
    public string ItemName;
    public void Start()
    {
        listManager = GameObject.Find("PlayerItemList").GetComponent<ItemListManager>();
    }
    public void Update()
    {
        if(couldUsed && Input.GetMouseButtonDown(0) && IsMouseOnTransform.IsMouseOnThisTransform(transform, DistanceCriterion))
        {
            IItemEffect[] itemEffects = GetComponents<IItemEffect>();
            if (itemEffects != null)
            {
                foreach (IItemEffect effect in itemEffects)
                {
                    effect.ItemEffect();
                }
                listManager.RemoveItem(this);
            }
            else
            {
                Debug.Log("no IItemEffect "+ gameObject.name);
            }
            
        }
    }
    public bool DistanceCriterion(Vector3 v1, Vector3 v2)
    {
        if(Mathf.Abs(v1.x-v2.x) < 7 && Mathf.Abs(v1.y - v2.y) < 7)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
