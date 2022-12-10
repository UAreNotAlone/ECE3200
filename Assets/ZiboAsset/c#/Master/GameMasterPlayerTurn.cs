using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMasterPlayerTurn : MonoBehaviour
{
    [Header("�Զ���ȡ�ı���")]
    public GameObject EndTurnButton;
    public Player player;
    //public BattleGameMaster gameMaster;
    public CardClass cardAroundMouse;
    public bool isSomeBodyAnimation = false;


    public enum Stage
    {       drawCard,
            NoChooseCard,
            ChosenCard,//ѡ�п�Ƭ
            CardAniamtion//�ѿ������
    }
    public Stage stage;

    public BattleGameMaster battleGameMaster;

    public CardClass cardChoosen;
    public List<CardClip> cardClips;

    public int numberOfCardATurn;
    public List<CardClass> playerTotalCard;
    public List<CardClass> playerLeftCard = new List<CardClass>();

    // Start is called before the first frame update
   
    private void Start()//���µĻغϷ���enable masterʱ���µ��ã�����
    {
        EndTurnButton = GameObject.Find("EndTurnButtonUI");
        battleGameMaster = GameObject.Find("BattleGameMaster").GetComponent<BattleGameMaster>();
        player = GameObject.Find("BattleGameMaster").GetComponent<BattleGameMaster>().player;
        cardClips = battleGameMaster.cardClips;
        numberOfCardATurn = battleGameMaster.numberOfCardATurn;
        playerTotalCard = battleGameMaster.playerTotalCard;

}
    public void OnEnable()
    {
        //�ظ�����
        stage = Stage.drawCard;
        playerLeftCard = new List<CardClass>();
    }

    // Update is called once per frame
    void Update()
    {
        if(stage == Stage.drawCard)
        {
            player.RecoverEnergy();
            Initial();
            stage = Stage.NoChooseCard;//ע�����ں������غ�ѭ����Ʋ����ٻص��鿨�׶Σ�����ʵ�������ֻ��ִ��һ��
                                        //ʵ���������ĵ�ͼ������ÿ��ִ��һ�Σ����Կ��Կ����ƻ�����һ˵
        }
        if (stage == Stage.NoChooseCard)  //�ж�����Ƿ���ĳ�ſ������������Ѿ�ѡ��׶�ִ�в���ֹ�ж��Ƿ���ɶ���
        {
            EndTurnButton.SetActive(true);
            if (IsInvoking(nameof(isPerformAnimation)))
            {
                CancelInvoke(nameof(isPerformAnimation));
            }
            cardAroundMouse = IsMouseAroundSomeCard.IsMouseOnSomeCard(playerLeftCard);
        }
        if (stage != Stage.CardAniamtion &&stage != Stage.drawCard)//�÷񶨾�ʽ�ǲ�����δ������ʱ��̫��ȫ
        {
            PlayerChoseOrGiveUpCard();//���ĳ�ſ�ѡ���Ҽ��ظ�ԭװ
        }
        
        //Debug.Log(enemies[0].gameObject == null);//�м�Ĵ�������ɿ��ƾ������
        
        if(stage == Stage.ChosenCard && cardChoosen.thisCardStage == CardClass.ThisCardStage.SentOut)//������ƺ󣬲�ѡ�����ſ���,������player����
        {
            Debug.Log("Card is Sent out");
            
            InvokeRepeating(nameof(isPerformAnimation),0.1f,0.5f);//���ڿ��ƵĶ�������ܶ࣬���õķ���Ҳ������ͬ�������ðѿ��ƴ����Ķ�������Ϊ����Ч����ϸ��
            //���������̽����������壬��������ʵ�ֹ���//ע���ڿ��ƶ����¼����GameMasterPlayerTrun ��stage����
            //�ҷϳ�ǰ��˵�Ļ�����Ϊ�����ڿ��ƺ���һغϿ�����֮����player��������һ����card�������еĶ���������player������ã�
            //��һ����player��һ�������Ƿ���ɶ���������trigger ��bool���ƵĶ����в�ͬ�Ĵ���Ч�����������غϿ������ж��Ƿ�Ҫ������һ�׶�
            //����дһ��ս��UI�࣬�������е�UIԪ�أ�������ЩUI���ࣨ��public��ɣ����ڷ�����ɺ�ͳһ��ĳ���׶ζ�ĳ��UI���н��õ��ã�����button ��
            //���0.1fs��Ϊ�˸�player��enemy�㹻�ķ�Ӧʱ������������
            CardIsSentOut();
            

        }
        
        
        if(playerLeftCard.Count == 0)//�غϽ�������
        {
            EndTurnButton.SetActive(false);
            BattleGameMaster.IsPlayerTurn = false;
            
        }
        //�����غϰ�ť

}
    public void CardIsSentOut()
    {
        EndTurnButton.SetActive(false);
        stage = Stage.CardAniamtion;
        playerLeftCard.Remove(cardChoosen);
        UpdateCardPosition(cardChoosen);
        
    }
    public void UpdateCardPosition(CardClass card)
    {
        //ȥ������Ŀ���
        //�ƶ����п���λ�ã���ȡplayerLefCard�����и����忨�ۣ�����x�������ģ���ĳ������Ϊ���ģ���һ������ȷ���ƶ���
    }
    public void PlayerChoseOrGiveUpCard()
    {
        
        //Debug.Log(Enemy.isMouseAroundSomeEnemy);

        if (Input.GetMouseButtonDown(0) && cardAroundMouse != null && stage == Stage.NoChooseCard)//�������������������������ĳ�ſ���
        {

            //Debug.Log(CardClass.isExistCardChosen);
            
            //CardClass.isExistCardChosen = true;
            //Debug.Log("we will draw a line from card to mouse");
            cardChoosen = IsMouseAroundSomeCard.IsMouseOnSomeCard(playerLeftCard);
            Debug.Log("CARD is choosen" + cardChoosen.name);

            stage = Stage.ChosenCard;
            cardChoosen.thisCardStage = CardClass.ThisCardStage.Chosen;

        }
        if (Input.GetMouseButtonDown(1) && stage == Stage.ChosenCard)
        {
            stage = Stage.NoChooseCard;
        }
    }

    public void isPerformAnimation()
    {
        Debug.Log("Judging is Animation");
       //�ж��Ƿ��ɵ��˻�������ڽ��ж���
       if(Enemy.AllEnemyFinishAniamtion && player.IsPlayerFinishAnimation)
        {
            if(stage ==Stage.CardAniamtion)//�������
            {
                stage = Stage.NoChooseCard;
            }
        }

        
    }
    
    private void Initial()
    {
        stage = Stage.NoChooseCard;
        for (int i = 0; i < numberOfCardATurn; i++)
        {
            int randomInt = Random.Range(0, playerTotalCard.Count);
            //Debug.Log(randomInt);
            if (cardClips[i].GetComponentsInChildren<CardClass>().Length != 0)
            {
                //Debug.Log(cardClips[i].GetComponentsInChildren<CardClass>().Length);
                Destroy(cardClips[i].transform.GetChild(0).gameObject);
            }
            CardClass card = Instantiate(playerTotalCard[randomInt], cardClips[i].transform.position, cardClips[i].transform.rotation);
            card.transform.SetParent(cardClips[i].transform);//�ڿ��۴����ɵĿ��Ʋ���Ϊ���۵������壻//ע�Ᵽ֤�������Ƶ��������ڿ�������
            playerLeftCard.Add(card);
            card.holdPlayer = player;
                //Debug.Log(card.holdPlayer+player.name);
            
        }
    }
}