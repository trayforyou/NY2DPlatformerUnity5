public class Health : Storage
{
    public void TakeDamage(int damage)
    {
        int tempValue;

        damage = CheckCorrectNumber(damage);

        tempValue = _points - damage;

        if (tempValue <= 0)
            _points = 0;
        else
            _points = tempValue;

        if (_points <= 0)
            End();

        ChangeState(); ;
    }

    public void Heal(int healPoints)
    {
        int tempValue;

        healPoints = CheckCorrectNumber(healPoints);

        tempValue = _points + healPoints;

        if (tempValue > _maxValue)
            _points = _maxValue;
        else
            _points = tempValue;

        ChangeState();
    }

    private int CheckCorrectNumber(int number)
    {
        if (number <= 0)
            number = 0;

        return number;
    }
}