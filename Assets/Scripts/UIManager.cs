using System.Collections.Generic;
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
    private GameObject StatUpgradePanelTemplate = null;
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

    [SerializeField]
    private GameObject optionPanel = null;
    private bool optioning = false;
    [SerializeField]
    private GameObject AchievementsPanel = null;
    private bool aching = false;
    private List<UpgradePanel> upgradePanels = new List<UpgradePanel>();
    private List<StatUpUpgradePanel> statupgradePanels = new List<StatUpUpgradePanel>();
    private List<GameObject> upgradeOb = new List<GameObject>();
    private List<GameObject> statupOb = new List<GameObject>();

    [SerializeField]
    private GameObject GoStatUpPanelButton = null;
    [SerializeField]
    private GameObject GoFactoryPanelButton = null;
    private void Start()
    {

        UpdateEnergyPanel();
        CreatePanels();
        StatCreatePanels();
        upAnchoredPositionY = upgradeBackGround.rectTransform.anchoredPosition.y;
        downAnchoredPositionY = upgradeBackGround.rectTransform.anchoredPosition.y - 300f;
        GoFactoryPanel();
    }
    private void CreatePanels()
    {
        GameObject newPanel = null;
        UpgradePanel newPanelComponenet = null;
        foreach (Soldier soldier in GameManager.Instance.CurrentUser.soldierList)
        {
            newPanel = Instantiate(upgradePanelTemplate, upgradePanelTemplate.transform.parent);
            upgradeOb.Add(newPanel);
            newPanelComponenet = newPanel.GetComponent<UpgradePanel>();
            newPanelComponenet.SetValue(soldier);
            newPanel.SetActive(true);
            upgradePanels.Add(newPanelComponenet);
        }
    }
    private void StatCreatePanels()
    {
        GameObject newPanel = null;
        StatUpUpgradePanel newPanelComponenet = null;
        foreach (StatUp statUp in GameManager.Instance.CurrentUser.statUpList)
        {
            newPanel = Instantiate(StatUpgradePanelTemplate, StatUpgradePanelTemplate.transform.parent);
            statupOb.Add(newPanel);
            newPanelComponenet = newPanel.GetComponent<StatUpUpgradePanel>();
            newPanelComponenet.StatSetValue(statUp);
            newPanel.SetActive(true);
            statupgradePanels.Add(newPanelComponenet);
        }
    }
    public void OnClickBeaker()
    {
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
        energyText.text = string.Format("{0} ??", GameManager.Instance.CurrentUser.energy);
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
    public void OnOption()
    {
        if(!optioning)
        optionPanel.SetActive(true);
        optioning = true;
    }
    public void OffOption()
    {
        optionPanel.SetActive(false);
        optioning = false;
    }
    public void OnAchi()
    {
        if (!aching)
        {
            AchievementsPanel.SetActive(true);
            aching = true;
        }
    }
    public void OffAchi()
    {
        AchievementsPanel.SetActive(false);
        aching = false;
    }
    public void GoFactoryPanel()
    {
        GoFactoryPanelButton.SetActive(false);
        GoStatUpPanelButton.SetActive(true);
        for (int i = 0; i < GameManager.Instance.CurrentUser.statUpList.Count; i++)
        {
            statupOb[i].SetActive(false);
        }
        for (int i =0; i< GameManager.Instance.CurrentUser.soldierList.Count; i++)
        {
            upgradeOb[i].SetActive(true);
        }
    }
    public void GoStatUpPanel()
    {
        GoStatUpPanelButton.SetActive(false);
        GoFactoryPanelButton.SetActive(true);
        for (int i = 0; i < GameManager.Instance.CurrentUser.soldierList.Count; i++)
        {
            upgradeOb[i].SetActive(false);
        }
        for (int i = 0; i < GameManager.Instance.CurrentUser.statUpList.Count; i++)
        {
            statupOb[i].SetActive(true);
        }
    }
}
