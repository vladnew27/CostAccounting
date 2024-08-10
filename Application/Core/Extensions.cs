namespace CostAccounting.Core
{
    public static class Extensions
    {
        public static double WeightedAverage<T>(this IEnumerable<T> records, Func<T, double> value, Func<T, double> weight)
        {
            if (records == null)
                throw new ArgumentNullException(nameof(records), $"{nameof(records)} is null.");

            int count = 0;
            double valueSum = 0;
            double weightSum = 0;

            foreach (var record in records)
            {
                count++;
                double recordWeight = weight(record);

                valueSum += value(record) * recordWeight;
                weightSum += recordWeight;
            }

            if (count == 0)
                throw new ArgumentException($"{nameof(records)} is empty.");

            if (count == 1)
                return value(records.Single());

            if (weightSum != 0)
                return valueSum / weightSum;
            else
                throw new DivideByZeroException($"Division of {valueSum} by zero.");
        }

    }
}