using System;

namespace NoRealm.Phi.Metadata.Test.Data
{
    public class User
    {
        public const int MaxUsers = 10;
        public readonly int SomeValue = 20;

        public Guid userId = Guid.Empty;
        public string name = string.Empty;
        public bool isActive = true;
        public DateTime dob = DateTime.MinValue;

        public event EventHandler UserCreated;

        public User() { }

        public User(Guid userId) => this.userId = userId;

        public User(Guid userId, string name) : this(userId)
            => this.name = name;

        public User(Guid userId, string name, bool isActive) : this(userId, name)
            => this.isActive = isActive;

        public User(Guid userId, string name, bool isActive, DateTime dob) : this(userId, name, isActive)
            => this.dob = dob;

        public Guid UserId
        {
            get => userId;
            set => userId = value;
        }

        public string Name
        {
            get => name;
            set => name = value;
        }

        public bool IsActive
        {
            get => isActive;
            set => isActive = value;
        }

        public DateTime BirthDate
        {
            get => dob;
            set => dob = value;
        }

        public string WriteOnly
        {
            set
            {
                return;
            }
        }

        public string ReadOnly => nameof(ReadOnly);

        public void OnUserCreated()
        {
            UserCreated?.Invoke(this, EventArgs.Empty);
        }

        public int GetOldAge(int age)
        {
            return age + 5;
        }

        public int GetOldAge()
        {
            return 10;
        }

        public void GetName(out string name)
        {
            name = nameof(User);
        }

        public int GetAge(out int age)
        {
            age = 7;
            return int.MaxValue;
        }
    }
}
