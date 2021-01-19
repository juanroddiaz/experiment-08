using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 3c. Write a method to determine if any path exists between a starting room and an ending room with a 
// given name. You may add additional members and methods to your Room class or struct as needed.You
// do not need to worry about finding the shortest path. It is only necessary to determine if any path exists.
public class MazeLogic
{
    private List<RoomLogic> _maze = new List<RoomLogic>();

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

    private void CreateMazeConnection(RoomDirections direction, RoomLogic targetRoom)
    {
        var connectedRoom = _maze[Random.Range(0, _maze.Count)];
        targetRoom.Connect(direction, connectedRoom);
    }

    public bool AreConnectedRooms(string start, string end)
    {
        return true;
    }
}
