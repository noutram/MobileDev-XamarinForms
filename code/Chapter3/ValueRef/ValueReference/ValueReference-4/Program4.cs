﻿using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace ValueReference
{
    class Program4
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            _ = new Program4();
        }

        void Display(MyObj obj, [CallerMemberName]string memberName = "") => Console.WriteLine(memberName + $" = {obj}");

        public Program4()
        {
            //Entry point

            // *******************
            // Reference semantics
            // *******************
            MyObj r1 = new MyObj(2, 3);
            MyObj r2 = new MyObj(-2, -3);
            MyObj r3 = r1;

            Display(r1, "r1");
            Display(r2, "r2");
            Display(r3, "r3");

            Console.WriteLine("Update r1");
            r1.a *= -1;
            r1.b *= -1;

            Display(r1, "r1");
            Display(r2, "r2");
            Display(r3, "r3");

            //Compare two absolutely equal objects
            if (r1.Equals(r3))
            {
                Console.WriteLine("r1 has the same values as r3");
            }
            if (r1 == r3)
            {
                Console.WriteLine("r1 is a reference to r3");
            }

            Console.WriteLine("***** Test for equality *****");
            Console.WriteLine("Now set the values of r2 to the same values as r1");

            //Compare two similar objects
            r2.a = r1.a;
            r2.b = r1.b;
            Display(r1, "r1");
            Display(r2, "r2");

            if (r1.Equals(r2))
            {
                Console.WriteLine("r1 has the same values as r2");
            }
            if (r1 == r2)
            {
                Console.WriteLine("r1 is a reference to r2");
            }
        }

    }

    public class MyObj : IEquatable<MyObj>
    {
        public int a;
        public int b;
        public MyObj(int aa, int bb) => (a, b) = (aa, bb);

        //To perform a member by member comparison
        public bool Equals([AllowNull] MyObj other)
        {
            if (other == null) return false;
            return ((other.a == a) && (other.b == b));
        }

        public override string ToString() => $"a={a}, b={b}";
    }
}
