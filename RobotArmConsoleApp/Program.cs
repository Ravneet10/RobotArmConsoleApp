using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace RobotArmConsoleApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            int xCoordinate = 0;
            int yCoordinate = 0;
            int[,] squarePlateArray2D = Constants.squarePlateArray;

            Console.WriteLine("This app can accept following commands: \n" +
                "1) PLACE X,Y \n" +
                "2) DETECT \n" +
                "3) DROP \n" +
                "4) MOVE N, S, E or W \n" +
                "5) REPORT \n" +
                "6) EXIT \n");
            string command = RobotArmService.InitiateRobotArm();
           squarePlateArray2D = RobotArmService.ExecuteCommand(command, squarePlateArray2D, ref xCoordinate, ref yCoordinate);
            string nextCommand = Console.ReadLine();

            while (!nextCommand.ToLower().Contains("exit"))
            {
                squarePlateArray2D = RobotArmService.ExecuteCommand(nextCommand,squarePlateArray2D, ref xCoordinate, ref yCoordinate);
                nextCommand = Console.ReadLine();
            }
            return;
        }

      
    }
}
