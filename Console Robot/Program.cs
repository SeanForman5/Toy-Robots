using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Robot {
    class Program {
        static void Main(string[] args) {
            Table table = new Table();
            Robot robot = new Robot(table);

            while (true) {
                try { 
                Console.WriteLine(
                    robot.Command(
                        Console.ReadLine()
                    )
                    );
                } catch(Exception ex) {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
