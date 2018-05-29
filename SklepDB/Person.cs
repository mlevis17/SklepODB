using Perst;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SklepDB
{
    abstract class Person : Persistent
    {
        internal String name;
        internal String address;

        public virtual string Describe()
        {
            return "I am a person";
        }

    }
}
