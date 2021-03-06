using System.Collections.Generic;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using koalunch_api.Models;

namespace koalunch_api.Repositories
{
	public class GoogleOptions
	{
		public string AppName { get; set; }
		public string SpreadsheetId { get; set; }
		public string Range { get; set; }

		public string CredentialJson { get; set; }
	}

	public class GoogleFeedbackRepository : FeedbackRepository
	{
		private string[] _scopes = { SheetsService.Scope.Spreadsheets };
		private SheetsService _service;
		private GoogleOptions _options;

		public GoogleFeedbackRepository(GoogleOptions options)
		{
			_options = options;
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
			var credential = GoogleCredential
				.FromJson(_options.CredentialJson)
				.CreateScoped(_scopes);

			_service = new SheetsService(
				new BaseClientService.Initializer()
				{
					HttpClientInitializer = credential,
					ApplicationName = _options.AppName
				}
			);
		}

		private void InsertData(FeedbackItem feedback)
		{
			var requestBody = new ValueRange()
			{
				MajorDimension = "ROWS"
			};

			requestBody.Values = new List<IList<object>> {
				new List<object> { feedback.Name, feedback.RestaurantName, feedback.RestaurantUrl, feedback.Note }
			};

			var request = _service.Spreadsheets.Values.Append(requestBody, _options.SpreadsheetId, _options.Range);
			request.ValueInputOption = SpreadsheetsResource.ValuesResource.AppendRequest.ValueInputOptionEnum.RAW;
			request.InsertDataOption = SpreadsheetsResource.ValuesResource.AppendRequest.InsertDataOptionEnum.INSERTROWS;

			var response = request.Execute();
		}
	}
}