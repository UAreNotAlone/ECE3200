using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TestClik : MonoBehaviour
{

    private Button btn_Start;//����һ��Button���͵ı���

    private void Start()
    {
        btn_Start = GameObject.Find("Button").GetComponent<Button>();//ͨ��Find�������ƻ������Ҫ��Button���
        //Debug.Log(GameObject.Find("Button").GetComponent<Button>() == null);
        btn_Start.onClick.AddListener(OnStartButtonClick);//��������¼�
    }
    /// <summary>
    /// �����֮����õķ���
    /// </summary>
    private void OnStartButtonClick()
    {
        Debug.Log("���Ǵ����");
    }

///��������������������������������
//��Ȩ����������ΪCSDN������ľľ�.����ԭ�����£���ѭCC 4.0 BY-SA��ȨЭ�飬ת���븽��ԭ�ĳ������Ӽ���������
//ԭ�����ӣ�https://blog.csdn.net/muziiii/article/details/118995029

   
}
