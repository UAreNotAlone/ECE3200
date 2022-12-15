using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Vodka: CardItem
{
    public buffIconManager buffIconManager;
    public void Awake()
    {
        buffIconManager = GameObject.Find("BuffTransform").transform.GetComponent<buffIconManager>();
        
    }
    public override void OnEndDrag(PointerEventData eventData)
    {
        if (TryUseCard_Mana() == true)
        {

            Debug.Log("vodka is used");
            //��ȡ�������岿debufff�����ɾ��һ��

            List<Buff> debuffs = new List<Buff>();
            foreach (Transform buff in buffIconManager.buffs)
            {
                if (buff.GetComponent<Buff>().isDebuff == true && buff.GetComponent<Buff>().buffType == Buff.BuffType.bodyBuff)
                {
                    debuffs.Add(buff.GetComponent<Buff>());
                }
            }
            if (debuffs.Count != 0)//�������岿debuff����ɾ��ʱ��������һ��ɾ���Ĺ���
            {
                buffIconManager.RemoveBuff(debuffs[Random.Range(0, debuffs.Count)].transform);
            }
            //������һ��buff
            Buff addBuff = buffIconManager.totalBuff[Random.Range(0,buffIconManager.totalBuff.Count)];
            Buff buffInstance = Instantiate(addBuff, buffIconManager.transform.position, buffIconManager.transform.rotation);//ʵ����buff
                                                                                                                             //Debug.Log(buff.gameObject.GetComponent<IBuff>());
            buffInstance.SetActiveParameter(addBuff.startBuffTurn, addBuff.buffExecutedTurns, addBuff.endBuffTurn);//����buffʵ��Ч��
            buffIconManager.AddBuff(buffInstance.transform);//����ͼ��


        }
        else
        {
            base.OnEndDrag(eventData);
        }
    }
}

