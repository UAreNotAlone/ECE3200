using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBuff 
{
    public Character character
    {
        set;
        get;
    }
    //buff�����ɺ�ִ�����������׶ε�����Ҫ�����ò���
    public void BuffEffect();
}
