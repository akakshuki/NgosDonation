using System;
using Domain.EF;

namespace Domain.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ProjectSem3Entities _dbContext;

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

        public UnitOfWork(ProjectSem3Entities dbContext)
        {
            _dbContext = dbContext;
        }

        public IRepository<AboutU> AboutUsRepository
        {
            get
            {
                return _aboutUBaseRepository ?? (_aboutUBaseRepository = new BaseRepository<AboutU>(_dbContext));
            }
        }

        public IRepository<Category> CategoryRepository
        {
            get
            {
                return _categoryBaseRepository ?? (_categoryBaseRepository = new BaseRepository<Category>(_dbContext));
            }
        }

        public IRepository<Donate> DonateRepository
        {
            get
            {
                return _donateBaseRepository ?? (_donateBaseRepository = new BaseRepository<Donate>(_dbContext));
            }
        }

        public IRepository<Partner> PartnerRepository
        {
            get
            {
                return _partnerBaseRepository ?? (_partnerBaseRepository = new BaseRepository<Partner>(_dbContext));
            }
        }

        public IRepository<Program> ProgramRepository
        {
            get
            {
                return _programBaseRepository ?? (_programBaseRepository = new BaseRepository<Program>(_dbContext));
            }
        }

        public IRepository<ProgramImage> ProgramImageRepository
        {
            get
            {
                return _programImageBaseRepository ??
                       (_programImageBaseRepository = new BaseRepository<ProgramImage>(_dbContext));
            }
        }

        public IRepository<Role> RoleRepository
        {
            get
            {
                return _roleBaseRepository ?? (_roleBaseRepository = new BaseRepository<Role>(_dbContext));
            }
        }

        public IRepository<TypeProgram> TypeProgramRepository
        {
            get
            {
                return _typeProgramBaseRepository ?? (_typeProgramBaseRepository = new BaseRepository<TypeProgram>(_dbContext));
            }
        }

        public IRepository<User> UserRepository
        {
            get
            {
                return _userBaseRepository ??
                       (_userBaseRepository = new BaseRepository<User>(_dbContext));
            }
        }

        public IRepository<UserDonate> UserDonateRepository
        {
            get { return _userDonateBaseRepository = new BaseRepository<UserDonate>(_dbContext); }
        }

        public IRepository<UserQuestion> UserQuestionRepository
        {
            get
            {
                return _userQuestionBaseRepository ?? (_userQuestionBaseRepository = new BaseRepository<UserQuestion>(_dbContext));
            }
        }

        public bool Commit()
        {
            try
            {
                return _dbContext.SaveChanges() > 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}