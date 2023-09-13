using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Final.Models
{

    public class Paladin : Adventurer
    {
        public Paladin(string name) : base(name)
        {
            Type = "Paladin";
        }

        public override double StrengthMultiplier
        {
            get { return 1.5; }
        }

        public override double ManaMultiplier
        {
            get { return 0.5; }
        }

        public override string Greeting()
        {
            return "I live to serve.";
        }
    }
}