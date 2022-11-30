using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsMouseOnTransform : MonoBehaviour
{
    public static bool IsMouseOnThisTransform(Transform trans)
    {
        Vector3 mousePos = Input.mousePosition;//ע��˲����ȡ��ֵ����Ļ����
        Vector3 RealMousePos = Camera.main.ScreenToWorldPoint(mousePos);
        //Debug.Log(RealMousePos);//���������zͳͳΪ-10��()
        RealMousePos = new Vector3(RealMousePos.x, RealMousePos.y, trans.transform.position.z);

        if (Vector3.Distance(trans.transform.position, RealMousePos) < 10)
        {

            return true;
        }
        else
        {
            return false;
        }
    }
}
