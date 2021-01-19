using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

// Your ElevatorController class should implement the IElevatorController interface below. 
public class ElevatorController : IElevatorController
{
    public event Action<int> ReachedSummonedFloor;
    public event Action<int> ReachedDestinationFloor;

    private ElevatorMotor _motor;
    private int _currentFloor = 0;
    private int _targetFloor = 0;

    public ElevatorController()
    {
        _motor = new ElevatorMotor(_currentFloor);
    }

    public void SummonButtonPushed(int floor, ElevatorDirection d)
    {
        _motor.GoToFloor(floor);

    }

    public void FloorButtonPushed(int floor)
    {
    }
}
