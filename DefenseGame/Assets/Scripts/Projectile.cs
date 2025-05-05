using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Movement2D movement2D;
    private Transform target;
    private float damage;

    public void SetUp(Transform target, float damage)
    {
        movement2D = GetComponent<Movement2D>();
        this.target = target;  //Ÿ���� �������� target
        this.damage = damage;
    }

    private void Update()
    {
        if(target != null) //target�� �����ϸ�
        {
            //�߻�ü�� target�� ��ġ�� �̵�
            Vector3 direction = (target.position - transform.position).normalized;
            movement2D.MoveTo(direction);
        }
        else //���� ������ target�� �������
        {
            //�߻�ü ������Ʈ ����
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy"))  //���� �ƴ� ���� �ε�����
            return;
        if (collision.transform != target)  //���� target�� ���� �ƴҶ�
            return;

        collision.GetComponent<EnemyHp>().TakeDamage(damage); //�� ��� �Լ� ȣ��
        Destroy(gameObject); // �߻�ü ������Ʈ ����
    }
}
