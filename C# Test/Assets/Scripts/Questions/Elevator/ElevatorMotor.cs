using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

// Your ElevatorMotor class should implement the IElevatorMotor interface below and needs to be
// referenced by the code you submit
public class ElevatorMotor : IElevatorMotor
{
    public ElevatorDirection CurrentDirection { get; set; }
    public int CurrentFloor { get; set; }
    public event Action<int> ReachedFloor;

    private int _secondsBetweenFloors = 1;
    private bool _isMoving = false;

    public ElevatorMotor(int currentFloor)
    {
        CurrentDirection = ElevatorDirection.Idle;
        CurrentFloor = currentFloor;
    }

    public void GoToFloor(int floor)
    {
        if (_isMoving)
        {
            // please wait ~
            return;
        }

        _ = MoveToFloor(floor);
    }

    private async Task MoveToFloor(int targetFloor)
    {
        if (targetFloor == CurrentFloor)
        {
            _isMoving = false;
            return;
        }

        int diff = CurrentFloor - targetFloor;
        int direction = Math.Sign(diff);
        CurrentDirection = direction > 0 ? ElevatorDirection.Down : ElevatorDirection.Up;

        while (CurrentFloor != targetFloor)
        {
            await Task.Delay(_secondsBetweenFloors * 1000);
            CurrentFloor += direction;
        }

        CurrentDirection = ElevatorDirection.Idle;
        _isMoving = false;
        return;
    }
}
