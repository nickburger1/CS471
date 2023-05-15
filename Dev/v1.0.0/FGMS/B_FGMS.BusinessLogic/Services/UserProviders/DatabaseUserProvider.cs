using A_FGMS.DataLayer;
using A_FGMS.DataLayer.Entities;
using B_FGMS.BusinessLogic.Constants;
using B_FGMS.BusinessLogic.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Data.SqlClient;
using System.Security.Cryptography;


namespace B_FGMS.BusinessLogic.Services.UserProviders
{
    /// <summary>
    /// Class Name: DatabaseUserProvider
    /// Created By: Kiefer Thorson, Nathan VanSnepson & Richard Nader, Jr.
    /// Date Created: 2/16/2023
    /// Additional Contributors: Kiefer Thorson & Nathan VanSnepson
    /// Last Modified: 2/18/2023
    /// Last Modified By: Richard Nader, Jr.
    /// 
    /// Purpose:
    /// The purpose of this class is to provide connection to Users table in database
    /// </summary>
    public class DatabaseUserProvider : IUserProvider
    {
        private readonly ApplicationDbContext _dbContext;

        public event EventHandler<Events.ErrorEventArgs> DatabaseError;


        /// <summary>
        /// Function Name: DatabaseUserProvider
        /// Created By: Kiefer Thorson, Nathan VanSnepson & Richard Nader, Jr.
        /// Date Created: 2/16/2023
        /// Additional Contributors:
        /// Last Modified: 2/16/2023
        /// Last Modified By: Kiefer Thorson & Nathan VanSnepson
        /// 
        /// Purpose:
        /// The Purpose of this Function is to:
        ///     - Constructor for the User Provider. Sets the Context.
        /// </summary>
        /// <param name="dbContext"></param>
        public DatabaseUserProvider(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// When called with invoke an event to inform user of an error
        /// </summary>
        /// <param name="errorMessage"></param>
        /// <param name="errorCode"></param>
        /// <author>Nathan VanSnepson</author>
        /// <created>4/4/23</created>
        private void OnDatabaseError(string errorMessage, string errorCode)
        {
            DatabaseError?.Invoke(this, new Events.ErrorEventArgs(errorMessage, errorCode));
        }

        /// <summary>
        /// Function Name: GetAllUsers
        /// Created By: Kiefer Thorson, Nathan VanSnepson & Richard Nader, Jr.
        /// Date Created: 2/16/2023
        /// Additional Contributors:
        /// Last Modified: 2/16/2023
        /// Last Modified By: Kiefer Thorson & Nathan VanSnepson
        /// 
        /// Purpose:
        /// The Purpose of this Function is to:
        ///     - Retrieve all users from the database
        /// </summary>
        /// <returns>List of User's</returns>
        public IEnumerable<UserModel> GetAllUsers()
        {
            try
            {
                return _dbContext.Users.Select(x =>
                    new UserModel
                    {
                        Tuid = x.Tuid,
                        Name = x.Name,
                        Email = x.Email,
                        PhoneNumber = x.PhoneNumber,
                        IsActive = x.IsActive,
                        IsAdmin = x.IsAdmin,
                        IsReadOnly = x.IsReadOnly
                    }).ToList();
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1400._message, ErrorMessages._1400._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1401._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1401._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1402._message + e.Message, ErrorMessages._1402._code);
            }

            return new List<UserModel>();
        }

        /// <summary>
        /// Function Name: CreateUser
        /// Created By: Kiefer Thorson, Nathan VanSnepson & Richard Nader, Jr.
        /// Date Created: 2/16/2023
        /// Additional Contributors:
        /// Last Modified: 2/18/2023
        /// Last Modified By: Richard Nader, Jr.
        /// 
        /// Purpose:
        /// The Purpose of this Function is to:
        ///     - Create a new user entry into the database
        /// </summary>
        /// <param name="user">User to add</param>
        /// <param name="createdPassword">New password (separate from the model)</param>
        /// <exception cref="Exception">Throws exception if a user already exists with the specified email</exception>
        public void CreateUser(UserModel user, string createdPassword)
        {
            try 
            {
                // Check if user exists in the database, if not add the user
                var existingUser = _dbContext.Users.FirstOrDefault(x => x.Email.Equals(user.Email));
                if (existingUser == null)
                {
                    // Hash password
                    var hashedPassword = HashNewPassword(createdPassword);

                    // Create new user entitiy for db row
                    var newUser = new User()
                    {
                        Email = user.Email.Trim(),
                        Name = user.Name,
                        PhoneNumber = user.PhoneNumber,
                        IsActive = user.IsActive,
                        IsAdmin = user.IsAdmin,
                        HashedPassword = hashedPassword
                    };

                    // Add new user and save
                    _dbContext.Users.Add(newUser);
                    _dbContext.SaveChanges();

                    // When we save changes to the database the tuid will be generated and set in the entity that is passed to the add method.
                    // We update the passed user's tuid after saving.
                    user.Tuid = newUser.Tuid;
                }
                else
                {
                    throw new Exception($"User with Email = {user.Email} already exists");
                }
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1403._message, ErrorMessages._1403._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1404._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1404._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1405._message + e.Message, ErrorMessages._1405._code);
            }
        }

