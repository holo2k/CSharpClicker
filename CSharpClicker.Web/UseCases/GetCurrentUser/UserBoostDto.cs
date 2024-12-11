namespace CSharpClicker.Web.UseCases.GetCurrentUser
{
    public class UserBoostDto
    {
        public int BoostId { get; init; }
        public int CurrentPrice { get; set; }
        public int Quantity { get; set; }
    }
    
}
