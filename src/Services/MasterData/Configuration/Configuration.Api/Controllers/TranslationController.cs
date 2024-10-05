using Asp.Versioning;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Resources;

namespace Configuration.Api.Controllers
{

	/// <summary>
	/// Translation service to perform language translation services
	/// </summary>
	[ApiVersion("1.0")]
	[Route("api/v{version:apiVersion}/[controller]")]
	[ApiController]
	public class TranslationController : ControllerBase
	{
		/// <summary>
		/// Get all translations for the application for the given language
		/// </summary>
		/// <param name="language">Two digit ISO code of the language</param>
		/// <returns>List of resources and its language translations</returns>
		[HttpGet]
		public IActionResult Get(string language)
		{
			//This is a mockup to return static data. later needs to be integrated with backend
			Dictionary<string, string> result;
			switch(language.ToLower())
			{
				case "es":
					result = new Dictionary<string, string>
					{
						{ "CPQ", "CPQ" },
						{ "Schedule", "Cronograma"},
						{ "ETicket", "Boleto electrónico"},
						{ "Invoice", "Factura"},
						{ "Inventory", "Inventario"},
						{ "Safety", "Seguridad"},
						{ "BI", "BI"},
						{ "Setting", "Configuración"},
						{ "WhatsNew", "What's New"},
						{ "LogOut", "Cerrar sesión"},
						{ "CPQ.CreateQuoteFooters.Name", "Nombre" },
						{ "CPQ.CreateQuoteFooters.Company", "Compañía(s)" },
						{ "CPQ.CreateQuoteFooters.Status", "Estado" },
						{ "CPQ.CreateQuoteFooters.Module", "Módulo(s)" },
					};
					break;
				case "en":
				default:
					result = new Dictionary<string, string>
					{
						{ "CPQ", "CPQ" },
						{ "Schedule", "Schedule"},
						{ "ETicket", "E-Ticket"},
						{ "Invoice", "Invoice"},
						{ "Inventory", "Inventory"},
						{ "Safety", "Safety"},
						{ "BI", "BI"},
						{ "Setting", "Setting"},
						{ "WhatsNew", "What's New"},
						{ "LogOut", "Log Out"},
						{ "CPQ.CreateQuoteFooters.Name", "Name" },
						{ "CPQ.CreateQuoteFooters.Company", "Company(ies)" },
						{ "CPQ.CreateQuoteFooters.Status", "Status" },
						{ "CPQ.CreateQuoteFooters.Module", "Module(s)" },
					};
					break;
			}

			return Ok(result);
		}
	}
}
