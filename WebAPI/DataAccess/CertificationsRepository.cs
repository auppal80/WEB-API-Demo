using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess
{
    public class CertificationsRepository : ICertificationRespository
    {
        private CertificationsEntities _ctx;
        public CertificationsRepository(CertificationsEntities ctx)
        {
            _ctx = ctx;
        }

        public Task<IQueryable<Certification>> GetAllCertifications()
        {
            return Task.Run(() => _ctx.Certifications.Where(c => c.Active == true));

        }

        public Certification GetCertificationbyCertificationId(int Id)
        {
            return _ctx.Certifications.Where(c => c.CertificationId == Id).FirstOrDefault();
        }

        public Certification GetCertificationByName(string Name)
        {
            return _ctx.Certifications.Where(c => c.Name == Name).FirstOrDefault();

        }
        public IQueryable<User> GetAllUsers()
        {
            return _ctx.Users;
        }
        public User GetUserByUserId(int userId)
        {
            return _ctx.Users.Where(u => u.UserId == userId).FirstOrDefault();
        }
        public User GetUserByUserName(string userName)
        {
            return _ctx.Users.Where(u => u.UserName == userName).FirstOrDefault();
        }

        public bool InsertCertification(Certification entry)
        {
            try
            {
                _ctx.Certifications.Add(entry);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateCertification(Certification entry)
        {
            return UpdateEntity(_ctx.Certifications, entry);
        }
        public bool DeleteCertification(int id)
        {
            try
            {
                var entity = _ctx.Certifications.Where(d => d.CertificationId == id).FirstOrDefault();
                if (entity != null)
                {
                    _ctx.Certifications.Remove(entity);
                    return true;
                }
            }
            catch
            {
                // TODO Logging
            }

            return false;
        }


        public bool InsertUser(User entry)
        {
            try
            {
                _ctx.Users.Add(entry);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateUsers(User entry)
        {
            return UpdateEntity(_ctx.Users, entry);
        }
        public bool DeleteUser(int id)
        {
            var entity = _ctx.Users.Where(d => d.UserId == id).FirstOrDefault();
            if (entity != null)
            {
                _ctx.Users.Remove(entity);
                return true;
            }
            return false;
        }

        public IQueryable<UsersCertification> GetCertificationForUser(int userID)
        {
            return _ctx.UsersCertifications.Include("Users").Include("Certifications").Where(uc => uc.UserId == userID);
        }

        public bool SaveAll()
        {
            return _ctx.SaveChanges() > 0;
        }
        bool UpdateEntity<T>(DbSet<T> dbSet, T entity) where T : class
        {
            try
            {
                dbSet.AttachAsModified(entity, _ctx);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public IQueryable<ApiUser> GetApiUsers()
        {
            return _ctx.ApiUsers;
        }
        public AuthToken GetAuthToken(string token)
        {
            return _ctx.AuthTokens.Include("ApiUser").Where(t => t.Token == token).FirstOrDefault();
        }

        public bool Insert(AuthToken token)
        {
            try
            {
                _ctx.AuthTokens.Add(token);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
