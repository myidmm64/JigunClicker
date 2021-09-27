using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text energyText = null;
    [SerializeField]
    private Animator beakerAnimator = null;
    [SerializeField]
    private GameObject upgradePanelTemplate = null;
    [SerializeField]
    private EnergyText energyTextTemplate = null;
    [SerializeField]
    private Transform pool = null;
    [SerializeField]
    private Image upgradeBackGround = null;
    [SerializeField]
    private GameObject backGroundUpButton = null;
    [SerializeField]
    private GameObject backGroundDownButton = null;
    private float upAnchoredPositionY = 0f;
    private float downAnchoredPositionY = 0f;

    [SerializeField]
    private Image goBossButton = null;
    [SerializeField]
    private Image returnBossButton = null;
    [SerializeField]
    private RectTransform upRectTransform = null;
    [SerializeField]
    private RectTransform downRectTransform = null;


    private List<UpgradePanel> upgradePanels = new List<UpgradePanel>();

    private void Start()
    {

        UpdateEnergyPanel();
        CreatePanels();
        upAnchoredPositionY = upgradeBackGround.rectTransform.anchoredPosition.y;
        downAnchoredPositionY = upgradeBackGround.rectTransform.anchoredPosition.y - 300f;
    }
    private void CreatePanels()
    {
        GameObject newPanel = null;
        UpgradePanel newPanelComponenet = null;
        foreach (Soldier soldier in GameManager.Instance.CurrentUser.soldierList)
        {
            newPanel = Instantiate(upgradePanelTemplate, upgradePanelTemplate.transform.parent);
            newPanelComponenet = newPanel.GetComponent<UpgradePanel>();
            newPanelComponenet.SetValue(soldier);
            newPanel.SetActive(true);
            upgradePanels.Add(newPanelComponenet);
        }
    }
    public void OnClickBeaker()
    {
        //GameManager.Instance.CurrentUser.energy += GameManager.Instance.CurrentUser.ePc;
        UpdateEnergyPanel();
        beakerAnimator.Play("Click");
        EnergyText newText = null;
        if (pool.childCount > 0)
        {
            newText = pool.GetChild(0).GetComponent<EnergyText>();
        }
        else
        {
            newText = Instantiate(energyTextTemplate, energyTextTemplate.transform.parent);
        }
        newText.Show(Input.mousePosition);
    }
    public void UpdateEnergyPanel()
    {
        energyText.text = string.Format("{0} ¿ø", GameManager.Instance.CurrentUser.energy);
    }
    public void ScrollDown()
    {
        backGroundDownButton.SetActive(false);
        backGroundUpButton.SetActive(true);
        upgradeBackGround.rectTransform.DOAnchorPosY
            (downAnchoredPositionY, 0.5f);
        goBossButton.rectTransform.DOAnchorPosY
            (downRectTransform.anchoredPosition.y, 0.5f);
        returnBossButton.rectTransform.DOAnchorPosY
            (downRectTransform.anchoredPosition.y, 0.5f);
    }
    public void ScrollUp()
    {
        backGroundUpButton.SetActive(false);
        backGroundDownButton.SetActive(true);
        upgradeBackGround.rectTransform.DOAnchorPosY
            (upAnchoredPositionY, 0.5f);
        goBossButton.rectTransform.DOAnchorPosY
            (upRectTransform.anchoredPosition.y, 0.5f);
        returnBossButton.rectTransform.DOAnchorPosY
            (upRectTransform.anchoredPosition.y, 0.5f);
    } 
}
