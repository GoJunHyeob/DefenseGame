using System.Collections;
using UnityEngine;

public class EnemyHp : MonoBehaviour
{
    [SerializeField]
    private float maxHp;        // 최대 체력
    private float currentHp;    // 현재 체력
    private bool isDie = false; //적이 사망 상태이면 isDie를 true로 설정
    private Enemy enemy;
    private SpriteRenderer spriteRenderer;

    public float MaxHp => maxHp;
    public float CurrentHp => currentHp;

    private void Awake()
    {
        currentHp = maxHp; //현재 체력을 최대 체력과 같게 설정
        enemy = GetComponent<Enemy>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(float damage)
    {
        //현재 적의 상태가 사망 상태이면 아래코드를 실행하지 않는다.릭
        if (isDie == true) return;
        //현재 체력을 damage만큼 감소
        currentHp -= damage;

        StopCoroutine("HitAlphaAnimation");
        StartCoroutine("HitAlphaAnimation");

        //체력이 0이하 = 적 태릭터 사망
        if(currentHp <= 0)
        {
            isDie = true;
            //적 캐릭터 사망
            enemy.OnDie(EnemyDestroyType.kill);
        }
    }

    private IEnumerator HitAlphaAnimation()
    {
        //현재 적의 색상을 color변수에 저장
        Color color = spriteRenderer.color;

        //적의 투명도를 40퍼로 설정
        color.a = 0.4f;
        spriteRenderer.color = color;

        //0.05초 동안 대기
        yield return new WaitForSeconds(0.05f);

        //적의 투명도를 100퍼로 설정
        color.a = 1.0f;
        spriteRenderer.color = color;
    }
}
