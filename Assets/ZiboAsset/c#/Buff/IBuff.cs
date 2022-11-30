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
    //buff的生成和执行是有两个阶段的所以要先设置参数
    public void BuffEffect();
}
