namespace WeirdFlex.Data.Model
{
    public class User
    {
        public long Id { get; set; }

        public User(string displayName)
        {
            this.DisplayName = displayName;
        }

        public string DisplayName { get; set; }

        public string? Uid { get; set; }
    }
}
