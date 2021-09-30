using UnityEngine.UI;
using UnityEngine;

public class StatUpUpgradePanel : MonoBehaviour
{

    [SerializeField]
    private Text statupNameText = null;
    [SerializeField]
    private Text statUpPriceText = null;
    [SerializeField]
    private Text statUpAmountText = null;
    [SerializeField]
    private Button statUpPurchaseButton = null;
    [SerializeField]
    private Image statUpImage = null;
    [SerializeField]
    private Sprite[] statUpSprite;
    [SerializeField]
    private Text upStatText = null;
    private StatUp statUp = null;

    public void StatSetValue(StatUp statUp)
    {
        this.statUp = statUp;
        StatUpUpdateUI();
    }
    public void StatUpUpdateUI()
    {
        statupNameText.text = statUp.statUpName;
        statUpPriceText.text = string.Format("{0} ¿ø", statUp.price);
        statUpAmountText.text = string.Format("{0}", statUp.amount);
        statUpImage.sprite = statUpSprite[statUp.statUpNumber];
        upStatText.text = statUp.statUpString;
    }
    public void OnClickPurchase()
    {
        if (GameManager.Instance.CurrentUser.energy < statUp.price)
        {
            return;
        }

        GameManager.Instance.CurrentUser.energy -= statUp.price;
        statUp.price = (long)(statUp.price * 1.25f);
        statUp.amount++;
        StatUpUpdateUI();
        GameManager.Instance.UI.UpdateEnergyPanel();
    }
}
