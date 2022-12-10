using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ModeMoveCard : MonoBehaviour//����ͬ���Ʊ�ѡ��֮������
{
    [Header("�Զ���ȡ�ı���")]
    public CardClass ThisCard;
    public Transform selectedMapPoint;
    public GetOverlayContainer GotOverlayContainer;
    public Player player;
    public LayerMask overlay;
    private MoveRange _moveRange;
    // Start is called before the first frame update
    void Start()
    {
        overlay = 1<<LayerMask.NameToLayer("Overlay");
        _moveRange = gameObject.GetComponent<MoveRange>();
        player = gameObject.GetComponent<CardClass>().holdPlayer;
        ThisCard = gameObject.GetComponent<CardClass>();
        GotOverlayContainer = GameObject.Find("GetContainer").GetComponent<GetOverlayContainer>();
    }
    private void OnEnable()
    {
        
    }
    // Update is called once per frame
    void Update()
    {

        if (ThisCard.thisCardStage == CardClass.ThisCardStage.Chosen)   //�����ѡ���ƶ���ʱ����̽�����λ���Ƿ���ĳ����ͼ�ڵ���
        {
            
            //Debug.Log("AttackChosenByPlayer");
            selectedMapPoint = IsMouseOnSomeOverlay();

            
            //Debug.Log("selected null is" + selectedMapPoint == null);
            //unity ������transform��Ϊnull �����ݲ�����
            //Debug.Log("Enemy.isMouseAroundSomeEnemy" + Enemy.isMouseAroundSomeEnemy);
        }
        if (Input.GetMouseButtonDown(0) && ThisCard.thisCardStage == CardClass.ThisCardStage.Chosen && 
            _moveRange.IsCheckValidMove(selectedMapPoint,player))//ע��Ҫ��֤�ӵڶ��׶ε���3�׶Σ�Ҫ���ôӵ�һ�׶ε��ڶ��׶�
        {
            //������Ѿ�ѡ�����ſ�����£������ĳ����ͼ�ڵ㰴�����
            Debug.Log("use moveEffect");
            List<Transform> targetList = new List<Transform>();
            targetList.Add(selectedMapPoint);
            ThisCard.UseCauseEffect(targetList);//
        }
        if (Input.GetMouseButtonDown(1))
        {
            ThisCard.thisCardStage = CardClass.ThisCardStage.cardNotAroundMouse;
        }
    }

    public Transform IsMouseOnSomeOverlay() //���߼������Ƿ���ĳ��ovelay�ϲ�����transforms
    {


        var hit = GetFocusOnTile();
        if (hit.HasValue)
        {
            //Debug.Log(hit.Value.collider.transform.position);
            return hit.Value.collider.gameObject.transform;
            
        }
        else
        {
            return null;
        }
        
    }
    
    
    public RaycastHit2D? GetFocusOnTile()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2d = new Vector2(mousePos.x, mousePos.y);

        Debug.Log("begin to get mouse on overlay");
        RaycastHit2D[] hits = Physics2D.RaycastAll(mousePos2d, Vector2.zero,Mathf.Infinity,
            overlay);//��������ô��ֹ����������ѡ�е�
        Debug.Log(hits.Length);
        if (hits.Length > 0)
        {
            Debug.Log("The RayCast success");
            return hits.OrderByDescending(i => i.collider.transform.position.z).First();
        }

        return null;
    }


}
