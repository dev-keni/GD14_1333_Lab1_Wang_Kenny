using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3.scripts
{
    internal abstract class Room
    {
        public void RoomDescription() 
        { 
            //Return description of room
        }

        protected abstract void OnRoomEntered();

        protected abstract void OnRoomSearched();

        public void OnRoomExit()
        {

        }

        public void Setup()
        {

        }
    }

    internal class EncounterRoom : Room
    {
        bool encounter = true;
        protected override void OnRoomEntered()
        {
            //Encounter
        }
        protected override void OnRoomSearched()
        {
            Console.WriteLine("There is nothing here.");
        }
    }

    internal class TreasureRoom : Room
    {
        protected override void OnRoomEntered()
        {
            Console.WriteLine("There is nothing here.");
        }
        protected override void OnRoomSearched()
        {
            //Add die to player inventory
        }
    }
}
