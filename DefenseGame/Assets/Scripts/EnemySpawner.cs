using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    //[SerializeField]
    //private GameObject enemyPrefab; // �� ������
    [SerializeField]
    private GameObject enemyHPSliderPrefab; // ���� ü���� ��Ÿ���� Slider UI ������
    [SerializeField]
    private Transform canvasTransform;     //UI�� ǥ���ϴ� Canvas ������Ʈ�� Transform
    //[SerializeField]
    //private float spawnTime; // �� ���� �ֱ�
    [SerializeField]
    private Transform[] wayPoints; //���� ���������� �̵� ���
    [SerializeField]
    private PlayerHp playerHp; // �÷��̾��� ü�� ������Ʈ
    [SerializeField]
    private PlayerGold playerGold; // �÷��̾��� ��� ������Ʈ
    private Wave currentWave; // ���� ���̺� ����
    private int currentEnemyCount; //���� ���̺꿡 �����ִ� �� ����(���̺� ���۽� max�� ����, �� ��� �� -1)
    private List<Enemy> enemyList; // ���� �ʿ� �����ϴ� ��� ���� ����

    //���� ������ ������ EnemySpawner���� �ϱ� ������ set�� �ʿ� ����.
    public List<Enemy> EnemyList => enemyList;
    //���� ���̺��� �����ִ� ��, �ִ� �� ����
    public int CurrentEnemyCount => currentEnemyCount;
    public int MaxEnemyCount => currentWave.maxEnemyCount;

    private void Awake()
    {
        //�� ����Ʈ �޸� �Ҵ�
        enemyList = new List<Enemy>();
        ////�� ���� �ڷ�ƾ �Լ� ȣ��
        //StartCoroutine("SpawnEnemy");
    }

    public void StartWave(Wave wave)
    {
        //�Ű������� �޾ƿ� ���̺� ���� ����
        currentWave = wave;
        //���� ���̺��� �ִ� �� ���ڸ� ����
        currentEnemyCount = currentWave.maxEnemyCount;
        //���� ���̺� ����
        StartCoroutine("SpawnEnemy");
    }

    private IEnumerator SpawnEnemy()
    {
        //���� ���̺꿡�� ������ �� ����
        int spawnEnemyCount = 0;

        //���� ���̺꿡�� �����Ǿ�� �ϴ� ���� ���ڸ�ŭ ���� �����ϰ� �ڷ�ƾ ����
        while(spawnEnemyCount < currentWave.maxEnemyCount)
        {
            //GameObject clone = Instantiate(enemyPrefab); //�� ������Ʈ ����
            //���̺꿡 �����ϴ� ���� ������ ���� ������ �� ������ ���� �����ϵ��� �����ϰ�, �� ������Ʈ ����
            int enemyIndex = Random.Range(0, currentWave.enemyPrefabs.Length);
            GameObject clone = Instantiate(currentWave.enemyPrefabs[enemyIndex]);
            Enemy enemy = clone.GetComponent<Enemy>();  //��� ������ ���� Enemy ������Ʈ

            enemy.Setup(this,wayPoints);// wayPoint ������ �Ű������� Setup() ȣ��
            enemyList.Add(enemy); // ����Ʈ�� ��� ������ �� ���� ����

            SpawnEnemyHpSlider(clone); // �� ü���� ��Ÿ���� SliderUI���� �� ����

            //���� ���̺꿡�� ������ ���� ���� +1
            spawnEnemyCount++;

            //���� ���̺��� SpawnTime ���
            yield return new WaitForSeconds(currentWave.spawnTime); // spawnTime �ð� ���� ���
        }
    }

    public void DestroyEnemy(EnemyDestroyType type, Enemy enemy, int gold)
    {
        //���� ��ǥ�������� �������� ��
        if(type == EnemyDestroyType.Arrive)
        {
            playerHp.TakeDamage(1);
        }
        //���� �÷��̾��� �߻�ü���� ������� ��
        else if(type == EnemyDestroyType.kill)
        {
            //���� ������ ���� ��� �� ��� ȹ��
            playerGold.CurrentGold += gold;
        }
        //���� ����� ������ ���� ���̺��� ���� �� ���� ����(UI ǥ�ÿ�)
        currentEnemyCount--;
        //����Ʈ���� ����ϴ� �� ���� ����
        enemyList.Remove(enemy);
        //�� ������Ʈ ����
        Destroy(enemy.gameObject);
    }

    private void SpawnEnemyHpSlider(GameObject enemy)
    {
        //�� ü���� ��Ÿ���� Slider UI����
        GameObject sliderClone = Instantiate(enemyHPSliderPrefab);
        // Slider.UI ������Ʈ�� parent("Canvas" ������Ʈ)�� �ڽ����� ����
        sliderClone.transform.SetParent(canvasTransform);
        //���� �������� �ٲ� ũ�⸦ �ٽ�(1,1,1)�� ����
        sliderClone.transform.localScale = Vector3.one;

        //slider ui�� �Ѿƴٴ� ����� �������� ����
        sliderClone.GetComponent<SlidePositionAutoSetter>().Setup(enemy.transform);
        //slider ui�� �ڽ��� ���� ������ ǥ���ϵ��� ����
        sliderClone.GetComponent<EnemyHpViewer>().Setup(enemy.GetComponent<EnemyHp>());
    }
}