        /// <summary>
        /// Function Name: UpdateUser
        /// Created By: Kiefer Thorson, Nathan VanSnepson & Richard Nader, Jr.
        /// Date Created: 2/16/2023
        /// Additional Contributors:
        /// Last Modified: 2/18/2023
        /// Last Modified By: Richard Nader, Jr.
        /// 
        /// Purpose:
        /// The Purpose of this Function is to:
        ///     - Update existing user
        /// </summary>
        /// <param name="user">The usermodel being updated</param>
        /// <param name="updatedPassword">If </param>
        /// <exception cref="Exception">Throws exception if the user with the given tuid does not exist</exception>
        public void UpdateUser(UserModel user, string? updatedPassword = null)
        {
            try
            {
                // Check if user exists in the database, if not add the user
                var existingEmail = _dbContext.Users.FirstOrDefault(x => x.Email.Equals(user.Email));
                var existingUser = _dbContext.Users.FirstOrDefault(x => x.Tuid == user.Tuid);

                if (existingUser != null && (existingEmail != null ? existingEmail.Tuid == user.Tuid : true))
                {

                    // User exists, update any fields that might have changed
                    existingUser.Name = user.Name!;
                    existingUser.Email = user.Email!;
                    existingUser.PhoneNumber = user.PhoneNumber!;
                    existingUser.IsActive = user.IsActive;
                    existingUser.IsAdmin = user.IsAdmin;

                    // Update password if it has changed
                    if (updatedPassword != null)
                    {
                        existingUser.HashedPassword = HashNewPassword(updatedPassword);
                    }

                    // Save changes to updated entity. Entity framework automatically knows what properties have changed
                    // from the code above and will apply them to the proper entity record in the database
                    _dbContext.SaveChanges();
                }
                else
                {
                    throw new Exception($"User with Tuid = {user.Tuid} does not exists");
                }
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1406._message, ErrorMessages._1406._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1407._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1407._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1408._message + e.Message, ErrorMessages._1408._code);
            }
        }

        /// <summary>
        /// Function Name: DeleteUser
        /// Created By: Kiefer Thorson, Nathan VanSnepson & Richard Nader, Jr.
        /// Date Created: 2/16/2023
        /// Additional Contributors:
        /// Last Modified: 2/18/2023
        /// Last Modified By: Richard Nader, Jr.
        /// 
        /// Purpose:
        /// The Purpose of this Function is to:
        ///     - Deletes user from the database
        /// </summary>
        /// <param name="user"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void DeleteUser(UserModel user)
        {
            try 
            { 
                var dbUser = _dbContext.Users.FirstOrDefault(x => x.Tuid == user.Tuid);
                if (dbUser != null)
                {
                    _dbContext.Users.Remove(dbUser);
                    _dbContext.SaveChanges();
                }
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1409._message, ErrorMessages._1409._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1410._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1410._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1411._message + e.Message, ErrorMessages._1411._code);
            }
        }

        /// <summary>
        /// Function Name: TryUserPasswordLogin
        /// Created By: Richard Nader, Jr.
        /// Date Created: 2/18/2023
        /// 
        /// Purpose:
        /// The Purpose of this Function is to:
        ///     - Validate a user's email and password against the database of users
        /// </summary>
        /// <param name="email">Email of the user</param>
        /// <param name="password">The password entered by the user</param>
        /// <param name="signedInUser">If the login is successful, a signed in user object is set</param>
        /// <returns>True if the login was successful, false otherwise</returns>
        public bool TryUserPasswordLogin(string email, string password, out UserModel? signedInUser)
        {
            signedInUser = null;

            try 
            { 
                // Find user
                var user = _dbContext.Users.FirstOrDefault(x => x.Email.Equals(email));

                if (user != null)
                {
                    byte[] decodedHashedPassword = Convert.FromBase64String(user.HashedPassword);

                    // Check to ensure the hashing version is correct
                    // This should match the hashing version in HashNewPassword method
                    if (decodedHashedPassword[0] == 0x00)
                    {
                        // Get hashing settings from byte array
                        KeyDerivationPrf keyDerivation = (KeyDerivationPrf)ReadNetworkByteOrder(decodedHashedPassword, 1);
                        int itterationCount = (int)ReadNetworkByteOrder(decodedHashedPassword, 5);
                        int saltLength = (int)ReadNetworkByteOrder(decodedHashedPassword, 9);

                        // Get salt from stored hashedPassword
                        byte[] salt = new byte[saltLength];
                        Buffer.BlockCopy(decodedHashedPassword, 13, salt, 0, saltLength);

                        // Get subkey from stored hashedPassword
                        int subKeyLength = decodedHashedPassword.Length - 13 - salt.Length;
                        byte[] expectedSubKey = new byte[subKeyLength];
                        Buffer.BlockCopy(decodedHashedPassword, 13 + salt.Length, expectedSubKey, 0, expectedSubKey.Length);

                        // Hash the password being verified with settings from user's existing hashed password
                        byte[] actualSubkey = KeyDerivation.Pbkdf2(password, salt, keyDerivation, itterationCount, subKeyLength);

                        // Compare twp subKeys to see if they are equal
                        var validUser = CryptographicOperations.FixedTimeEquals(actualSubkey, expectedSubKey);

                        // If login is successful, build user model and return
                        if (validUser)
                        {
                            // Check if user is active
                            if (!user.IsActive)
                            {
                                return false;
                            }

                            signedInUser = new UserModel
                            {
                                Tuid = user.Tuid,
                                Name = user.Name,
                                Email = user.Email,
                                PhoneNumber = user.PhoneNumber,
                                IsActive = user.IsActive,
                                IsAdmin = user.IsAdmin
                            };
                            return validUser;
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1412._message, ErrorMessages._1412._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1413._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1413._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1414._message + e.Message, ErrorMessages._1414._code);
            }

            return false;
        }

        /// <summary>
        /// Function Name: HashNewPassword
        /// Created By: Richard Nader, Jr.
        /// Date Created: 2/18/2023
        /// 
        /// Purpose:
        /// The Purpose of this Function is to:
        ///     - Run the HMACSHA512 hashing algorithm on a giving plain-text password
        /// </summary>
        /// <param name="rawPassword">Plain-text password</param>
        /// <returns>Hashed Base64 password</returns>
        /// Borrowed from https://github.com/dotnet/AspNetCore/blob/main/src/Identity/Extensions.Core/src/PasswordHasher.cs
		private string HashNewPassword(string rawPassword)
        {
            // Hashing settings (DONT MESS WITH THESE)
            int saltBits = 128 / 8;
            int subKeyBits = 256;
            int iterations = 100000;
            KeyDerivationPrf keyDerivation = KeyDerivationPrf.HMACSHA512;
            byte hashVersion = 0x00;

            // Generate salt for password
            byte[] salt = RandomNumberGenerator.GetBytes(saltBits);

            // Generate sub key
            byte[] subKey = KeyDerivation.Pbkdf2(rawPassword, salt, keyDerivation, iterations, subKeyBits / 8);

            // Combine salt and subkey into one byte array
            var outputBytes = new byte[13 + salt.Length + subKey.Length];

            // This is used to version the password, if someone adds a new password hashing algorithm, this would
            // be used to determine how to verify the hash above in TryUserPasswordLogin method, but it is
            // not used at this time
            outputBytes[0] = hashVersion;

            // Write settings into hash string
            WriteNetworkByteOrder(outputBytes, 1, (uint)keyDerivation);
            WriteNetworkByteOrder(outputBytes, 5, (uint)iterations);
            WriteNetworkByteOrder(outputBytes, 9, (uint)(saltBits));

            // Copy salt and subkey to output buffer
            Buffer.BlockCopy(salt, 0, outputBytes, 13, salt.Length);
            Buffer.BlockCopy(subKey, 0, outputBytes, 13 + (saltBits), subKey.Length);

            // Convert byte array to base 64 string to be saved to db
            return Convert.ToBase64String(outputBytes);
        }

        /// <summary>
        /// Function Name: WriteNetworkByteOrder
        /// Created By: Richard Nader, Jr.
        /// Date Created: 2/18/2023
        /// 
        /// Purpose:
        /// The Purpose of this Function is to:
        ///     - Writes a uint value to a specified offset in a byte buffer
        /// </summary>
        /// <param name="buffer">Buffer to write to</param>
        /// <param name="offset">Byte offset to begin writing to in buffer</param>
        /// <param name="value">Value to write in buffer beginning at offset</param>
        /// Borrowed from https://github.com/dotnet/AspNetCore/blob/main/src/Identity/Extensions.Core/src/PasswordHasher.cs
        private static void WriteNetworkByteOrder(byte[] buffer, int offset, uint value)
        {
            buffer[offset + 0] = (byte)(value >> 24);
            buffer[offset + 1] = (byte)(value >> 16);
            buffer[offset + 2] = (byte)(value >> 8);
            buffer[offset + 3] = (byte)(value >> 0);
        }

        /// <summary>
        /// Function Name: ReadNetworkByteOrder
        /// Created By: Richard Nader, Jr.
        /// Date Created: 2/18/2023
        /// 
        /// Purpose:
        /// The Purpose of this Function is to:
        ///     - Reads a uint value at a specified offset from a byte buffer
        /// </summary>
        /// <param name="buffer">Buffer to read from</param>
        /// <param name="offset">Byte offset to begin reading from</param>
        /// <returns>uint read at offset from buffer</returns>
        /// Borrowed from https://github.com/dotnet/AspNetCore/blob/main/src/Identity/Extensions.Core/src/PasswordHasher.cs
        private static uint ReadNetworkByteOrder(byte[] buffer, int offset)
        {
            return ((uint)(buffer[offset + 0]) << 24)
            | ((uint)(buffer[offset + 1]) << 16)
            | ((uint)(buffer[offset + 2]) << 8)
            | ((uint)(buffer[offset + 3]));
        }

        /// <summary>
        /// Function Name: GetUser
        /// Created By: Kiefer Thorson & Nathan VanSnepson
        /// Date Created: 2/25/2023
        /// 
        /// Purpose:
        /// The Purpose of this Function is to:
        ///     - Get a userModel from the database
        /// </summary>
        /// <param name="Tuid">ID of the user</param>
        /// <returns>UserModel</returns>
        public UserModel GetUser(int Tuid)
        {
            try 
            { 
                return _dbContext.Users.Select(x =>
                    new UserModel
                    {
                        Tuid = x.Tuid,
                        Name = x.Name,
                        Email = x.Email,
                        PhoneNumber = x.PhoneNumber,
                        IsActive = x.IsActive,
                        IsAdmin = x.IsAdmin
                    }).FirstOrDefault(x => x.Tuid == Tuid);
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1415._message, ErrorMessages._1415._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1416._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1416._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1417._message + e.Message, ErrorMessages._1417._code);
            }

            return new UserModel();
        }

        /// <summary>
        /// Function Name: EmailExists
        /// Created By: Nathan VanSnepson
        /// Date Created: 3/27/2023
        /// Purpose:
        /// The Purpose of this Function is to:
        ///     - Check is a user exists
        /// </summary>
        /// <param name="email">email to check</param>
        /// <returns>bool</returns>
        public bool EmailExists(string email)
        {
            try { 
                return _dbContext.Users.Any(x => x.Email == email);
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1418._message, ErrorMessages._1418._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1419._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1419._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1420._message + e.Message, ErrorMessages._1420._code);
            }

            //Return true to block the user from potential duplicate emails
            return true;
        }

        /// <summary>
        /// Function Name: EmailExistsForOtherUser
        /// Created By: Nathan VanSnepson
        /// Date Created: 3/27/2023
        /// Purpose:
        /// The Purpose of this Function is to:
        ///     - Check is a user exists
        /// </summary>
        /// <param name="email">email to check</param>
        /// <returns>bool</returns>
        public bool EmailExistsForOtherUser(string email, int Tuid)
        {
            try
            {
                return _dbContext.Users.Any(x => x.Email == email && x.Tuid != Tuid);
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1421._message, ErrorMessages._1421._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1422._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1422._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1423._message + e.Message, ErrorMessages._1423._code);
            }

            //Return true to block the user from potential duplicate emails
            return true;
        }
    }
}
