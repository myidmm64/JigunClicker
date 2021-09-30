using UnityEngine.UI;
using UnityEngine;

public class UpgradePanel : MonoBehaviour
{
    [SerializeField]
    private Text soldierNameText = null;
    [SerializeField]
    private Text priceText = null;
    [SerializeField]
    private Text amountText = null;
    [SerializeField]
    private Text epcText = null;
    [SerializeField]
    private Button purchaseButton = null;
    [SerializeField]
    private Image soldierImage = null;
    [SerializeField]
    private Sprite[] soldierSprite;
    [SerializeField]
    private Text UserNameText = null;
    
    private Soldier soldier = null;
    private void Start()
    {
        UserNameText.text = string.Format(GameManager.Instance.CurrentUser.userName);
    }
    public void SetValue(Soldier soldier)
    {
        this.soldier = soldier;
        UpdateUI();
    }
    public void UpdateUI()
    {
        soldierNameText.text = soldier.soldierName;
        priceText.text = string.Format("{0} ø¯", soldier.price);
        amountText.text = string.Format("{0}", soldier.amount);
        soldierImage.sprite = soldierSprite[soldier.soldierNumber];
        if(soldier.amount > 0)
            epcText.text = string.Format("{0} / √ " ,(long)(soldier.ePs + ((soldier.ePs * 0.7f) * (soldier.amount - 1))));
    }   
    public void OnClickPurchase()
    {
        if(GameManager.Instance.CurrentUser.energy < soldier.price)
        {
            return;
        }

        GameManager.Instance.CurrentUser.energy -= soldier.price;
        soldier.price = (long)(soldier.purePrice * Mathf.Pow(1.07f, soldier.amount+1));
        soldier.amount++;
        UpdateUI();
        GameManager.Instance.UI.UpdateEnergyPanel();
    }
}
