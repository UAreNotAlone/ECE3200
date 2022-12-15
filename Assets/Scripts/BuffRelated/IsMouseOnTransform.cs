using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class IsMouseOnTransform : MonoBehaviour
{
    public static bool IsMouseOnThisTransform(Transform trans,Func<Vector3,Vector3,bool> distance)
    {
        Vector3 mousePos = Input.mousePosition;//ע��˲����ȡ��ֵ����Ļ����
        Vector3 RealMousePos = Camera.main.ScreenToWorldPoint(mousePos);
        //Debug.Log(RealMousePos);//���������zͳͳΪ-10��()
        RealMousePos = new Vector3(RealMousePos.x, RealMousePos.y, trans.transform.position.z);

        return distance(RealMousePos, trans.position);
    }
    public static Transform IsMouseAroundSomeTransform( List<Transform> targets, Func<Vector3, Vector3, bool> distance)
    {
        
        for (int i = 0; i < targets.Count; i++)
        {

            if (IsMouseOnThisTransform(targets[i],distance))
            {

                //Debug.Log(Enemy.isMouseAroundSomeEnemy);
                return targets[i];
            }
        }

        return null;
    }
    public static bool Distance(Vector3 v1,Vector3 v2)
    {
        if (Vector3.Distance(v1, v2) < 10)
        {

            return true;
        }
        else
        {
            return false;
        }
    }

    
}
