using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blind : MonoBehaviour,IBuff
{
    public Character character
    {
        set { _character = value; }
        get { return _character; }
    }

    public Character _character = new Character();//ʹ��buffʱ����ʵ����buff���ٴ���setCharacter,�������buff����
    
        //
         // Start is called before the first frame update

    public void BuffEffect()
    {
        character.isCannotHurtOnce = true;
    }
}
