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
        if (ThisCard.thisCardStage == CardClass.ThisCardStage.Chosen)
        {
            //Debug.Log("AttackChosenByPlayer");
            EnemyAroundMouse = IsMouseAroundSomeEnemy.IsMouseAroundEnemy();//1ʵ��������ֻϣ���ӿ��Ʊ�ѡ�е�ѡ��ĳ������֮��ִ�иú���
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
        if (Input.GetMouseButtonDown(0) &&EnemyTobeUnderAttack!= null && EnemyTobeUnderAttack.enemyStage == Enemy.EnemyStage.ShowAttackPart && 
            ThisCard.thisCardStage == CardClass.ThisCardStage.Chosen && true)//this part true stand for undecided attack which part is choosen
        {                                                                                        //��������������Ұ������ڵ���ĳ��partʱ��������   
            //Debug.Log("CardAgreeToAttack");
            List<Transform> targetList = new List<Transform>();
            targetList.Add(EnemyTobeUnderAttack.transform);
            ThisCard.UseCauseEffect(targetList);
            
        }
        if (Input.GetMouseButtonDown(1))
        {
            ThisCard.thisCardStage = CardClass.ThisCardStage.cardNotAroundMouse;
            
        }
    }
    
}
