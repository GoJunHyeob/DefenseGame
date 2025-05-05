using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectDetector : MonoBehaviour
{
    [SerializeField]
    private TowerSpawner towerSpawner;
    [SerializeField]
    private TowerDataViewer towerDataViewer;

    private Camera mainCamera;
    private Ray ray;
    private RaycastHit hit;
    private Transform hitTransform = null;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        //���콺�� UI�� �ӹ��� ���� ���� �Ʒ� �ڵ尡 ������� �ʵ��� ��
        if(EventSystem.current.IsPointerOverGameObject() == true)
        {
            return;
        }

        //���콺 ���� ��ư�� ��������
        if(Input.GetMouseButtonDown(0))
        { 
            //ī�޶� ��ġ���� ȭ���� ���콺 ��ġ�� �����ϴ� ���� ����
            ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            //������ �ε����� ������Ʈ�� �����ؼ� hit�� ����
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                hitTransform = hit.transform;

                //������ �ε��� ������Ʈ�� �±װ�"Tile"�̸�
                if (hit.transform.CompareTag("Tile"))
                {
                    towerSpawner.SpawnTower(hit.transform);
                }
                //Ÿ���� �����ϸ� �ش� Ÿ�� ������ ����ϴ� Ÿ�� ����â On
                else if(hit.transform.CompareTag("Tower"))
                {
                    towerDataViewer.OnPanel(hit.transform);
                }
            }
        }
        else if(Input.GetMouseButtonUp(0))
        {
            //���콺�� ������ �� ������ ������Ʈ�� ���ų� ������ ������Ʈ�� Ÿ���� �ƴϸ�
            if( hitTransform == null || hitTransform.CompareTag("Tower") == false)
            {
                //Ÿ�� ���� �г��� ��Ȱ��ȭ �Ѵ�
                towerDataViewer.OffPanel();
            }

            hitTransform = null;
        }
    }
}
