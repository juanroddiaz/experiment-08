using System.Collections.Generic;
using UnityEngine;

// 3c. Write a method to determine if any path exists between a starting room and an ending room with a 
// given name. You may add additional members and methods to your Room class or struct as needed.You
// do not need to worry about finding the shortest path. It is only necessary to determine if any path exists.
public class MazeLogic
{
    private List<RoomLogic> _maze = new List<RoomLogic>();
    private List<string> _mazeSolverHelper = new List<string>();
    private List<string> _mazeDirectionHelper = new List<string>();

    public void CreateMaze(int roomsAmount)
    {
        _maze.Clear();
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

    // - Each room has a unique name and can be connected to between zero and four other rooms through
    //   doors named north, south, east, and west.
    // - Rooms are not necessarily spatially coherent.If A is north of B, B might not be south of A.
    // - Doors are not necessarily bidirectional. If A can be reached from B, B might not be reachable from A.
    // - Rooms might connect to themselves.
    private void CreateMazeConnection(RoomDirections direction, RoomLogic targetRoom)
    {
        // [JD] 25% of no connection with a room
        var connectedRoom = Random.Range(0.0f, 1.0f) < 0.25f ? null : _maze[Random.Range(0, _maze.Count)];
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
        _mazeDirectionHelper.Clear();
        _mazeSolverHelper.Add(start);
        _mazeDirectionHelper.Add("Origin");

        bool areConnected = false;
        if (CheckRoomConnections(startRoom, end))
        {
            _mazeSolverHelper.Add(end);
            _mazeDirectionHelper.Add("End");
            areConnected = true;
        }

        string roomConnection = "";
        for(int i=0; i<_mazeSolverHelper.Count; i++)
        {
            roomConnection += _mazeSolverHelper[i] + " - " + _mazeDirectionHelper[i] + ", ";
        }

        if (areConnected)
        {
            Debug.Log("Rooms connected: " + roomConnection);
            return true;
        }

        Debug.Log("Rooms not connected: Tried " + roomConnection + " not reached: " + end);
        return false;
    }

    private bool CheckRoomConnections(RoomLogic startRoom, string targetRoom)
    {
        if (startRoom.PathExistsTo(targetRoom))
        {
            return true;
        }

        if (CheckRoomDirection(startRoom, RoomDirections.North, targetRoom))
        {
            return true;
        }

        if (CheckRoomDirection(startRoom, RoomDirections.South, targetRoom))
        {
            return true;
        }

        if (CheckRoomDirection(startRoom, RoomDirections.East, targetRoom))
        {
            return true;
        }

        if (CheckRoomDirection(startRoom, RoomDirections.West, targetRoom))
        {
            return true;
        }

        return false;
    }

    private bool CheckRoomDirection(RoomLogic startRoom, RoomDirections direction, string targetRoom)
    {
        var nextRoom = startRoom.GetConnection(direction);
        if (nextRoom != null && CacheConnectedRoom(nextRoom.GetName(), direction.ToString()))
        {
            return CheckRoomConnections(nextRoom, targetRoom);
        }

        return false;
    }

    // [JD] Returns true if it's a new room in the map
    private bool CacheConnectedRoom(string nextRoomName, string directionName)
    {
        if (!_mazeSolverHelper.Contains(nextRoomName))
        {
            _mazeSolverHelper.Add(nextRoomName);
            _mazeDirectionHelper.Add(directionName);
            return true;
        }

        return false;
    }
}
