using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using koalunch_api.Models;

namespace koalunch_api.Repositories
{
	public class GoogleFeedbackRepository : FeedbackRepository
	{
		private string[] _scopes = { SheetsService.Scope.Spreadsheets };
		private string _appName = "Koalunch";
		private string _spreadsheetId = "1gCPjo4C3pfcpHPdjjN1gORA35cZe_6oujAIwKxek2mo";
		private SheetsService _service;

		public GoogleFeedbackRepository()
		{
		}

		public async Task<FeedbackItem[]> GetAll()
		{
			return await Task.FromResult(
				new FeedbackItem[] { }
			);
		}

		public async Task Commit(FeedbackItem feedback)
		{
			if (_service == null)
			{
				ConnectToGoogle();
			}

			InsertData(feedback);

			await Task.CompletedTask;
		}

		private void ConnectToGoogle()
		{
			GoogleCredential credential;

			credential = GoogleCredential.FromJson(
				Environment.GetEnvironmentVariable("GOOGLE_SERVICE_CREDENTIAL")
			).CreateScoped(_scopes);

			_service = new SheetsService(
				new BaseClientService.Initializer()
				{
					HttpClientInitializer = credential,
					ApplicationName = _appName
				}
			);
		}

		private void InsertData(FeedbackItem feedback)
		{
			var range = "Feedback!A1:D";
			var requestBody = new ValueRange()
			{
				MajorDimension = "ROWS"
			};

			requestBody.Values = new List<IList<object>> {
				new List<object> { feedback.Name, feedback.RestaurantName, feedback.RestaurantUrl, feedback.Note }
			};

			var request = _service.Spreadsheets.Values.Append(requestBody, _spreadsheetId, range);
			request.ValueInputOption = SpreadsheetsResource.ValuesResource.AppendRequest.ValueInputOptionEnum.RAW;
			request.InsertDataOption = SpreadsheetsResource.ValuesResource.AppendRequest.InsertDataOptionEnum.INSERTROWS;

			var response = request.Execute();
		}
	}
}