using System.Collections;
using System.Collections.Generic;
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
    private void Start()
    {
        ResetHPSlider();
        if (GameManager.Instance.CurrentEnemyManager.bossing)
            ONBossUISet();
        else
            OffBossUISet();
        
    }

    public void ONBossUISet()
    {
        timerSlider.gameObject.SetActive(true);
        for(int i =0; i< smallEnemyUI.Length; i++)
        {
            smallEnemyUI[i].SetActive(false);
        }
        hpSlider.transform.position = bossEnemyPosition.position;
        hpSlider.transform.localScale = new Vector3(1f, 1f, 1f);
    }
    public void OffBossUISet()
    {
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
