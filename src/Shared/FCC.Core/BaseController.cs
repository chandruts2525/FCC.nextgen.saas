using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCC.Core
{
	public class BaseController<T> :ControllerBase
	{
		public BaseController(ILogger<T> logger)
		{ 
		
		}
	}
}
