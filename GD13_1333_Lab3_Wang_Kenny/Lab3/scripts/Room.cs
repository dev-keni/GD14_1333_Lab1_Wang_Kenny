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
        protected bool Searched = false;
        
        public void RoomDescription() 
        { 
            //Return description of room
        }

        protected abstract void OnRoomEntered();

        public abstract bool OnRoomSearched();

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
        
        public override bool OnRoomSearched()
        {
            Console.WriteLine("There is nothing here.");
            return false;
        }
    }

    internal class TreasureRoom : Room
    {
        protected override void OnRoomEntered()
        {
            Encounter = false;
            Console.WriteLine("There is no one in the room but it seems to be untouched since the outbreak.");
        }
        
        public override bool OnRoomSearched()
        {
            //Add die to player inventory
            if (Searched == false)
            {
                Console.WriteLine("You look around for supplies. After a bit of searching you find...");
                Searched = true;
                return true;
            }
            else
            {
                Console.WriteLine("You've already looked through everything here.");
                return false;
            }
        }
    }
}
