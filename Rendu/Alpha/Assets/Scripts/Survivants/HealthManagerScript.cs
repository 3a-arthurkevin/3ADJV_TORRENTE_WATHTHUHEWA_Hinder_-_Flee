using UnityEngine;
using System.Collections;

public class HealthManagerScript : MonoBehaviour {

    [SerializeField]
    private int _playerMaxLifePoint;

    [SerializeField]
    private int _currentLifePoint;

    public void addLifePoint(int lifePointToAdd)
    {
        _currentLifePoint += lifePointToAdd;

        if (_currentLifePoint > _playerMaxLifePoint)
            _currentLifePoint = _playerMaxLifePoint;
    }

    public void removeLifePoint(int lifePointToRemove)
    {
        _currentLifePoint -= lifePointToRemove;

        if (_currentLifePoint < 0)
            _currentLifePoint = 0;
    }

    public bool isDead()
    {
        return (_currentLifePoint == 0);
    }
}
