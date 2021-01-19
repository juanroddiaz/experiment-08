using System.Collections.Generic;

public class AllDigitsUniqueLogic
{
    private List<int> _uniqueNumbers = new List<int>();

    //  Write a method that takes an unsigned integer as input and returns true if all the digits in the base
    //  10 representation of that number are unique.
    private bool AllDigitsUnique(int value)
    {
        if (value < 0)
        {
            return false;
        }

        _uniqueNumbers.Clear();
        int temp = 0;
        while (value > 0)
        {
            temp = value % 10;
            if (_uniqueNumbers.Contains(temp))
            {
                return false;
            }

            _uniqueNumbers.Add(temp);
            value /= 10;
        }
        
        return true;
    }

    public bool TestAllDigitsUnique()
    {
        bool result = true;
        result &= AllDigitsUnique(48778584) == false;
        result &= AllDigitsUnique(17308459) == true;
        result &= AllDigitsUnique(-1) == false;
        result &= AllDigitsUnique(0) == true;
        result &= AllDigitsUnique(1234567890) == true;
        result &= AllDigitsUnique(0987654321) == true;
        result &= AllDigitsUnique(11) == false;
        result &= AllDigitsUnique(1) == true;
        return result;
    }
}
