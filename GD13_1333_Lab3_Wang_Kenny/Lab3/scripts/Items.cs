using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3.scripts
{
    internal abstract class Items
    {
        protected string? itemName;
        protected int Damage;

        public abstract string ReturnItemName();
        public abstract int ReturnDamage();

    }

    internal class Revolver : Items
    {
        public override string ReturnItemName()
        {
            itemName = "Revolver";
            return itemName;
        }
        public override int ReturnDamage() 
        {
            Damage = 6;
            return Damage;
        }
        
    }

    internal class Shotgun : Items
    {
        public override string ReturnItemName()
        {
            itemName = "Shotgun";
            return itemName;
        }
        public override int ReturnDamage()
        {
            Damage = 10;
            return Damage;
        }
    }

    internal class MP5 : Items
    {
        public override string ReturnItemName()
        {
            itemName = "MP5";
            return itemName;
        }
        public override int ReturnDamage()
        {
            Damage = 20;
            return Damage;
        }
    }

    internal class PipeBomb : Items
    {
        public override string ReturnItemName()
        {
            itemName = "Pipe Bomb";
            return itemName;
        }
        public override int ReturnDamage()
        {
            Damage = 20;
            return Damage;
        }
    }
}
