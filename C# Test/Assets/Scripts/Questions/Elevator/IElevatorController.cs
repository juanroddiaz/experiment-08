using System;

public interface IElevatorController
{
    void SummonButtonPushed(int floor, ElevatorDirection d);
    void FloorButtonPushed(int floor);
    event Action<int> ReachedSummonedFloor;
    event Action<int> ReachedDestinationFloor;
}
