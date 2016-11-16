using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common {
    public class Table {
        public Robot[,] Positions { get; set; }

        public Table() {
            Positions = new Robot[5,5];
        }

        //public Table(Int32 Width, Int32 Height) {
        //    Positions = new Robot[Width, Height];
        //}

        /// <summary>
        /// This will check if the passed robot can move to a given position, and will move it and apply the positions if it can, or will throw an exception.
        /// </summary>
        /// <param name="robot">The robot that is attempting to move</param>
        /// <param name="position">The position the robot is attempting to move to</param>
        /// <returns>true, indicating the robot was moved successfully. An exception is thrown otherwise.</returns>
        public Boolean Move(Robot robot, TablePosition position) {
            if(position.X < 0 || position.X >= this.Positions.GetLength(0)
                || position.Y < 0 || position.Y >= this.Positions.GetLength(1)) {
                    return false;
            }

            var currentlyThere = this.Positions[position.X, position.Y];
            //If the space is not empty, and robot there is not the moving robot, throw an exception
            if (currentlyThere != null && currentlyThere != robot) {
                throw new Exception("Position is not empty.");
            }

            if (robot.Placed == true) {
                //Remove it from its current position
                this.Positions[robot.Position.X, robot.Position.Y] = null;
            }

            currentlyThere = robot;
            robot.Position = position;

            return true;
        }
    }
}
