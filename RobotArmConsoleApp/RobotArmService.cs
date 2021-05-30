using NLog.Fluent;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace RobotArmConsoleApp
{
    public class RobotArmService
    {
        public static int[,] ExecuteCommand(string command, int[,] array2D, ref int xCoordinate, ref int yCoordinate)
        {
            try
            {
                if (command.ToLower().Contains(Constants.Place))
                {
                    if (ValidatePlaceCommandFormat(command))
                    {
                        command = command.Replace(" ", String.Empty);
                        string[] splittedCommand = Regex.Split(command, @"\D+");
                        List<int> coordinateList = new List<int>();
                        foreach (string value in splittedCommand)
                        {
                            if (!string.IsNullOrEmpty(value))
                            {
                                int i = int.Parse(value);
                                coordinateList.Add(i);
                            }
                        }
                        if (coordinateList != null && coordinateList.Count > 0)
                        {
                            int[] xyCoordinates = coordinateList.ToArray();

                            xCoordinate = xyCoordinates[0];
                            yCoordinate = xyCoordinates[1];
                            array2D[xCoordinate, yCoordinate] = 0;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid place command format");
                    }

                }
                else if (command.ToLower().Contains(Constants.Detect))
                {
                    Console.WriteLine(IsWellFull(array2D, xCoordinate, yCoordinate));
                }
                else if (command.ToLower().Contains(Constants.Move))
                {
                    if (ValidateMoveCommand(command))
                    {
                        var splittedCommand = command.IndexOf(" ");
                        string direction = command.Substring(splittedCommand + 1, 1);
                        switch ((Direction)Enum.Parse(typeof(Direction), direction.ToUpper()))
                        {
                            case Direction.N:
                                if (yCoordinate < 4)
                                {
                                    yCoordinate++;
                                }
                                else
                                {
                                    Console.WriteLine("Invalid Move Command, robot arm will move out of the plate");
                                }
                                break;
                            case Direction.E:
                                if (xCoordinate < 4)
                                {
                                    xCoordinate++;
                                }
                                else
                                {
                                    Console.WriteLine("Invalid Move Command, robot arm will move out of the plate");
                                }
                                break;
                            case Direction.S:
                                if (yCoordinate > 0)
                                {
                                    yCoordinate--;
                                }
                                else
                                {
                                    Console.WriteLine("Invalid Move Command, robot arm will move out of the plate");
                                }
                                break;
                            case Direction.W:
                                if (xCoordinate > 0)
                                {
                                    xCoordinate--;
                                }
                                else
                                {
                                    Console.WriteLine("Invalid Move Command, robot arm will move out of the plate");
                                }
                                break;
                            default:
                                Console.WriteLine("Invalid move");
                                break;

                        }

                    }
                    else
                    {
                        Console.WriteLine("Invalid Move Command");
                    }


                }
                else if (command.ToLower().Contains(Constants.Report))
                {
                    string wellCondition = IsWellFull(array2D, xCoordinate, yCoordinate);
                    Console.WriteLine("Output: {0} {1} {2}", xCoordinate, yCoordinate, wellCondition);
                }
                else if (command.ToLower().Contains(Constants.Drop))
                {
                    string wellCondition = IsWellFull(array2D, xCoordinate, yCoordinate);
                    if (wellCondition == Constants.Empty)
                    {
                        array2D[xCoordinate, yCoordinate] = 1;
                    }
                }
                else
                {
                    Log.Info("Invalid command executed");
                }
            }
            catch (Exception e)
            {
                Log.Error("Error occured while executing command");
                Console.WriteLine("Error occured {0}", e.Message);
            }
            return array2D;
        }

        public static string IsWellFull(int[,] array2D, int xCoordinate, int yCoordinate)
        {
            return array2D[xCoordinate, yCoordinate] == 0 ? Constants.Empty : Constants.Full;
        }
        public static bool ValidatePlaceCommand(string command)
        {
            if (!command.Trim().ToLower().Contains("place"))
            {
                Log.Error("Place is not the first command to initiate the program");
                return false;
            }
            return true;
        }

        public static string InitiateRobotArm()
        {
            Log.Info("PLace robot arm on the plate");
            Console.WriteLine("Enter Command PLACE X,Y to place robot above the plate");
            string command = Console.ReadLine();
            bool validCommand = ValidatePlaceCommand(command);
            if (!validCommand)
            {
                Console.WriteLine("Invalid Command to place robot arm on the plate.");
                Log.Error("First commad is not place command");
                InitiateRobotArm();

            }
            Log.Info("Robot arm has been placed on the plate");
            return command;
        }

        public static bool ValidateMoveCommand(string command)
        {
            return Regex.IsMatch(command, "^Move [N,S,E,W]$", RegexOptions.IgnoreCase);
        }
        public static bool ValidatePlaceCommandFormat(string command)
        {
            return Regex.IsMatch(command, "^Place [0-4],[0-4]$", RegexOptions.IgnoreCase);
        }
    }
}
