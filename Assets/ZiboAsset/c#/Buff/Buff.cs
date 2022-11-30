using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff : MonoBehaviour
{
    
    //���ʵ�ʵ�buff���ڿ�ʼ�غϽ����غ�֮����£������ṩ����buffͼ��λ�õķ���
    [Header("�Զ���ȡ����")]
    //������ʹ�����buffʱ��Ҫʵ����һ��buffͼ�꣬���ø����buffһЩ����
    
    public bool beginAffect = false;
    private BattleGameMaster battleGameMaster;
    private int oldTurn = 0;

    public float speed = 20;
    private bool startMove = false;
    private Vector3 movePosition;
    private Transform buffTransform;
    [Header("�����ֶ����õı���")]
    public int startBuffTurn = 0;//
    public int endBuffTurn = 1;
    public int buffExecutedTurns = 1;
    // Start is called before the first frame update

    public void UseBuffEffectDefault(Character cha)//�ṩһ�����ýӿ�,ʹ��Ĭ�ϲ�������buff
    {
        
        SetActiveParameter(cha,0,1, 1);
    }
    public void SetActiveParameter(Character cha, int p_startBuffAfter,int p_executedTurns,int p_endBuffAfter)//��statBuffAfter���غϺ�ʼ����buff��ִ��buff����int p_executedTurns���غ�
                                                                                         //����endBuffTurnAfter���غϺ�ִ��buff��ʧ�������ݻ�ͼ��
    {                                                                                     //���������ڵ�һ�غ�ʹ��0��1��2����ִ�д����ڵ�һ�غ�����ִ�У�ִֻ��һ�غϣ�
                                                                                          //����һ�غϺ󣨵����غϣ�����buff��ע���ȸ��»غ���ִ�к�����
        gameObject.GetComponent<IBuff>().character = cha;
        startBuffTurn = battleGameMaster.currentTurn + p_startBuffAfter;//ִ��buff�����ûغ�
        endBuffTurn = battleGameMaster.currentTurn + p_endBuffAfter;//ע��˺������ڸûغϲ�ִ�и�buff���ݻ�ʵ������buffͼ��
        buffExecutedTurns = p_executedTurns;//����
        beginAffect = true;
    }
    public void GotoGradually(Vector3 position)
    {
        movePosition = position;
        startMove = true;
    }
    void Awake()
    {
        buffTransform = GameObject.Find("BuffTransform").transform;
        battleGameMaster =  GameObject.Find("BattleGameMaster").GetComponent<BattleGameMaster>();
    }

    // Update is called once per frame
    void Update()
    {   
        if (oldTurn != battleGameMaster.currentTurn && beginAffect)// ֻ��Ҫ�ڸ�turnִ��һ��,Ҳ���������һ֡�������turn�Ļ��Ͳ�ִ�У����ǵĻ�����oldturn��ִ��
        {
            oldTurn = battleGameMaster.currentTurn;
            if (battleGameMaster.currentTurn <= startBuffTurn + buffExecutedTurns-1 && battleGameMaster.currentTurn >= startBuffTurn)//����buffTurn
            {
                //�������Card���ϳ�Ϊһ��prefabs,����ֻ��battleGameMaster��ʼ��ʱ�ϳ����壨prefabs,add 4 script�ͽӿڣ���ð�ʵ����������̷�װ��ĳ���ű���,����ֻ��move card����
                gameObject.GetComponent<IBuff>().BuffEffect();
            }
            else if (battleGameMaster.currentTurn == endBuffTurn)
            {
                gameObject.GetComponent<IEndBuff>().EndBuff();
                buffTransform.GetComponent<buffIconManager>().RemoveBuff(transform);
            }
        }

        if (startMove)
        {
            float step = Time.deltaTime * speed;
            transform.position =   Vector2.MoveTowards(transform.position, movePosition, step);
            if(Vector2.Distance(transform.position,movePosition)< 0.01f)
            {
                Debug.Log("buff reach position");
                startMove = false;
            }
        }
    }
}
