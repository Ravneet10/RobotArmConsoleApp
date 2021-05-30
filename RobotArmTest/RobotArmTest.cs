using Microsoft.VisualStudio.TestTools.UnitTesting;
using RobotArmConsoleApp;

namespace RobotArmTest
{
    [TestClass]
    public class RobotArmTest
    {
        int xCoordinate = 0;
        int yCoordinate = 0;
        int[,] array2D = Constants.squarePlateArray;

        [TestMethod]
        public void ValidatePlaceCommand()
        {
            bool actualResult = RobotArmService.ValidatePlaceCommand("place");
            Assert.AreEqual(true, actualResult);
        }
        [TestMethod]
        public void ValidatePlaceCommandNegative()
        {
            bool actualResult = RobotArmService.ValidatePlaceCommand("detect");
            Assert.AreEqual(false, actualResult);
        }
        [TestMethod]
        public void IsWellEmpty()
        {
            string actualResult = RobotArmService.IsWellFull(array2D, xCoordinate, yCoordinate);
            Assert.AreEqual(Constants.Empty, actualResult);
        }

        [TestMethod]
        public void IsWellFull()
        {
            array2D[xCoordinate, yCoordinate] = 1;
            string actualResult = RobotArmService.IsWellFull(array2D, xCoordinate, yCoordinate);
            Assert.AreEqual(Constants.Full, actualResult);
        }
        [TestMethod]
        public void ExecuteMoveCommandNorth()
        {
            RobotArmService.ExecuteCommand("Move N",array2D, ref xCoordinate,ref yCoordinate);
            Assert.AreEqual(0, xCoordinate);
            Assert.AreEqual(1, yCoordinate);
            Assert.AreEqual(1, array2D[xCoordinate, yCoordinate]);
        }
        [TestMethod]
        public void ExecuteMultipleCommands()
        {
            RobotArmService.ExecuteCommand("Place 1,0", array2D, ref xCoordinate, ref yCoordinate);
            RobotArmService.ExecuteCommand("Move E", array2D, ref xCoordinate, ref yCoordinate);
            RobotArmService.ExecuteCommand("Drop", array2D, ref xCoordinate, ref yCoordinate);
            Assert.AreEqual(2, xCoordinate);
            Assert.AreEqual(0, yCoordinate);
            Assert.AreEqual(1, array2D[xCoordinate, yCoordinate]);
        }
        [TestMethod]
        public void ExecuteCommandToMoveRobotArmOutOfPlate()
        {
            RobotArmService.ExecuteCommand("Place 1,0", array2D, ref xCoordinate, ref yCoordinate);
            RobotArmService.ExecuteCommand("Move S", array2D, ref xCoordinate, ref yCoordinate);
            Assert.AreEqual(1, xCoordinate);
            Assert.AreEqual(0, yCoordinate);
            Assert.AreEqual(0, array2D[xCoordinate, yCoordinate]);
        }
        [TestMethod]
        public void ExecuteCommandToMoveRobotArm()
        {
            RobotArmService.ExecuteCommand("Place 1,0", array2D, ref xCoordinate, ref yCoordinate);
            RobotArmService.ExecuteCommand("Move N", array2D, ref xCoordinate, ref yCoordinate);
            RobotArmService.ExecuteCommand("Drop", array2D, ref xCoordinate, ref yCoordinate);
            RobotArmService.ExecuteCommand("Move W", array2D, ref xCoordinate, ref yCoordinate);
            RobotArmService.ExecuteCommand("Drop", array2D, ref xCoordinate, ref yCoordinate);
            RobotArmService.ExecuteCommand("Move N", array2D, ref xCoordinate, ref yCoordinate);
            RobotArmService.ExecuteCommand("Move E", array2D, ref xCoordinate, ref yCoordinate);
            RobotArmService.ExecuteCommand("Move E", array2D, ref xCoordinate, ref yCoordinate);
            RobotArmService.ExecuteCommand("Move E", array2D, ref xCoordinate, ref yCoordinate);
            RobotArmService.ExecuteCommand("Move E", array2D, ref xCoordinate, ref yCoordinate);
            RobotArmService.ExecuteCommand("Drop", array2D, ref xCoordinate, ref yCoordinate);
            Assert.AreEqual(4, xCoordinate);
            Assert.AreEqual(2, yCoordinate);
            Assert.AreEqual(1, array2D[xCoordinate, yCoordinate]);
        }
        [TestMethod]
        public void ValidateMoveCommandWithRightFormat()
        {
            var actualResult = RobotArmService.ValidateMoveCommand("Move N");
            Assert.AreEqual(true, actualResult);
        }
        [TestMethod]
        public void ValidateMoveCommandWithInvalidDirection()
        {
            var actualResult = RobotArmService.ValidateMoveCommand("Move K");
            Assert.AreEqual(false, actualResult);
        }
        [TestMethod]
        public void ValidateMoveCommandWithDiffCommand()
        {
            var actualResult = RobotArmService.ValidateMoveCommand("etcr K");
            Assert.AreEqual(false, actualResult);
        }
        [TestMethod]
        public void ValidateMoveCommandWithWrongFormat()
        {
            var actualResult = RobotArmService.ValidateMoveCommand("MoveK");
            Assert.AreEqual(false, actualResult);
        }
        [TestMethod]
        public void ValidateMoveCommandWithCaseSensitive()
        {
            var actualResult = RobotArmService.ValidateMoveCommand("move N");
            Assert.AreEqual(true, actualResult);
        }
        [TestMethod]
        public void ValidateMoveCommandWithIntDirection()
        {
            var actualResult = RobotArmService.ValidateMoveCommand("move 0");
            Assert.AreEqual(false, actualResult);
        }
        [TestMethod]
        public void ValidatePlaceCommandWithCorrectFormat()
        {
            var actualResult = RobotArmService.ValidatePlaceCommandFormat("place 0,0");
            Assert.AreEqual(true, actualResult);
        }
        [TestMethod]
        public void ValidatePlaceCommandWithInCorrectFormat()
        {
            var actualResult = RobotArmService.ValidatePlaceCommandFormat("place a,b");
            Assert.AreEqual(false, actualResult);
        }
        [TestMethod]
        public void ValidatePlaceCommandWithNoSpace()
        {
            var actualResult = RobotArmService.ValidatePlaceCommandFormat("placea,b");
            Assert.AreEqual(false, actualResult);
        }
        [TestMethod]
        public void ValidatePlaceCommandWithDiffCommand()
        {
            var actualResult = RobotArmService.ValidatePlaceCommandFormat("detect ,b");
            Assert.AreEqual(false, actualResult);
        }
        [TestMethod]
        public void ValidatePlaceCommandWithCaseInsensitive()
        {
            var actualResult = RobotArmService.ValidatePlaceCommandFormat("PLACE 2,1");
            Assert.AreEqual(true, actualResult);
        }
        [TestMethod]
        public void ValidatePlaceCommandWithOutOfBound()
        {
            var actualResult = RobotArmService.ValidatePlaceCommandFormat("PLACE 5,5");
            Assert.AreEqual(false, actualResult);
        }
    }
}
