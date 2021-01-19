using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IElevatorController
{
    void SummonButtonPushed(int floor, ElevatorDirection d);
    void FloorButtonPushed(int floor);
    event Action<int> ReachedSummonedFloor;
    event Action<int> ReachedDestinationFloor;
}
