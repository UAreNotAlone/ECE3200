using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleAttackModeCard : MonoBehaviour
{
    [Header("�Զ���ȡ�ı���")]
    public IsMouseAroundSomeEnemy isMouseAroundSomeEnemy;
    public CardClass ThisCard;
    public Enemy EnemyTobeUnderAttack;
    public IAttackRange CheckAttackValid;
    public BattleGameMaster battleGameMaster;
    // Start is called before the first frame update
    void Start()
    {
        ThisCard = gameObject.GetComponent<CardClass>();
        battleGameMaster = GameObject.Find("BattleGameMaster").GetComponent<BattleGameMaster>();
        CheckAttackValid = this.gameObject.GetComponent<IAttackRange>();
    }

    // Update is called once per frame
    void Update()
    {
        Enemy EnemyAroundMouse = null;
        if (ThisCard.thisCardStage == CardClass.ThisCardStage.Chosen && EnemyTobeUnderAttack == null)
        {
            //Debug.Log("AttackChosenByPlayer");
            EnemyAroundMouse = IsMouseAroundSomeEnemy.IsMouseAroundEnemy();//1ʵ��������ֻϣ���ӿ��Ʊ�ѡ�е�ѡ��ĳ������֮��ִ�иú���,������ѡ���е���֮��ѡ�����borg partʱ����ִ��
            //�����ж�����ֻ�п��Ʊ�ѡ�У�ʵ��������һ�����⿨��Ӧ�ö�һ���׶�
            //Debug.Log("Enemy.isMouseAroundSomeEnemy" + Enemy.isMouseAroundSomeEnemy);
        }
        if (Input.GetMouseButtonDown(0) && ThisCard.thisCardStage == CardClass.ThisCardStage.Chosen && EnemyAroundMouse != null)//ѡ��ĳ������չ���ɹ�������
        {
            // Debug.Log("MouseAroundSomeEnemy");
            
            if (CheckAttackValid.isInAttackRange(EnemyAroundMouse.onMapPoint,battleGameMaster.player))
            {
                EnemyTobeUnderAttack = EnemyAroundMouse;//���ǽ��1�����һ�ַ�ʽ��
                                   //����ѡ�е��˿�ʼֻ��ע��ѡ�е��Ǹ����˶����ں�������꿿���Ŀ���
                EnemyTobeUnderAttack.AttackedWhichPart();
                //����չ����֫��׼��ӭ�ӹ���
            }
        }
        
        if ( EnemyTobeUnderAttack!= null && EnemyTobeUnderAttack.enemyStage == Enemy.EnemyStage.attackedPartChosen && 
            ThisCard.thisCardStage == CardClass.ThisCardStage.Chosen )//this part true stand for undecided attack which part is choosen
        {
            if (EnemyTobeUnderAttack.listCy.Count == 0 )//���˽��뱻Ϯ��״̬�����ֿ��ܣ�һ��û�����壬������������ĳ�����屻ѡ��     //ע��������б�����null
            { //��������������Ұ������ڵ���ĳ��partʱ��������   
              //Debug.Log("CardAgreeToAttack");
                List<Transform> targetList = new List<Transform>();
                targetList.Add(EnemyTobeUnderAttack.transform);
                ThisCard.UseCauseEffect(targetList);
            }
            else
            {
                List<Transform> targetList = new List<Transform>();
                targetList.Add(EnemyTobeUnderAttack.chosenCyberBorg.transform);
                ThisCard.UseCauseEffect(targetList);
            }
            EnemyTobeUnderAttack = null;
            
        }
        if (Input.GetMouseButtonDown(1))
        {
            ThisCard.thisCardStage = CardClass.ThisCardStage.cardNotAroundMouse;
            EnemyTobeUnderAttack = null;
            
        }
    }
    
}
