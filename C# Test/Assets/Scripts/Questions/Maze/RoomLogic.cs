using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RoomDirections
{ 
    North = 0,
    South,
    East,
    West,
}

//Each room has a unique name and can be connected to between zero and four other rooms through
//doors named north, south, east, and west.
public class RoomStructure
{
    public string Name = "";
    public Dictionary<RoomDirections, RoomLogic> RoomConnections = new Dictionary<RoomDirections, RoomLogic>();

    // 3a.Write a class or struct declaration that shows the structure of a Room.Include a constructor(for classes) or initialization method(for structs).
    // [JD] for this type of data structures I prefer to use the Object Initializer method, so we can save this constructor declaration + base one.
    public RoomStructure()
    {
        Name = "UnknownRoom";
    }
    
    public RoomStructure(string name, RoomLogic north, RoomLogic south, RoomLogic east, RoomLogic west)
    {
        Name = name;
        RoomConnections.Add(RoomDirections.North, north);
        RoomConnections.Add(RoomDirections.South, south);
        RoomConnections.Add(RoomDirections.East, east);
        RoomConnections.Add(RoomDirections.West, west);
    }
}

public class RoomLogic : IPathable
{
    private RoomStructure _structure = new RoomStructure();
    private List<RoomLogic> _roomsHelper;

    public RoomLogic(RoomStructure structure)
    {
        _structure = structure;
        //[JD] Avoiding using LINQ
        _roomsHelper = new List<RoomLogic>(_structure.RoomConnections.Values);
    }

    public string GetName()
    {
        return _structure.Name;
    }

    public bool PathExistsTo(string endingRoomName)    
    {
        return _roomsHelper.FindIndex(r => string.Equals(r.GetName(),endingRoomName)) != -1;
    }

    // 3b. Write a method to connect a new Room to an existing Room
    public void Connect(RoomDirections direction, RoomLogic room)
    {
        _structure.RoomConnections[direction] = room;
        //[JD] Avoiding using LINQ
        _roomsHelper = new List<RoomLogic>(_structure.RoomConnections.Values);
    }
}
