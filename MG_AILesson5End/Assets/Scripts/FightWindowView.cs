using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FightWindowView : MonoBehaviour
{
    [SerializeField] private TMP_Text _countMoneyText;
    [SerializeField] private TMP_Text _countHealthText;
    [SerializeField] private TMP_Text _countPowerText;
    [SerializeField] private TMP_Text _playerMeleePowerText;
    [SerializeField] private TMP_Text _playerRangePowerText;
    [SerializeField] private TMP_Text _countCrimeText;

    [SerializeField] private TMP_Text _countPowerEnemyText;
    [SerializeField] private TMP_Text _enemyMeleePowerText;
    [SerializeField] private TMP_Text _enemyRangePowerText;

    [SerializeField] private Button _addMoneyButton;
    [SerializeField] private Button _minusMoneyButton;
    [SerializeField] private Button _addHealthButton;
    [SerializeField] private Button _minusHealthButton;
    [SerializeField] private Button _addPowerButton;
    [SerializeField] private Button _minusPowerButton;
    [SerializeField] private Button _addCrimeButton;
    [SerializeField] private Button _minusCrimeButton;

    [SerializeField] private Button _meleeButton;
    [SerializeField] private Button _rangeButton;
    [SerializeField] private Button _passButton;

    private Enemy _enemy;

    private Money _money;
    private Health _health;
    private Power _power;
    private Crime _crime;

    private int _allCountMoneyPlayer;
    private int _allCountHealthPlayer;
    private int _allCountPowerPlayer;
    private int _allCountCrimePlayer;

    private void Start()
    {
        _enemy = new Enemy("Flappy");

        _money = new Money(nameof(Money));
        _money.Attach(_enemy);

        _health = new Health(nameof(Health));
        _health.Attach(_enemy);

        _power = new Power(nameof(Power));
        _power.Attach(_enemy);

        _crime = new Crime(nameof(Crime));
        _crime.Attach(_enemy);

        _addMoneyButton.onClick.AddListener(() => ChangeMoney(true));
        _minusMoneyButton.onClick.AddListener(() => ChangeMoney(false));

        _addHealthButton.onClick.AddListener(() => ChangeHealth(true));
        _minusHealthButton.onClick.AddListener(() => ChangeHealth(false));

        _addPowerButton.onClick.AddListener(() => ChangePower(true));
        _minusPowerButton.onClick.AddListener(() => ChangePower(false));

        _addCrimeButton.onClick.AddListener(() => ChangeCrime(true));
        _minusCrimeButton.onClick.AddListener(() => ChangeCrime(false));

        _meleeButton.onClick.AddListener(Melee);
        _rangeButton.onClick.AddListener(Range);

        _passButton.onClick.AddListener(Pass);
        _passButton.gameObject.SetActive(_crime.Peaceful);
    }

    private void OnDestroy()
    {
        _addMoneyButton.onClick.RemoveAllListeners();
        _minusMoneyButton.onClick.RemoveAllListeners();

        _addHealthButton.onClick.RemoveAllListeners();
        _minusHealthButton.onClick.RemoveAllListeners();

        _addPowerButton.onClick.RemoveAllListeners();
        _minusPowerButton.onClick.RemoveAllListeners();

        _addCrimeButton.onClick.RemoveAllListeners();
        _minusCrimeButton.onClick.RemoveAllListeners();

        _meleeButton.onClick.RemoveAllListeners();
        _passButton.onClick.RemoveAllListeners();

        _money.Detach(_enemy);
        _health.Detach(_enemy);
        _power.Detach(_enemy);
        _crime.Detach(_enemy);
    }

    private int PlayerMeleePower()
    {
        return _allCountPowerPlayer + _allCountHealthPlayer;
    }

    private int PlayerRangePower()
    {
        return _allCountPowerPlayer + _allCountMoneyPlayer;
    }

    private void Range()
    {
        Debug.Log(PlayerRangePower() >= _enemy.Range ? "Win skirmish" : "Lose skirmish");
    }

    private void Melee()
    {
        Debug.Log(PlayerMeleePower() >= _enemy.Melee ? "Win melee" : "Lose melee");
    }

    private void Fight()
    {
        Debug.Log(_allCountPowerPlayer >= _enemy.Power ? "Win" : "Lose");
    }

    private void Pass()
    {
        Debug.Log("Passed by");
    }

    private void ChangePower(bool isAddCount)
    {
        ChangeStats(isAddCount, ref _allCountPowerPlayer, DataType.Power);        
    }

    private void ChangeHealth(bool isAddCount)
    {
        ChangeStats(isAddCount, ref _allCountHealthPlayer, DataType.Health);
    }

    private void ChangeMoney(bool isAddCount)
    {
        ChangeStats(isAddCount, ref _allCountMoneyPlayer, DataType.Money);
    }

    private void ChangeCrime(bool isAddCount)
    {
        ChangeStats(isAddCount, ref _allCountCrimePlayer, DataType.Crime);
    }

    private void ChangeStats(bool isAddCount, ref int _countPlayer, DataType _dataType)
    {
        if (isAddCount)
            _countPlayer++;
        else
            _countPlayer--;       

        ChangeDataWindow(ref _countPlayer, _dataType);
    }

    private void ChangeDataWindow(ref int countChangeData, DataType dataType)
    {
        switch (dataType)
        {
            case DataType.Money:
                _money.CountMoney = countChangeData;
                countChangeData = _money.CountMoney;
                _countMoneyText.text = $"Player {dataType}: {countChangeData}";

                break;

            case DataType.Health:
                _health.CountHealth = countChangeData;
                countChangeData = _health.CountHealth;
                _countHealthText.text = $"Player {dataType}: {countChangeData}";

                break;

            case DataType.Power:
                _power.CountPower = countChangeData;
                countChangeData = _power.CountPower;
                _countPowerText.text = $"Player {dataType}: {countChangeData}";
                
                break;

            case DataType.Crime:
                _crime.CountCrime = countChangeData;
                countChangeData = _crime.CountCrime;
                _countCrimeText.text = $"Player {dataType}: {countChangeData}";
                
                break;
        }

        _playerMeleePowerText.text = $"Player melee: {PlayerMeleePower()}";
        _playerRangePowerText.text = $"Player range: {PlayerRangePower()}";

        _countPowerEnemyText.text = $"Enemy power: {_enemy.Power}";
        _enemyMeleePowerText.text = $"Enemy melee: {_enemy.Melee}";
        _enemyRangePowerText.text = $"Enemy range: {_enemy.Range}";

        _passButton.gameObject.SetActive(_crime.Peaceful);
    }
}
