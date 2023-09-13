using System;

namespace Final.Models
{
    public class Mage : Adventurer
    {
        public Mage(string name) : base(name)
        {
            Type = "Mage";
        }

        public override double StrengthMultiplier
        {
            get { return 0.5; }
        }

        public override double ManaMultiplier
        {
            get { return 1.5; }
        }


        public override string Greeting()
        {
            return "Magic is all around us.";
        }
    }
}