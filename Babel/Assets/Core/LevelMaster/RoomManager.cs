using System;
using Assets.Characters.AiScripts;
using UnityEngine;

namespace Assets.Core.LevelMaster
{
    public class RoomManager : MonoBehaviour
    {
        public Transform[] Room0Waypoints;
        public Transform[] Room1Waypoints;
        public Transform[] Room2Waypoints;

        public int CurrentRoom { get; private set; }
        public AiMovement Ai { private get; set; }
        
        public void SetCurrentRoom(int room)
        {
            CurrentRoom = room;
            Ai.RoomChanged();
        }

        public Transform[] GetCurrnetWaypoints()
        {
            Transform[] waypoints;
            switch (CurrentRoom)
            {
                case 0:
                    waypoints = Room0Waypoints;
                    break;
                case 1:
                    waypoints = Room1Waypoints;
                    break;
                case 2:
                    waypoints = Room2Waypoints;
                    break;
                default:
                    waypoints = new Transform[0];
                    break;
            }

            return waypoints;
        }
    }
}
