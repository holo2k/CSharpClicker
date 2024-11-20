namespace CSharpClicker.Web.UseCases.GetBoosts
{
    public class BoostDto
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public long Price { get; set; }
        public long Profit { get; set; }
        public byte[] Image { get; set; }
        public bool IsAuto { get; set; }
    }
}
