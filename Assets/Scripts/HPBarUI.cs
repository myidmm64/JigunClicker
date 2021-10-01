
using UnityEngine;
using UnityEngine.UI;

public class HPBarUI : MonoBehaviour
{
    [SerializeField]
    private Slider hpSlider = null;
    [SerializeField]
    private Slider timerSlider = null;
    [SerializeField]
    private GameObject[] smallEnemyUI = null;
    [SerializeField]
    private Transform smallEnemyPosition = null;
    [SerializeField]
    private Transform bossEnemyPosition = null;
    private bool isLastBoss = false;
    private bool isTimer = false;
    private float isTime = 0f;
    private void Start()
    {
        ResetHPSlider();
        if (GameManager.Instance.CurrentEnemyManager.bossing)
            ONBossUISet();
        else
            OffBossUISet();

        timerSlider.value = 1f;
        isTime = 10f;
    }
    private void Update()
    {
        if (isTimer)
        {
            isTime -= Time.deltaTime;
            TimerValueSet();
            if (isTime <= 0f) 
            {
                GameManager.Instance.CurrentEnemyManager.BossDown();
                OffBossUISet();
            }
        }
    }
    private void TimerValueSet()
    {
        timerSlider.value = isTime / 10f; // 수정좀여
    }
    public void IsLastBoss()
    {
        isLastBoss = true;
    }
    public void ONBossUISet()
    {
        if (isLastBoss)
            return;
        timerSlider.gameObject.SetActive(true);
        isTimer = true;
        isTime = 10f;
        for(int i =0; i< smallEnemyUI.Length; i++)
        {
            smallEnemyUI[i].SetActive(false);
        }
        hpSlider.transform.position = bossEnemyPosition.position;
        hpSlider.transform.localScale = new Vector3(1f, 1f, 1f);
    }
    public void OffBossUISet()
    {
        isTimer = false;
        timerSlider.value = 1f;
        timerSlider.gameObject.SetActive(false);
        for (int i = 0; i < smallEnemyUI.Length; i++)
        {
            smallEnemyUI[i].SetActive(true);
        }
        hpSlider.transform.position = smallEnemyPosition.position;
        hpSlider.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
    }
    public void HandleHP(float curHP, float maxHP)
    {
        hpSlider.value = curHP / maxHP;
    }
    public void ResetHPSlider()
    {
        hpSlider.value = 1f;
    }
}
