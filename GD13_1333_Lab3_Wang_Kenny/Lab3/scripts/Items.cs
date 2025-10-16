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

        protected abstract string ReturnItemName();
        protected abstract int ReturnDamage();

    }

    internal class Revolver : Items
    {
        protected override string ReturnItemName()
        {
            itemName = "Revolver";
            return itemName;
        }
        protected override int ReturnDamage() 
        {
            Damage = 20;
            return Damage;
        }
        
    }

    internal class MP5 : Items
    {
        protected override string ReturnItemName()
        {
            itemName = "Revolver";
            return itemName;
        }
        protected override int ReturnDamage()
        {
            Damage = 20;
            return Damage;
        }
    }

    internal class PipeBomb : Items
    {
        protected override string ReturnItemName()
        {
            itemName = "Revolver";
            return itemName;
        }
        protected override int ReturnDamage()
        {
            Damage = 20;
            return Damage;
        }
    }
}
