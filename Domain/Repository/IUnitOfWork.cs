using Domain.EF;

namespace Domain.Repository
{
    /// <summary>
    /// Defines the interface for the Unit of Work pattern.
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Gets the repository for AboutU entities.
        /// </summary>
        IRepository<AboutU> AboutUsRepository { get; }

        /// <summary>
        /// Gets the repository for Category entities.
        /// </summary>
        IRepository<Category> CategoryRepository { get; }

        /// <summary>
        /// Gets the repository for Donate entities.
        /// </summary>
        IRepository<Donate> DonateRepository { get; }

        /// <summary>
        /// Gets the repository for Partner entities.
        /// </summary>
        IRepository<Partner> PartnerRepository { get; }

        /// <summary>
        /// Gets the repository for Program entities.
        /// </summary>
        IRepository<Program> ProgramRepository { get; }

        /// <summary>
        /// Gets the repository for ProgramImage entities.
        /// </summary>
        IRepository<ProgramImage> ProgramImageRepository { get; }

        /// <summary>
        /// Gets the repository for Role entities.
        /// </summary>
        IRepository<Role> RoleRepository { get; }

        /// <summary>
        /// Gets the repository for TypeProgram entities.
        /// </summary>
        IRepository<TypeProgram> TypeProgramRepository { get; }

        /// <summary>
        /// Gets the repository for User entities.
        /// </summary>
        IRepository<User> UserRepository { get; }

        /// <summary>
        /// Gets the repository for UserDonate entities.
        /// </summary>
        IRepository<UserDonate> UserDonateRepository { get; }

        /// <summary>
        /// Gets the repository for UserQuestion entities.
        /// </summary>
        IRepository<UserQuestion> UserQuestionRepository { get; }

        /// <summary>
        /// Commits all changes to the database.
        /// </summary>
        /// <returns>True if the commit was successful, otherwise false.</returns>
        bool Commit();
    }
}