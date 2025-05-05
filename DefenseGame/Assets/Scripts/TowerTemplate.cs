using UnityEditor.Build;
using UnityEngine;

[CreateAssetMenu(fileName = "TowerTemplate", menuName = "Scriptable Objects/TowerTemplate")]
public class TowerTemplate : ScriptableObject
{
    public GameObject towerPrefab; //Ÿ�� ������ ���� ������
    public GameObject followTowerPrefab;  // �ӽ� Ÿ�� ������
    public Weapon[] weapon;

    [System.Serializable]
    public struct Weapon
    {
        public Sprite sprite; //�������� Ÿ�� �̹���(UI)
        public float damage; //���ݷ�
        public float slow; //���� �ۼ�Ʈ (0.2 = 20%)
        public float buff; //���� ������ (0.2 = +20%)
        public float rate; // ���� �ӵ�
        public float range; //���� ����
        public int cost; //�ʿ� ���
        public int sell; // Ÿ�� �Ǹ� �� ȹ�� ���
    }
}
