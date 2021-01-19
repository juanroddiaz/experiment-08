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
    private List<int> _floorMemory = new List<int>();

    public bool IsWorking 
    {
        get 
        {
            return _isWorking;
        }
    }

    private bool _isWorking = false;

    public ElevatorController(int floorsAmount, Action<int> onReachedSummonedFloor, Action<int> onReachedDestinationFloor)
    {
        _motor = new ElevatorMotor(floorsAmount, _currentFloor);
        ReachedSummonedFloor = onReachedSummonedFloor;
        ReachedDestinationFloor = onReachedDestinationFloor;
    }

    // [JD] Is it direction a needed argument when you call for an elevator? Maybe when you have 2 or more elevators 
    // this direction could help the controller to decide which one is closer. I'll go for the one elevator in the building solution.
    public void SummonButtonPushed(int floor, ElevatorDirection d)
    {
        if (_motor.CurrentDirection != ElevatorDirection.Idle)
        {
            // motor moving, please wait ~
            return;
        }

        Debug.Log("The elevator is going to your floor, be patient bitte.");
        _isWorking = true;
        _motor.ReachedFloor += ReachedSummonedFloor;
        _motor.ReachedFloor += OnReachedFloor;
        _motor.GoToFloor(floor);
    }

    public void FloorButtonPushed(int floor)
    {
        if (_motor.CurrentDirection != ElevatorDirection.Idle)
        {
            // check if the button pushed is for a floor in the way of the current target
            // _floorMemory
            return;
        }

        Debug.Log("Playing elevator music...");
        _isWorking = true;
        _motor.ReachedFloor += ReachedDestinationFloor;
        _motor.ReachedFloor += OnReachedFloor;
        _motor.GoToFloor(floor);
    }

    private void OnReachedFloor(int floor)
    {
        _isWorking = false;
    }
}
