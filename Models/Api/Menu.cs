namespace luncher_api.Models.Api
{
	public enum MenuType
	{
		Standard,
		PDF
	}

	public class PDFInfo
	{
		public string url { get; set; }
		public int[] pages { get; set; }
		public byte[] content { get; set; }
	}

	public class Meal
	{
		public string name { get; set; }
		public string price { get; set; }
	}

	public class MenuSection
	{
		public string name { get; set; }
		public Meal[] meals { get; set; }
	}

	public class Menu
	{
		public Restaurant restaurant { get; set; }
		public MenuSection[] menus { get; set; }
		public MenuType type { get; set; }
		public PDFInfo pdfInfo { get; set; }
	}

}