using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 3c. Write a method to determine if any path exists between a starting room and an ending room with a 
// given name. You may add additional members and methods to your Room class or struct as needed.You
// do not need to worry about finding the shortest path. It is only necessary to determine if any path exists.
public class MazeLogic
{
    private List<RoomLogic> _maze = new List<RoomLogic>();
    private List<string> _mazeSolverHelper = new List<string>();

    public void CreateMaze(int roomsAmount)
    {
        string baseRoomName = "Room_";
        for (int i = 0; i < roomsAmount; i++)
        {
            string name = baseRoomName + i;
            RoomLogic room = new RoomLogic(new RoomStructure
            {
                Name = name,
            });

            _maze.Add(room);
        }

        foreach (var r in _maze)
        {
            CreateMazeConnection(RoomDirections.North, r);
            CreateMazeConnection(RoomDirections.South, r);
            CreateMazeConnection(RoomDirections.East, r);
            CreateMazeConnection(RoomDirections.West, r);
        }        
    }


    // - Rooms are not necessarily spatially coherent.If A is north of B, B might not be south of A.
    // - Doors are not necessarily bidirectional. If A can be reached from B, B might not be reachable from A.
    // - Rooms might connect to themselves.
    private void CreateMazeConnection(RoomDirections direction, RoomLogic targetRoom)
    {
        var connectedRoom = _maze[Random.Range(0, _maze.Count)];
        targetRoom.Connect(direction, connectedRoom);
    }

    public bool AreConnectedRooms(string start, string end)
    {
        RoomLogic startRoom = _maze.Find(r => string.Equals(r.GetName(), start));
        if (startRoom == null)
        {
            return false;
        }

        _mazeSolverHelper.Clear();
        _mazeSolverHelper.Add(start);

        if (CheckRoomConnections(startRoom, end))
        {
            _mazeSolverHelper.Add(end);
            string roomConnection = "Rooms connected! ";
            foreach (var s in _mazeSolverHelper)
            {
                roomConnection += s + ", ";
            }

            Debug.Log(roomConnection);
            return true;
        }

        Debug.Log("Rooms not connected!");
        return false;
    }

    private bool CheckRoomConnections(RoomLogic startRoom, string targetRoom)
    {
        if (startRoom.PathExistsTo(targetRoom))
        {
            return true;
        }

        var nextRoom = startRoom.GetConnection(RoomDirections.North);
        if(CacheConnectedRoom(nextRoom.GetName()))
        {
            return CheckRoomConnections(nextRoom, targetRoom);
        }

        nextRoom = startRoom.GetConnection(RoomDirections.South);
        if (CacheConnectedRoom(nextRoom.GetName()))
        {
            return CheckRoomConnections(nextRoom, targetRoom);
        }

        nextRoom = startRoom.GetConnection(RoomDirections.East);
        if (CacheConnectedRoom(nextRoom.GetName()))
        {
            return CheckRoomConnections(nextRoom, targetRoom);
        }

        nextRoom = startRoom.GetConnection(RoomDirections.West);
        if (CacheConnectedRoom(nextRoom.GetName()))
        {
            return CheckRoomConnections(nextRoom, targetRoom);
        }

        return false;
    }

    // [JD] Returns true if it's a new room in the map
    private bool CacheConnectedRoom(string nextRoomName)
    {
        if (!_mazeSolverHelper.Contains(nextRoomName))
        {
            _mazeSolverHelper.Add(nextRoomName);
            return true;
        }

        return false;
    }
}
