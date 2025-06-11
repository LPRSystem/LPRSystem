namespace LPRSystem.Web.UI.Models
{
    public class StateViewModel
    {
        public StateViewModel()
        {
            countries = new List<Country>();
        }
        public long StateId { get; set; }
        public long? CountryId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? StateCode { get; set; }
        public List<Country> countries { get; set; }
    }
}
