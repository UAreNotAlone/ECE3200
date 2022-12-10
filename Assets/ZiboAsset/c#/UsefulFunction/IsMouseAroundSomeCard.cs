using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsMouseAroundSomeCard:MonoBehaviour
{
    
    public void Enable()
    {
        
    }
    public static CardClass IsMouseOnSomeCard(List<CardClass> cardList)//����Ƿ�������ĳ���������棬����Ƿ��ظÿ��ƵĶ���ִ��cardClass��MousePutOn�����������������Ƹ�ԭ
    {
        
        
        
       
        for (int i = 0; i < cardList.Count; i++)
        {

            if (IsMouseOnThisCard(cardList[i]))
            {
                CardClass card = cardList[i];
                if (card != null)
                {

                    //Debug.Log(i);
                    card.thisCardStage = CardClass.ThisCardStage.cardAroundMouse;
                    return card;
                }


                //Debug.Log(CardClass.isExitMousePutOnSomeCard);

            }
            else
            {
                CardClass card = cardList[i];
                card.thisCardStage = CardClass.ThisCardStage.cardNotAroundMouse;
                card.notMousePutOnThisCard();
            }

        }
        
        //Debug.Log(CardClass.isExitMousePutOnSomeCard);
        return null;

    }
    public static bool IsMouseOnThisCard(CardClass card)
    {
        if (card == null) return false;
        Vector3 mousePos = Input.mousePosition;//ע��˲����ȡ��ֵ����Ļ����
        Vector3 RealMousePos = Camera.main.ScreenToWorldPoint(mousePos);
        //Debug.Log(RealMousePos);//���������zͳͳΪ-10��()
        RealMousePos = new Vector3(RealMousePos.x, RealMousePos.y, card.transform.position.z);

        if (Mathf.Abs(card.transform.position.y - RealMousePos.y) < card.CardSize.y*0.5 &&
            Mathf.Abs(card.transform.position.x - RealMousePos.x) < card.CardSize.x*0.5)
        {

            return true;
        }
        else
        {
            return false;
        }
    }
}
