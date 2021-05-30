using System;
using System.Collections.Generic;
using System.Text;

namespace RobotArmConsoleApp
{
    public enum Direction
    {
        N,
        S,
        E,
        W
    }
    public static class Constants
    {
        public const string Full = "Full";
        public const string Empty = "Empty";
        public const string Place = "place";
        public const string Detect = "detect";
        public const string Move = "move";
        public const string Report = "report";
        public const string Drop = "drop";
        public static int[,] squarePlateArray = new int[5, 5] {
                                                            {0,0,0,0,0},
                                                            {0,0,0,0,0},
                                                            {0,0,0,0,0} ,
                                                            {0,0,0,0,0},
                                                            {0,0,0,0,0}
                                                           };
    }
   
}
