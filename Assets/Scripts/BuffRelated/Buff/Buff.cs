using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class Buff : MonoBehaviour
{

    //���ʵ�ʵ�buff���ڿ�ʼ�غϽ����غ�֮����£������ṩ����buffͼ��λ�õķ���
    [Header("�Զ���ȡ����")]
    //������ʹ�����buffʱ��Ҫʵ����һ��buffͼ�꣬���ø����buffһЩ����


    public bool beginAffect = false;

    private int oldTurn = 0;

    
    private bool startMove = false;
    private Vector3 movePosition;
    public Transform buffTransform;
    public GameObject textPrefab;
    public GameObject shownText;
    public Transform canvas;
    [Header("�ֶ����ñ���")]
    public float speed = 100;
    public string description;
    public enum BuffType
    {
        headBuff,
        bodyBuff,
        legsBuff,
        armsBuff
    }
    [Header("�����ֶ����õı���")]
    public int startBuffTurn = 0;//
    public int endBuffTurn = 1;
    public int buffExecutedTurns = 1;

    public bool isDebuff = false;
    public BuffType buffType;

    // Start is called before the first frame update

   
    public void SetActiveParameter(int p_startBuffAfter,int p_executedTurns,int p_endBuffAfter)//��statBuffAfter���غϺ�ʼ����buff��ִ��buff����int p_executedTurns���غ�
                                                                                         //����endBuffTurnAfter���غϺ�ִ��buff��ʧ�������ݻ�ͼ��
    {                                                                                     //���������ڵ�һ�غ�ʹ��0��1��2����ִ�д����ڵ�һ�غ�����ִ�У�ִֻ��һ�غϣ�
                                                                                          //����һ�غϺ󣨵����غϣ�����buff��ע���ȸ��»غ���ִ�к�����
        
        startBuffTurn =FightManager.Instance.currentTurn + p_startBuffAfter;//ִ��buff�����ûغ�
        endBuffTurn = FightManager.Instance.currentTurn + p_endBuffAfter;//ע��˺������ڸûغϲ�ִ�и�buff���ݻ�ʵ������buffͼ��
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
        textPrefab =(GameObject) Resources.Load("Prefabs/textPrefab");
        canvas = GameObject.Find("Canvas").transform;

        
    }

    public bool DistanceCriterion(Vector3 v1, Vector3 v2)//�ж�����Ƿ���buffͼ������ı�׼
    {
        if(Mathf.Abs(v1.y -v2.y) < 10 && Mathf.Abs(v1.x - v2.x) < 10)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    // Update is called once per frame
    void Update()
    {   
        if (oldTurn != FightManager.Instance.currentTurn && beginAffect)// ֻ��Ҫ�ڸ�turnִ��һ��,Ҳ���������һ֡�������turn�Ļ��Ͳ�ִ�У����ǵĻ�����oldturn��ִ��
        {
            oldTurn = FightManager.Instance.currentTurn;
            if (FightManager.Instance.currentTurn <= startBuffTurn + buffExecutedTurns-1 && FightManager.Instance.currentTurn >= startBuffTurn)//����buffTurn
            {
                //�������Card���ϳ�Ϊһ��prefabs,����ֻ��battleGameMaster��ʼ��ʱ�ϳ����壨prefabs,add 4 script�ͽӿڣ���ð�ʵ����������̷�װ��ĳ���ű���,����ֻ��move card����
                gameObject.GetComponent<IBuff>().BuffEffect();
            }
            else if (FightManager.Instance.currentTurn == endBuffTurn)
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

        if (IsMouseOnTransform.IsMouseOnThisTransform(transform, DistanceCriterion))
        {
            if (shownText == null)
            {
                Debug.Log("Mouse On this position");
                RectTransform rec = Instantiate(textPrefab).GetComponent<RectTransform>();
                rec.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(-1, 0, 0) * 10);
                //Debug.Log(transform.position);
                rec.GetChild(1).GetComponent<Text>().text = description;
                Transform bg = rec.GetChild(0);
                bg.localScale = new Vector3(bg.localScale.x, bg.localScale.y * (Mathf.Floor(description.Length / 20) + 1), bg.localScale.z);
                //Debug.Log(rec.gameObject.name);
                rec.SetParent(canvas);
                shownText = rec.gameObject;
            }
        }
        else
        {
            if (shownText != null)
            {
                Destroy(shownText);
                Debug.Log("Destroy Description");
                shownText = null;
            }
        }
    }
}
