using System;
using UnityEngine;

namespace Assets.Core.LevelMaster
{
    public class RoomManager : MonoBehaviour
    {

        public Transform[] RoomOneWaypoints;
        public Transform[] RoomTwoWaypoints;

        private int _currentRoom;

        public void SetCurrentRoom(int room)
        {
            _currentRoom = room;
        }

        public Transform[] GetCurrnetWaypoints()
        {
            switch (_currentRoom)
            {
                case 0:
                    return RoomOneWaypoints;
                case 1:
                    return RoomTwoWaypoints;
                default:
                    throw new Exception(string.Format("Room numer {0} not supported", _currentRoom));
            }
        }
    }
}
