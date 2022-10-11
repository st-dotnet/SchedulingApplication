using SchedulingApplication.Data;
using SchedulingApplication.Data.Entities;
using SchedulingApplication.Infrastructure.Interface;

namespace SchedulingApplication.Infrastructure.Services
{
    public class UserServices : IUserServices
    {
        private readonly SchedulingApplicationContext _dbContext;
        private readonly IEmailServices _emailService;

        public UserServices(SchedulingApplicationContext dbContext, IEmailServices emailService)
		{
			_dbContext = dbContext;
			_emailService = emailService;
		}

		/// <summary>
		/// Rregister user
		/// </summary>
		/// <param name="entity"></param>
		/// <returns></returns>
		public async Task<bool> RegisterUser(User entity)
        {
            try
            {
                //get all users
                var users = _dbContext.Users.ToList();

                //check if email is already exists in database
                #region Register user
                if (!users.Any(e => e.Email == entity.Email))
                {
                    entity.CreatedOn = DateTime.Now;
                    entity.UpdatedOn = DateTime.Now;
                    entity.DeletedOn = DateTime.Now;
                    entity.CreatedBy = entity.Id;
                    _dbContext.Users.Add(entity);
                    await _dbContext.SaveChangesAsync();
                    _emailService.Send(entity.Email,"Email Verification","<h1>Email</h1>");
                }
                else
                    return false;
                #endregion

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Get roles
        /// </summary>
        /// <returns></returns>
        public List<Role> GetRoles()
        {
            try
            {
                return _dbContext.Roles.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}
