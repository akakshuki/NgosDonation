using Domain.EF;

namespace Domain.Repository
{
    public interface IUnitOfWork
    {
        IRepository<AboutU> AboutUsRepository { get; }
        IRepository<Category> CategoryRepository { get; }
        IRepository<Donate> DonateRepository { get; }
        IRepository<Partner> PartnerRepository { get; }
        IRepository<Program> ProgramRepository { get; }
        IRepository<ProgramImage> ProgramImageRepository { get; }
        IRepository<Role> RoleRepository { get; }
        IRepository<TypeProgram> TypeProgramRepository { get; }
        IRepository<User> UserRepository { get; }
        IRepository<UserDonate> UserDonateRepository { get; }
        IRepository<UserQuestion> UserQuestionRepository { get; }

        bool Commit();
    }
}