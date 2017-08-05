using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public interface ICertificationRespository
    {
        Task<IQueryable<Certification>> GetAllCertifications();
        Certification GetCertificationbyCertificationId(int Id);
        IQueryable<User> GetAllUsers();
        IQueryable<UsersCertification> GetCertificationForUser(int userID);
        bool InsertCertification(Certification entry);
        bool UpdateCertification(Certification entry);
        bool DeleteCertification(int id);
        AuthToken GetAuthToken(string token);
         bool SaveAll();
        Certification GetCertificationByName(string Name);
        User GetUserByUserId(int userId);
        bool InsertUser(User entry);
        bool UpdateUsers(User entry);
        bool DeleteUser(int id);
        User GetUserByUserName(string userName);
        bool Insert(AuthToken token);
        IQueryable<ApiUser> GetApiUsers();
    }
}
