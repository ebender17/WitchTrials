using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIHUDManager : MonoBehaviour
{
    public Image mask;
    float originialSize;

    [SerializeField] private TextMeshProUGUI _scoreText = default;

    private void Start()
    {
        originialSize = mask.rectTransform.rect.width;
    }

    public void SetValue(float value)
    {
        mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originialSize * value);
    }

    public void SetScore(int scoreText)
    {
        _scoreText.SetText(scoreText.ToString());
    }
}
