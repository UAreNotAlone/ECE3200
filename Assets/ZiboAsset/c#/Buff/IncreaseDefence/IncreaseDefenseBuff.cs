using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseDefenseBuff :MonoBehaviour, IBuff
{
    public Character character
    {
        set { _character = value; }
        get { return _character; }
    }

    public Character _character = new Character();//��������ʱ����ʵ����buff���ٴ���setCharacter,�������buff����
    [Header("�ֶ����ò���")]
    public float increaseDefense = 3;//
                                      // Start is called before the first frame update

    public void BuffEffect()
    {
        character.setDefenceAbility(character.defenceAbility + increaseDefense);
    }
}
