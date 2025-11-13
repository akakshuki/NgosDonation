using Domain.EF;
using System;

namespace Domain.Repository
{
    /// <summary>
    /// Implements the Unit of Work pattern.
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ProjectSem3Entities _dbContext;

        private BaseRepository<AboutU> _aboutUBaseRepository;
        private BaseRepository<Category> _categoryBaseRepository;
        private BaseRepository<Donate> _donateBaseRepository;
        private BaseRepository<Partner> _partnerBaseRepository;
        private BaseRepository<Program> _programBaseRepository;
        private BaseRepository<ProgramImage> _programImageBaseRepository;
        private BaseRepository<Role> _roleBaseRepository;
        private BaseRepository<TypeProgram> _typeProgramBaseRepository;
        private BaseRepository<User> _userBaseRepository;
        private BaseRepository<UserDonate> _userDonateBaseRepository;
        private BaseRepository<UserQuestion> _userQuestionBaseRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork"/> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        public UnitOfWork(ProjectSem3Entities dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Gets the repository for AboutU entities.
        /// </summary>
        public IRepository<AboutU> AboutUsRepository =>
            _aboutUBaseRepository ?? (_aboutUBaseRepository = new BaseRepository<AboutU>(_dbContext));

        /// <summary>
        /// Gets the repository for Category entities.
        /// </summary>
        public IRepository<Category> CategoryRepository =>
            _categoryBaseRepository ?? (_categoryBaseRepository = new BaseRepository<Category>(_dbContext));

        /// <summary>
        /// Gets the repository for Donate entities.
        /// </summary>
        public IRepository<Donate> DonateRepository =>
            _donateBaseRepository ?? (_donateBaseRepository = new BaseRepository<Donate>(_dbContext));

        /// <summary>
        /// Gets the repository for Partner entities.
        /// </summary>
        public IRepository<Partner> PartnerRepository =>
            _partnerBaseRepository ?? (_partnerBaseRepository = new BaseRepository<Partner>(_dbContext));

        /// <summary>
        /// Gets the repository for Program entities.
        /// </summary>
        public IRepository<Program> ProgramRepository =>
            _programBaseRepository ?? (_programBaseRepository = new BaseRepository<Program>(_dbContext));

        /// <summary>
        /// Gets the repository for ProgramImage entities.
        /// </summary>
        public IRepository<ProgramImage> ProgramImageRepository =>
            _programImageBaseRepository ?? (_programImageBaseRepository = new BaseRepository<ProgramImage>(_dbContext));

        /// <summary>
        /// Gets the repository for Role entities.
        /// </summary>
        public IRepository<Role> RoleRepository =>
            _roleBaseRepository ?? (_roleBaseRepository = new BaseRepository<Role>(_dbContext));

        /// <summary>
        /// Gets the repository for TypeProgram entities.
        /// </summary>
        public IRepository<TypeProgram> TypeProgramRepository =>
            _typeProgramBaseRepository ?? (_typeProgramBaseRepository = new BaseRepository<TypeProgram>(_dbContext));

        /// <summary>
        /// Gets the repository for User entities.
        /// </summary>
        public IRepository<User> UserRepository =>
            _userBaseRepository ?? (_userBaseRepository = new BaseRepository<User>(_dbContext));

        /// <summary>
        /// Gets the repository for UserDonate entities.
        /// </summary>
        public IRepository<UserDonate> UserDonateRepository =>
            _userDonateBaseRepository ?? (_userDonateBaseRepository = new BaseRepository<UserDonate>(_dbContext));

        /// <summary>
        /// Gets the repository for UserQuestion entities.
        /// </summary>
        public IRepository<UserQuestion> UserQuestionRepository =>
            _userQuestionBaseRepository ?? (_userQuestionBaseRepository = new BaseRepository<UserQuestion>(_dbContext));

        /// <summary>
        /// Commits all changes to the database.
        /// </summary>
        /// <returns>True if the commit was successful, otherwise false.</returns>
        public bool Commit()
        {
            try
            {
                return _dbContext.SaveChanges() > 0;
            }
            catch (Exception e)
            {
                Console.WriteLine("Commit error: " + e);
                throw;
            }
        }
    }
}