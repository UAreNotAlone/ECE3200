using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectHarmBuff : MonoBehaviour,IBuff
{


    //��д���˿���ʱ��Ӧ����ʵ����buff�����������˵Ķ���ͱ��������������buff�Ĳ������ﵽ����buff��Ч����,���и���buffͼ��

    //����ִ��˳��ʵ����buff�����÷��˶��󣬿���buff��buff���÷���Ч����buff��ֹ����Ч��
    public bool beginCheckUsed = false;
    public void BuffEffect()//��buff�б��������ӳټ��غϣ�����
    {
        FightManager.Instance.isPlayerReflectHarm = true;
        beginCheckUsed = true;
    }
    public void Update()
    {
        if (beginCheckUsed)
        {
            if(FightManager.Instance.isPlayerReflectHarm == false)
            {
                Debug.Log("reflect harm");
                gameObject.GetComponent<Buff>().buffTransform.GetComponent<buffIconManager>().RemoveBuff(transform);
            }
        }
    }

}
