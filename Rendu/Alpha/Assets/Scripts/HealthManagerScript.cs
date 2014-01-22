using UnityEngine;
using System.Collections;

public class HealthManagerScript : MonoBehaviour {

    [SerializeField]
    private int _playerMaxLifePoint;

    [SerializeField]
    private int _currentLifePoint;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

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
}
