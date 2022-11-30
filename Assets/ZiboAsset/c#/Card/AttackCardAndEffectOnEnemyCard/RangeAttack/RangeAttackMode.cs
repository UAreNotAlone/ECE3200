using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttackMode : MonoBehaviour
{
    //ѡ�п��ƺ��ÿ�����˽����ж����Ƿ�����Ч��Χ�ڣ�������򷢶�Ч����������ƣ�����ֱ�ӻص�δѡ��״̬������ʾ��ң�//����ʾ���Թ����ķ�Χ
    // Start is called before the first frame update
    public CardClass ThisCard;
    public IAttackRange CheckAttackValid;
    public BattleGameMaster battleGameMaster;
    void Start()
    {
        ThisCard = gameObject.GetComponent<CardClass>();
        battleGameMaster = GameObject.Find("BattleGameMaster").GetComponent<BattleGameMaster>();
        CheckAttackValid = this.gameObject.GetComponent<IAttackRange>();//����ӿ�������һ����������ĳ����ͼ���Ƿ�����ҵĹ�����Χ��
    }

    // Update is called once per frame
    void Update()
    {
        if(ThisCard.thisCardStage == CardClass.ThisCardStage.Chosen)
        {
            List<Transform> targetList = new List<Transform>();
            foreach(Enemy enemy in battleGameMaster.enemies)
            {
                if (CheckAttackValid.isInAttackRange(enemy.onMapPoint, battleGameMaster.player))
                {
                    targetList.Add(enemy.transform);
                }
            }
            if(targetList.Count == 0)
            {
                ThisCard.thisCardStage = CardClass.ThisCardStage.cardNotAroundMouse;
                Debug.Log("There is no enemy in valid ranges");
            }
            else
            {
                gameObject.GetComponent<CardClass>().UseCauseEffect(targetList);//�����˼���ܺģ�����Ч��������״̬���ݻٿ���//targetList
            }
        }
    }
}
