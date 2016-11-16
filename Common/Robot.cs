using Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common {
    public class Robot {

        #region Static Variables
        //Extendable to more directions, such as North-East
        private static List<MoveVector> Directions = new List<MoveVector>{ 
            new MoveVector{Name = "NORTH", X = 0, Y = 1},
            new MoveVector{Name = "EAST", X = 1, Y = 0},
            new MoveVector{Name = "SOUTH", X = 0, Y = -1},
            new MoveVector{Name = "WEST", X = -1, Y = 0}
        };

        public static class Commands {
            public static readonly String Move = "MOVE";
            public static readonly String Place = "PLACE";
            public static readonly String Left = "LEFT";
            public static readonly String Right = "RIGHT";
            public static readonly String Report = "REPORT";
        }
        #endregion

        #region Properties
        /// <summary>
        /// The Table the robot exists on, or will be placed on when the PLACE command is called
        /// </summary>
        public Table Table { get; set; }

        /// <summary>
        /// Whether or not the robot has been placed on the table
        /// </summary>
        public Boolean Placed { get; set; }

        public TablePosition Position { get; set; }
        public MoveVector Direction { get; set; }
        #endregion


        public Robot(Table table) {
            this.Table = table;
        }

        public String Command(String command) {
            if (command.IndexOf(Commands.Place) == 0) {
                if (command.Length < Commands.Place.Length + 1) {
                    throw new Exception("No parameters were passed.");
                }
                String[] placeParams = command.Substring(Commands.Place.Length + 1).Split(',');
                var placePosition = new TablePosition();

                if (placeParams.Length < 3) {
                    throw new Exception("Not enough parameters. Three parameters are required.");
                }

                try {
                    placePosition.X = Int32.Parse(placeParams[0]);
                } catch {
                    throw new Exception(placeParams[0] + " is not a valid number.");
                }

                try {
                    placePosition.Y = Int32.Parse(placeParams[1]);
                } catch {
                    throw new Exception(placeParams[1] + " is not a valid number.");
                }

                var direction = Robot.Directions.FirstOrDefault(d => d.Name == placeParams[2]);
                if (direction == null) {
                    throw new Exception("Could not find direction " + placeParams[2] + ".");
                }

                if (this.Table.Move(this, placePosition)) {
                    this.Placed = true;
                    this.Direction = direction;
                }

            } else if (command == Commands.Move) {
                Move(this.Direction);
            } else if (command == Commands.Left) {
                Turn(-1);
            } else if (command == Commands.Right) {
                Turn(1);
            } else if (command == Commands.Report) {
                if (this.Placed == false) {
                    throw new Exception("The robot has not been placed.");
                }
                return this.Position.X + "," + this.Position.Y + "," + this.Direction.Name;
            } else {
                //Specialise exception to be CommandException etc depending on needs
                throw new UnknownCommandException("Command not recognised.");
            }
            return "";
        }

        public Boolean Move(MoveVector vector) {
            if (this.Placed == false) {
                //Specialise exception to be MoveException etc depending on needs
                //Would normally throw exception, in this case, the command will just be ignored
                //throw new Exception("The robot has not been placed.");
                return false;
            }
            var newPosition = new TablePosition {
                X = this.Position.X,
                Y = this.Position.Y
            };
            newPosition.X += vector.X;
            newPosition.Y += vector.Y;
            return this.Table.Move(this, newPosition);


            //Check with the table that the position is valid

        }

        /// <summary>
        /// Rotates the robot in the specified direction by the specified amount
        /// </summary>
        /// <param name="direction">Direction in which to turn, positive number indicates clockwise, negative number indicates anticlockwise, magnitude is number of turns to rotate</param>
        public void Turn(Int32 direction) {
            if (this.Placed == false) {
                //Specialise exception to be TurnException etc depending on needs
                //Would normally throw exception, in this case, the command will just be ignored
                //throw new Exception("The robot has not been placed.");
                return;
            }
            var currentDirection = Robot.Directions.IndexOf(this.Direction);
            var newDirection = (currentDirection + direction) % Robot.Directions.Count;
            if (newDirection < 0) {
                newDirection += Robot.Directions.Count;
            }
            this.Direction = Robot.Directions[newDirection];
        }
    }
}
