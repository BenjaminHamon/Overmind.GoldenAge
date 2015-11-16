using System;

namespace Overmind.GoldenAge.Unity
{
	public class Selection<T> where T : ISelectable
	{
		public event Action<Selection<T>> Changed;

		private T item;
		public T Item
		{
			get { return item; }
			set
			{
				if (item != null)
					item.IsSelected = false;
				item = value;
				if (item != null)
					item.IsSelected = true;
				if (Changed != null)
					Changed(this);
			}
		}
	}
}
