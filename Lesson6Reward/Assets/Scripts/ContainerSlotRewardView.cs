using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ContainerSlotRewardView : MonoBehaviour
{
    [SerializeField]
    private Image _selectedBackground;

    [SerializeField]
    private Image _deadBackground;

    [SerializeField]
    private Image _iconCurrency;

    [SerializeField]
    private TMP_Text _textDay;

    [SerializeField]
    private TMP_Text _countReward;

    public void SetData(Reward reward, int countDay, bool isSelected, float fill = 1, float dead = 0)
    {
        _iconCurrency.sprite = reward.Sprite;
        _textDay.text = $"Day {countDay}";
        _countReward.text = reward.CountCurrency.ToString();
        _selectedBackground.gameObject.SetActive(isSelected);
        _deadBackground.gameObject.SetActive(isSelected);
        if (isSelected)
        {
            _selectedBackground.fillAmount = fill;
            _deadBackground.fillAmount = dead;
        }
    }
}
