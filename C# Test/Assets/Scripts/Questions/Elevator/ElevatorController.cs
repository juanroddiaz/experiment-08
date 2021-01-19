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
    private int _targetFloor = 0;
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
        _motor = new ElevatorMotor(floorsAmount, _targetFloor);
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

        Debug.Log("The elevator is going to the " + floor + " floor, be patient bitte.");
        _isWorking = true;
        _motor.GoToFloor(floor);
        _motor.ReachedFloor += ReachedSummonedFloor;
        _motor.ReachedFloor += OnReachedFloor;

    }

    public void FloorButtonPushed(int floor)
    {
        if (_motor.CurrentDirection != ElevatorDirection.Idle)
        {
            bool noMemory = true;
            // check if the button pushed is for a floor in the way of the current target
            if (_motor.CurrentDirection == ElevatorDirection.Down && floor > _targetFloor)
            {
                _floorMemory.Add(_targetFloor);
                noMemory = false;
            }

            if (_motor.CurrentDirection == ElevatorDirection.Up && floor < _targetFloor)
            {
                _floorMemory.Add(_targetFloor);
                noMemory = false;
            }

            if (noMemory)
            {
                return;
            }            
        }

        _targetFloor = floor;

        Debug.Log("Going to " + floor + " floor. Playing elevator music...");
        _isWorking = true;
        _motor.GoToFloor(floor);
        _motor.ReachedFloor += ReachedDestinationFloor;
        _motor.ReachedFloor += OnReachedFloor;
        _motor.ReachedFloor += OnMemorizedFloor;

    }

    private void OnReachedFloor(int floor)
    {
        _isWorking = false;
    }

    private void OnMemorizedFloor(int floor)
    {
        if (_floorMemory.Count > 0)
        {
            int nextFloor = _floorMemory[0];
            _floorMemory.RemoveAt(0);
            FloorButtonPushed(nextFloor);
        }
    }
}
