using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Revenger: MonoBehaviour, IBuff
{
    //ʹ��buffʱ����ʵ����buff���ٴ���setCharacter,�������buff����
    [Header("�ֶ����ò���")]
    public int increaseAttack = 2;
    private bool executed = false;                      // Start is called before the first frame update

    public void BuffEffect()
    {
        if (!executed) 
        { 
            FightManager.Instance.player_AttackValue += increaseAttack;
            executed = true;
        }
        Debug.Log("player attack increased to" + FightManager.Instance.player_AttackValue);
    }
}
