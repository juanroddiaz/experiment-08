using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
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

        var question2 = new SortLettersLogic();
        if (!question2.TestSortLetters())
        {
            OnTestNotPassed(2);
            return;
        }

        var mazeQuestion3 = new MazeLogic();
        mazeQuestion3.CreateMaze(8);
        mazeQuestion3.AreConnectedRooms("Room_1", "Room_6");
        mazeQuestion3.AreConnectedRooms("Room_0", "Room_7");
        mazeQuestion3.AreConnectedRooms("Room_4", "Room_5");
        mazeQuestion3.CreateMaze(16);
        mazeQuestion3.AreConnectedRooms("Room_5", "Room_10");
        mazeQuestion3.AreConnectedRooms("Room_11", "Room_15");

        Debug.Log("Success! Stating Elevator Test");
        StartCoroutine(ElevatorTest());
    }

    private IEnumerator ElevatorTest()
    {
        var elevatorQuestion = new ElevatorController(10, OnReachedSummonedFloor, OnReachedDestinationFloor);
        elevatorQuestion.SummonButtonPushed(8, ElevatorDirection.Up);
        yield return new WaitUntil(() => { return !elevatorQuestion.IsWorking; });
        elevatorQuestion.FloorButtonPushed(0);
        yield return new WaitUntil(() => { return !elevatorQuestion.IsWorking; });
        elevatorQuestion.SummonButtonPushed(5, ElevatorDirection.Down);
        yield return new WaitUntil(() => { return !elevatorQuestion.IsWorking; });
        elevatorQuestion.FloorButtonPushed(0);
        yield return new WaitForSeconds(0.5f);
        Debug.Log("OMG you forgot something! Pressing Floor 3 to go back to your flat");
        elevatorQuestion.FloorButtonPushed(3);
    }

    private void OnReachedSummonedFloor(int floor)
    {
        Debug.Log("The elevator is in the " + floor + " floor. Take it!");
    }

    private void OnReachedDestinationFloor(int floor)
    {
        Debug.Log("The elevator arrived to the " + floor + " floor.");
    }

    private void OnTestNotPassed(int testIndex)
    { 
        Debug.LogError("testIndex: " + testIndex + " NOT PASSED!");
    }

    // [JD] Adding auxiliar thread killer logic as Unity keeps the Editor's SyncContext by itself. More in -> https://forum.unity.com/threads/non-stopping-async-method-after-in-editor-game-is-stopped.558283/
    void OnApplicationQuit()
    {
#if UNITY_EDITOR
        var constructor = SynchronizationContext.Current.GetType().GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(int) }, null);
        var newContext = constructor.Invoke(new object[] { Thread.CurrentThread.ManagedThreadId });
        SynchronizationContext.SetSynchronizationContext(newContext as SynchronizationContext);
#endif
    }
}
