namespace BookStore.Models.Requests
{
    public class UpdateAuthorRequest
    {
        public int Id { get; set; }

        public string Bio { get; set; }

        public string Name { get; set; }
    }
}
