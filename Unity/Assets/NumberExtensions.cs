using System;
using System.Collections.Generic;

namespace Overmind.GoldenAge.Unity
{
	public static class NumberExtensions
	{
		private static IList<string> ThousandsUnits = new List<string>() { "", "K", "M", "G" };

		public static string Format(this int number)
		{
			if (number < 1000)
				return number.ToString();
			return Format(number / 1000d, 1);
		}

		private static string Format(double number, int thousandMultiplier)
		{
			// Truncate to avoid the string format rounding, which messes up our format
			if (number < 10)
				return String.Format("{0:0.00}{1}", Math.Truncate(number * 100) / 100, ThousandsUnits[thousandMultiplier]);
			if (number < 100)
				return String.Format("{0:0.0}{1}", Math.Truncate(number * 10) / 10, ThousandsUnits[thousandMultiplier]);
			if (number < 1000)
				return String.Format("{0:0}{1}", Math.Truncate(number), ThousandsUnits[thousandMultiplier]);
			return Format(number / 1000, thousandMultiplier + 1);

		}
	}
}
