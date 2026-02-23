namespace MYAPP
{
    public interface IAverageCalculation
    {
        int[] GetValues();
    }

    public class MyCalcApp
    {
        private readonly IAverageCalculation _avgCalc;

        public MyCalcApp(IAverageCalculation avgCalc)
        {
            _avgCalc = avgCalc;
        }

        public double CalculateAverageHO()
        {
            var values = _avgCalc.GetValues();

            int sum = 0;

            foreach (var v in values)
            {
                sum += v;
            }

            return (double)sum / values.Length;
        }
    }
}