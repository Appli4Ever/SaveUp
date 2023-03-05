using SaveUp.ViewModels;

namespace SaveUp
{
    public class UserIdentity : ViewModelBase
    {
        private string? username;

        public string? Username
        {
            get => this.username;
            set
            {
                if (Equals(this.username, value))
                {
                    return;
                }

                this.SetField(ref this.username, value);
            }
        }

        private bool isLoggedIn;

        public bool IsLoggedIn
        {
            get => this.isLoggedIn;
            set
            {
                this.SetField(ref this.isLoggedIn, value);
                this.OnPropertyChanged(nameof(this.IsLoggedOut));
            }
        }

        public bool IsLoggedOut => !this.isLoggedIn;
    }
}
