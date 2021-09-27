using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    private long hP = 0;
    [SerializeField]
    private long maxHP = 0;
    [SerializeField]
    private long energyGive = 0;
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
    private void GiveEnergy()
    {
        GameManager.Instance.CurrentUser.energy += energyGive;
        GameManager.Instance.UI.UpdateEnergyPanel();

    }
    public void ColorWhite()
    {
        if(spriteRenderer ??= spriteRenderer = GetComponent<SpriteRenderer>())
        spriteRenderer.color = Color.white;
    }
    private IEnumerator Damaged()
    {
        for(int i =0; i<3; i++)
        {
            spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(0.1f);
        }
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

            spriteRenderer.color = Color.white;
            HPSet();
            GameManager.Instance.barUI.ResetHPSlider();
            GiveEnergy();

        }
    }

}
