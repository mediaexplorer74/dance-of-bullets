using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Windows.UI.Xaml.Markup;


namespace DoB.Xaml
{
	[ContentProperty]
	public class Constant<T> : ConstantBase
	{
		public T Value { get; set; }

		public override object GetValue()
		{
			return Value;
		}
	}
}
