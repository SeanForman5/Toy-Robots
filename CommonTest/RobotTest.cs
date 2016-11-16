using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Common;
using Common.Exceptions;

namespace CommonTest {
    [TestClass]
    public class RobotTest {
        [TestMethod]
        public void TestBoardBottom() {
            Table table = new Table();
            Robot robot = new Robot(table);
            robot.Command("PLACE 1,-1,NORTH");
            Assert.AreEqual(robot.Placed, false);
        }

        [TestMethod]
        public void TestBoardTop() {
            Table table = new Table();
            Robot robot = new Robot(table);
            robot.Command("PLACE 1,5,NORTH");
            Assert.AreEqual(robot.Placed, false);
        }

        [TestMethod]
        public void TestBoardLeft() {
            Table table = new Table();
            Robot robot = new Robot(table);
            robot.Command("PLACE -1,1,NORTH");
            Assert.AreEqual(robot.Placed, false);
        }

        [TestMethod]
        public void TestBoardRight() {
            Table table = new Table();
            Robot robot = new Robot(table);
            robot.Command("PLACE 5,1,NORTH");
            Assert.AreEqual(robot.Placed, false);
        }

        [TestMethod]
        public void TestRotationLeft() {
            Table table = new Table();
            Robot robot = new Robot(table);

            robot.Command("PLACE 1,1,NORTH");
            robot.Command("LEFT");
            Assert.AreEqual(robot.Direction.Name, "WEST");
        }
        [TestMethod]
        public void TestRotationRight() {
            Table table = new Table();
            Robot robot = new Robot(table);

            robot.Command("PLACE 1,1,NORTH");
            robot.Command("RIGHT");
            Assert.AreEqual(robot.Direction.Name, "EAST");
        }
        [TestMethod]
        public void TestRotationLeftTwice() {
            Table table = new Table();
            Robot robot = new Robot(table);

            robot.Command("PLACE 1,1,NORTH");
            robot.Command("LEFT");
            robot.Command("LEFT");
            Assert.AreEqual(robot.Direction.Name, "SOUTH");
        }

        [TestMethod]
        public void TestMovement() {
            Table table = new Table();
            Robot robot = new Robot(table);

            robot.Command("PLACE 1,1,NORTH");
            robot.Command("MOVE");
            Assert.AreEqual(robot.Position.X, 1);
            Assert.AreEqual(robot.Position.Y, 2);
        }

        [TestMethod]
        public void RejectUnplacedCommands() {
            Table table = new Table();
            Robot robot = new Robot(table);
            robot.Command("MOVE");
            robot.Command("RIGHT");

            Assert.AreEqual(robot.Position, null);
            Assert.AreEqual(robot.Placed, false);
        }

        [TestMethod]
        [ExpectedException(typeof(UnknownCommandException))]
        public void RejectUnknownCommands() {
            Table table = new Table();
            Robot robot = new Robot(table);
            robot.Command("nrtmyt");
        }

        [TestMethod]
        public void RejectFallingOff() {
            Table table = new Table();
            Robot robot = new Robot(table);

            robot.Command("PLACE 1,4,NORTH");

            Assert.AreEqual(robot.Position.Y, 4);
            Assert.AreEqual(robot.Position.X, 1);
        }
        [TestMethod]
        public void PlaceAgain() {
            Table table = new Table();
            Robot robot = new Robot(table);

            robot.Command("PLACE 1,4,NORTH");

            robot.Command("PLACE 2,3,NORTH");

            Assert.AreEqual(robot.Position.Y, 3);
            Assert.AreEqual(robot.Position.X, 2);
        }

        [TestMethod]
        public void Scenario1() {
            Table table = new Table();
            Robot robot = new Robot(table);

            robot.Command("PLACE 0,0,NORTH");

            robot.Command("MOVE");

            Assert.AreEqual(robot.Command("REPORT"), "0,1,NORTH");
        }

        [TestMethod]
        public void Scenario2() {
            Table table = new Table();
            Robot robot = new Robot(table);

            robot.Command("PLACE 0,0,NORTH");

            robot.Command("LEFT");

            Assert.AreEqual(robot.Command("REPORT"), "0,0,WEST");
        }

        [TestMethod]
        public void Scenario3() {
            Table table = new Table();
            Robot robot = new Robot(table);

            robot.Command("PLACE 1,2,EAST");

            robot.Command("MOVE");

            robot.Command("MOVE");

            robot.Command("LEFT");

            robot.Command("MOVE");

            Assert.AreEqual(robot.Command("REPORT"), "3,3,NORTH");
        }
    }
}
