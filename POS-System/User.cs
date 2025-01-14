namespace POS_System
{
    public enum UserRole
    {
        Admin,
        Cashier
    }

    public abstract class User
    {
        public string Name { get; }
        public UserRole Role { get; }

        protected User(string name, UserRole role)
        {
            Name = name;
            Role = role;
        }
    }
}