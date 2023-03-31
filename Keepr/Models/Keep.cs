namespace Keepr.Models
{
    public class Keep
    {
        public int Id { get; set; }
        public string CreatorId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Img { get; set; }
        public int Views { get; set; }

        // kept as an int probably goes here as a "virtual", but I am not exactly sure how this differs from Views yet. 
        public Profile Creator { get; set; }
    }
}