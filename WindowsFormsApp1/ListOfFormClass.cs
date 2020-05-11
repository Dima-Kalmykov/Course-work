using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public static class ListOfFormClass
    {
        public static List<ChartForTestingProgram> chartForTestingProgramList =
            new List<ChartForTestingProgram>();


        public static int Pointer;
        public static void SetSwitcherTools(List<ChartForTestingProgram> newList, int formCounter)
        {
            chartForTestingProgramList = newList.GetRange(0, newList.Count);
            Pointer = formCounter;
        }
    }
}
