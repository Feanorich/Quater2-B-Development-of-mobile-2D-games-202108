using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DailyRewardController
{
    private DailyRewardView _dailyRewardView;
    private List<ContainerSlotRewardView> _slots;

    private bool _isGetReward;

    public DailyRewardController(DailyRewardView generateLevelView)
    {
        _dailyRewardView = generateLevelView;
    }

    public void RefreshView()
    {
        InitSlots();

        _dailyRewardView.StartCoroutine(RewardsStateUpdater());

        RefreshUi();
        SubscribeButtons();
    }

    private void InitSlots()
    {
        _slots = new List<ContainerSlotRewardView>();

        for (var i = 0; i < _dailyRewardView.Rewards.Count; i++)
        {
            var instanceSlot = GameObject.Instantiate(_dailyRewardView.ContainerSlotRewardView,
                _dailyRewardView.MountRootSlotsReward, false);

            _slots.Add(instanceSlot);
        }
    }

    private IEnumerator RewardsStateUpdater()
    {
        while (true)
        {
            RefreshRewardsState();
            yield return new WaitForSeconds(1);
        }
    }

    private void RefreshRewardsState()
    {
        _isGetReward = true;

        if (_dailyRewardView.TimeGetReward.HasValue)
        {
            var timeSpan = DateTime.UtcNow - _dailyRewardView.TimeGetReward.Value;

            if (timeSpan.TotalSeconds > _dailyRewardView.TimeDeadline)
            {
                _dailyRewardView.TimeGetReward = null;
                _dailyRewardView.CurrentSlotInActive = 0;
            }
            else if (timeSpan.TotalSeconds < _dailyRewardView.TimeCooldown)
            {
                _isGetReward = false;
            }
            else if (timeSpan.TotalSeconds >= _dailyRewardView.TimeCooldown)
            {
                _isGetReward = true;
            }
        }

        RefreshUi();
    }

    private float TimerNewRewardText(float deltaTime, string timeMessage = "timer:", float adjustment = 0)
    {
        var nextClaimTime = _dailyRewardView.TimeGetReward.Value.AddSeconds(deltaTime);
        var currentClaimCooldown = nextClaimTime - DateTime.UtcNow;
        var timeGetReward = $"{currentClaimCooldown.Days:D2}:{currentClaimCooldown.Hours:D2}:{currentClaimCooldown.Minutes:D2}:{currentClaimCooldown.Seconds:D2}";

        float fillValue = 1 - ((float)currentClaimCooldown.TotalSeconds / (deltaTime - adjustment));        

        _dailyRewardView.TimerNewReward.text = $"{timeMessage} {timeGetReward}";

        return fillValue;
    }
    private void RefreshUi()
    {
        float _fillAward = 1;
        float _deadDeadline = 0;

        _dailyRewardView.GetRewardButton.interactable = _isGetReward;

        if (_isGetReward)
        {
            _dailyRewardView.TimerNewReward.text = "Награда сегодня уже получена";

            if (_dailyRewardView.TimeGetReward != null)
            {
                _deadDeadline = TimerNewRewardText(_dailyRewardView.TimeDeadline,  "Награда будет доступна еще:", 
                    _dailyRewardView.TimeCooldown);
                Debug.Log($"dead {_deadDeadline}");
            }
        }
        else
        {
            if (_dailyRewardView.TimeGetReward != null)
            {
                _fillAward = TimerNewRewardText(_dailyRewardView.TimeCooldown, "Время до следующей награды:");
                Debug.Log($"fill {_fillAward}");
            }
        }

        for (var i = 0; i < _slots.Count; i++)
            _slots[i].SetData(_dailyRewardView.Rewards[i], i + 1, i == _dailyRewardView.CurrentSlotInActive, _fillAward, _deadDeadline);
    }

    private void SubscribeButtons()
    {
        _dailyRewardView.GetRewardButton.onClick.AddListener(ClaimReward);
        _dailyRewardView.ResetButton.onClick.AddListener(ResetTimer);
    }

    private void ClaimReward()
    {
        if (!_isGetReward)
            return;

        var reward = _dailyRewardView.Rewards[_dailyRewardView.CurrentSlotInActive];

        switch (reward.RewardType)
        {
            case RewardType.Wood:
                CurrencyView.Instance.AddWood(reward.CountCurrency);
                break;
            case RewardType.Diamond:
                CurrencyView.Instance.AddDiamond(reward.CountCurrency);
                break;
        }

        _dailyRewardView.TimeGetReward = DateTime.UtcNow;
        _dailyRewardView.CurrentSlotInActive = (_dailyRewardView.CurrentSlotInActive + 1) % _dailyRewardView.Rewards.Count;

        RefreshRewardsState();
    }

    private void ResetTimer()
    {
        PlayerPrefs.DeleteAll();
        CurrencyView.Instance.RefreshText();
    }
}
