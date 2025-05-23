using UnityEngine;
using TMPro;

public class TextTMPViewer : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textPlayerHp; //Text - TextMeshPro UI [플레이어의 체력]
    [SerializeField]
    private TextMeshProUGUI textPlayerGold; //Text - TextMeshPro UI [플레이어의 골드]
    [SerializeField]
    private TextMeshProUGUI textWave; //Text - TextMeshPro UI [현재 웨이브 / 총 웨이브]
    [SerializeField]
    private TextMeshProUGUI textEnemyCount; // Text - TextMeshPro UI [현재 적 숫자 / 최대 적 숫자]
    [SerializeField]
    private PlayerHp playerHp; // 플레이어의 체력 정보
    [SerializeField]
    private PlayerGold playerGold; // 플레이어의 골드 정보
    [SerializeField]
    private WaveSystem waveSystem; // 웨이브 정보
    [SerializeField]
    private EnemySpawner enemySpawner; // 적 정보

    private void Update()
    {
        textPlayerHp.text = playerHp.CurrentHp + "/" + playerHp.MaxHp;
        textPlayerGold.text = playerGold.CurrentGold.ToString();
        textWave.text = waveSystem.CurrentWave + "/" + waveSystem.MaxWave;
        textEnemyCount.text = enemySpawner.CurrentEnemyCount + "/" + enemySpawner.MaxEnemyCount;
    }
}
