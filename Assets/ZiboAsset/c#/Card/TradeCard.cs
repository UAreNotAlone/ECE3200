using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TradeCard : MonoBehaviour//�������ſ���ʱֻ�������ű�cardClass and this
{
    //������ſ�Ƭ����������ѡ���������ƣ���ᶪ��������ƣ��ݻٲ���playerturn�������Ƴ���������ϵͳ�ص�noChooseCard״̬
    //��Ϊ���ſ��Ʊ�Ȼ��������Բ��ÿ�������;���Ҫ��������������������������
    public CardClass thisCard;
    public GameMasterPlayerTurn playerTurnMaster;
    public BattleGameMaster battleGameMaster;
    public List<CardClass> cardListExceptMe;
    private void Start()
    {
        
        battleGameMaster = GameObject.Find("BattleGameMaster").GetComponent<BattleGameMaster>();//Ϊ�˷����Ժ��޸ģ����нű���player���ֱ�Ӵ�һ���ط���ȡ
        thisCard = gameObject.GetComponent<CardClass>();
        playerTurnMaster = GameObject.Find("PlayerTurn").GetComponent<GameMasterPlayerTurn>();
        foreach (CardClass card in playerTurnMaster.playerLeftCard)
        {
            if (card != thisCard)
            {
                cardListExceptMe.Add(card);
            }
        }
    }
    private void Update()
    {
        
        if (thisCard.thisCardStage == CardClass.ThisCardStage.Chosen)
        {
            cardListExceptMe = new List<CardClass>();
            foreach (CardClass card in playerTurnMaster.playerLeftCard)
            {
                if (card != thisCard)
                {
                    cardListExceptMe.Add(card);
                }
            }
            //copy playerLeftcard
            CardClass cardAroundMouse = IsMouseAroundSomeCard.IsMouseOnSomeCard(cardListExceptMe);//��������ڵ���ʱ��ı�card��״̬,�������Ƕ����������player turn��ʹ�������ϸ�����
                                                                                  //��ֻ��nochoose �׶�ʹ�ã����Ըı�״̬������Ӱ�죬
                                                                                  //�������������ֻ��ı�������ſ����⿨�Ƶ�״̬
            //Debug.Log(cardAroundMouse.name);
            if(Input.GetMouseButtonDown(0) && cardAroundMouse != null)//�������Ʋ���,�ų����ſ���
            {
                //�ȱ����ʱ��mouse card
                Debug.Log("choose abandoned card");
                CardClass cardTobeAbandon = cardAroundMouse;
                playerTurnMaster.playerLeftCard.Remove(cardTobeAbandon);
                playerTurnMaster.playerLeftCard.Remove(thisCard);//�������
                thisCard.thisCardStage = CardClass.ThisCardStage.SentOut;//����Զ�������trader�����Ƴ����飬��û��ʹ��UseCauseEffect;//����ϵͳ�ص�noChoose״̬
                battleGameMaster.player.currentEnergy += 1;
                Destroy(cardTobeAbandon.gameObject);
                Destroy(thisCard.gameObject);//ע��ݻ�������ǽű�

            }
        }
        //������Ҽ�ʱ���ڿ���״̬�ᣨ��playerTurnϵͳ���Զ�ת��Ϊnochoose��
    }
}
