using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Revenger: MonoBehaviour, IBuff
{
    public Character character
    {
        set { _character = value; }
        get { return _character; }
    }

    public Character _character = new Character();//ʹ��buffʱ����ʵ����buff���ٴ���setCharacter,�������buff����
    [Header("�ֶ����ò���")]
    public float increaseAttack = 2
        ;//
                                     // Start is called before the first frame update

    public void BuffEffect()
    {
        character.attackForce += increaseAttack;
    }
}
