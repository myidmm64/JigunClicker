using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class EnemyManager : MonoBehaviour
{
    [SerializeField]
    private GameObject returnButton = null;
    [SerializeField]
    private List<EnemyMove> enemyMoves = new List<EnemyMove>();
    [SerializeField]
    private List<EnemyMove> bossMoves = new List<EnemyMove>();
    [SerializeField]
    private string[] bossName = null;
    [SerializeField]
    private string[] smallEnemyName = null;
    [SerializeField]
    private Text bossNameText = null;
    [SerializeField]
    private Text smallEnemyNameText = null;

    [SerializeField]
    private Image silhouetteImage = null;
    [SerializeField]
    private Sprite[] silhouetteSprite = null;
    public bool bossing { get; private set; }
    private int enemyNum = 0;
    private int bossNum = 0;
    [SerializeField]
    private int[] nextEnemyPrice = null;
    [SerializeField]
    private Text priceText = null;
    private void Start()
    {
        EnemysNumberSet();
        EnemySpawn();
        SilhouetteSet();
        Debug.Log(enemyMoves.Count);
        PriceTextUISet();
    }
    private void SilhouetteSet()
    {
        if (enemyNum < enemyMoves.Count-1)
            silhouetteImage.sprite = silhouetteSprite[enemyNum+1];
        else
            silhouetteImage.sprite = silhouetteSprite[enemyNum];
    }
    private void EnemysNumberSet()
    {
        enemyNum = GameManager.Instance.CurrentUser.enemyNum;
        bossNum = GameManager.Instance.CurrentUser.bossNum;
    }
    public void EnemySpawn()
    {
        Debug.Log(enemyNum);
        enemyMoves[enemyNum].gameObject.SetActive(true);
        NameSet();
    }
    public void NameSet()
    {
        bossNameText.text = string.Format(bossName[bossNum]);
        smallEnemyNameText.text = string.Format(smallEnemyName[enemyNum]);
    }
    public void EnemyDamage()
    {
        if (!bossing)
            enemyMoves[enemyNum].ClickAndDamaged();
        else
            bossMoves[bossNum].ClickAndDamaged();
    }
    public void BossComing()
    {
        bossing = true;
        enemyMoves[enemyNum].gameObject.SetActive(false);
        returnButton.gameObject.SetActive(true);
        bossMoves[bossNum].gameObject.SetActive(true);
        bossMoves[bossNum].ColorWhite();
        bossMoves[bossNum].transform.DOScale(new Vector3(2f, 2f, 1f), 3f);
        bossMoves[bossNum].HPSet();
        NameSet();
    }
    public void BossDown()
    {
        bossing = false;
        enemyMoves[enemyNum].gameObject.SetActive(true);
        enemyMoves[enemyNum].ColorWhite();
        returnButton.gameObject.SetActive(false);
        bossMoves[bossNum].gameObject.SetActive(false);
        bossMoves[bossNum].transform.DOScale(new Vector3(1f, 1f, 1f), 3f);
        enemyMoves[enemyNum].HPSet();
        NameSet();
    }
    public void NextBoss()
    {
        if (bossNum < bossMoves.Count - 1)
        {
            GameManager.Instance.CurrentUser.bossNum++;
            EnemysNumberSet();
        }
        NameSet();
        Debug.Log(bossNum);
    }
    public void NextEnemy()
    {
        if (enemyNum < enemyMoves.Count - 1 && nextEnemyPrice[enemyNum] < GameManager.Instance.CurrentUser.energy)
        {
            GameManager.Instance.CurrentUser.energy -= nextEnemyPrice[enemyNum];
            GameManager.Instance.CurrentUser.enemyNum++;
            EnemysNumberSet();
            SmallEnemyOnOff();
            NameSet();
            SilhouetteSet();
            PriceTextUISet();
            Debug.Log(enemyNum);
        }
    }
    private void PriceTextUISet()
    {
        priceText.text = string.Format("�� ���׷��̵�\n{0}��", nextEnemyPrice[enemyNum]);
    }
    private void SmallEnemyOnOff()
    {
        enemyMoves[enemyNum].gameObject.SetActive(true);
        enemyMoves[enemyNum - 1].gameObject.SetActive(false);
    }
}
