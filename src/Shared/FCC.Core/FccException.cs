using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCC.Core
{
	public class FCCException : Exception
	{
		public FCCException(string errorMessage): base(errorMessage) { }

	}
}
