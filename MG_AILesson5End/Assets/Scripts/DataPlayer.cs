using System.Collections.Generic;

public class DataPlayer
{
    private const int _minCrime = 0;
    private const int _middleCrime = 3;
    private const int _maxCrime = 5;

    private string _titleData;

    private int _countMoney;
    private int _countHealth;
    private int _countPower;
    private int _countCrime;

    private List<IEnemy> _enemies = new List<IEnemy>();

    public DataPlayer(string titleData)
    {
        _titleData = titleData;
    }

    public string TitleData => _titleData;

    public int CountMoney 
    { 
        get => _countMoney;
        set
        {            
            SetStats(ref _countMoney, in value, DataType.Money);
        }
    }

    public int CountHealth
    {
        get => _countHealth;
        set
        {            
            SetStats(ref _countHealth, in value, DataType.Health);
        }
    }

    public int CountPower
    {
        get => _countPower;
        set
        {            
            SetStats(ref _countPower, in value, DataType.Power);
        }
    }
    public int CountCrime
    {
        get => _countCrime;
        set
        {
            if (value < _minCrime) value = _minCrime;
            if (value > _maxCrime) value = _maxCrime;
            SetStats(ref _countCrime, in value, DataType.Crime);
        }
    }

    public bool Peaceful
    {
        get
        {
            if (_countCrime < _middleCrime) return true;
            else return false;            
        }
    }

    public void Attach(IEnemy enemy)
    {
        _enemies.Add(enemy);
    }

    public void Detach(IEnemy enemy)
    {
        _enemies.Remove(enemy);
    }

    private void SetStats(ref int _characterStat, in int _value, DataType _statType)
    {
        if (_characterStat != _value)
        {
            _characterStat = _value;
            Notifier(_statType);
        }
    }

    private void Notifier(DataType dataType)
    {
        foreach(var enemy in _enemies)
            enemy.Update(this, dataType);
    }
}

public class Money : DataPlayer
{
    public Money(string titleData) : base(titleData)
    {
    }
}

public class Health : DataPlayer
{
    public Health(string titleData) : base(titleData)
    {
    }
}

public class Power : DataPlayer
{
    public Power(string titleData) : base(titleData)
    {
    }
}

public class Crime : DataPlayer
{
    public Crime(string titleData) : base(titleData)
    {
    }
}