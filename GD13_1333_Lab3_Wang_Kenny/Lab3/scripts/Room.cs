using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3.scripts
{
    internal abstract class Room
    {
        protected bool BeenHere = false;
        protected bool Encounter;
        
        public void RoomDescription() 
        { 
            //Return description of room
        }

        protected abstract void OnRoomEntered();

        protected abstract void OnRoomSearched();

        public bool Enter()
        {
            OnRoomEntered();
            if (Encounter == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }                

        public void OnRoomExit()
        {
            
        }

        public void Setup()
        {

        }
    }

    internal class EncounterRoom : Room
    {
        protected override void OnRoomEntered()
        {
            
            if (BeenHere == false)
            {
                Encounter = true;
                BeenHere = true;
                Console.WriteLine("You open the door and an enemy bandit is there.");
            }
            else if (BeenHere == true)
            {
                Encounter = false;
                Console.WriteLine("The room is bloody and the body of the enemy lies there in silence.");
            }
            
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
            Encounter = false;
            Console.WriteLine("There is no one in the room but it seems to be untouched since the outbreak.");
        }
        
        protected override void OnRoomSearched()
        {
            //Add die to player inventory
            Console.WriteLine("You look around for supplies. After a bit of searching you find...");
        }
    }
}
