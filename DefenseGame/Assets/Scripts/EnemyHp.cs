using System.Collections;
using UnityEngine;

public class EnemyHp : MonoBehaviour
{
    [SerializeField]
    private float maxHp;        // �ִ� ü��
    private float currentHp;    // ���� ü��
    private bool isDie = false; //���� ��� �����̸� isDie�� true�� ����
    private Enemy enemy;
    private SpriteRenderer spriteRenderer;

    public float MaxHp => maxHp;
    public float CurrentHp => currentHp;

    private void Awake()
    {
        currentHp = maxHp; //���� ü���� �ִ� ü�°� ���� ����
        enemy = GetComponent<Enemy>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(float damage)
    {
        //���� ���� ���°� ��� �����̸� �Ʒ��ڵ带 �������� �ʴ´�.��
        if (isDie == true) return;
        //���� ü���� damage��ŭ ����
        currentHp -= damage;

        StopCoroutine("HitAlphaAnimation");
        StartCoroutine("HitAlphaAnimation");

        //ü���� 0���� = �� �¸��� ���
        if(currentHp <= 0)
        {
            isDie = true;
            //�� ĳ���� ���
            enemy.OnDie(EnemyDestroyType.kill);
        }
    }

    private IEnumerator HitAlphaAnimation()
    {
        //���� ���� ������ color������ ����
        Color color = spriteRenderer.color;

        //���� ������ 40�۷� ����
        color.a = 0.4f;
        spriteRenderer.color = color;

        //0.05�� ���� ���
        yield return new WaitForSeconds(0.05f);

        //���� ������ 100�۷� ����
        color.a = 1.0f;
        spriteRenderer.color = color;
    }
}
