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
    }
}
