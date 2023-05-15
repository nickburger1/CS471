using A_FGMS.DataLayer.Entities;
using B_FGMS.BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B_FGMS.BusinessLogic.Services.UserProviders
{
	/// <summary>
	/// Interface Name: IUserProvider
	/// Created By: Kiefer Thorson & Nathan VanSnepson
	/// Date Created: 2/16/23
	/// Additional Contributors:
	/// Last Modified: 2/26/23
	/// Last Modified By: Kiefer Thorson, Nathan VanSnepson, and Richard Nader, Jr.
	/// 
	/// Purpose:
	/// This interface is the contract that has method declarations for the User provider 
	/// </summary>
	public interface IUserProvider
	{
        event EventHandler<Events.ErrorEventArgs> DatabaseError;
        IEnumerable<UserModel> GetAllUsers();
		UserModel GetUser(int Tuid);
		void CreateUser(UserModel user, string createdPassword);
		void UpdateUser(UserModel user, string? updatedPassword);
		void DeleteUser(UserModel user);
		bool TryUserPasswordLogin(string email, string password, out UserModel signedInUser);
		bool EmailExists(string email);
		bool EmailExistsForOtherUser(string email, int Tuid);
    }
}
