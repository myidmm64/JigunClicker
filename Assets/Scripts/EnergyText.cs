using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class EnergyText : MonoBehaviour
{
    [SerializeField]
    private Canvas canvas = null;
    [SerializeField]
    private Transform pool = null;
    private Text energyText = null;

    public void Show(Vector2 mousePosition)
    {
        energyText = GetComponent<Text>();
        energyText.text = string.Format("-{0}",GameManager.Instance.CurrentUser.ePc);

        transform.SetParent(canvas.transform);
        transform.position = Camera.main.ScreenToWorldPoint(mousePosition);
        transform.position = new Vector3(transform.position.x, transform.position.y, 0f);

        gameObject.SetActive(true);

        RectTransform rectTransform = GetComponent<RectTransform>();
        float targetPositionY = rectTransform.anchoredPosition.y + 50f;

        energyText.DOFade(0f, 0.5f).OnComplete(() => DeSpawn());
        rectTransform.DOAnchorPosY(targetPositionY,0.5f);
    }


    public void DeSpawn()
    {
        energyText.DOFade(1f, 0f);
        transform.SetParent(pool);
        gameObject.SetActive(false);
    }
}
