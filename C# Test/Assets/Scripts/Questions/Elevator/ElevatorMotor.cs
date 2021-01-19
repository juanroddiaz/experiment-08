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
    private int _floorsAmount = 0;

    public ElevatorMotor(int floorsAmount, int currentFloor)
    {
        _floorsAmount = floorsAmount;
        CurrentDirection = ElevatorDirection.Idle;
        CurrentFloor = currentFloor;
    }

    public void GoToFloor(int floor)
    {
        if (floor < 0 || floor >= _floorsAmount)
        {
            // invalid floor entry
            return;
        }

        if (CurrentDirection != ElevatorDirection.Idle)
        {
            // please wait ~ playing elevator music
            return;
        }

        _ = MoveToFloor(floor);
    }

    private async Task MoveToFloor(int targetFloor)
    {
        if (targetFloor == CurrentFloor)
        {
            CurrentDirection = ElevatorDirection.Idle;
            return;
        }

        int diff = targetFloor - CurrentFloor;
        int direction = Math.Sign(diff);
        CurrentDirection = direction > 0 ? ElevatorDirection.Up : ElevatorDirection.Down;

        await Task.Delay(_secondsBetweenFloors * 1000);

        while (CurrentFloor != targetFloor)
        {
            await Task.Delay(_secondsBetweenFloors * 1000);
            CurrentFloor += direction;
            Debug.Log("Going to " + targetFloor + ", current: " + CurrentFloor);
        }

        ReachedFloor?.Invoke(CurrentFloor);
        ReachedFloor = null;
        CurrentDirection = ElevatorDirection.Idle;        
        return;
    }
}
