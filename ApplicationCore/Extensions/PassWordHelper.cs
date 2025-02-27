namespace ApplicationCore.Extensions
{
    class PassWordHelper
    {
        /// <summary>
        /// Generate encrypted password hash (Hash)
        /// </summary>
        public static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, workFactor: 12); // 12 is the recommended calculation cost
        }

        /// <summary>
        /// Verify that the entered password matches the hash in the database
        /// </summary>
        public static bool VerifyPassword(string enteredPassword, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(enteredPassword, hashedPassword);
        }
    }
}
