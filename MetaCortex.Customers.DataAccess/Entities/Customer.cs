namespace MetaCortex.Customers.DataAccess.Entities
{
    public class Customer : EntityBase
    {
        public string Name { get; set; } = default!;
        public string Email { get; set; } = default!;
        public bool IsVip { get; set; }
        public bool AllowNotifications { get; set; }
    }
}
