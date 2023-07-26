using System.Security.Cryptography;
using System.Text;

namespace SVNSkillFactory112
{
    public class Program
    {
        public static Dictionary<User, string> users = new Dictionary<User, string>();
        static void Main(string[] args)
        {
            //регистрируем пользователей в системе
            users.Add(new User { Login = "catWoman", Name = "Lucy", IsPremium = false }, GetMD5("myPrivatePassword"));
            users.Add(new User { Login = "coolMan", Name = "John", IsPremium = true }, GetMD5("OhMyGod"));

            //приглашение к авторизации
            string inputLogin = "";
            string inputPassword = "";
            while (inputLogin?.Length <= 0)
            {
                Console.WriteLine("Введите ваш логин");
                inputLogin = Console.ReadLine();
            }
            while (inputPassword?.Length <= 0)
            {
                Console.WriteLine("Введите ваш пароль");
                inputPassword = Console.ReadLine();
            }
            SignIn(inputLogin, inputPassword);
        }

        static void SignIn(string inputLogin, string inputPassword)
        {
            foreach (var user in users)
            {
                if (inputLogin == user.Key.Login)
                {
                    string tempHashPassword = GetMD5(inputPassword);
                    if (tempHashPassword == user.Value)
                    {
                        Console.WriteLine($"Здравствуйте, {user.Key.Name}");
                        if (!user.Key.IsPremium)
                        {
                            ShowAds();
                        }
                        return;
                    }
                    ShowAuthenticationFailureMessage();
                    return;
                }
            }
            ShowAuthenticationFailureMessage();
        }

        static void ShowAuthenticationFailureMessage()
        {
            Console.WriteLine("Неправильно введён логин или пароль");
        }

        static string GetMD5(string inputPassword)
        {
            StringBuilder sb = new StringBuilder();
            using (MD5 md5 = MD5.Create())
            {
                byte[] hashValue = md5.ComputeHash(Encoding.UTF8.GetBytes(inputPassword));
                foreach (byte b in hashValue)
                {
                    sb.Append($"{b:X2}");
                }
            }
            return sb.ToString();
        }

        static void ShowAds()
        {
            Console.WriteLine("Посетите наш новый сайт с бесплатными играми free.games.for.a.fool.com");
            // Остановка на 1 с
            Thread.Sleep(1000);

            Console.WriteLine("Купите подписку на МыКомбо и слушайте музыку везде и всегда.");
            // Остановка на 2 с
            Thread.Sleep(2000);

            Console.WriteLine("Оформите премиум-подписку на наш сервис, чтобы не видеть рекламу.");
            // Остановка на 3 с
            Thread.Sleep(3000);
        }
    }
}