using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alcoholic : MonoBehaviour,IBuff
{
    

    //ʹ��buffʱ����ʵ����buff���ٴ���,�������buff����
    
    
    //
    // Start is called before the first frame update

    public void BuffEffect()
    {
        //�������غ�
        //
        StartCoroutine(nameof(EndTurn));
        
    }
    public IEnumerator EndTurn()
    {
        yield return new WaitForSeconds(3f);
        FightManager.Instance.ChangeFightType(FightType.Enemy);
    }
    
}
