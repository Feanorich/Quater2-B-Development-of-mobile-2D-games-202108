using UnityEngine;

public class InstallView : MonoBehaviour
{
    [SerializeField]
    private DailyRewardView _dailyRewardView;

    [SerializeField]
    private DailyRewardView _weeklyRewardView;

    private DailyRewardController _dailyRewardController;
    private DailyRewardController _weeklyRewardController;

    private void Awake()
    {
        _dailyRewardController = new DailyRewardController(_dailyRewardView);
        _weeklyRewardController = new DailyRewardController(_weeklyRewardView);
        if (_dailyRewardView.RewardID == _weeklyRewardView.RewardID)
        {
            Debug.LogError("конфликт одинаковых RewardID");
            return;
        }
    }

    private void Start()
    {
        _dailyRewardController.RefreshView();
        _weeklyRewardController.RefreshView();
    }
}
