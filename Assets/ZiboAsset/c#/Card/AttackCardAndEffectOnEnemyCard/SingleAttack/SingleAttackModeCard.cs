using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleAttackModeCard : MonoBehaviour
{
    [Header("自动获取的变量")]
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
            EnemyAroundMouse = IsMouseAroundSomeEnemy.IsMouseAroundEnemy();//1实际上我们只希望从卡牌被选中到选中某个敌人之间执行该函数
            //但是判断条件只有卡牌被选中，实际上这样一种特殊卡牌应该多一个阶段
            //Debug.Log("Enemy.isMouseAroundSomeEnemy" + Enemy.isMouseAroundSomeEnemy);
        }
        if (Input.GetMouseButtonDown(0) && ThisCard.thisCardStage == CardClass.ThisCardStage.Chosen && EnemyAroundMouse != null)//选中某个敌人展开可攻击部分
        {
            // Debug.Log("MouseAroundSomeEnemy");
            
            if (CheckAttackValid.isInAttackRange(EnemyAroundMouse.onMapPoint,battleGameMaster.player))
            {
                EnemyTobeUnderAttack = EnemyAroundMouse;//这是解决1问题的一种方式，
                                   //即从选中敌人开始只关注被选中的那个敌人而不在乎后续鼠标靠近的卡牌
                EnemyTobeUnderAttack.AttackedWhichPart();
                //敌人展开四肢，准备迎接攻击
            }



        }
        if (Input.GetMouseButtonDown(0) &&EnemyTobeUnderAttack!= null && EnemyTobeUnderAttack.enemyStage == Enemy.EnemyStage.ShowAttackPart && 
            ThisCard.thisCardStage == CardClass.ThisCardStage.Chosen && true)//this part true stand for undecided attack which part is choosen
        {                                                                                        //当点击鼠标左键，且把鼠标放在敌人某个part时攻击敌人   
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
