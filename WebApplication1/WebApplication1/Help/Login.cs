namespace WebApplication1.Help
{
    public class Login
    {
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
        //public Answer Answer { get; set; }

        public Login() { 
        
        }

        //public Login(Answer answer)
        //{
        //    Answer = answer;
        //}

        public Login(string accessToken, string refreshToken)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
            //Answer = answer;
        }
    }
}
