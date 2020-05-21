using System.Collections.Generic;

namespace WindowsFormsApp1
{
    public static class ListOfFormsForTestingProgram
    {
        public static List<ChartDisplayForTestingProgramForm> ChartFormsForTestingProgram =
            new List<ChartDisplayForTestingProgramForm>();

        public static int Pointer;

        /// <summary>
        /// Изменяет лист и укзаатель.
        /// </summary>
        /// <param name="newForms"> Новый список форм </param>
        /// <param name="formCounter"> Количество форм </param>
        public static void SetSwitcherTools(List<ChartDisplayForTestingProgramForm> newForms, int formCounter)
        {
            ChartFormsForTestingProgram = newForms.GetRange(0, newForms.Count);
            Pointer = formCounter;
        }
    }
}
