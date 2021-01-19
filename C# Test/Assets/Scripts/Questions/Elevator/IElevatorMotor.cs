using System;

public enum ElevatorDirection
{ 
    Idle = 0,
    Up,
    Down,
}

public interface IElevatorMotor
{
    ElevatorDirection CurrentDirection { get; }
    int CurrentFloor { get; }
    event Action<int> ReachedFloor;
    void GoToFloor(int floor);
}
