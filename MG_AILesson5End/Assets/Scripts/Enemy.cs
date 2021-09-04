using UnityEngine;

public class Enemy : IEnemy
{
    private string _name;

    private int _moneyPlayer;
    private int _healthPlayer;
    private int _powerPlayer;

    public Enemy(string name)
    {
        _name = name;
    }

    public void Update(DataPlayer dataPlayer, DataType dataType)
    {
        switch (dataType)
        {
            case DataType.Health:
                _healthPlayer = dataPlayer.CountHealth;
                Debug.Log($"Update {_name}, change {dataType} = {_healthPlayer}");
                break;

            case DataType.Money:
                _moneyPlayer = dataPlayer.CountMoney;
                Debug.Log($"Update {_name}, change {dataType} = {_moneyPlayer}");
                break;

            case DataType.Power:
                _powerPlayer = dataPlayer.CountPower;
                Debug.Log($"Update {_name}, change {dataType} = {_powerPlayer}");
                break;
        }

        
    }

    public int Power
    {
        get
        {
            var power = _moneyPlayer + _healthPlayer - _powerPlayer;
            return power;
        }
    }

    public int Melee
    {
        get
        {
            var melee = Power + _moneyPlayer;
            return melee;
        }
    }

    public int Range
    {
        get
        {
            var melee = Power + _healthPlayer;
            return melee;
        }
    }
}
