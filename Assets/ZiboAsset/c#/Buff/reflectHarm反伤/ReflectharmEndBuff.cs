using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectharmEndBuff : MonoBehaviour,IEndBuff
{
    //ע��˽ű�������ڷ��˿��Ʊ��ݻٺ󣬱�buff������ã����ý���ǰ���������Ч���ͱ�����ԭΪfalse��1�� 
    //ʵ���϶��ڣ�����once��˵������ǲ���Ҫ�ģ����Ƕ��ڱ��غ�������˵������Ǳ���ģ�
    //��ʹ��ʱ�ٻ�ȡͷbuff��character����
    public void EndBuff()
    {
        Character cha = gameObject.GetComponent<ReflectHarmBuff>().character;//��Ե�buff��ͬһ������
        cha.isReflectHarmOnce = false;
        cha.reflectProportion = 1f;//���˱���Ҫ��Ҫ�����أ��������Ҫ���Ժ�ע��һ�¡�
    }

}
