using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectHarmBuff : MonoBehaviour,IBuff
{
    public Character character
    {
        set { _character = value; }
        get { return _character; }
    }

    public Character _character;
    public float percentage = 1f;
    public void setCharacter(Character p_cha, float p_percentage = 1)//��д���˿���ʱ��Ӧ����ʵ����buff�����������˵Ķ���ͱ��������������buff�Ĳ������ﵽ����buff��Ч����,���и���buffͼ��
    {
        character = p_cha;                                           //����ִ��˳��ʵ����buff�����÷��˶��󣬿���buff��buff���÷���Ч����buff��ֹ����Ч��
        percentage = p_percentage;//
    }
    public void BuffEffect()//��buff�б��������ӳټ��غϣ�����
    {
        character.isReflectHarmOnce = true;
        character.reflectProportion = percentage;
    }
    
}
