using System.Collections;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    private long hP = 0;
    [SerializeField]
    private long maxHP = 0;
    [SerializeField]
    private long energyGive = 0;
    [SerializeField]
    private int diaGive = 0;
    [SerializeField]
    private bool isBoss = false;
    private SpriteRenderer spriteRenderer = null;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        hP = maxHP;
    }
    public void HPSet()
    {
        hP = maxHP;
        GameManager.Instance.barUI.HandleHP(hP, maxHP);
    }
    private void GiveRewards()
    {
        GameManager.Instance.CurrentUser.energy += energyGive;
        GameManager.Instance.CurrentUser.dia += diaGive;
        GameManager.Instance.UI.UpdateEnergyPanel();

    }
    public void ColorWhite()
    {
        if(spriteRenderer ??= spriteRenderer = GetComponent<SpriteRenderer>())
        spriteRenderer.color = Color.white;
    }
    private IEnumerator Damaged()
    {
            spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.color = Color.white;
    }
    public void ClickAndDamaged()
    {
            if (hP > 1)
            {
                hP -= GameManager.Instance.CurrentUser.ePc;
                GameManager.Instance.barUI.HandleHP(hP,maxHP);
                StartCoroutine(Damaged());
            }
            else
            {
            if (isBoss)
            {
                spriteRenderer.color = Color.white;
                GiveRewards();
                GameManager.Instance.CurrentEnemyManager.NextBoss();
                return;
            }
            spriteRenderer.color = Color.white;
            HPSet();
            GameManager.Instance.barUI.ResetHPSlider();
            GiveRewards();

        }
    }

}
