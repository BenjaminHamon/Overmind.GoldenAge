﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Overmind.GoldenAge.Console
{
	public class GridView<TItem>
	{
		public GridView(TextWriter output)
		{
			if (output == null)
				throw new ArgumentNullException("output", "[GridView.Constructor] Output must not be null.");
			this.output = output;
		}

		private readonly TextWriter output;

		public int GridWidth;
		public int GridHeight;
		public int DisplayWidth;
		public int DisplayHeight;

		/// <summary>Function to retrieve the items to display.</summary>
		public Func<IEnumerable<TItem>> ItemSource;

		/// <summary>Function to retrieve the item position in the grid.</summary>
		public Func<TItem, IEnumerable<double>> PositionGetter;

		/// <summary>Function to convert an item to a string representation which can be displayed in the grid.</summary>
		public Func<TItem, string> Descriptor;

		/// <summary>Action executed before an element is drawn.</summary>
		public Action<TItem> PreWrite;

		/// <summary>Action executed after an element is drawn.</summary>
		public Action<TItem> PostWrite;

		public bool DrawAxis = true;
		public char ColumnSeparator = '|';
		public char RowSeparator = '-';

		//private string CreateCompleteRowSeparator()
		//{
		//	if (String.IsNullOrEmpty(RowSeparator))
		//		return "";

		//	StringBuilder builder = new StringBuilder();
		//	for (int i = 0; i < Width / RowSeparator.Length; i++)
		//		builder.Append(RowSeparator);
		//	return builder.ToString();
		//}

		private double step = 1;
		public double Step
		{
			get { return step; }
			set
			{
				if (value <= 0)
					throw new ArgumentOutOfRangeException("value", value, "Step must be strictly positive");
				step = value;
			}
		}

		private int cellSize = 5;
		public int CellSize
		{
			get { return cellSize; }
			set
			{
				if (value <= 0)
					throw new ArgumentOutOfRangeException("value", value, "Cell size must be strictly positive");
				cellSize = value;
			}
		}

		//public void Draw(IReadOnlyList<double> lookAt,
		//	IEnumerable<TItem> itemCollection, Func<TItem, IEnumerable<double>> positionGetter, Func<TItem, string> descriptor)
		//{
		//	double left = (lookAt[0] - Width / 2) / (CellSize + ColumnSeparator.Length) + (DrawAxis ? 0.5 : 0);
		//	double right = (lookAt[0] + Width / 2) / (CellSize + ColumnSeparator.Length) - (DrawAxis ? 0.5 : 0);
		//	double top = (lookAt[1] + Height / 2) / CellSize;
		//	double bottom = (lookAt[1] - Height / 2) / CellSize + (DrawAxis ? 1 : 0);

		//	Draw((int)left, (int)right, (int)bottom, (int)top, itemCollection, positionGetter, descriptor);
		//}

		public void Draw(IReadOnlyList<double> origin)
		{
			Draw(origin[0], DisplayWidth / (CellSize + 1 /* Separator */) - 1 - (DrawAxis ? 1 : 0),
				origin[1], DisplayHeight / (1 /* Cell height */ + 1 /* Separator */) - 1 - (DrawAxis ? 1 : 0));
		}

		public void Draw(double columnMin, double columnMax, double rowMin, double rowMax)
		{
			IEnumerable<TItem> itemCollection = ItemSource();
			columnMin = Math.Max(columnMin, 0);
			columnMax = Math.Min(columnMax, GridHeight - 1);
			rowMin = Math.Max(rowMin, 0);
			rowMax = Math.Min(rowMax, GridWidth - 1);

			//string rowCompleteSeparator = CreateCompleteRowSeparator();
			string rowCompleteSeparator = new String(RowSeparator, (CellSize + 1 /* Separator */)
				* ((int)Math.Floor((columnMax - columnMin) / Step) + (DrawAxis ? 1 : 0)) + CellSize);

			for (double rowIndex = rowMax; rowIndex >= rowMin; rowIndex -= Step)
			{
				if (rowIndex != rowMax)
					output.WriteLine(rowCompleteSeparator);

				if (DrawAxis)
					output.Write(Pad(rowIndex.ToString()) + ColumnSeparator);

				for (double columnIndex = columnMin; columnIndex <= columnMax; columnIndex += Step)
				{
					if (columnIndex != columnMin)
						output.Write(ColumnSeparator);
					double[] position = { columnIndex, rowIndex };
					TItem item = itemCollection.FirstOrDefault(x => PositionGetter(x).SequenceEqual(position));
					if (EqualityComparer<TItem>.Default.Equals(item, default(TItem)) == false)
						DrawItem(item);
					else
						output.Write(Pad(""));
				}

				output.WriteLine();

			}

			if (DrawAxis)
			{
				output.WriteLine(rowCompleteSeparator);

				output.Write(Pad("") + ColumnSeparator);
				for (double columnIndex = columnMin; columnIndex <= columnMax; columnIndex += Step)
				{
					if (columnIndex != columnMin)
						output.Write(ColumnSeparator);
					output.Write(Pad(columnIndex.ToString()));
				}
				output.WriteLine();
			}
		}

		private void DrawItem(TItem item)
		{
			if (PreWrite != null)
				PreWrite(item);
			output.Write(Pad(Descriptor(item)));
			if (PostWrite != null)
				PostWrite(item);
		}

		private string Pad(string content)
		{
			int totalPadding = CellSize - content.Length;
			if (totalPadding <= 0)
				return content.Substring(0, CellSize);
			else
			{
				int leftPadding = totalPadding / 2;
				return new String(' ', leftPadding) + content + new String(' ', totalPadding - leftPadding);
			}
		}
	}
}
