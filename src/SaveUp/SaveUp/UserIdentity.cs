using SaveUp.ViewModels;

namespace SaveUp
{
    public class UserIdentity : ViewModelBase
    {
        public UserIdentity()
        {
            this.CheckForLogin();
        }

        public void CheckForLogin()
        {
            var jwt = Preferences.Get("JWT", null);
            var usernmae = Preferences.Get("Username", null);

            if (jwt is null || usernmae is null)
            {
                this.IsLoggedIn = false;
            }

            this.IsLoggedIn = true;
            this.Username = usernmae;
        }

        private string? username;

        public string? Username
        {
            get => this.username;
            private set
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
            private set
            {
                this.SetField(ref this.isLoggedIn, value);
                this.OnPropertyChanged(nameof(this.IsLoggedOut));
            }
        }

        public bool IsLoggedOut => !this.isLoggedIn;
    }
}
