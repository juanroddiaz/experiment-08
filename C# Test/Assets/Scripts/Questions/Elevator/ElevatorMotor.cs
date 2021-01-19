using System;
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
    private Task _currentTask;
    private int _targetFloor;

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

        ReachedFloor = null;
        if (CurrentDirection != ElevatorDirection.Idle)
        {
            _targetFloor = floor;
            return;
        }

        _currentTask = MoveToFloor(floor);
    }

    private async Task MoveToFloor(int targetFloor)
    {
        _targetFloor = targetFloor;
        if (_targetFloor == CurrentFloor)
        {
            CurrentDirection = ElevatorDirection.Idle;
            return;
        }

        int diff = _targetFloor - CurrentFloor;
        int direction = Math.Sign(diff);
        CurrentDirection = direction > 0 ? ElevatorDirection.Up : ElevatorDirection.Down;

        await Task.Delay(_secondsBetweenFloors * 1000);

        while (CurrentFloor != _targetFloor)
        {
            await Task.Delay(_secondsBetweenFloors * 1000);
            CurrentFloor += direction;
            Debug.Log("Going to " + _targetFloor + ", current: " + CurrentFloor);
        }

        CurrentDirection = ElevatorDirection.Idle;
        ReachedFloor?.Invoke(CurrentFloor);
        return;
    }
}
