using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardClass: MonoBehaviour
{
    //�����������Ƿ��п����Ѿ���ѡ��

    [Header("�Զ���ȡ�ı���")]
    public bool isExtended = false;

    public Player holdPlayer;
    
    public enum ThisCardStage
    {
        cardNotAroundMouse,
        cardAroundMouse,
        Chosen,
        SentOut
    }
    public ThisCardStage thisCardStage = ThisCardStage.cardNotAroundMouse;

    //λ��
    public Transform positionTransform;
    //��һ��ս���з��뿨�۵�λ��
    //���󶨵�Ч������
    //�Ƿ����������������
    public bool isInPlayerCardList = false;
    //�Ƿ�����һ�غϱ�����
    public bool isGettonThisTurn = false;
    public GameObject cardPrefab;
    public float Speed = 40;
    [Header("��Ҫ�ֶ����õı���")]
    public int CostEnergy = 1;
    public Vector2 CardSize = new Vector2(16,20);
    //����Ƿ�������ſ���
    public string cardName;
    
    #region function
    public void MousePutOnThisCard()
    {
        float step = Speed * Time.deltaTime;
        //Debug.Log("MousePutOnThisCard");
        //�������ŵ�ʱ���᲻�ٸ�����̧��
        if (!isExtended)
        {
            CardSize.y = CardSize.y * 2;
            isExtended = true;
        }

        transform.position = Vector2.MoveTowards(transform.position, 
            transform.parent.transform.position + new Vector3(0,0.25f*CardSize.y, 0),step);

    }
    public void notMousePutOnThisCard()
    {
        float step = Speed * Time.deltaTime;
        //Debug.Log("noMouseOnCard"+cardName);
        //û��������ʱ�ظ�ԭ״
        if (isExtended)
        {
            CardSize.y = CardSize.y * 0.5f;
            isExtended = false;
        }
        transform.position = Vector2.MoveTowards(transform.position,
           transform.parent.transform.position, step);
       
    }
    public void OnCardChoosen()
    {
        //Debug.Log(cardName + "is chosen");
        //����������Ե����
       
    }
    public void UseCauseEffect(List<Transform> targetList)
    {
        
        //��������
        //Ȼ����ܲ�������Ч��
        if (holdPlayer.TryConsumeEnergy(CostEnergy) == true)//ʵ�������ǿ��Զ��岻ͬ��Ч���ӿڣ���move��attack,
                                                            //�������������ʵ�ֲ�ͬ��Ⱥ��������ò����������
                                                            //��������������ж��Ƿ�Ӧ�ý��������ű����������������ж�Ҫ���ܶ��
        {
            thisCardStage = ThisCardStage.SentOut;//
            IEffect[] effects = gameObject.GetComponents<IEffect>();
            foreach (IEffect effect in effects) effect.CauseEffect(targetList);
            Destroy(gameObject);
        }
        else
        {
            thisCardStage = ThisCardStage.cardNotAroundMouse;
        }
        
        //ʵ���˽ű��������������ݣ��ͽű������������ԣ�
        //Debug.Log("card is sent out");
    }
    #endregion
    public void Start()
    {
        
        cardPrefab = gameObject;
        positionTransform = this.gameObject.transform;
        
    }
    public void Update()
    {
        if(thisCardStage == ThisCardStage.Chosen)
        {
            OnCardChoosen();
        }
        if(thisCardStage == ThisCardStage.cardAroundMouse)
        {
            MousePutOnThisCard();
        }
        else if(thisCardStage == ThisCardStage.cardNotAroundMouse)
        {
            notMousePutOnThisCard();
        }
       
    }

}
