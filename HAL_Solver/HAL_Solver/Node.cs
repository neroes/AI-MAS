﻿using System;

namespace HAL_Solver
{
    public struct Node
    {
        public Byte x;
        public Byte y;
        public Node (Byte x, Byte y)
        {
            this.x = x;
            this.y = y;
        }
        public Node(Node old)
        {
            this.x = old.x;
            this.y = old.y;
        }
        public static bool operator ==(Node x, Node y)
        {
            return (x.x == y.x && x.y == y.y);
        }
        public static bool operator !=(Node x, Node y)
        {
            return !(x.x == y.x && x.y == y.y);
        }
        public static int operator -(Node x, Node y)
        {
            return (Math.Abs(x.x - y.x) + Math.Abs(x.y - y.y));
        }
        public override bool Equals(Object obj)
        {
            return obj is Node && this == (Node)obj;
        }
        public override int GetHashCode()
        {
            return 37 * (37 + this.x) + this.y;
        }
    }
}
