using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class ReflectHarmCard : CardItem
{
    public buffIconManager buffIconManager;
    public void Awake()
    {
        buffIconManager = GameObject.Find("BuffTransform").transform.GetComponent<buffIconManager>();
    }
    //  Use the card
    public override void OnEndDrag(PointerEventData eventData)
    {
        //  Detect whether the mouse is one the player's position.

        if (TryUseCard_Mana() == true)
        {
            int effect_val = int.Parse(cardData["Arg0"]);
            //�ҵ�resource����ķ���buff��ʵ����
            Debug.Log("reflectharmcard is used");
            
            Buff buffPrefab = Resources.Load<Buff>("Buff/reflectHarmBuff");
            Buff buffInstance = Instantiate(buffPrefab, buffIconManager.transform.position, buffIconManager.transform.rotation);//ʵ����buff
                                                                                                                             //Debug.Log(buff.gameObject.GetComponent<IBuff>());
            buffInstance.SetActiveParameter(buffPrefab.startBuffTurn, buffPrefab.buffExecutedTurns, buffPrefab.endBuffTurn);//����buffʵ��Ч��
            buffIconManager.AddBuff(buffInstance.transform);//����ͼ��
        }
        else
        {
            base.OnEndDrag(eventData);
        }


    }
}