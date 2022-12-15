using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blind : MonoBehaviour,IBuff
{

    public bool beginCheckUsed = false;
    public void BuffEffect()//��buff�б��������ӳټ��غϣ�����
    {
        FightManager.Instance.isCannotHurtOnce = true;
        beginCheckUsed = true;
    }
    public void Update()
    {
        if (beginCheckUsed)
        {
            if (FightManager.Instance.isCannotHurtOnce == false)
            {
                Debug.Log("used cannot hurt once");
                gameObject.GetComponent<Buff>().buffTransform.GetComponent<buffIconManager>().RemoveBuff(transform);
            }
        }
    }
}
