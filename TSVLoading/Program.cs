// See https://aka.ms/new-console-template for more information

using OncologieApplicatie.Core.Controllers;
using System.Net.Http;





class Program
	{
		static async Task Main(string[] args)
		{
			await GeneController.PostRequest();
		}
	}

