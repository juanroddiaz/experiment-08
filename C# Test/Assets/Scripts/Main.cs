using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{


    void Start()
    {
        // question 1
        var question1 = new AllDigitsUniqueLogic();
        if (!question1.TestAllDigitsUnique())
        {
            OnTestNotPassed(1);
            return;
        }

        Debug.Log("Success!");
    }


    private void OnTestNotPassed(int testIndex)
    { 
        Debug.LogError("testIndex: " + testIndex + " NOT PASSED!");
    }
}
